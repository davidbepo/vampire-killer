using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Clase asociada al jugador que se encarga de su control

public class Player : MonoBehaviour {

	private Rigidbody2D rb;
	private float distToGround;
	private float moveHorizontal;
	private bool isAttacking;
    private IEnumerator coroutine;
	//Hitbox
	[SerializeField] GameObject attackRight;
	[SerializeField] LayerMask groundLayer;
	[SerializeField] float speed;
	[SerializeField] float jumpForce;
    [SerializeField] GameObject projectile;
    [SerializeField] float projectilespeed;
    [SerializeField] Animator animator;
	//[SerializeField] GameObject gamecontroller;
    [SerializeField] float damage;

    //true = right  : false = left
    bool facing;
    Vector2 facingDirection = Vector2.right;

	void Start () {
		rb = GetComponent<Rigidbody2D>();
		attackRight.SetActive(false);
		isAttacking = false;
	}

    void Update() {

        checkJumpingAnimation();
        moveHorizontal = Input.GetAxis("Horizontal");
        if (moveHorizontal != 0 && !isAttacking)
            animator.SetBool("Walking", true);
        else
            animator.SetBool("Walking", false);

		if(!isAttacking) {
	        if (moveHorizontal > 0 && facingDirection.Equals(Vector2.left))
	            flip();
	        else if (moveHorizontal < 0 && facingDirection.Equals(Vector2.right))
	            flip();
	
			if(onGround())
		        rb.transform.position = new Vector2(rb.transform.position.x + moveHorizontal * speed * Time.deltaTime, rb.transform.position.y);
			else {
				float velocityx = rb.velocity.x + (moveHorizontal * speed *Time.deltaTime);
				rb.velocity = new Vector2(Mathf.Clamp(velocityx, -10f, 10f), rb.velocity.y );
			}
		
		    if ((Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Space)) && onGround()) {
				rb.AddForce(Vector2.up  * jumpForce, ForceMode2D.Impulse);
				animator.SetBool("isJumping", true);
			}
		
			if (Input.GetKeyDown(KeyCode.Q)) {
				lightAttack();
			}
			
			if(Input.GetKeyDown(KeyCode.E)&&!isAttacking&&gameController.instance.hasAmmo()) {
				shoot();
					gameController.instance.reduceAmmo();
			}
			
			/*if(Input.GetKeyDown(KeyCode.R)) {
				//ultimate();
			}*/
			
			if(Input.GetKeyDown(KeyCode.T)) {
				gameController.instance.activateFury();
				StartCoroutine(TransformCoroutine(2.5f));
			}
			
		}	
		
	}
	

	//Comprueba si el jugador se encuentra sobre tierra
	bool onGround() {
		Vector2 position = transform.position;
		Vector2 direction = Vector2.down;
		float extentsy = gameObject.GetComponent<Collider2D>().bounds.extents.y;
		float extentsx = gameObject.GetComponent<Collider2D>().bounds.extents.x;
		float distance = 0.1f + extentsy;
		RaycastHit2D hit = Physics2D.Raycast(position, direction, distance, groundLayer);
		RaycastHit2D hit2 = Physics2D.Raycast(new Vector2(transform.position.x+extentsx, transform.position.y), direction, distance, groundLayer);
		RaycastHit2D hit3 = Physics2D.Raycast(new Vector2(transform.position.x-extentsx, transform.position.y), direction, distance, groundLayer);
		if(hit.collider!=null || hit2.collider!=null || hit3.collider!=null) {
			return true;
		}
		return false;
	}
	
	void OnEnable() {
		isAttacking = false;
	}

	void shoot() {
            coroutine = shootCoroutine(0.6f);
            StartCoroutine(coroutine);
            animator.SetBool("Shoot", true);
	}
	
	void lightAttack() {
		coroutine = attackCooldown(0.6f);
		StartCoroutine(coroutine);
        animator.SetBool("Attack", true);
        attackRight.GetComponent<PlayerAttack>().setDamage(damage);
        attackRight.GetComponent<PlayerAttack>().setDirection(facingDirection);
        attackRight.SetActive(true);
    }
	
	void checkJumpingAnimation() {
        if (rb.velocity.y < 0) {
            animator.SetBool("isJumping", false);
            animator.SetBool("isFalling", true);
        }
        if(rb.velocity.y == 0)
            animator.SetBool("isFalling", false);
	}

	//Empuja al jugador en la direccion pasada como parametro
    public void knockBack(Vector2 direction) {
        if(direction == Vector2.up) {
            coroutine = attackCooldown(0.5f);
            StartCoroutine(coroutine);
            rb.AddForce(new Vector2(Mathf.Floor(Random.Range(-10f,10f)), 5f), ForceMode2D.Impulse);
        }
        else if (!isAttacking) {
            coroutine = attackCooldown(0.2f);
            StartCoroutine(coroutine);
            rb.AddForce(new Vector2(200f*direction.x, 0f), ForceMode2D.Force);
            /*if (facing)
            {
                rb.AddForce(new Vector2(-200f, 0f), ForceMode2D.Force);
            }
            else
            {
                rb.AddForce(new Vector2(200f, 0f), ForceMode2D.Force);
            }*/
        }
    }

    //Cambia la orientacion del sprite 
    void flip() {
        facingDirection = -facingDirection;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
    
	IEnumerator attackCooldown(float time) {
		isAttacking = true;
		yield return new WaitForSecondsRealtime(time);
		isAttacking = false;
		attackRight.SetActive(false);
        animator.SetBool("Attack", false);
    }

    IEnumerator shootCoroutine(float time) {
        isAttacking = true;
        yield return new WaitForSecondsRealtime(time);
        isAttacking = false;
        float sign = facingDirection.x;
        Vector2 projectileVector = new Vector2(transform.position.x + 1.5f * sign, transform.position.y + 0.7f);
        GameObject clone = Instantiate(projectile, projectileVector, transform.rotation);
        clone.GetComponent<Rigidbody2D>().velocity = new Vector2(sign>0 ? projectilespeed : -projectilespeed, 0f);
        animator.SetBool("Shoot", false);
    }

    IEnumerator TransformCoroutine(float time) {
        isAttacking = true;
        yield return new WaitForSecondsRealtime(time);
        isAttacking = false;
    }

}
