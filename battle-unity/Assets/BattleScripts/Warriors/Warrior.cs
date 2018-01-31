using UnityEngine;

namespace Examples.Battle.Scripts.Warriors
{
    public class Warrior : MonoBehaviour
    {
        private Team _team;

        public Team Team => _team;

        public void Init(Team team)
        {
            _team = team;
        }
    }
}