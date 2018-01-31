using System;
using UnityEngine;

namespace Examples.Battle.Scripts.Warriors.Weapon
{
    public class ArrowController : MonoBehaviour
    {
        public Action<GameObject> OnCollision;
        
        private void OnCollisionEnter(Collision other)
        {
            OnCollision?.Invoke(other.gameObject);
            Destroy(gameObject);
        }
    }
}