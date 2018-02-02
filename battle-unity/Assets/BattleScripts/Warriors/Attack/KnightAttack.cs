using System;
using UnityEngine;

namespace Examples.Battle.Scripts.Warriors.Attack
{
    public class KnightAttack : MonoBehaviour, IAttack
    {
        [SerializeField] private SphereCollider AttackRange;

        public Action OnAttack { get; set; }
        public Action OnKill { get; set; }
        public Action OnCollisionWall { get; set; }

        private Animator _animator;
        private Warrior _warrior;
        private LifeController _lifeController;
        private float _lastAttack;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _warrior = GetComponent<Warrior>();
            _lifeController = GetComponent<LifeController>();
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.collider.CompareTag("wall"))
            {
                _lifeController.Attack(1f);
                OnCollisionWall?.Invoke();
            }
        }

        public void Attack()
        {
            if (Time.fixedTime - _lastAttack < 1f) return;
            _lastAttack = Time.fixedTime;

            var colliders = Physics.OverlapSphere(AttackRange.transform.position, AttackRange.radius,
                LayerMask.GetMask(_warrior.Team == Team.Blue ? "Red" : "Blue"));
            foreach (var collider in colliders)
            {
                Attack(collider.gameObject);
            }
        }

        private void Attack(GameObject warriorObject)
        {
            var lifeController = warriorObject.GetComponent<LifeController>();
            var warrior = warriorObject.GetComponent<Warrior>();

            if (!lifeController || !warrior) return;
            if (_warrior.Team == warrior.Team) return;

            _animator.SetTrigger("Attack");
            lifeController.Attack(.35f);

            if (lifeController.Life <= 0f)
            {
                _lifeController.AddLife(.3f);
                OnKill?.Invoke();
            }

            OnAttack?.Invoke();
        }
    }
}