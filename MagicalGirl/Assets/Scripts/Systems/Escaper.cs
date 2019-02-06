using UnityEngine;

namespace Systems {
    public class Escaper : MonoBehaviour {
        private static Escaper instance;
        public static Escaper Instance => instance;
        
        void Awake() {
            if (instance == null) {
                instance = this;
            } else {
                Destroy(gameObject);
            }

            DontDestroyOnLoad(gameObject);
        }

        private void Update() {
            if(Input.GetKeyDown(KeyCode.Escape)) Application.Quit();
        }
    }
}
