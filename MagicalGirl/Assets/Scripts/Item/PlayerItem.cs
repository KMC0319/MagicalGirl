using UnityEngine;

namespace Item {
    public abstract class PlayerItem : ItemBase {
        public virtual void Attack(){}
        protected virtual void OnTriggerEnter(Collider other) {
            if (other.CompareTag("Player")) {
                other.GetComponent<IHitItem>().GetItem(this);
            }
        }
    }
}
