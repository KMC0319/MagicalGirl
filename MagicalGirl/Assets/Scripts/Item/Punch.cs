using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Item {
    public class Punch  : PlayerItem {
        [SerializeField] private Collider col;
        public override void Attack() {
            col.enabled = true;
            StartCoroutine(EndAttack());
        }

        private IEnumerator EndAttack() {
            yield return new WaitForSeconds(0.5f);
            col.enabled = false;
        }
    }
}
