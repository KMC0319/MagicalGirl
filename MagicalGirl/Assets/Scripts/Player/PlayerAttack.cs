using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Item;
using UnityEngine;

public class PlayerAttack : MonoBehaviour, IHitItem
{
    public List<PlayerItem> Items;

    public void GetItem( PlayerItem item)
    {
        Items.Add(gameObject.GetComponents<PlayerItem>().First(i => i.ItemName == item.ItemName));
    }
    public void Heal(int healPower)
    {

    }

}
