using Systems;
using UnityEngine;

namespace Item {
    public class AttackCol : MonoBehaviour {
        [SerializeField] private int power;
        private void OnTriggerEnter(Collider other) {
            if (!other.CompareTag("Player") && other.GetComponent<IHitAttack>() != null) {
                other.gameObject.GetComponent<IHitAttack>().Damage(power);
            }
        }
    }
}
