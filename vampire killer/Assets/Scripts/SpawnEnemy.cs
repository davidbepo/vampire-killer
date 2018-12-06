using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour {

    public GameObject[] enemies;
    public Vector3 SpawnValues;
    public float spawnWait;
    public float spawnMostWait;
    public float spawnLeasWait;
    public int startWait;
    public bool stop;
    //int randEnemy;
    // Use this for initialization
    void Start () {
        StartCoroutine(waitSpawner());
    }

    // Update is called once per frame
    void Update () {

	}
    IEnumerator waitSpawner() {
        yield return new WaitForSeconds(3);
        while (!stop) {
            //randEnemy = Random.Range (0,0);
            //Vector3 SpawnPosition = new Vector3(Random.Range(-SpawnValues.x, SpawnValues.x), 1, Random.Range(-SpawnValues.z, SpawnValues.z));
            Instantiate(enemies[0], transform.position, gameObject.transform.rotation);
            yield return new WaitForSeconds(spawnWait);
        }
    }
}
