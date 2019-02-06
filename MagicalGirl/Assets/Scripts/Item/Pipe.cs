using Systems;
using UnityEngine;

namespace Item {
    public class Pipe : MonoBehaviour {
        [SerializeField] int power = 15;

        private void Start() {
            Destroy(gameObject, 5f);
            GetComponent<Rigidbody>().velocity = new Vector3(transform.localScale.x * 3, 0, 0);
        }

        private void OnTriggerEnter(Collider other) {
            if (!other.CompareTag("Player") && other.gameObject.GetComponent<IHitAttack>() != null) {
                Destroy(gameObject);
                other.gameObject.GetComponent<IHitAttack>().Damage(power);
            }
        }
    }
}