using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDirector2 : LeverDirector
{
    [SerializeField]
    private Vector3 UpBanPos, DownBanPos;
    public override void Decorate()
    {
        InputManager inputManager = InputManager.Instance;
        GameManager.Instance.MainBan = Instantiate(BanPrefab, UpBanPos, Quaternion.identity);
        inputManager.BanUp = GameManager.Instance.MainBan;
        inputManager.BanDown = Instantiate(BanPrefab, DownBanPos, Quaternion.identity);

    }
}
