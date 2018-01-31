using Examples.Battle.Scripts.Environment;
using UnityEngine;

namespace Examples.Battle.Scripts.Brain
{
    public class BattleBrain : Academy
    {
        [SerializeField] private TeamManager Blue;
        [SerializeField] private TeamManager Red;
        
        public override void AcademyReset()
        {
            base.AcademyReset();
            
            Blue.ResetBattle();
            Red.ResetBattle();
        }
    }
}