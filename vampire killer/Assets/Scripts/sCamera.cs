using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sCamera : MonoBehaviour {

    [SerializeField] GameObject player;
    private Vector3 v3;
    public bool inBossRoom;
    private Vector3 offset;

    void Start()
    {
        v3 = transform.position - player.transform.position;
        offset = new Vector3(0f, 4.5f, 0f);
        inBossRoom = false;
    }

    void LateUpdate()
    {
        if (!inBossRoom)
        {
            transform.position =player.transform.position + v3;
        }
        else
        {
            transform.position = player.transform.position + v3 + offset;
        }
    }

    public void ChangePlayer(GameObject player)
    {
        this.player = player;
    }
}
