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

    public DotLine DotLine { get { return dotLine; } }

    public abstract void Decorate();
    

}
