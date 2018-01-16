using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    enum PlayerCount { One, Two }

    [SerializeField]
    private int life = 3;
    private MainBan mainBan;
    //private MainBan mBan;
    [SerializeField]
    private PlayerCount currentPlayerCount;
    [SerializeField]
    private LeverDirector director1;
    [SerializeField]
    private LeverDirector director2;
    private LeverDirector currentDirector;
    public LeverDirector CurrentDirector { get { return currentDirector; } }

    public MainBan MainBan { get { return mainBan; } set { mainBan = value; } }
    //mainBan = mBan;
    //public MainBan MBan { get { return mBan; }set { mBan = value;} }

    private int score;
    public int Score { get { return score; } set { score = value; } }
    public int Life
    {
        get { return life; }
        set
        {
            life = value;
            if (life <= 0)
            {
                GameOver();
            }
        }
    }

    private UIManager uiManager;
    private bool gameActived;
    private bool playerActived;
    public  bool PlayerActived { get { return playerActived; }  set { playerActived = value; } }

    public bool GameActived { get { return gameActived; } private set { gameActived = value; } }

    private void Start()
    {
        uiManager = UIManager.instance;
        //EventService.Instance.GetEvent<GameActiveEvent>().Subscribe(ActiveGame);
    }

    public void ActiveGame()
    {
        gameActived = true;
        PlayerActived = true;
        if (currentPlayerCount == PlayerCount.One)
            currentDirector = director1;
        else
            currentDirector = director2;
        currentDirector.Decorate();

        PlayerCtrlActiveEvent();
    }

    private void GameOver()
    {
        print("GameOver!");
        uiManager.GameOver();
    }

    public void GameWin()
    {
        print("YouWin!");
        uiManager.GameWin();
    }

    public void GameActiveEvent()
    {
        EventService.Instance.GetEvent<GameActiveEvent>().Publish();
        ActiveGame();
    }

    public void GameStartEvent()
    {
        EventService.Instance.GetEvent<GameStartEvent>().Publish();
    }
    public void PlayerRunEvent()
    {
        EventService.Instance.GetEvent<PlayerRunEvent>().Publish();
    }
    public void PlayerSpawnEvent()
    {
        EventService.Instance.GetEvent<PlayerSpawnEvent>().Publish();
    }
    public void PlayerCtrlActiveEvent()
    {
        EventService.Instance.GetEvent<PlayerCtrlActiveEvent>().Publish();
    }

    public void PlayerDeadEvent()
    {
        PlayerActived = false;
        EventService.Instance.GetEvent<PlayerDeadEvent>().Publish();
    }
    public void PlayerReGoEvent()
    {
        PlayerActived = true ;
        EventService.Instance.GetEvent<PlayerReGoEvent>().Publish();
    }
}
