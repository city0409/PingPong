using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LeverDirector : MonoBehaviour 
{
    [SerializeField]
    protected  MainBan  BanPrefab;
    [SerializeField]
    private RewardSpawner rewardSpawner;
    [SerializeField]
    private DotLine  dotLine;
    [SerializeField]
    protected MainBall ballPrefab;

    protected MainBan downBan;
    protected MainBan upBan;
    public MainBan DownBan { get { return downBan; } }
    public MainBan UpBan { get { return upBan; } }

    protected MainBan initBan;
    public MainBan InitBan { get { return initBan; } }


    public DotLine DotLine { get { return dotLine; } }
    public Vector3 PongRelativePos { get; protected set; }

    public abstract void Decorate();
    

}
