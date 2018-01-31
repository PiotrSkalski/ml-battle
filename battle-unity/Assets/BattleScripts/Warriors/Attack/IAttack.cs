using System;

namespace Examples.Battle.Scripts.Warriors.Attack
{
    public interface IAttack
    {
        void Attack();

        Action OnAttack { get; set; }
        Action OnKill { get; set; }
        Action OnCollisionWall { get; set; }
    }
}