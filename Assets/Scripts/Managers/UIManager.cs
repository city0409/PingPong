using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton <UIManager > 
{
    [SerializeField]
    private GameObject gameOverPannel;
    [SerializeField]
    private GameObject gameWinPannel;

    public  void GameOver () 
	{
        gameOverPannel.SetActive(true);

    }

    public  void GameWin () 
	{
        gameWinPannel.SetActive(true);
	}
}
