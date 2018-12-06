using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

	[SerializeField] const float PROJECTILE_LIFE = 2f;
    [SerializeField] float damage;
	IEnumerator coroutine;

	// Use this for initialization
	void Start () {
		coroutine = projectileLife();
		StartCoroutine(coroutine);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D other){
		if(other.gameObject.CompareTag("Player")){
			}else if(other.gameObject.CompareTag("Enemy")){
				other.gameObject.GetComponent<Enemy>().takeDamage(damage);
				Destroy(gameObject);
			}else if(other.gameObject.CompareTag("Ground")){
				Destroy(gameObject);
			}
	}

	IEnumerator projectileLife(){
		yield return new WaitForSecondsRealtime(PROJECTILE_LIFE);
		Destroy(gameObject);
		Debug.Log("Se destruye");
	}
}
