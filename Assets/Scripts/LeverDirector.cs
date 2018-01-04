using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverDirector : MonoBehaviour 
{
    [SerializeField]
    private RewardSpawner rewardSpawner;
    [SerializeField]
    private DotLine  dotLine;
    public DotLine DotLine { get { return dotLine; } }


    
}
