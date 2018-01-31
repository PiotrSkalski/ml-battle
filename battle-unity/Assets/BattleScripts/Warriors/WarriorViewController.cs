using UnityEngine;

namespace Examples.Battle.Scripts.Warriors
{
    public class WarriorViewController : MonoBehaviour, IReset
    {
        [SerializeField] private GameObject View;
        
        public void PlayerEnabled()
        {
            View.SetActive(true);
        }

        public void PlayerDisabled()
        {
            View.SetActive(false);
        }

        public bool IsEnabled => View.active;
        
        public void Reset()
        {
            PlayerEnabled();
        }
    }
}