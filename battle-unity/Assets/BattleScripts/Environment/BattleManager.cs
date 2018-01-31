using UnityEngine;

namespace Examples.Battle.Scripts.Environment
{
    public class BattleManager : MonoBehaviour
    {
        [SerializeField] private TeamManager Blue;
        [SerializeField] private TeamManager Red;
        [SerializeField] private Academy Academy;

        private void Awake()
        {
            Blue.OnAllWarriorsDead += OnRedWin;
            Red.OnAllWarriorsDead += OnBlueWin;
        }

        private void OnDestroy()
        {
            Blue.OnAllWarriorsDead -= OnRedWin;
            Red.OnAllWarriorsDead -= OnBlueWin;
        }

        private void OnRedWin()
        {
            Red.WinBattle();
            Academy.done = true;
            
            Debug.Log("OnRedWin");
        }
        
        private void OnBlueWin()
        {
            Blue.WinBattle();
            Academy.done = true;
            
            Debug.Log("OnBlueWin");
        }
    }
}