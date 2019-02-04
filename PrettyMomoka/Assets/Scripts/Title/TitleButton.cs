using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleButton : MonoBehaviour {
    public void OnClickStart() {
        SceneManager.LoadScene("Game");
    }

    public void Quit() {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#elif UNITY_STANDALONE
      Application.Quit();
    #endif
    }
}
