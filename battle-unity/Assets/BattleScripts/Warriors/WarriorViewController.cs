using System.Collections;
using UnityEngine;

namespace Examples.Battle.Scripts.Warriors
{
    public class WarriorViewController : MonoBehaviour, IReset
    {
        [SerializeField] private GameObject View;

        private int _layer;

        private void Awake()
        {
            _layer = View.layer;
        }

        public void PlayerEnabled()
        {
            View.SetActive(true);
            View.layer = _layer;
        }

        public void PlayerDisabled()
        {
            View.layer = LayerMask.NameToLayer("Ignore Raycast");
            StartCoroutine(OffView());
        }

        public bool IsEnabled => View.active;

        public void Reset()
        {
            StopAllCoroutines();
            PlayerEnabled();
        }

        private IEnumerator OffView()
        {
            yield return new WaitForSeconds(5f);
            View.SetActive(false);
        }
    }
}