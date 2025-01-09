using System;
using Unit.Visual;
using Unit.Wearing;
using UnityEngine;

namespace Unit
{
    public class UnitView : MonoBehaviour
    {
        private EUnitState _currentState;

        public UnitVisualisation Visualisation { get; private set; }

        private Unit _model;

        public void Init(Unit model)
        {
            _model = model;
            
            Visualisation = Instantiate(model.Config.visualBase, transform);
            foreach (var (_, wearing) in model.Wearings)
            {
                Visualisation.AddWearing(wearing);
            }
        }

        private void WearingUnEquip(EWearingSlot wearing)
        {
            Visualisation.RemoveWearing(wearing);
        }

        private void WearingEquip(IWearing wearing)
        {
            Visualisation.AddWearing(wearing);
        }

        public void SetPlaceholder(Transform root)
        {
            transform.SetParent(root, false);
        }

        public void SetState(EUnitState state)
        {
            if (_currentState == state)
                return;
            
            _currentState = state;
            Visualisation.Animator.SetTrigger(UnitStateAnimations.Get(state));
        }
    }
}
