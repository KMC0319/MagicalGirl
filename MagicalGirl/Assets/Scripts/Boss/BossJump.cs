using System.Collections;
using UnityEngine;

namespace Boss {
    public class BossJump : BossActionBase {
        private LayerMask groundLayer;
        protected override void Start() {
            base.Start();
            groundLayer = LayerMask.GetMask("Ground");
        }

        public override void Action() {
            base.Action();
            StartCoroutine(Jump());
        }

        private IEnumerator Jump() {
            animator.SetBool("OnGround", false);
            animator.SetTrigger("Jump");
            rigid.AddForce((transform.parent.forward + transform.parent.up) * 10f, ForceMode.Impulse);
            bool onGround = false;
            while (!onGround) {
                yield return new WaitForSeconds(0.1f);
                onGround = Physics.Raycast(transform.parent.position, -Vector3.up, 1f, groundLayer);
                animator.SetBool("OnGround", onGround);
                Debug.Log(onGround);
            }
            rigid.velocity = Vector3.zero;
            transform.parent.LookAt(new Vector3(Player.transform.position.x, transform.parent.position.y, Player.transform.position.z));
            End();
        }
    }
}
