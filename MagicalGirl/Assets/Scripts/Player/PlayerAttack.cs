﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Item;
using Player;
using UI;
using UnityEngine;

public class PlayerAttack : MonoBehaviour, IHitItem {
    [SerializeField] private EquipItem equipItem;
    public List<PlayerItem> Items;
    private PlayerManager playerManager;
    private Animator animator;
    private int currentItemNum = 0;
    private bool isAttacking;

    private void Start() {
        animator = GetComponent<Animator>();
        playerManager = GetComponent<PlayerManager>();
        Items = new List<PlayerItem>();
        Items.Add(GetComponent<Punch>());
        Items.Add(GetComponent<MagicalStick>());
    }

    public void GetItem(EItemName itemName) {
        if (Items.Any(i=>i.ItemName == itemName)) return;
        Items.Add(gameObject.GetComponents<PlayerItem>().First(i => i.ItemName == itemName));
    }

    public void Heal(int healPower) {
        playerManager.Damage(-healPower);
    }

    private void Update() {
        if (Input.GetButtonDown("ChangeItem") && !isAttacking) {
            ChangeItem();
        }

        animator.SetBool("attack", Input.GetButton("Attack"));
        animator.SetInteger("attackNumber", (int) Items[currentItemNum].ItemName);
    }

    private void ChangeItem() {
        currentItemNum = (currentItemNum + 1) % Items.Count;
        equipItem.ChangeItem(Items[currentItemNum].Sprite);
    }

    public void StartAttack(int num) {
        Items.First(i => i.ItemName == (EItemName) num).Attack();
    }
}
