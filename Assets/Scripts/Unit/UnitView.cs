using Unit.Visual;
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

            Visualisation = model.Template.CreateVisualisation(transform);
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
