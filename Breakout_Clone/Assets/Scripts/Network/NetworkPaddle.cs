
using UnityEngine;
using Mirror;

public class NetworkPaddle : NetworkBehaviour
{
    private Rigidbody _rb;
    private int savedAngle;
    Camera _mainCamera;
    public GameObject ballPrefab;
    public Transform ballSpawnLoc;
    private GameObject savedObj;
    public bool isShotPresent;



    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _mainCamera = FindObjectOfType<Camera>();
    }

    void Update()
    {
        if (isLocalPlayer)
        {
            PaddleMovement();

            if (Input.GetKeyDown(KeyCode.Space) && isShotPresent == false)
            {
                uint playerId = this.netId;
                savedAngle = randomAngleGenerator();
                HideBall(playerId);
                CmdLaunch(savedAngle, playerId);
                isShotPresent = true;
            }
        }

    }

    private void PaddleMovement()
    {
        float wallEstPixel = ((Screen.width / 2) - (0.38f * Screen.width / 2));
        float leftClamp = Screen.width / 2 - wallEstPixel;
        float RightClamp = Screen.width / 2 + wallEstPixel;
        float mousePoslimit = Mathf.Clamp(Input.mousePosition.x, leftClamp, RightClamp);
        float mouseWorldXPos = _mainCamera.ScreenToWorldPoint(new Vector3(mousePoslimit, 0, 40)).x;
        _rb.MovePosition(new Vector3(mouseWorldXPos, this.transform.position.y, 0));
    }


    void HideBall(uint id)
    {
        if (isServer)
        {
            savedObj = NetworkClient.spawned[id].gameObject;
            savedObj.transform.GetChild(1).gameObject.SetActive(false);
            sendToClient(id);
        }
        else if (isClient)
        {
            Debug.Log("I am client");
            savedObj = NetworkClient.spawned[id].gameObject;
            savedObj.transform.GetChild(1).gameObject.SetActive(false);
            sendToServer(id);
        }
    }

    [Command]
    void sendToServer(uint id)
    {
        savedObj = NetworkClient.spawned[id].gameObject;
        savedObj.transform.GetChild(1).gameObject.SetActive(false);
    }

    [ClientRpc]
    void sendToClient(uint id)
    {
        savedObj = NetworkClient.spawned[id].gameObject;
        savedObj.transform.GetChild(1).gameObject.SetActive(false);
    }

    [Command]
    void CmdLaunch(int angle, uint id)
    {
        Quaternion newAngle = Quaternion.Euler(0, 0, angle);
        GameObject projectile = Instantiate(ballPrefab, ballSpawnLoc.position, newAngle);
        NetworkServer.Spawn(projectile);
        projectile.GetComponent<NetworkBall>().SetPaddle(id);
    }

 

    private int randomAngleGenerator()
    {
        int angle = UnityEngine.Random.Range(45, 140);

        //Debug.Log("The current angle is" + angle);
        return angle;
    }

}

