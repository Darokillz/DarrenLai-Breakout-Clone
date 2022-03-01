using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Only Used in Singleplayer
public class Paddle : MonoBehaviour
{

    private static Paddle _instance;
    public static Paddle Instance => _instance;

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

    Rigidbody _rigidbody;
    Camera _mainCamera;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _mainCamera = FindObjectOfType<Camera>();

    }

    void FixedUpdate()
    {
            PaddleMovement();
    }

    private void PaddleMovement()
    {
        float wallEstPixel = ((Screen.width / 2) - (0.38f * Screen.width / 2));
        float leftClamp = Screen.width / 2 - wallEstPixel;
        float RightClamp = Screen.width / 2 + wallEstPixel;
        float mousePoslimit = Mathf.Clamp(Input.mousePosition.x, leftClamp, RightClamp);
        float mouseWorldXPos = _mainCamera.ScreenToWorldPoint(new Vector3(mousePoslimit, 0, 40)).x;
        _rigidbody.MovePosition( new Vector3(mouseWorldXPos, -15, 0));
    }
}
