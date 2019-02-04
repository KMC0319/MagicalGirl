using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Systems;


public class PlayerManager : IHitAttack
{
    public int hp;
    
    public void Damage(int damage)
    {
        hp -= damage;
    }
    

}
