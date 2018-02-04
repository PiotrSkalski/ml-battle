using System.Collections.Generic;
using Examples.Battle.Scripts.Environment;
using Examples.Battle.Scripts.Warriors.Attack;
using UnityEngine;

namespace Examples.Battle.Scripts.Warriors
{
    public class WarriorAgent : Agent
    {
        private EyeController _eyeController;
        private IAttack _attack;
        private Animator _animator;
        private float _angleAnimatorMove = 180f;

        private TeamManager _teamManager;
        private TeamManager _enemyTeamManager;

        public void Init(TeamManager my, TeamManager enemy)
        {
            _teamManager = my;
            _enemyTeamManager = enemy;
        }

        private void Start()
        {
            GiveBrain(brain);
            _eyeController = GetComponentInChildren<EyeController>();
            _attack = GetComponentInChildren<IAttack>();
            _animator = GetComponentInChildren<Animator>();
        }

        public override List<float> CollectState()
        {
            List<float> state = new List<float>();
            state.AddRange(_eyeController.GetRaycastInfo());
            //state.Add(_teamManager.PercentLife);
            //state.Add(_enemyTeamManager.PercentLife);
            return state;
        }

        public override void AgentStep(float[] act)
        {
            reward -= 0.0001f;

            float directionX = 0;
            float directionZ = 0;

            if (brain.brainParameters.actionSpaceType == StateType.continuous)
            {
                directionX = Mathf.Clamp(act[0], -1f, 1f);
                directionZ = (Mathf.Clamp(act[1], -1f, 1f) + 1f) / 2f;
            }
            else
            {
                int movement = Mathf.FloorToInt(act[0]);
                if (movement == 1)
                {
                    directionX = -1;
                }

                if (movement == 2)
                {
                    directionX = 1;
                }

                if (movement == 3)
                {
                    directionZ = -1;
                }

                if (movement == 4)
                {
                    directionZ = 1;
                }
            }

            _attack.Attack();
            directionZ *= _eyeController.EyeAngle / _angleAnimatorMove;

            _animator.SetFloat("Forward", directionZ, .5f, Time.fixedDeltaTime);
            _animator.SetFloat("Turn", directionX, .5f, Time.fixedDeltaTime);
        }

        public override void AgentReset()
        {
            var reset = GetComponentsInChildren<IReset>(true);
            foreach (var r in reset)
            {
                r.Reset();
            }
        }
    }
}