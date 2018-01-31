using Examples.Battle.Scripts.Warriors;
using UnityEngine;

namespace Examples.Battle.Scripts.Environment
{
    public class WarriorSpawnerFormationGrid : MonoBehaviour, ISpawner
    {
        [SerializeField] private Team Team;
        [SerializeField] private GameObject Prefab;
        [SerializeField] private global::Brain Brain;
        [SerializeField] private int NumberOf;
        [SerializeField] private int NumberOfX = 5;
        [SerializeField] private float PaddingX = 2f;
        [SerializeField] private float PaddingZ = 2f;
        [SerializeField] private float RotationY;

        private void Awake()
        {
            for (int i = 0; i < NumberOf; i++)
            {
                var obj = Instantiate(Prefab, transform);
                obj.transform.position = GetPosition(i);
                obj.transform.rotation = Quaternion.Euler(0f, RotationY, 0f);

                obj.GetComponent<WarriorAgent>().brain = Brain;
                obj.GetComponent<Warrior>().Init(Team);
                obj.GetComponent<ResetPositionController>().Init(this, i);
            }
        }

        public Vector3 GetPosition(int i)
        {
            var a = i % NumberOfX;
            var row = i / NumberOfX;
            var vec = transform.position + new Vector3(a, 0f, row) -
                      new Vector3(NumberOfX, 0f, NumberOf / (float) NumberOfX) * 2f;
            vec.x += PaddingX * a;
            vec.z += row * PaddingZ;
            return vec;
        }
    }
}