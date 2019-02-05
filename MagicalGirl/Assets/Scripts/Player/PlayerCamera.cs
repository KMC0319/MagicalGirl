using System.Collections;
using System.Collections.Generic;
using Systems;
using UnityEngine;

namespace Player {
    /// <summary>
    /// プレイヤーが端に行くと動きを止めてカメラを移動する
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
            pausable.Pause();
            var targetPos = mainCamera.ViewportToWorldPoint(new Vector3(1.5f, 0.5f));
            Debug.Log(targetPos);
            var playerTargetPos = mainCamera.ViewportToWorldPoint(new Vector3(1 + BorderLine.xMin,
                mainCamera.WorldToViewportPoint(player.transform.position).y,
                player.transform.position.z - mainCamera.transform.position.z));
            while (transform.position != targetPos) {
                transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
                player.transform.position = Vector3.MoveTowards(player.transform.position, playerTargetPos, moveSpeed / 3f * Time.deltaTime);
                yield return null;
            }

            player.transform.position = playerTargetPos;
            pausable.Resume();
        }

        private IEnumerator CameraMoveLeft() {
            pausable.Pause();
            var targetPos = mainCamera.ViewportToWorldPoint(new Vector3(-0.5f, 0.5f));
            var playerTargetPos = mainCamera.ViewportToWorldPoint(new Vector3(BorderLine.xMax - 1,
                mainCamera.WorldToViewportPoint(player.transform.position).y,
                player.transform.position.z - mainCamera.transform.position.z));
            while (transform.position != targetPos) {
                transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
                player.transform.position = Vector3.MoveTowards(player.transform.position, playerTargetPos, moveSpeed / 3f * Time.deltaTime);
                yield return null;
            }

            player.transform.position = playerTargetPos;
            pausable.Resume();
        }

        private IEnumerator CameraMoveUp() {
            pausable.Pause();
            var targetPos = mainCamera.ViewportToWorldPoint(new Vector3(0.5f, 1.5f));
            var playerTargetPos = mainCamera.ViewportToWorldPoint(new Vector3(mainCamera.WorldToViewportPoint(player.transform.position).x,
                BorderLine.yMin + 1,
                player.transform.position.z - mainCamera.transform.position.z));
            while (transform.position != targetPos) {
                transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
                player.transform.position = Vector3.MoveTowards(player.transform.position, playerTargetPos, moveSpeed / 3f * Time.deltaTime);
                yield return null;
            }

            player.transform.position = playerTargetPos;
            pausable.Resume();
        }

        private IEnumerator CameraMoveDown() {
            pausable.Pause();
            var targetPos = mainCamera.ViewportToWorldPoint(new Vector3(0.5f, -0.5f));
            var playerTargetPos = mainCamera.ViewportToWorldPoint(new Vector3(mainCamera.WorldToViewportPoint(player.transform.position).x,
                BorderLine.yMax - 1,
                player.transform.position.z - mainCamera.transform.position.z));
            while (transform.position != targetPos) {
                transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
                player.transform.position = Vector3.MoveTowards(player.transform.position, playerTargetPos, moveSpeed / 3f * Time.deltaTime);
                yield return null;
            }

            player.transform.position = playerTargetPos;
            pausable.Resume();
        }
    }
}
