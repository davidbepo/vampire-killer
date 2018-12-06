using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBoss : Enemy {
	[SerializeField] GameObject tvictoria;
    [SerializeField] private GameObject projectile;
    public GameObject Projectile {
        get {
            return projectile;
        }
    }

    [SerializeField]
    private GameObject minion;
    public GameObject Minion {
        get {
            return minion;
        }
    }

    [SerializeField] private GameObject spawnPoint;
    public GameObject SpawnPoint {
        get {
            return spawnPoint;
        }
    }

    [SerializeField] private GameObject chargingPoint;
    public GameObject ChargingPoint {
        get {
            return chargingPoint;
        }
    }

    [SerializeField] private GameObject[] shootingPoints;
    public GameObject[] ShootingPoints {
        get {
            return shootingPoints;
        }
        set {
            shootingPoints = value;
        }
    }

    [SerializeField] private GameObject minionSpawnPoint;
    public GameObject MinionSpawnPoint {
        get {
            return minionSpawnPoint;
        }
    }
    public bool isSpawning { get; set; }
    public bool isShooting { get; set; }
    public bool isJumping { get; set; }
    public bool isCharging { get; set; }

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        isSpawning = true;
    }
	
	// Update is called once per frame
	void Update () {
		if(health <= 0) {
             gameObject.SetActive(false);
             tvictoria.SetActive(true);
		 }
	}

    public override void takeDamage(float damage) {
        health -= damage;
    }

    public override void KnockBack(Vector2 direction) {
        Debug.Log("Nada");
    }
}
