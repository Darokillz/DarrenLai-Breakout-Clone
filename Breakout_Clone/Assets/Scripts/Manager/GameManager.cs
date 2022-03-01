using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if(_instance == null)
            {
                Debug.LogError("GameManger is null");
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

    public bool isBallShot { get; set; }
    public bool rdySpawn { get; set; }

    public GameObject menuPannel;
    public GameObject inPlayPannel;
    public GameObject victoryPannel;
    public GameObject pausePannel;
    public GameObject MuteText;
    public GameObject UnmuteText;


    public GameObject DeathZone;
    public GameObject BrickManager;
    public GameObject NetworkManager;

    private GameObject _paddle;
    private bool gamePause;
    private bool Online = false;


    [SerializeField]
    private Score score = null;
    [SerializeField]
    private NetworkScore NetworkScore = null;
    [SerializeField]
    private GameObject playerPrefab = null;
    [SerializeField]
    private GameObject brickSpawnerObj = null;

    private bool toggle = true;

    public enum State { MENU, INIT, PLAY, VICTORY}
    State _state;
   

    private void Start()
    {
        switchState(State.MENU);
    }

    public void switchState(State newState)
    {
        endState();
        _state = newState;
        startState(newState);

    }

    void startState(State newState)
    {
        switch (newState)
        {
            case State.MENU:
                inPlayPannel.SetActive(false);
                menuPannel.SetActive(true);
                if (Online == false)
                {
                    rdySpawn = false;
                }
                break;
            case State.INIT:
                inPlayPannel.SetActive(true);
                if (Online == false)
                {
                    score.Initialize();
                    brickSpawnerObj.GetComponent<BrickSpawner>().restLoopCount();
                    brickSpawnerObj.GetComponent<BrickSpawner>().loopDone = false;
                    isBallShot = false;
                    rdySpawn = true;
                    playerSetup();
                    Cursor.visible = false;

                }
                else if (Online == true)
                {
                    inPlayPannel.transform.GetChild(0).gameObject.SetActive(false);
                    NetworkScore.Initialize();
                    Ball.onBallDeath += onBallDeath;
                }
                switchState(State.PLAY);
                break;
            case State.PLAY:
                gamePause = false;
                break;
            case State.VICTORY:
                Cursor.visible = true;
                inPlayPannel.SetActive(false);
                victoryPannel.SetActive(true);
                Destroy(_paddle);
                break;
        }

    }

     void Update()
    {
        switch (_state)
        {
            case State.MENU:
                break;
            case State.INIT:
                break;
            case State.PLAY:

                if (Online == false)
                {
                    if (isBallShot == false)
                    {
                        BallManager.Instance.updateBalltoPaddle();
                    }


                    if (brickSpawnerObj.GetComponent<BrickSpawner>().loopDone == true)
                    {
                        switchState(State.VICTORY);
                    }
                }

                if (Input.GetKeyDown(KeyCode.Escape)){
                    gamePause = !gamePause;

                    if(gamePause == true)
                    {
                        pauseGame();
                    }
                    else if (gamePause == false)
                    {
                        resumeGame();
                    }
                }
                break;
            case State.VICTORY:
                victoryPannel.SetActive(true);
                break;
        }
    }

    void endState()
    {
        switch (_state)
        {
            case State.MENU:
                menuPannel.SetActive(false);
                break;
            case State.INIT:
                break;
            case State.PLAY:
                break;
            case State.VICTORY:
                inPlayPannel.SetActive(false);
                victoryPannel.SetActive(false);
                break;
        }
    }

    private void playerSetup()
    {
        Ball.onBallDeath += onBallDeath;
        _paddle = Instantiate(playerPrefab, playerPrefab.transform.position,playerPrefab.transform.rotation);
        BallManager.Instance.InitBall();
    }


    private void onBallDeath(Ball obj)
    {
        if(BallManager.Instance.Balls <= 0 && _state == State.PLAY)
        {
            BallManager.Instance.InitBall();
            isBallShot = false;
        }

    }

    public void pauseGame()
    {
        Time.timeScale = 0;
        Cursor.visible = true;
        pausePannel.SetActive(true);
    }

    public void resumeGame()
    {
        Time.timeScale = 1;
        Cursor.visible = false;
        pausePannel.SetActive(false);
    }

    private void OnDisable()
    {
        Ball.onBallDeath -= onBallDeath;
    }

    public void singlePlayerBtn()
    {
        Online = false;
        switchState(State.INIT);
    }

    public void MultiPlayerBtn()
    {
        Online = true;
        BallManager.Instance.GetComponent<BallManager>().enabled = false;
        DeathZone.GetComponent<DeathZone>().isOnline = true;
        BrickManager.SetActive(false);
        switchState(State.INIT);
    }



    public void MenuBtn()
    {
        
        switchState(State.MENU);
    }

    public void settingMenuBtn()
    {
        Time.timeScale = 1;
        switchState(State.MENU);
        Cursor.visible = true;
        pausePannel.SetActive(false);
        if (Online == false)
        {
            brickSpawnerObj.GetComponent<BrickSpawner>().stageClean();
            BallManager.Instance.destoryBall();
            Destroy(_paddle);
        }
        if(Online == true)
        {
            Destroy(GameObject.Find("NetworkManager"));
            SceneManager.LoadScene(0);
        }
    }

    public void Mutetogglebtn()
    {

        toggle = !toggle;

        if (toggle == false)
        {
            AudioManager.instance.Mute();
            MuteText.SetActive(false);
            UnmuteText.SetActive(true);
        }

        if (toggle == true)
        {
            AudioManager.instance.Unmute();
            UnmuteText.SetActive(false);
            MuteText.SetActive(true);

        }


    }



}
