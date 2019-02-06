using UnityEngine;
using UnityEngine.UI;

namespace UI {
    public class EquipItem : MonoBehaviour {
        private Image image;

        private void Start() {
            image = GetComponent<Image>();
        }

        public void ChangeItem(Sprite sprite) {
            image.sprite = sprite;
        }
    }
}
