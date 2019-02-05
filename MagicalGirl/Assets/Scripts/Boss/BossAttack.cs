using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Boss {
    public class BossAttack : MonoBehaviour {
        [SerializeField] private GameObject[] weapons;
        [SerializeField] private float shotSpeed;
        [SerializeField] private float instantiateInterval = 0.1f;
        private GameObject player;

        private void Start() {
            player = GameObject.FindWithTag("Player");
        }

        public IEnumerator WeaponBurst() {
            var num = 0;
            var baseobj = new GameObject("base");
            baseobj.transform.position = transform.position + Vector3.up;
            baseobj.transform.rotation = transform.rotation;
            baseobj.transform.Rotate(new Vector3(0, -30, 0));
            List<BossWeapon> bossWeapons = new List<BossWeapon>();
            foreach (var weapon in weapons) {
                var pos = baseobj.transform.position
                          + 2 * baseobj.transform.right * Mathf.Cos(Mathf.PI * num / (weapons.Length - 1))
                          + 2 * baseobj.transform.up * Mathf.Sin(Mathf.PI * num / (weapons.Length - 1));
                bossWeapons.Add(Instantiate(weapon, pos, Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(90,0,0))).GetComponent<BossWeapon>());
                yield return new WaitForSeconds(instantiateInterval);
                num++;
            }

            Destroy(baseobj);
            yield return new WaitForSeconds(0.5f);

            foreach (var weapon in bossWeapons) {
                weapon.Shot(shotSpeed, player);
            }
        }
    }
}
