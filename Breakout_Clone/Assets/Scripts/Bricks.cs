using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bricks : MonoBehaviour
{

    public int life = 1;
    public int points = 100;

    private Score score;
    private NetworkScore networkScore;
    void Start()
    {
        score = FindObjectOfType<Score>();
        networkScore = FindObjectOfType<NetworkScore>();
    }


    private void TakeDamage()
    {
        life--;
        if (networkScore != null)
        {
            networkScore.AddPoint(points);

        }else if (score != null)
        {
            score.AddPoint(points);
        }

        if (life <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision c)
    {
        if (c.gameObject.tag == "Ball")
        { 
            TakeDamage();
        }
        
    }
}
