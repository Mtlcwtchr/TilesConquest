using UnityEngine;

namespace Unit
{
    public class UnitView : MonoBehaviour
    {
        [SerializeField] private Animator animator;

        private EUnitState _currentState;

        public void Init(Unit model)
        {
            
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
            animator.SetTrigger(UnitStateAnimations.Get(state));
        }
    }
}
