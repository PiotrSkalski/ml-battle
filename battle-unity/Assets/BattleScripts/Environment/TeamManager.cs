using System;
using Examples.Battle.Scripts.Warriors;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Examples.Battle.Scripts.Environment
{
    public class TeamManager : MonoBehaviour, ISpawner
    {
        [SerializeField] private Team Team;
        [SerializeField] private GameObject Prefab;
        [SerializeField] private global::Brain Brain;
        [SerializeField] private int NumberOf;
        [SerializeField] private float RotateY;

        [SerializeField] private TeamManager Enemy;

        private BoxCollider _boxCollider;
        private int _lifeWarriors;

        public Action OnAllWarriorsDead;

        private void Awake()
        {
            _boxCollider = GetComponent<BoxCollider>();

            for (int i = 0; i < NumberOf; i++)
            {
                var obj = Instantiate(Prefab, GetPosition(i), Quaternion.Euler(0f, RotateY, 0f));
                obj.transform.parent = transform;

                var agent = obj.GetComponent<WarriorAgent>();
                
                agent.brain = Brain;
                agent.Init(this, Enemy);

                agent.GetComponentInChildren<LifeController>().OnDead += OnDone;
                
                obj.GetComponentInChildren<Warrior>().Init(Team);
                obj.GetComponentInChildren<ResetPositionController>().Init(this, i);
            }

            _lifeWarriors = NumberOf;
        }

        private void OnDestroy()
        {
            foreach (Transform o in transform)
            {
                var component = o.GetComponent<LifeController>();
                if (component)
                {
                    component.OnDead += OnDone;
                }
            }
        }

        public Vector3 GetPosition(int i)
        {
            float randX = Random.Range(-_boxCollider.size.x, _boxCollider.size.x);
            float randZ = Random.Range(-_boxCollider.size.z, _boxCollider.size.z);

            Vector3 randPos = new Vector3(randX * .5f, 1f, randZ * .5f);
            randPos = transform.TransformPoint(randPos);

            return randPos;
        }

        private void OnDone()
        {
            _lifeWarriors--;

            if (_lifeWarriors <= 0)
            {
                OnAllWarriorsDead?.Invoke();
            }
        }

        public void WinBattle()
        {
            foreach (Transform o in transform)
            {
                var agent = o.GetComponent<WarriorAgent>();
                var view = o.GetComponent<WarriorViewController>();

                if (view.IsEnabled)
                {
                    agent.reward = 1f;
                    agent.done = true;
                }
                
                view.PlayerDisabled();
            }
        }

        public void ResetBattle()
        {
            _lifeWarriors = NumberOf;
            
            foreach (Transform o in transform)
            {
                o.GetComponent<WarriorViewController>().PlayerEnabled();
            }
        }

        public float PercentLife => (float)_lifeWarriors / (float)NumberOf;
    }
}