using System;
using Examples.Battle.Scripts.Warriors.Weapon;
using UnityEngine;

namespace Examples.Battle.Scripts.Warriors.Attack
{
    public class ArcherAttack : MonoBehaviour, IAttack, IReset
    {
        [SerializeField] private GameObject Arrow;

        public Action OnAttack { get; set; }
        public Action OnKill { get; set; }
        public Action OnCollisionWall { get; set; }

        private Animator _animator;
        private Warrior _warrior;
        private LifeController _lifeController;

        private float _lastAttack = -CoolDown;
        private int _missed;
        private const float CoolDown = 8f;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _warrior = GetComponent<Warrior>();
            _lifeController = GetComponent<LifeController>();
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.collider.CompareTag("iWall"))
            {
                OnCollisionWall?.Invoke();
            }
        }

        public void Attack()
        {
            if (!(Time.fixedTime - _lastAttack > CoolDown)) return;
            _lastAttack = Time.fixedTime;

            _animator.SetTrigger("Attack");

            var obj = Instantiate(Arrow, transform.position + transform.forward + transform.up * 5, transform.rotation);
            obj.transform.Rotate(Vector3.right, 90f);

            obj.GetComponent<ArrowController>().OnCollision += OnArrowCollision; // TODO: -=
            obj.GetComponent<Rigidbody>().AddForce(transform.forward * 100f);
        }

        private void OnArrowCollision(GameObject obj)
        {
            var lifeController = obj.GetComponent<LifeController>();
            var warrior = obj.GetComponent<Warrior>();

            if (!lifeController || !warrior || _warrior.Team == warrior.Team)
            {
                _missed++;

                if (_missed > 4)
                {
                    _missed = 0;
                    //OnMissed?.Invoke();
                }

                return;
            }

            lifeController.Attack(.35f);
            if (lifeController.Life <= 0f)
            {
                _lifeController.Attack(-1f);
                OnKill?.Invoke();
            }

            OnAttack?.Invoke();
        }

        public void Reset()
        {
            _missed = 0;
        }
    }
}