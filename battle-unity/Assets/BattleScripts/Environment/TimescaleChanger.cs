using UnityEngine;

namespace Examples.Battle.Scripts.Environment
{
    public class TimescaleChanger : MonoBehaviour
    {
        private void Update()
        {
            if (Input.GetKey(KeyCode.A))
            {
                Time.timeScale = 100f;
            }
            
            if (Input.GetKey(KeyCode.S))
            {
                Time.timeScale = 1f;
            }
        }
    }
}