using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sEnemyHitbox : MonoBehaviour {

    // Use this for initialization
    private float damage;
    public float Damage
    {
        set
        {
            damage = value;
        }
    }
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other != null)
        {
            if(other.gameObject.CompareTag("Player")){
                Debug.Log("Damage player for " + damage);
                //Direccion en la que va a ser empujado el jugador
                Vector2 direction = checkDirection(other);
                other.gameObject.GetComponent<Player>().knockBack(direction);
                gameController.instance.takeDamage(damage);
            }else if (other.gameObject.CompareTag("PlayerWolf"))
            {
                //Direccion en la que va a ser empujado el enemigo
                Vector2 direction = checkDirection(other);
                //Si el jugador esta en forma de hombrelobo el enemigo es empujado
                this.transform.parent.gameObject.GetComponent<Enemy>().KnockBack(direction);
            }
            
        }
    }

    Vector2 checkDirection(Collider2D other)
    {
        Vector2 direction;
        if (this.transform.parent.position.x > other.gameObject.transform.position.x)
        {
            direction = Vector2.left;
        }
        else
        {
            direction = Vector2.right;
        }
        return direction;
    }
}
