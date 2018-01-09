using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDirector2 : LeverDirector
{
    [SerializeField]
    private Vector3 UpBanPos, DownBanPos;
    public override void Decorate()
    {
        upBan = Instantiate(BanPrefab, UpBanPos, Quaternion.identity);
        downBan = Instantiate(BanPrefab, DownBanPos, Quaternion.identity);
        Vector3 ballPos;
        if (Random.value > 0.5f)
        {
            ballPos = DownBanPos + new Vector3(0, 0.1f, 0);
            initBan = downBan;
        }
        else
        {
            ballPos = UpBanPos - new Vector3(0, 0.1f, 0);
            initBan = UpBan;

        }
        Instantiate(ballPrefab, ballPos, Quaternion.identity);
        //InputManager inputManager = InputManager.Instance;
        //GameManager.Instance.MainBan = Instantiate(BanPrefab, UpBanPos, Quaternion.identity);
        //inputManager.BanUp = GameManager.Instance.MainBan;
        //inputManager.BanDown = Instantiate(BanPrefab, DownBanPos, Quaternion.identity);

    }
}
