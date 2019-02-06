using UnityEngine;

namespace Item {
    public class MagicalStick : PlayerItem {
        [SerializeField] private GameObject bullet;
        public override void Attack() {
            base.Attack();
            var obj = Instantiate(bullet, transform.parent);
            obj.transform.position = transform.position + new Vector3(transform.localScale.x/4f, 0, 0);
            obj.transform.localScale = transform.localScale;
        }
    }
}
