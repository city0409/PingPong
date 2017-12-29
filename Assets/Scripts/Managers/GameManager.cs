﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton <GameManager> 
{
    [SerializeField]
    private int life = 3;
    [SerializeField]
    private Transform mainBan;
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

	private void GameOver () 
	{
        print("GameOver!");
	}
}
