using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Movement : MonoBehaviour
{
    public float movespeed;

    public Rigidbody2D rb;
    private Vector3 velocity = Vector3.zero;

    PhotonView view;

    private void Start()
    {
        view = GetComponent<PhotonView>();
    }
    void FixedUpdate()
    {
        if (view.IsMine) {
            float horyzontaleMouvement = Input.GetAxis("Horizontal") * movespeed * Time.deltaTime;
            float verticaleMouvement = Input.GetAxis("Vertical") * movespeed * Time.deltaTime;

            Vector3 targetVelocity = new Vector2(horyzontaleMouvement, verticaleMouvement);
            rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, .05f);
        }
    }
}
