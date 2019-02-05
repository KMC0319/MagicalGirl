using Systems;
using UnityEngine;

namespace Boss {
    public class BossController : MonoBehaviour, IHitAttack {
        private enum BossState {
            Wait, 
        }
        [SerializeField] private int hp;
        
        private BossState state;
        private bool isActive;

        private Camera mainCamera;
        private BossAttack bossAttack;
        
        void Start() {
            mainCamera = Camera.main;
            bossAttack = GetComponent<BossAttack>();
        }

        void Update() {
            if (!isActive) {
                isActive = IsInCamera();
                if (isActive) Init();
                return;
            }
        }

        private void Init() {
            var player = GameObject.FindGameObjectWithTag("Player");
            transform.LookAt(new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z));
            state = BossState.Wait;
            StartCoroutine(bossAttack.WeaponBurst());
        }

        private bool IsInCamera() {
            var rect = new Rect(0, 0, 1, 1);
            return rect.Contains(mainCamera.WorldToViewportPoint(transform.position));
        }
        
        private void Dead() {
            Debug.Log("Boss is dead");
        }
        
        public void Damage(int damage) {
            hp -= damage;
            if(hp <= 0) Dead();
        }
    }
}
