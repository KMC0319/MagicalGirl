using System;
using UniRx;

namespace Systems {
    /// <summary>
    /// シーン遷移をして現在シーンを更新するクラス
    /// このクラスから現在シーンがわかる
    /// Unity上でシーンの登録できてないと大抵エラー吐く
    /// </summary>
    public static class SceneManager {
        public static Scene NowScene { get; private set; }

        static SceneManager() {
            NowScene = GetActiveScene();
            SceneActivate();
        }

        private static Scene GetActiveScene() {
            return (Scene) UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;
        }

        public static void LoadScene(Scene next) {
            NowScene = next;
            UnityEngine.SceneManagement.SceneManager.LoadScene((int) next);
            SceneActivate();
        }

        public static void StartBossBattle() {
            NowScene = Scene.Boss;
            SceneActivate();
        }

        //シーン遷移時に行うこと
        private static void SceneActivate() {
            Observable.NextFrame().Subscribe(_ => BackGroundMusicManager.Instance.StartMusic(NowScene));
            switch (NowScene) {
                case Scene.Title:
                    break;
                case Scene.Game:
                    break;
                case Scene.Boss:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }

    public enum Scene {
        Title,
        Game,
        Boss,
    }
}
