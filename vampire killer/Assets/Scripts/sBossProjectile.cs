using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sBossProjectile : MonoBehaviour {

    float timer=0f;

    void Update()
    {
        if (timer > 3f)
        {
            Destroy(gameObject);
        }
        timer += 1f * Time.deltaTime;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other != null)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                gameController.instance.takeDamage(1000000f);
            }else if (other.gameObject.CompareTag("Ground"))
            {
                Destroy(gameObject);
            }

        }
    }
}
