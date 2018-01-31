using UnityEngine;
using UnityEngine.UI;

namespace Examples.Battle.Scripts.Warriors
{
    public class DisplayLifeController : MonoBehaviour
    {
        [SerializeField] private LifeController LifeController;
        private Image _image;

        private void Awake()
        {
            _image = GetComponent<Image>();

            LifeController.OnUpdate += OnUpdate;
        }

        private void OnDestroy()
        {
            LifeController.OnUpdate -= OnUpdate;
        }

        private void OnUpdate(float value)
        {
            _image.fillAmount = value;
        }
    }
}