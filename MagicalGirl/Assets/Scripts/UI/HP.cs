using Player;
using UnityEngine;
using UnityEngine.UI;

namespace UI {
    public class HP : MonoBehaviour {
        [SerializeField] private PlayerManager playerManager;

        private Image image;

        private float max;
        // Start is called before the first frame update
        void Start() {
            image = GetComponent<Image>();
            max = playerManager.HP;
        }

        // Update is called once per frame
        void Update() {
            image.fillAmount = playerManager.HP / max;
        }
    }
}
