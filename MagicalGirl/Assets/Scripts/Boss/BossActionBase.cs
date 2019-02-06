using System;
using UniRx;
using UnityEngine;

namespace Boss {
    public abstract class BossActionBase : MonoBehaviour{
        private readonly Subject<Unit> endStream = new Subject<Unit>();
        public IObservable<Unit> EndStream => endStream;
        protected Animator animator;
        protected Rigidbody rigid;
        protected BossController bossController;
        protected GameObject Player => bossController.Player;

        protected virtual void Start() {
            animator = GetComponent<Animator>();
            rigid = GetComponentInParent<Rigidbody>();
            bossController = GetComponent<BossController>();
        }

        public virtual void Action() {
        }
        protected void End() {
            endStream.OnNext(Unit.Default);
        }
    }
}
