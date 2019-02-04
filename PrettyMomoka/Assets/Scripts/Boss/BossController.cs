using Systems;
using UnityEngine;

namespace Boss {
    public class BossController : MonoBehaviour, IHitAttack {
        private enum BossState {
            Wait, 
        }
        [SerializeField] private int hp;
        private Camera mainCamera;
        private BossState state;
        private bool isActive;

        void Start() {
            mainCamera = Camera.main;
        }

        void Update() {
            if (!isActive) {
                isActive = IsInCamera();
                if (isActive) Init();
                return;
            }
        }

        private void Init() {
            state = BossState.Wait;
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
