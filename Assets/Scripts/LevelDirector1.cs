using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDirector1 : LeverDirector  
{
    [SerializeField]
    private Vector3 DownBanPos;

	public  override void Decorate () 
	{
        InputManager inputManager = InputManager.Instance ;
        GameManager.Instance.MainBan = Instantiate(BanPrefab, DownBanPos, Quaternion.identity);
        inputManager.BanDown = GameManager.Instance.MainBan;

    }
}
