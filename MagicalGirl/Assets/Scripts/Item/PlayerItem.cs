using UnityEngine;

namespace Item {
    public abstract class PlayerItem : ItemBase {
        [SerializeField] protected EItemName itemName;
        public virtual void Attack(){}
        protected virtual void OnTriggerEnter(Collider other) {
            if (other.CompareTag("Player")) {
                other.GetComponent<IHitItem>().GetItem(this);
            }
        }
    }
}
