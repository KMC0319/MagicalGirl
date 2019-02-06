using System.Collections;
using Systems;
using UnityEngine;
using UnityEngine.UI;

namespace Player {
    public class PlayerManager : MonoBehaviour, IHitAttack {
        [SerializeField] private int hp;
        [SerializeField] private Image fadeimg;
        private SpriteRenderer render;
        public int HP => hp;
        private int maxHp;
        private bool damaged = false;

        private void Start() {
            render = GetComponent<SpriteRenderer>();
            maxHp = hp;
        }

        public void Damage(int damage) {
            if (damaged) return;
            damaged = true;
            hp = Mathf.Clamp(hp - damage, 0, maxHp);
            if (hp <= 0) StartCoroutine(Dead());
            else StartCoroutine(StopDamage());
        }

        private IEnumerator StopDamage() {
            var temp = 2f;
            while (temp > 0) {
                render.material.color = new Color(render.material.color.r, render.material.color.g, render.material.color.b, 0.1f);
                yield return new WaitForSeconds(0.125f);
                render.material.color= new Color(render.material.color.r, render.material.color.g, render.material.color.b, 1f);
                yield return new WaitForSeconds(0.125f);
                temp -= 0.25f;
            }

            damaged = false;
        }

        private IEnumerator Dead() {
            yield return new WaitForSeconds(1f);
            var time = 2f;
            var a = 0f;
            fadeimg.color = new Color(fadeimg.color.r, fadeimg.color.b, fadeimg.color.b, a);
            fadeimg.gameObject.SetActive(true);
            while (time >= a) {
                a += Time.deltaTime;
                fadeimg.color = new Color(fadeimg.color.r, fadeimg.color.b, fadeimg.color.b, a / time);
                yield return null;
            }

            fadeimg.color = new Color(fadeimg.color.r, fadeimg.color.b, fadeimg.color.b, 1);

            yield return new WaitForSeconds(1f);
            SceneManager.LoadScene(Scene.Title);
        }
    }
}
