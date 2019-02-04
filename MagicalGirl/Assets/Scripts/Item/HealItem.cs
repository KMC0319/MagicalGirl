using UnityEngine;

namespace Item {
    public class HealItem : ItemBase {
        [SerializeField] private int healPower;
        protected override void OnTriggerEnter(Collider other) {
            if (other.CompareTag("Player")) {
                other.GetComponent<IHitItem>().Heal(healPower);
            }
        }
    }
}
