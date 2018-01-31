using UnityEngine;

namespace Examples.Battle.Scripts.Warriors
{
    public class DeadController : MonoBehaviour
    {
        private LifeController _lifeController;
        private WarriorViewController _warriorViewController;
        private void Awake()
        {
            _lifeController = GetComponent<LifeController>();
            _warriorViewController = GetComponentInParent<WarriorViewController>();
            
            _lifeController.OnDead += OnDead;
        }

        private void OnDestroy()
        {
            _lifeController.OnDead -= OnDead;
        }

        private void OnDead()
        {
            _warriorViewController.PlayerDisabled();
        }
    }
}