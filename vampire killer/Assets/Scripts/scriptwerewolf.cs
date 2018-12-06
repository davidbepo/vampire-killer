using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scriptwerewolf : MonoBehaviour {
	[SerializeField] Animator animator;
    [SerializeField] float jumpForce;
    [SerializeField] float regeneracion;
    [SerializeField] GameObject collattack;
    Rigidbody2D rb;
	
	float va = 3f;
	float vc = 5f;
	private float contador = 0f;
	
	bool isDoingAction;
	int isMoving = 0;
	
	// Use this for initialization
	void Start () {
		collattack.SetActive(false);
        rb = GetComponent<Rigidbody2D>();
        isDoingAction = false;
	}

    private void OnEnable() {
        isDoingAction = false;
    }
	
	IEnumerator attack() { //codigo para hacer el ataque normal
		animator.SetBool("attack" , true);
        collattack.GetComponent<scriptatk>().dmg = 25f;
        collattack.SetActive(true);
		isDoingAction = true;
		yield return new WaitForSecondsRealtime(0.5f);
		animator.SetBool("attack" , false);
		collattack.SetActive(false);
		isDoingAction = false;
	}
	
	IEnumerator special() { //codigo para hacer el ataque especial
		animator.SetBool("SpecialAttack", true);
        collattack.GetComponent<scriptatk>().dmg = 50f;
        collattack.SetActive(true);
		isDoingAction = true;
		yield return new WaitForSecondsRealtime(0.25f);
		animator.SetBool("SpecialAttack", false);
		collattack.SetActive(false);
		isDoingAction = false;
	}
	
	IEnumerator jump() {
		isDoingAction = true;
		animator.SetBool("jump", true);
		Vector3 position = transform.position;
		if(GetComponent<Rigidbody2D>().velocity.y > -0.1) {
			position.y+=3;
			transform.position = position;
		}
		yield return new WaitForSecondsRealtime(0.5f);
		animator.SetBool("jump", false);
		isDoingAction = false;
	}
	
	// Update is called once per frame
	void Update () {
		contador += Time.deltaTime;
        checkJumpingAnimation();
        //regeneracion en modo hombre lobo
        if(gameController.instance.currentHealth < 100) {
			if (contador >= 0.5f) { 
				gameController.instance.currentHealth += regeneracion;
				contador = 0f;
			}
		}
		if(Input.GetKeyUp(KeyCode.LeftAlt)) {
			animator.SetBool("run", false);
			isDoingAction = false;
		}
		
		if(Input.GetKey(KeyCode.LeftArrow)) {
			if(!isDoingAction) {
				if(isMoving < 2)
					animator.SetBool("move", true);
				isMoving = 1;
				transform.localRotation = Quaternion.Euler(0, 180, 0);
				transform.position += Vector3.left * va * Time.deltaTime;
				if(Input.GetKey(KeyCode.LeftAlt)) {
					isMoving = 2;
					animator.SetBool("move", false);
					animator.SetBool("run", true);
					transform.position += Vector3.left * vc * Time.deltaTime;
				}
			}
		} else if(!(Input.GetKey(KeyCode.RightArrow))){
			animator.SetBool("move", false);
			isMoving = 0;
		}
		if(Input.GetKey(KeyCode.RightArrow)) {
			if(!isDoingAction) {
				if(isMoving < 2)
					animator.SetBool("move", true);
				isMoving = 1;
				transform.localRotation = Quaternion.Euler(0, 0, 0);
				transform.position += Vector3.right * va * Time.deltaTime;
				if(Input.GetKey(KeyCode.LeftAlt)) {
					isMoving = 2;
					animator.SetBool("move", false);
					animator.SetBool("run", true);
					transform.position += Vector3.right * vc * Time.deltaTime;
				}
			}
		} else if(!(Input.GetKey(KeyCode.LeftArrow))) {
			animator.SetBool("move", false);
			isMoving = 0;
		}
		if(Input.GetKeyDown(KeyCode.Q)) {
			//ataque normal
			if(!isDoingAction && isMoving == 0)
				StartCoroutine(attack());
		}
		/*if(Input.GetKey(KeyCode.W)) {
			//ataque fuerte
		}*/
		if(Input.GetKey(KeyCode.E)) {
			//ataque especial
			if(!isDoingAction && isMoving == 0)
				StartCoroutine(special());
		}
		if((Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Space))&&onGround()) {
			if(!isDoingAction) {
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                animator.SetBool("isJumping", true);
                //StartCoroutine(jump());
            }
		}
	}

    void checkJumpingAnimation() {
        if(rb.velocity.y < 0 && animator.GetBool("isJumping")) {
            animator.SetBool("isJumping", false);
            animator.SetBool("isFalling", true);

        }
        if(rb.velocity.y == 0 && animator.GetBool("isFalling"))
            animator.SetBool("isFalling", false);
    }

    bool onGround() {
        return(rb.velocity.y == 0);
    }
}
