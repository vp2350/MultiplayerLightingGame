using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public GameObject playerPrefab;
    // Start is called before the first frame update
    void Start()
    {
        SpawnPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnPlayer()
    {
        PhotonNetwork.Instantiate(playerPrefab.name, transform.position, playerPrefab.transform.rotation);
    }
}
