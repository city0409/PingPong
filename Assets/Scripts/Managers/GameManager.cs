using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton <GameManager> 
{
    enum PlayerCount { One,Two}

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

    public MainBan  MainBan { get { return mainBan; }set { mainBan = value; } }
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
            if (life <=0)
            {
                GameOver();
            }
        }
    }

    private UIManager uiManager;

    private void Start()
    {
        uiManager = UIManager.instance;
        StartGame();
    }

    public void StartGame()
    {
        if (currentPlayerCount == PlayerCount.One)
            currentDirector = director1;
        else
            currentDirector = director2;
        currentDirector.Decorate();
        EventService.Instance.GetEvent<GameStartEvent>().Publish ();
    }

    private void GameOver () 
	{
        print("GameOver!");
        uiManager.GameOver();

    }

    public  void GameWin()
    {
        print("YouWin!");
        uiManager.GameWin();

    }
}
