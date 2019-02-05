using Systems;
using UnityEditor;
using UnityEngine;

namespace Title {
    public class TitleButton : MonoBehaviour {
        public void OnClickStart() {
            SceneManager.LoadScene(Scene.Game);
        }

        public void Quit() {
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#elif UNITY_STANDALONE
      Application.Quit();
    #endif
        }
    }
}
