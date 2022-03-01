using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Only used in SinglePlayer
public class DeathZone : MonoBehaviour
{
    public bool isOnline = false;

    private void OnTriggerEnter(Collider c)
    {
        if (isOnline == false)
        {
            if (c.tag == "Ball")
            {
                Ball ball = c.GetComponent<Ball>();
                BallManager.Instance.Balls--;
                ball.Death(0.5f);

            }
        }
    }
}
