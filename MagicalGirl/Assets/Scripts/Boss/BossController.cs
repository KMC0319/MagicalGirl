using System;
using System.Collections;
using Systems;
using UI;
using UniRx;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Boss {
    public class BossController : MonoBehaviour, IHitAttack {
        [SerializeField] private int hp;
        public int HP => hp;
        [SerializeField] private float waitTime;
        [SerializeField] private BossHP hpBar;

        private bool isActive;
        private bool canAction;
        public bool IsDead { get; private set; }

        private GameObject player;
        public GameObject Player => player;

        private Camera mainCamera;
        private BossActionBase[] bossActions;

        private void Start() {
            mainCamera = Camera.main;
            player = GameObject.FindWithTag("Player");
            bossActions = GetComponents<BossActionBase>();
            foreach (var action in bossActions) {
                action.EndStream.Subscribe(_ => StartCoroutine(Waiting()));
            }
        }

        private void Update() {
            if (!isActive) {
                isActive = IsInCamera();
                if (isActive) Init();
            }
        }

        private void SelectNextAction() {
            int num = Random.Range(0, bossActions.Length);
            Debug.Log(num);
            bossActions[num].Action();
        }

        private void Init() {
            var player = GameObject.FindGameObjectWithTag("Player");
            transform.parent.LookAt(new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z));
            SceneManager.StartBossBattle();
            StartCoroutine(Waiting());
            hpBar.Init();
        }

        IEnumerator Waiting() {
            var time = 0f;
            while (waitTime > time) {
                time += Time.deltaTime;
               
                yield return null;
            }

            SelectNextAction();
        }


        private bool IsInCamera() {
            var rect = new Rect(0, 0, 1, 1);
            return rect.Contains(mainCamera.WorldToViewportPoint(transform.position));
        }

        private void Dead() {
            IsDead = true;
            GetComponent<Animator>().SetBool("IsDead", true);
        }

        public void Damage(int damage) {
            hp -= damage;
            if (hp <= 0) Dead();
        }
    }
}
