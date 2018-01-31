using System.Collections.Generic;
using UnityEngine;

namespace Examples.Battle.Scripts.Environment
{
    public class WarriorDecision : MonoBehaviour, Decision
    {
        private global::Brain _brain;

        private void Awake()
        {
            _brain = gameObject.GetComponent<global::Brain>();
        }

        public float[] Decide(List<float> state, List<Camera> observation, float reward, bool done, float[] memory)
        {
            if (_brain.brainParameters.actionSpaceType == StateType.continuous)
            {
                for (var index = 0; index < 30; index++)
                {
                    var s = state[index];
                    if (s < 0f)
                    {
                        float normalized = ((float) index * 2f / 60f) * 2f - 1f;
                        float angle = normalized * (60f / 360f);

                        return new float[3] {angle, .8f, Random.Range(0, 10) % 2 == 0 ? 1f : 0f};
                    }
                }

                return new float[3] {Random.Range(-1f, 1f), 0f, 0f};
            }
            else
            {
                return new float[1] {1f};
            }
        }

        public float[] MakeMemory(List<float> state, List<Camera> observation, float reward, bool done, float[] memory)
        {
            return new float[0];
        }
    }
}