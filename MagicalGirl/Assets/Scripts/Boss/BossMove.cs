using System.Collections;
using UnityEngine;

namespace Boss {
    public class BossMove : BossActionBase {
        private float speed = 4f;

        public override void Action() {
            base.Action();
            StartCoroutine(Walk());
        }

        IEnumerator Walk() {
            animator.SetBool("Walk", true);
            var walktime = 3f;
            while (walktime > 0) {
                walktime -= Time.deltaTime;
                rigid.velocity = transform.parent.forward * speed;
                yield return null;
                if (bossController.IsDead) {
                    rigid.velocity = Vector3.zero;
                    yield break;
                }
            }

            rigid.velocity = Vector3.zero;
            transform.parent.LookAt(new Vector3(Player.transform.position.x, transform.parent.position.y, Player.transform.position.z));
            animator.SetBool("Walk", false);
            End();
        }
    }
}
