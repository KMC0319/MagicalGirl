using UnityEngine;

namespace Item {
    public class FallItem : ItemBase {
        [SerializeField] private EItemName item;

        protected void OnTriggerEnter(Collider other) {
            if (other.GetComponent<IHitItem>() != null) {
                other.GetComponent<IHitItem>().GetItem(item);
                Destroy(gameObject);
            }
        }
    }
}
