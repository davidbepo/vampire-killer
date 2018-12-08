using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sKey : MonoBehaviour {


    void OnTriggerEnter2D(Collider2D other) {
        if (other != null) {
            if(other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("PlayerWolf")) {
                ++gameController.instance.KeysRecollected;
                gameController.instance.openBossDoor();
                Destroy(gameObject);
            }
        }
    }
}
