using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;

public class BossGateCol : MonoBehaviour {
    private bool once;
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player") && !once) {
            Camera.main.GetComponent<PlayerCamera>().MoveToBoss();
            once = true;
        }
    }
}
