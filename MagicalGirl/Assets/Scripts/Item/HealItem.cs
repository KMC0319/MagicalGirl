using UnityEngine;

namespace Item {
    public class HealItem : ItemBase {
        [SerializeField] private int healPower;
        protected override void OnTriggerEnter(Collider other) {
            if (other.GetComponent<IHitItem>() != null) {
                other.GetComponent<IHitItem>().Heal(healPower);
            }
        }
    }
}
