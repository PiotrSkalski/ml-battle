using UnityEngine;

namespace Examples.Battle.Scripts.Environment
{
    public interface ISpawner
    {
        Vector3 GetPosition(int i);
    }
}