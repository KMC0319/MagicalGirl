using System.Collections;
using System.Collections.Generic;
using Systems;
using UnityEngine;

public class EnemyCol : MonoBehaviour {
    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player") && other.gameObject.GetComponent<IHitAttack>() != null) {
            other.gameObject.GetComponent<IHitAttack>().Damage(10);
        }
    }
}
