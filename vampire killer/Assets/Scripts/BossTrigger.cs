using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BossTrigger : MonoBehaviour {


    [SerializeField] GameObject boss;
    [SerializeField] GameObject goCamera;
    Camera camera;

    void Start() {
        camera = goCamera.GetComponent<Camera>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other != null) {
            if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("PlayerWolf")) {
                boss.SetActive(true);
                camera.orthographicSize = 9;
                goCamera.GetComponent<sCamera>().inBossRoom = true;
            }
        }
    }
}
