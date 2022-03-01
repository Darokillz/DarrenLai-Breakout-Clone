using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Only Used in Singleplayer
public class Score : MonoBehaviour
{

    [SerializeField]
    private Text scoreText = null;

    private int score;

    private void Start()
    {
        Initialize();
    }

    public void Initialize()
    {
        score = 0;
    }

    public void AddPoint(int point)
    {
        score = score + point;
    }

    void Update()
    {
        scoreText.text = "Score: " + score.ToString();
    }
}
