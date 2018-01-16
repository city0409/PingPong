﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDirector1 : LeverDirector  
{
    [SerializeField]
    private Vector3 DownBanPos;

	public  override void Decorate () 
	{
        downBan = Instantiate(BanPrefab, DownBanPos, Quaternion.identity);
        initBan = downBan;
        Instantiate(ballPrefab, DownBanPos + new Vector3(0, 0.3f, 0), Quaternion.identity);
        //InputManager inputManager = InputManager.Instance ;
        //GameManager.Instance.MainBan = Instantiate(BanPrefab, DownBanPos, Quaternion.identity);
        //inputManager.BanDown = GameManager.Instance.MainBan;

    }
}
