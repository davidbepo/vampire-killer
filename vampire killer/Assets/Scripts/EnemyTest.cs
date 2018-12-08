using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTest : Enemy {

    float beforePosition;

	// Use this for initialization
	void Start () {
        facingDirection = Vector2.left;
        player = GameObject.FindGameObjectWithTag("Player");
        attackHitbox.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
        checkDirection();
        if (health <= 0 && !isDead) {
            GetComponent<Animator>().SetTrigger("Death");
            Destroy(GetComponent<StateController>());
            StartCoroutine(death());
            isDead = true;
            gameController.instance.increaseAmmo(1);
        }
    }

    public override void KnockBack(Vector2 direction) {
        isKnockedBack = true;
        knockBackDirection = direction;
    }

    void checkDirection() {
        if (beforePosition > transform.position.x && facingDirection == Vector2.right)
            flip();
        else if (beforePosition < transform.position.x && facingDirection == Vector2.left)
            flip();
        beforePosition = transform.position.x;
    }

    public override void takeDamage(float damage) {
        this.health = health - damage;
    }

    void flip() {
        facingDirection = -facingDirection;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    IEnumerator death() {
        yield return new WaitForSecondsRealtime(0.5f);
        Destroy(gameObject);
    }

}
