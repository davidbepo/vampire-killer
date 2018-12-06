using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Clase abstracta de la cual herederan todos los enemigos

public abstract class Enemy : MonoBehaviour {
    //Puntos de patrulla del enemigo
    [SerializeField]
    protected GameObject[] patrolPoints;
    public GameObject[] PatrolPoints
    {
        get
        {
            return patrolPoints;
        }
        set
        {
            patrolPoints = value;
        }
    }
    //Vida actual del enemigo
    [SerializeField]
    protected float health;
    public float Health
    {
        get
        {
            return health;
        }
        set
        {
            health = value;
        }
    }
    //Vida maxima del enemigo
    [SerializeField]
    protected float maxHealth;
    public float MaxHealth
    {
        get
        {
            return maxHealth;
        }
        set
        {
            health = value;
        }
    }
    [SerializeField]
    protected Vector2 facingDirection;
    public Vector2 FacingDirection
    {
        get
        {
            return facingDirection;
        }
        set
        {
            facingDirection = value;
        }
    }
    //Si el enemigo ha sido empujado
    protected bool isKnockedBack;
    public bool IsKnockedBack
    {
        get
        {
            return isKnockedBack;
        }
        set
        {
            isKnockedBack = value;
        }
    }
    //Direccion en la que el enemigo ha sido empujado
    protected Vector2 knockBackDirection;
    public Vector2 KnockBackDirection
    {
        get
        {
            return knockBackDirection;
        }
        set
        {
            knockBackDirection = value;
        }
    }
    //Maxima distancia que se puede alejar de los puntos patrulla el enemigo antes de volver a patrullar
    [SerializeField]
    protected float maxDistance;
    public float MaxDistance
    {
        get
        {
            return maxDistance;
        }
        set
        {
            maxDistance = value;
        }
    }
    //Destino objetivo del enemigo patrullando
    [SerializeField]
    protected GameObject currentPatrol;
    public GameObject CurrentPatrol
    {
        get
        {
            return currentPatrol;
        }
        set
        {
            currentPatrol = value;
        }
    }
    //velocidad de persecucion
    [SerializeField]
    protected float followSpeed;
    public float FollowSpeed
    {
        get
        {
            return followSpeed;
        }
        set
        {
            followSpeed = value;
        }
    }
    protected float maxSpeed;
    public float MaxSpeed
    {
        get
        {
            return maxSpeed;
        }
        set
        {
            maxSpeed = value;
        }
    }
    [SerializeField]
    protected float acceleration;
    public float Acceleration
    {
        get
        {
            return acceleration;
        }
        set
        {
            acceleration = value;
        }
    }
    [SerializeField]
    protected float damage;
    public float Damage
    {
        get
        {
            return damage;
        }
        set
        {
            damage = value;
        }
    }
    protected GameObject player;
    public GameObject Player
    {
        get
        {
            return player;
        }
        set
        {
            player = value;
        }
    }

    [SerializeField]
    protected GameObject attackHitbox;
    public GameObject AttackHitbox
    {
        get
        {
            return attackHitbox;
        }
        set
        {
            AttackHitbox = value;
        }
    }

    protected bool isAttacking;
    public bool IsAttacking
    {
        get
        {
            return isAttacking;
        }
        set
        {
            isAttacking = value;
        }
    }

    protected bool isDead;
    protected IEnumerator coroutine;

    void Update () {
		if(health<=0){
			Destroy(gameObject);
		}
	}

    public abstract void takeDamage(float damage);

    public abstract void KnockBack(Vector2 direction);

}
