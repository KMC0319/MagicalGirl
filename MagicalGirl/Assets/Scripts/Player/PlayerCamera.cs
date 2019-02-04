using System.Collections;
using System.Collections.Generic;
using Systems;
using UnityEngine;

namespace Player {
    /// <summary>
    /// プレイヤーが端っこに行くと動きを止めてカメラを移動する
    /// </summary>
    public class PlayerCamera : MonoBehaviour {
        [SerializeField] private Rect moveArea = new Rect(0.05f, 0.05f, 0.9f, 0.9f);
        [SerializeField] private float moveSpeed;
        private GameObject player;
        private Camera mainCamera;
        private Rect BorderLine => new Rect(moveArea.x + 0.05f, moveArea.y + 0.05f, moveArea.width - 0.1f, moveArea.height - 0.1f);
        [SerializeField] private Pausable pausable;

        private void Start() {
            player = GameObject.FindWithTag("Player");
            mainCamera = Camera.main;
        }

        private void Update() {
            if (pausable.Pausing) return;
            var pos = mainCamera.WorldToViewportPoint(player.transform.position);
            if (moveArea.Contains(pos)) return;
            pausable.Pause();
            if (moveArea.xMin > pos.x) {
                StartCoroutine(CameraMoveLeft());
            } else if (moveArea.xMax < pos.x) {
                StartCoroutine(CameraMoveRight());
            }

            if (moveArea.yMin > pos.y) {
                StartCoroutine(CameraMoveDown());
            } else if (moveArea.yMax < pos.y) {
                StartCoroutine(CameraMoveUp());
            }
        }

        private IEnumerator CameraMoveRight() {
            var pos = mainCamera.WorldToViewportPoint(player.transform.position);
            while (new Rect(0.5f, moveArea.y, 0.5f, moveArea.height).Contains(pos)) {
                transform.position += new Vector3(moveSpeed * Time.deltaTime, 0, 0);
                pos = mainCamera.WorldToViewportPoint(player.transform.position);
                yield return null;
            }

            while (BorderLine.Contains(pos)) {
                transform.position += new Vector3(moveSpeed * Time.deltaTime, 0, 0);
                pos = mainCamera.WorldToViewportPoint(player.transform.position);
                yield return null;
            }

            pausable.Resume();
        }

        private IEnumerator CameraMoveLeft() {
            var pos = mainCamera.WorldToViewportPoint(player.transform.position);
            while (new Rect(0f, moveArea.y, 0.5f, moveArea.height).Contains(pos)) {
                transform.position -= new Vector3(moveSpeed * Time.deltaTime, 0, 0);
                pos = mainCamera.WorldToViewportPoint(player.transform.position);
                yield return null;
            }

            while (BorderLine.Contains(pos)) {
                transform.position -= new Vector3(moveSpeed * Time.deltaTime, 0, 0);
                pos = mainCamera.WorldToViewportPoint(player.transform.position);
                yield return null;
            }

            pausable.Resume();
        }

        private IEnumerator CameraMoveUp() {
            var pos = mainCamera.WorldToViewportPoint(player.transform.position);
            while (new Rect(moveArea.x, 0.5f, moveArea.width, 0.5f).Contains(pos)) {
                transform.position += new Vector3(0, moveSpeed * Time.deltaTime, 0);
                pos = mainCamera.WorldToViewportPoint(player.transform.position);
                yield return null;
            }

            while (BorderLine.Contains(pos)) {
                transform.position += new Vector3(0, moveSpeed * Time.deltaTime, 0);
                pos = mainCamera.WorldToViewportPoint(player.transform.position);
                yield return null;
            }

            pausable.Resume();
        }

        private IEnumerator CameraMoveDown() {
            var pos = mainCamera.WorldToViewportPoint(player.transform.position);
            while (new Rect(moveArea.x, 0f, moveArea.width, 0.5f).Contains(pos)) {
                transform.position -= new Vector3(0, moveSpeed * Time.deltaTime, 0);
                pos = mainCamera.WorldToViewportPoint(player.transform.position);
                yield return null;
            }

            while (BorderLine.Contains(pos)) {
                transform.position -= new Vector3(0, moveSpeed * Time.deltaTime, 0);
                pos = mainCamera.WorldToViewportPoint(player.transform.position);
                yield return null;
            }

            pausable.Resume();
        }
    }
}
