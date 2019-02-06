using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace Boss {
    public class WeaponBrust : BossActionBase {
        [SerializeField] private GameObject[] weapons;
        [SerializeField] private float shotSpeed;
        [SerializeField] private float instantiateInterval = 0.1f;

        protected override void Start() {
            base.Start();
        }

        public override void Action() {
            base.Action();
            StartCoroutine(WeaponBurst());
        }

        private IEnumerator WeaponBurst() {
            animator.SetTrigger("Attack");
            var num = 0;
            var baseobj = new GameObject("base");
            baseobj.transform.position = transform.parent.position + Vector3.up;
            baseobj.transform.rotation = transform.parent.rotation;
            baseobj.transform.Rotate(new Vector3(0, -30, 0));
            List<BossWeapon> bossWeapons = new List<BossWeapon>();
            foreach (var weapon in weapons) {
                var pos = baseobj.transform.position
                          + 2 * baseobj.transform.right * Mathf.Cos(Mathf.PI * num / (weapons.Length - 1))
                          + 2 * baseobj.transform.up * Mathf.Sin(Mathf.PI * num / (weapons.Length - 1));
                bossWeapons.Add(Instantiate(weapon, pos, Quaternion.Euler(transform.parent.rotation.eulerAngles + new Vector3(90, 0, 0)))
                    .GetComponent<BossWeapon>());
                yield return new WaitForSeconds(instantiateInterval);
                num++;
            }

            Destroy(baseobj);
            yield return new WaitForSeconds(0.5f);

            transform.parent.LookAt(new Vector3(Player.transform.position.x, transform.position.y, Player.transform.position.z));
            foreach (var weapon in bossWeapons) {
                weapon.Shot(shotSpeed, Player);
            }

            yield return new WaitForSeconds(1f);

            End();
        }
    }
}
