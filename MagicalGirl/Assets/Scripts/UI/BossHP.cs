using Boss;
using UnityEngine;
using UnityEngine.UI;

namespace UI {
    public class BossHP : MonoBehaviour {
        [SerializeField] private BossController bossController;
        private Image image;

        private float max;

        private void Start() {
            max = bossController.HP;
            image = GetComponent<Image>();
        }

        private void Update() {
            image.fillAmount = bossController.HP / max;
        }

        public void Init() {
            transform.parent.gameObject.SetActive(true);
        }
    }
}
