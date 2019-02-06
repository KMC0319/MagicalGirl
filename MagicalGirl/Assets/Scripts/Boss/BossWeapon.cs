using Systems;
using UnityEngine;

namespace Boss {
    public class BossWeapon : MonoBehaviour {
        [SerializeField] private float chaseTime;
        [SerializeField] private int damage;
        private float speed;
        private GameObject player;
        private Rigidbody rigid;
        private bool isShoot;

        private void Start() {
            rigid = GetComponentInParent<Rigidbody>();
            Destroy(gameObject, 5f);
        }

        public void Shot(float _speed, GameObject _player) {
            player = _player;
            speed = _speed;
            isShoot = true;
            transform.LookAt(player.transform);
            rigid.velocity = transform.forward * speed;
        }

        private void Update() {
            if (!isShoot) return;
            chaseTime -= Time.deltaTime;
            if (chaseTime > 0) {
                var targetRotation = Quaternion.LookRotation(player.transform.position - transform.position);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 1 / 2f);
            }

            rigid.velocity = transform.forward * speed;
        }

        private void OnTriggerEnter(Collider other) {
            if (other.CompareTag("Player") && other.gameObject.GetComponent<IHitAttack>() != null) {
                other.gameObject.GetComponent<IHitAttack>().Damage(damage);
            }

            if (other.CompareTag("Ground")) {
                speed = 0;
            }
        }
    }
}
