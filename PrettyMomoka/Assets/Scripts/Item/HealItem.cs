using UnityEngine;

namespace Item {
    public class HealItem : ItemBase {
        protected override void OnTriggerEnter(Collider other) {
            if (other.CompareTag("Player")) {
                other.GetComponent<IHitItem>().Heal();
            }
        }
    }
}
