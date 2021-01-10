using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class MyPlayer : MonoBehaviourPun, IPunObservable
{
    public PhotonView PV;
    public float moveSpeed = 0.01f;
    public GameObject bulletPrefab;
    public GameObject lightmask;
    public GameObject lightmask1;
    private Vector3 smoothMove;
    private Quaternion smoothRotation;
    private Camera camera;
    public float health;
    private Rigidbody2D rb;
    [SerializeField] GameObject firePoint;
  
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        camera = Camera.main;
        health = 100;
    }
    void Update()
    {
        if(!PV.IsMine)
        {
            lightmask.SetActive(false);
            lightmask1.SetActive(false);
            SmoothMovement();
            SmoothRotation();
        }

        if(PV.IsMine)
        {

        }

        if (Input.GetKeyDown(KeyCode.Mouse0) && PV.IsMine)
        {
            GameObject bullet = PhotonNetwork.Instantiate(bulletPrefab.name, firePoint.transform.position, Quaternion.identity);
            bullet.GetComponent<PhotonView>().RPC("SetVector", RpcTarget.AllBuffered, firePoint.transform.up);
        }

    }

    void FixedUpdate()
    {
        if(PV.IsMine) ProcessInputs();
    }

    private void SmoothRotation()
    {
        transform.rotation = (Quaternion.Slerp(transform.rotation, smoothRotation, Time.deltaTime * 10));
    }

    private void SmoothMovement()
    {
        transform.position = Vector3.Lerp(transform.position, smoothMove, Time.deltaTime * 10);
    }

    private void ProcessInputs()
    {
        var move = new Vector2(0, 0);
        move.x = Input.GetAxisRaw("Horizontal");
        move.y = Input.GetAxisRaw("Vertical");
        rb.MovePosition(rb.position + move * moveSpeed * Time.deltaTime);

        Vector2 mousePos = camera.ScreenToWorldPoint(Input.mousePosition);
        Vector2 lookDir = mousePos - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        rb.SetRotation(Quaternion.Euler(new Vector3(0,0,angle)));
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
        }
        else if (stream.IsReading)
        {
            smoothMove = (Vector3)stream.ReceiveNext();
            smoothRotation = (Quaternion)stream.ReceiveNext();
        }
    }

    [PunRPC]
    public void TakeDamage(float damage)
    {
        health-=damage;
        Debug.Log(health);
    }
}
