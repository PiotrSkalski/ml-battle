using UnityEngine;

namespace Examples.Battle.Scripts.Warriors
{
    public class DeadController : MonoBehaviour, IReset
    {
        private LifeController _lifeController;
        private WarriorViewController _warriorViewController;
        private Animator _animator;
        
        private void Awake()
        {
            _lifeController = GetComponent<LifeController>();
            _warriorViewController = GetComponentInParent<WarriorViewController>();
            _animator = GetComponent<Animator>();
            
            _lifeController.OnDead += OnDead;
        }

        private void OnDestroy()
        {
            _lifeController.OnDead -= OnDead;
        }

        private void OnDead()
        {
            _animator.SetBool("dead", true);
            _warriorViewController.PlayerDisabled();
        }

        public void Reset()
        {
            _animator.SetBool("dead", false);
        }
    }
}