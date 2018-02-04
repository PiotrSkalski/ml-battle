using System;
using UnityEngine;

namespace Examples.Battle.Scripts.Warriors
{
    public class LifeController : MonoBehaviour, IReset
    {
        private float _life;

        public Action<float> OnUpdate;
        public Action OnDead;

        public float Life => _life;

        public void Attack(float power)
        {
            _life -= power;
            OnUpdate?.Invoke(_life);

            if (_life <= 0f)
            {
                OnDead?.Invoke();
            }
        }

        public void AddLife(float add)
        {
            _life -= add;
            OnUpdate?.Invoke(_life);
        }

        public void Reset()
        {
            _life = 1f;
            OnUpdate?.Invoke(_life);
        }

        private void FixedUpdate()
        {
            if (_life > 0f)
            {
                Attack(Time.fixedDeltaTime * 0.01f);
            }
        }
    }
}