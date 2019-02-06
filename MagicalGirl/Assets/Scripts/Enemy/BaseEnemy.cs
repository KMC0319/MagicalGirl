using System;
using System.Collections;
using System.Collections.Generic;
using Systems;
using UniRx;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Enemy {
    public abstract class BaseEnemy : MonoBehaviour, IHitAttack {
        [SerializeField] protected int hp;
        [SerializeField] protected float speed;
        [SerializeField] protected GameObject bullet;
        private float attackTime = 3f;
        private SpriteRenderer render;
        private Animator animator;
        private bool damaged;
        protected GameObject player;
        private Rigidbody rigid;

        private Camera mainCamera;
        protected bool isActive;
        private bool canMove = true;

        private void Start() {
            animator = GetComponent<Animator>();
            mainCamera = Camera.main;
            player = GameObject.FindWithTag("Player");
            rigid = GetComponent<Rigidbody>();
            render = GetComponent<SpriteRenderer>();
        }

        protected virtual void Update() {
            if (Vector3.Magnitude(transform.position - player.transform.position) > 400) {
                isActive = false;
                return;
            }

            isActive = IsInCamera();
            if (isActive && canMove) Move();
            else rigid.velocity = Vector3.zero;
        }

        private bool IsInCamera() {
            var rect = new Rect(-0.1f, -0.1f, 1.2f, 1.2f);
            return rect.Contains(mainCamera.WorldToViewportPoint(transform.position));
        }

        protected virtual void Move() {
            animator.SetBool("Walk", true);
            rigid.velocity = -transform.right * speed * transform.localScale.x;
            attackTime -= Time.deltaTime;
            if (attackTime <= 0) {
                animator.SetBool("Walk", false);
                rigid.velocity = Vector3.zero;
                canMove = false;
                attackTime = Random.Range(1f, 3f);
                Attack();
            }
        }

        protected void Attack() {
            animator.SetTrigger("Attack");
            var obj = Instantiate(bullet,transform.parent);
            obj.transform.position = transform.position;
            obj.transform.localScale = transform.localScale;
            Observable.Timer(TimeSpan.FromSeconds(1f))
                .Subscribe(_ => {
                    canMove = true;
                    transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
                })
                .AddTo(this);
        }

        public void Damage(int damage) {
            if (damaged) return;
            hp -= damage;
            if(hp <= 0) Destroy(gameObject);
            else StartCoroutine(StopDamage());
        }

        private IEnumerator StopDamage() {
            var temp = 1f;
            while (temp > 0) {
                render.material.color = new Color(render.material.color.r, render.material.color.g, render.material.color.b, 0.1f);
                yield return new WaitForSeconds(0.125f);
                render.material.color= new Color(render.material.color.r, render.material.color.g, render.material.color.b, 1f);
                yield return new WaitForSeconds(0.125f);
                temp -= 0.25f;
            }

            damaged = false;
        }
    }
}
