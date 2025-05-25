using Game.Infrastructure.AssetsManagement;
using Game.Services.Canvases;
using Game.Services.Save;
using Infrastructure.StateMachine;
using UniRx;
using UnityEngine;

namespace Game.Infrastructure.StateMachine.GameStates
{
    public class BootstrapGameState : IGameState
    {
        private readonly ISceneLoader _sceneLoader;
        private readonly ISaveService _saveService;
        private GameStateMachine _stateMachine;

        private const string MAIN_SCENE_NAME = "MainScene";

        public BootstrapGameState(ISceneLoader sceneLoader,
            ISaveService saveService)
        {
            _sceneLoader = sceneLoader;
            _saveService = saveService;
            Application.targetFrameRate = 60;
        }
        public void SetStateMachine(GameStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }
        public void Enter()
        {
            _saveService.Load();
            _sceneLoader.LoadScene(MAIN_SCENE_NAME, OnEnterMainScene);
        }

        private void OnEnterMainScene()
        {
            _stateMachine.EnterState<ResourceCollectState>();
        }
        
        public void Exit()
        {
        }
    }
}