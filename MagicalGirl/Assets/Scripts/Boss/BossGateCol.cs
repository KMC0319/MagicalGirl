using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;

public class BossGateCol : MonoBehaviour {
    
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            Camera.main.GetComponent<PlayerCamera>().MoveToBoss();
        }
    }
}
