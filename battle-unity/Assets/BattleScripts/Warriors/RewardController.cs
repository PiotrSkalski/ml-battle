using Examples.Battle.Scripts.Warriors.Attack;
using UnityEngine;

namespace Examples.Battle.Scripts.Warriors
{
    public class RewardController : MonoBehaviour
    {
        private WarriorAgent _warriorAgent;
        private LifeController _lifeController;
        private IAttack _knightAttack;

        private void Awake()
        {
            _warriorAgent = GetComponentInParent<WarriorAgent>();
            _lifeController = GetComponent<LifeController>();
            _knightAttack = GetComponent<IAttack>();

            _lifeController.OnDead += OnDead;
            _knightAttack.OnCollisionWall += OnWall;
            _knightAttack.OnAttack += OnAttack;
            _knightAttack.OnKill += OnKill;
        }

        private void OnDestroy()
        {
            _lifeController.OnDead -= OnDead;
            _knightAttack.OnCollisionWall -= OnWall;
            _knightAttack.OnAttack -= OnAttack;
            _knightAttack.OnKill -= OnKill;
        }

        private void OnWall()
        {
            _warriorAgent.reward -= 1f;
            _warriorAgent.done = true;
        }

        private void OnDead()
        {
            _warriorAgent.reward -= 0.01f;
            _warriorAgent.done = true;
        }

        private void OnAttack()
        {
            _warriorAgent.reward = .5f;
        }

        private void OnKill()
        {
            _warriorAgent.reward = 1f;
        }
    }
}