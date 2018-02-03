using Examples.Battle.Scripts.Environment;
using UnityEngine;

namespace Examples.Battle.Scripts.Warriors
{
    public class ResetPositionController : MonoBehaviour, IReset
    {
        private ISpawner _spawner;
        private int index;
        private Vector3 _rotation;

        public void Init(ISpawner warriorSpawnerRandom, int i)
        {
            index = i;
            _spawner = warriorSpawnerRandom;
            _rotation = transform.rotation.eulerAngles;
        }

        public void Reset()
        {
            GetComponent<Animator>().SetBool("dead", false);
            
            transform.parent.position = _spawner.GetPosition(index);
            transform.rotation = Quaternion.Euler(_rotation);

            transform.localPosition = Vector3.zero;
        }
    }
}