using UnityEngine;

namespace Item {
    public abstract class PlayerItem : ItemBase {
        [SerializeField] protected EItemName itemName;
        [SerializeField] private Sprite sprite;
        public Sprite Sprite => sprite;
        public EItemName ItemName => itemName;

        public virtual void Attack(){}
    }
}
