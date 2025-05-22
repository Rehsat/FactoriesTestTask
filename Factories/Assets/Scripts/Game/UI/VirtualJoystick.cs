using System;
using Game.Services.Cameras;
using Game.Services.Input;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace Game.UI
{
    public class VirtualJoystick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
    {
        [Header("Joystick Settings")]
        [SerializeField] private RectTransform joystickBackground;
        [SerializeField] private RectTransform joystickHandle;
        [SerializeField] private float handleRange = 1f;
        [SerializeField] private float deadZone = 0.1f;
    
        [Header("Output")]
        public Vector2 outputVector;

        private Vector2 inputVector = Vector2.zero;
        private Canvas canvas;
        private Camera cam;
        private IInputService _inputService;

        [Inject] //Мог скачать нормальный джойстик из ассет стора, но впринцпе сгенеренный нейронкой тоже пойдет
        private void Construct(ICameraService cameraService, IInputService inputService)
        {
            canvas = GetComponentInParent<Canvas>();
            cam = cameraService.MainCamera;
            _inputService = inputService;
            joystickHandle.anchoredPosition = Vector2.zero;
        }

        //Немножко балуюсь
        private void Update()
        {
            _inputService.SetCurrentMovement(Direction());
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
                joystickBackground, 
                eventData.position, 
                eventData.pressEventCamera, 
                out Vector2 position))
            {
                position.x = position.x / (joystickBackground.sizeDelta.x * 0.5f);
                position.y = position.y / (joystickBackground.sizeDelta.y * 0.5f);
            
                inputVector = (position.magnitude > 1.0f) ? position.normalized : position;
            
                joystickHandle.anchoredPosition = new Vector2(
                    inputVector.x * (joystickBackground.sizeDelta.x * 0.5f * handleRange),
                    inputVector.y * (joystickBackground.sizeDelta.y * 0.5f * handleRange));
            
                outputVector = (position.magnitude < deadZone) ? Vector2.zero : inputVector;
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            OnDrag(eventData);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            // При отпускании сбрасываем джойстик
            inputVector = Vector2.zero;
            outputVector = Vector2.zero;
            joystickHandle.anchoredPosition = Vector2.zero;
        }

        // Методы для получения ввода
        public float Horizontal()
        {
            return outputVector.x;
        }

        public float Vertical()
        {
            return outputVector.y;
        }

        public Vector2 Direction()
        {
            return new Vector2(Horizontal(), Vertical());
        }
    }
}
