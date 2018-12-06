using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class scriptatk : MonoBehaviour {
	
	private float count = 0.001f;
	public float dmg;
	[SerializeField] float positionx;
	
	// Use this for initialization
	void Start () {
		
	}

	void OnEnable () {
		transform.localPosition = new Vector2(positionx, 0.0f);
	}
	
	// Update is called once per frame
	void Update () {
		transform.localPosition = new Vector2(transform.localPosition.x + count, 0.0f);
		count+=0.001f + Time.deltaTime;
		if(count>0.002f){
			count = 0.001f;		
		}
	}
	
	
	 void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Enemy") {
			other.gameObject.GetComponent<Enemy>().takeDamage(dmg);
        }
     }
}
