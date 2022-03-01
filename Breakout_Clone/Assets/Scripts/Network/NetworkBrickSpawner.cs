using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class NetworkBrickSpawner : NetworkBehaviour
{
    public GameObject[] brickPrefab;
    public Transform startingPos;
    float xIncrement = 3.56f;
    float yIncrement = 1.77f;
    int maxRow = 5;
    int maxCol = 10;
    float resetX = -16.29f;
    private Vector3 brickPos;

    // Start is called before the first frame update
    void Start()
    {
        brickPos = startingPos.position;
        spawnBrick();
    }


    public void spawnBrick()
    {
        brickPos = startingPos.position;
        for (int row=0; row < maxRow; row++)
        {
            float currentY = brickPos.y;
            for(int col = 0; col < maxCol; col++)
            {
                GameObject Brick = Instantiate(brickPrefab[row], brickPos, Quaternion.identity);
                NetworkServer.Spawn(Brick);
                brickPos = new Vector3(brickPos.x + xIncrement, currentY, 0);
            }
            brickPos = new Vector3(resetX, brickPos.y - yIncrement, 0);
        }

    }
}
