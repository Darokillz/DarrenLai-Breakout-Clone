using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public static event Action<Ball> onBallDeath;

    Rigidbody _rigidbody;
    Vector3 _ballvelocity;
    public GameObject explosionPrefab;


    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        BallMovement();
    }

    private void BallMovement()
    {
        _rigidbody.velocity = _rigidbody.velocity.normalized * BallManager.Instance.ballspeed;
        _ballvelocity = _rigidbody.velocity; // store value before impact
    }

    public void Death(float time)
    {
        onBallDeath?.Invoke(this);
        Destroy(gameObject,time);

    }

    //Ball refect when collison is detected
    private void OnCollisionEnter(Collision c)
    {
        
        _rigidbody.velocity = Vector3.Reflect(_ballvelocity, c.GetContact(0).normal);
        AudioManager.instance.Play("Hit");

        if (c.collider.CompareTag("Wall"))
        {
            Explode();
        }
    }


    void Explode()
    {
        AudioManager.instance.Play("Explosion");
        GameObject explode = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        Destroy(explode, 3f);
    }
}
