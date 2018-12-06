using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class gameController : MonoBehaviour {
    public static gameController instance = null;
    [SerializeField] float startingHealth;
    [SerializeField] public float currentHealth;
    [SerializeField] float startingFury;
    [SerializeField] float currentFury;
    [SerializeField] GameObject canvas;
    [SerializeField] GameObject tgameover;
    [SerializeField] GameObject ttransform;
	[SerializeField] Text tfuria;
    [SerializeField] Text tvida;
    [SerializeField] GameObject tvictoria;
    [SerializeField] float startingSpecial;
    float currentSpecial;
    [SerializeField] int startingAmmo;
    [SerializeField] GameObject patrolPoints;
    [SerializeField] GameObject enemy;
    int currentAmmo;
    private bool hasKey;
    int keysRecollected;
    public int KeysRecollected {
        get {
            return keysRecollected;
        }
        set {
            keysRecollected = value;
        }
    }

    public bool HasKey {
        set {
            hasKey = value;
        }
    }
    [SerializeField] GameObject bossDoor;
    [SerializeField] GameObject player;
    [SerializeField] GameObject playerWolf;
    [SerializeField] Animator anim_HToW;
    [SerializeField] GameObject mainCamera;
    
    bool isDead = false;
    private float contador = 0f;

    private void Awake() {
        if(instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    // Use this for initialization
    void Start () {
        currentHealth = startingHealth;
        currentFury = startingFury;
        tvictoria.SetActive(false);
        tgameover.SetActive(false);
        player.SetActive(true);
        playerWolf.SetActive(false);
        //Pruebas
        currentAmmo = startingAmmo;
        currentFury = 100f;
        hasKey = false;
        isDead = false;
        Time.timeScale = 1;
        SpawnEnemies();

        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    // Update is called once per frame
    void Update () {
		if (player.activeSelf) {
			contador += Time.deltaTime;
			if (contador >= 1f) { 
				increaseFury(5);
				contador = 0f;
			}
		}
        tfuria.text = "Furia: " + currentFury;
        tvida.text = "Vida: " + currentHealth;
        if (hasKey)
            bossDoor.SetActive(false);
        if(Input.GetKeyDown("return") && isDead)
            SceneManager.LoadScene("Menu");

        if (player.activeSelf)
            playerWolf.transform.position = player.transform.position;
        else if(playerWolf.activeSelf)
            player.transform.position = playerWolf.transform.position;
    }
   
    public void reduceAmmo() {
        currentAmmo -= 1;
    }
    public void increaseAmmo(int ammo) {
        if(currentAmmo < 6)
            currentAmmo += ammo;
    }

    public bool hasAmmo() {
        return (currentAmmo > 0);
    }

    public void takeDamage(float amount) {
        currentHealth -= amount;
        //TODO: Introducir barra de vida visible, ¿Integrada en unity?.
        if(currentHealth <= 0 && !isDead)
            death();
    }
    
    public void death() {
        isDead = false;
        //TODO: Activar animacion de muerte al desactivar jugador.
        ttransform.SetActive(false);
        tgameover.SetActive(true);
        Time.timeScale = 0;
        isDead = true;
        player.SetActive(false);
        playerWolf.SetActive(false);
       
    }


    public void SpecialHability(int amount) {
        if(currentSpecial < 100)
            currentSpecial += amount;
        /*else if (currentSpecial == 100) {
            //TODO: activar habilidad especial, como con la furia
        }*/
    }

    IEnumerator timerSpecial() {
        //TODO:Activar sprite del lobo para la habilidad especial y desactivar el del lobo normal.
        yield return new WaitForSecondsRealtime(5f);//Temporalmente 5 segundos.
        //TODO:Activar sprite del lobo normal y desactivar el del lobo para la habilidad especial.
    }

    public void increaseFury(int amount) {
        if (currentFury < 100)
            currentFury += amount; 
    }
    
    public void activateFury() {//Coger tecla de habilidad especial
        if (currentFury >= 100)//y se haya apretado la tecla especial.
            StartCoroutine(Transformation());//Inicia el modo hombre lobo
    }
    
    public void activateWolf() {
        ttransform.SetActive(false);
        player.SetActive(false);
        playerWolf.transform.position = player.transform.position;
        playerWolf.SetActive(true);
        
        mainCamera.GetComponent<sCamera>().ChangePlayer(playerWolf);
    }

    public void desactivateWolf() {
        playerWolf.SetActive(false);
        player.transform.position = playerWolf.transform.position;
        mainCamera.GetComponent<sCamera>().ChangePlayer(player);
        player.SetActive(true);

    }
    
    public void openBossDoor() {
        hasKey = (keysRecollected >= 4);
    }

    void SpawnEnemies() {
        Transform currentPatrol;
        //Vector2 enemieSpawnPoint;
        Transform pointA;
        Transform pointB;
        GameObject enemyInstance;

        for (int i = 0; i < patrolPoints.transform.childCount; ++i) {
            currentPatrol = patrolPoints.transform.GetChild(i);
            pointA = currentPatrol.GetChild(0).transform;
            pointB = currentPatrol.GetChild(1).transform;

            enemyInstance = Instantiate(this.enemy);
            enemyInstance.transform.position = pointA.transform.position;

            enemyInstance.GetComponent<Enemy>().PatrolPoints[0] = pointA.gameObject;
            enemyInstance.GetComponent<Enemy>().PatrolPoints[1] = pointB.gameObject;
            enemyInstance.GetComponent<Enemy>().CurrentPatrol = pointA.gameObject;
        }
    }

    IEnumerator wolfMode() {
        activateWolf();
        yield return new WaitForSecondsRealtime(20f);
        currentFury = 0f;
        desactivateWolf();
    }

    IEnumerator Transformation() {
        anim_HToW.SetFloat("Fury",currentFury);
        yield return new WaitForSecondsRealtime(2.5f);
        anim_HToW.SetFloat("Fury", currentFury);
        StartCoroutine(wolfMode());
        currentFury = 0f;
    }

}
