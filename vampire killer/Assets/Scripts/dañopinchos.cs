using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dañopinchos : MonoBehaviour {
    void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag.Equals("Player"))
            gameController.instance.takeDamage(20);
        if (other.gameObject.tag.Equals("PlayerWolf"))
            gameController.instance.takeDamage(10);
    }
}
