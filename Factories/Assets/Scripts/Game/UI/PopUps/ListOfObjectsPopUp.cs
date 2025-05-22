using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utils.Extensions;

namespace Game.UI.PopUps
{
    public class ListOfObjectsPopUp : PopUp
    {
        [SerializeField] private LayoutGroup _layoutGroup;

        public void Construct(List<Transform> objects)
        {
            objects.ForEach(ui => ui.transform.SetParent(_layoutGroup.transform));
        }

    }
}
