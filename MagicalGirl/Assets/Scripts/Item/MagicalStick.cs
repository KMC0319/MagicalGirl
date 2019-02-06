using UnityEngine;

namespace Item {
    public class MagicalStick : PlayerItem {
        [SerializeField] private GameObject bullet;
        public override void Attack() {
            base.Attack();
            var obj = Instantiate(bullet, transform.parent);
            obj.transform.position = transform.position;
            obj.transform.localScale = transform.localScale;
        }
    }
}
