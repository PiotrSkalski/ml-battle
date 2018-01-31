using System.Collections.Generic;
using UnityEngine;

namespace Examples.Battle.Scripts.Warriors
{
    public class EyeController : MonoBehaviour
    {
        [SerializeField] private int NumberOfRay = 10;
        [SerializeField] private float AngleBetweenRay = 10f;
        [SerializeField] private float RayLenght = 3f;

        private float _halfAngle;
        private Warrior _warrior;

        private void Awake()
        {
            _halfAngle = NumberOfRay * AngleBetweenRay / 2f;
            _warrior = GetComponent<Warrior>();
        }

        private void Update()
        {
            for (int i = 0; i < NumberOfRay; i++)
            {
                var dir = Quaternion.AngleAxis(i * AngleBetweenRay - _halfAngle, Vector3.up) * transform.forward;
                Debug.DrawRay(transform.position, dir * RayLenght, Color.magenta);
            }
        }

        public float EyeAngle
        {
            get { return NumberOfRay * AngleBetweenRay; }
        }

        public List<float> GetRaycastInfo()
        {
            var list = new List<float>();
            for (int i = 0; i < NumberOfRay; i++)
            {
                var dir = Quaternion.AngleAxis(i * AngleBetweenRay - _halfAngle, Vector3.up) * transform.forward;

                RaycastHit rayInfo;
                if (Physics.Raycast(transform.position, dir, out rayInfo, RayLenght))
                {
                    if (rayInfo.collider.CompareTag("iWall"))
                    {
                        list.Add(-1f);
                        continue;
                    }

                    var lifeController = rayInfo.collider.GetComponent<LifeController>();
                    var warrior = rayInfo.collider.GetComponent<Warrior>();
                    if (lifeController && warrior)
                    {
                        var distance = NormalizeLife(RayLenght - rayInfo.distance);
                        if (warrior.Team != _warrior.Team)
                        {
                            list.Add(distance);
                        }
                        else
                        {
                            list.Add(-distance);
                        }
                    }
                    else
                    {
                        list.Add(0f);
                    }
                }
                else
                {
                    list.Add(0f);
                }
            }

            return list;
        }

        private float NormalizeLife(float life)
        {
            return life / (RayLenght * 2f);
        }
    }
}