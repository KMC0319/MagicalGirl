using UnityEngine;

namespace Item {
    public abstract class ItemBase : MonoBehaviour {
        protected virtual void OnTriggerEnter(Collider other) {
            if (other.CompareTag("Player")) {
                other.GetComponent<IHitItem>().GetItem();
            }
        }
    }
}
