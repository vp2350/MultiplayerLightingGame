using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class MyPlayer : MonoBehaviourPun, IPunObservable
{
    public PhotonView PV;
    public float moveSpeed = 0.01f;

    private Vector3 smoothMove;
    private Quaternion smoothRotation;
    private Camera camera;
    private Rigidbody2D rb;
  
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        camera = Camera.main;
    }
    void Update()
    {
        if(!PV.IsMine)
        {
            SmoothMovement();
            SmoothRotation();
        }
    }

    void FixedUpdate()
    {
        if(PV.IsMine) ProcessInputs();
    }

    private void SmoothRotation()
    {
        rb.SetRotation(Quaternion.Slerp(transform.rotation, smoothRotation, Time.deltaTime * 10));
    }

    private void SmoothMovement()
    {
        rb.MovePosition(Vector3.Lerp(transform.position, smoothMove, Time.deltaTime * 10));
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
}
