using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class MyBullet : MonoBehaviourPun
{
    public PhotonView PV;
    public float destroyTime = 2f;
    private Vector3 forwardVector;
    private Vector3 smoothMove;
    private bool canHit;
   
    // Start is called before the first frame update
    void Start()
    {
        canHit = true;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(forwardVector * Time.deltaTime * 10f); //if(!PV.IsMine) SmoothMovement();
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (canHit)
        {
            canHit = false;
            if (col.collider.tag == "Player")
            {
                col.collider.gameObject.GetComponent<PhotonView>().RPC("TakeDamage", RpcTarget.All, 10f);
            }
        }
        if(photonView.IsMine) PhotonNetwork.Destroy(this.gameObject);
    }
    private void SmoothMovement()
    {
        transform.position = Vector3.Lerp(transform.position, smoothMove, Time.deltaTime * 10);
    }

    [PunRPC]
    public void SetVector(Vector3 forward)
    {
        forwardVector = forward;
    }

}
