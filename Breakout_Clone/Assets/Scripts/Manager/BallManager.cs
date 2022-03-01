using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Only used in singleplayer
public class BallManager : MonoBehaviour
{
    private static BallManager _instance;
    public static BallManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("BallManager is null");
            }
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    [SerializeField]
    private Ball BallPrefab;
    private Ball initBall;
    private Rigidbody initBallRb;

    public float ballspeed = 15f;

   
    public int Balls { get; set; }
  

    void Start()
    {
        Balls = 0;
    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space) && shotStatus() == false)
        {
            initBallRb.isKinematic = false;
            initBall.transform.rotation = Quaternion.Euler(0, 0, randomAngleGenerator());
            initBallRb.velocity = initBall.transform.right * ballspeed;
            GameManager.Instance.isBallShot = true;

        }
    }

    public void updateBalltoPaddle()
    {
        initBall.transform.position = setBallPosToPaddle();
    }

    public void InitBall()
    {
        initBall = Instantiate(BallPrefab, setBallPosToPaddle(), Quaternion.identity);
        Balls++;
        initBallRb = initBall.GetComponent<Rigidbody>();
    }

    private Vector3 setBallPosToPaddle()
    {
        Vector3 paddlePos = Paddle.Instance.gameObject.transform.position;
        Vector3 StartingPos = new Vector3(paddlePos.x, paddlePos.y + 2f, 0);

        return StartingPos;
    }

    public void destoryBall()
    {
        Balls--;
        initBall.Death(0);
    }


    public Ball getBall()
    {
        return initBall;
    }

    private bool shotStatus() => GameManager.Instance.isBallShot;


    //generating the inital angle of ball to shoot
    private int randomAngleGenerator()
    {
        int angle = Random.Range(45, 140);

        //Debug.Log("The current angle is" + angle);
        return angle;
    }
}
