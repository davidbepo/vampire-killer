using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Clase asociada a los collider de ataque del jugador

public class PlayerAttack : MonoBehaviour {

    private float damage;
	private float count = 0.001f;
    // true = right;  false = left;
    Vector2 direction;
    //Posicion inicial en el sistema cartesiano del jugador
	[SerializeField] float positionx;

	void Start () {
		transform.localPosition = new Vector2(positionx, 0.0f);
	}

	void OnEnable(){
		transform.localPosition = new Vector2(positionx, 0.0f);
	}
	
	void Update () {
		transform.localPosition = new Vector2(transform.localPosition.x + count, 0.0f);
		count+=0.001f + Time.deltaTime;
		if(count>0.002f){
			count =0.001f;		
		}
	}
	
	void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Enemy")) {
			other.gameObject.GetComponent<Enemy>().takeDamage(damage);
            other.gameObject.GetComponent<Enemy>().KnockBack(direction);
		}
	}
	
	public void setDamage(float damage) {
		this.damage = damage;
	}
	
    public void setDirection(Vector2 direction) {
        this.direction = direction;
    }
}
