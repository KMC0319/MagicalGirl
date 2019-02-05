using Systems;
using UnityEngine;

namespace Enemy {
    public abstract class BaseEnemy : MonoBehaviour, IHitAttack {
        [SerializeField] protected int hp;
        [SerializeField] protected int power;
        [SerializeField] protected float speed;
        protected GameObject player;
        private Rigidbody rigid;

        private Camera mainCamera;
        protected bool isActive;

        private void Start() {
            mainCamera = Camera.main;
            player = GameObject.FindWithTag("Player");
            rigid = GetComponent<Rigidbody>();
        }

        protected virtual void Update() {
            if (Vector3.Magnitude(transform.position - player.transform.position) > 400) {
                isActive = false;
                return;
            }

            isActive = IsInCamera();
            if (isActive) Move();
            else rigid.velocity = Vector3.zero;
        }

        private bool IsInCamera() {
            var rect = new Rect(-0.1f, -0.1f, 1.2f, 1.2f);
            return rect.Contains(mainCamera.WorldToViewportPoint(transform.position));
        }


        protected virtual void Move() {
            rigid.velocity = transform.forward * speed;
        }

        public void Damage(int damage) {
            hp -= damage;
        }

        private void OnTriggerEnter(Collider other) {
            if (other.CompareTag("Player") && other.GetComponent<IHitAttack>() != null) {
                other.GetComponent<IHitAttack>().Damage(power);
            }
        }
    }
}
