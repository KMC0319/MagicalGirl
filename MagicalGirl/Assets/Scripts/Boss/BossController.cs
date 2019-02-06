using System;
using System.Collections;
using Systems;
using UI;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Boss {
    public class BossController : MonoBehaviour, IHitAttack {
        [SerializeField] private int hp;
        public int HP => hp;
        [SerializeField] private float waitTime;
        [SerializeField] private BossHP hpBar;
        [SerializeField] private Image fadeimg;

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
            if (IsDead) yield break;
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
            GetComponent<Animator>().SetTrigger("IsDead");
            StartCoroutine(Ending());
        }

        private IEnumerator Ending() {
            yield return new WaitForSeconds(1f);
            var time = 2f;
            var a = 0f;
            fadeimg.color = new Color(fadeimg.color.r, fadeimg.color.b, fadeimg.color.b, a);
            fadeimg.gameObject.SetActive(true);
            while (time >= a) {
                a += Time.deltaTime;
                fadeimg.color = new Color(fadeimg.color.r, fadeimg.color.b, fadeimg.color.b, a / time);
                yield return null;
            }

            fadeimg.color = new Color(fadeimg.color.r, fadeimg.color.b, fadeimg.color.b, 1);
            
            yield return new WaitForSeconds(1f);
            SceneManager.LoadScene(Scene.Title);
        }

        public void Damage(int damage) {
            if (IsDead) return;
            hp -= damage;
            if (hp <= 0) Dead();
        }
    }
}
