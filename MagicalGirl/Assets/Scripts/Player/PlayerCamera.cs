using System.Collections;
using Systems;
using UnityEngine;

namespace Player {
    /// <summary>
    /// 基本プレイヤー追従
    /// プレイヤーが端に行くと動きを止めてカメラを移動する
    /// </summary>
    public class PlayerCamera : MonoBehaviour {
        private GameObject player;
        private Camera mainCamera;
        [SerializeField] private Pausable pausable;
        [SerializeField] private GameObject bossStageCenter;
        private bool isChase;
        private Vector3 offset;

        private void Start() {
            player = GameObject.FindWithTag("Player");
            mainCamera = Camera.main;
            isChase = true;
            offset = transform.position - player.transform.position;
        }

        private void LateUpdate() {
            if(isChase) transform.position = player.transform.position + offset;
        }

        public void MoveToBoss() {
            isChase = false;
            StartCoroutine(CameraMoveRight());
        }

        private IEnumerator CameraMoveRight() {
            pausable.Pause();
            var targetPos = new Vector3(bossStageCenter.transform.position.x, transform.position.y, transform.position.z);
            var playerTargetPos = mainCamera.ViewportToWorldPoint(new Vector3(1.1f,
                mainCamera.WorldToViewportPoint(player.transform.position).y,
                player.transform.position.z - mainCamera.transform.position.z));
            var moveSpeed = 20f;
            while (transform.position != targetPos) {
                transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
                player.transform.position = Vector3.MoveTowards(player.transform.position, playerTargetPos, moveSpeed / 3f * Time.deltaTime);
                yield return null;
            }
            pausable.Resume();
        }
    }
}
