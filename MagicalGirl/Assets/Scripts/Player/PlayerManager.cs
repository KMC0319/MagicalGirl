using Systems;
using UnityEngine;

namespace Player {
    public class PlayerManager : MonoBehaviour, IHitAttack {
        [SerializeField]private int hp;
        public int HP => hp;
        private int maxHp;

        private void Start() {
            maxHp = hp;
        }

        public void Damage(int damage) {
            hp = Mathf.Clamp(hp - damage, 0, maxHp);
            if(hp <= 0) Dead();
        }

        private void Dead() {
            
        }
    }
}
