using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Mirror;


public class NetworkBall : NetworkBehaviour
{
    public float ballSpeed;
    public Rigidbody rb;
    public GameObject explosionPrefab;
    private Vector3 savedV;
    private GameObject _paddleP1;
    bool P1ballLaunched; 
    bool P2ballLaunched; 
    private GameObject _paddleP2;

    public static event Action<NetworkBall> onBallDeath;

    void Start()
    {
        rb.velocity = rb.transform.right * ballSpeed;
    }

    [ClientRpc]
    public void SetPaddle(uint paddleID)
    {
        if (paddleID == 53)
        {
            _paddleP1 = NetworkClient.spawned[paddleID].gameObject;
            P1ballLaunched = true;
        } else if (paddleID == 54)
        {
            _paddleP2 = NetworkClient.spawned[paddleID].gameObject;
            P2ballLaunched = true;
        }
    }

    [ClientRpc]
    public void resetBall()
    {
        if(P1ballLaunched == true)
        {
            _paddleP1.transform.GetChild(1).gameObject.SetActive(true);
            _paddleP1.GetComponent<NetworkPaddle>().isShotPresent = false;
            P1ballLaunched = false;
        }

        if (P2ballLaunched == true)
        {
            _paddleP2.transform.GetChild(1).gameObject.SetActive(true);
            _paddleP2.GetComponent<NetworkPaddle>().isShotPresent = false;
            P2ballLaunched = false;
        }

    }

    [Server]
    public void Death(float time)
    {
        onBallDeath?.Invoke(this);
        Destroy(gameObject, time);

    }

    void FixedUpdate()
    {
        if (isServer)
        {
            BallMovement();
        }
    }

    [ClientCallback]
    private void BallMovement()
    {
        rb.velocity = rb.velocity.normalized * ballSpeed;
        savedV = rb.velocity; // store value before impact
    }


    //Ball refect when collison is detected
    [ServerCallback]
    private void OnCollisionEnter(Collision c)
    {

        rb.velocity = Vector3.Reflect(savedV, c.GetContact(0).normal);
        AudioManager.instance.Play("Hit");

        if (c.collider.CompareTag("Wall"))
        {
            Explode();
        }
    }


    [ServerCallback]
    private void OnTriggerEnter(Collider c)
    {
        if (c.tag == "DeathZone")
        {
            Death(0.5f);
            resetBall();
        }
    }

    [ClientRpc]
    void Explode()
    {
        AudioManager.instance.Play("Explosion");
        GameObject explode = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        Destroy(explode, 3f);
    }

}
