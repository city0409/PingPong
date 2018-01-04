using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton <GameManager> 
{
    [SerializeField]
    private int life = 3;
    [SerializeField]
    private Transform mainBan;
    [SerializeField]
    private LeverDirector currentDirector;
    public LeverDirector CurrentDirector { get { return currentDirector; } }

    public Transform MainBan { get { return mainBan; } }

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
