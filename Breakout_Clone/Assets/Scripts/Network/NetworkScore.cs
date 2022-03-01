using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class NetworkScore : NetworkBehaviour
{
    [SerializeField]
    private Text scoreText = null;

    [SyncVar]
    private int Networkscore;

    private void Start()
    {
        Initialize();
    }

    public void Initialize()
    {
        Networkscore = 0;
    }

    public void AddPoint(int point)
    {
        Networkscore = Networkscore + point;
    }

    void Update()
    {
        scoreText.text = "Score: " + Networkscore.ToString();
    }
}
