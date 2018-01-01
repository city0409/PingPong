﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "CreateScriptable/PlayerData", order = 1)]
public class PlayerData : ScriptableObject
{
    //public int maxScore;
    public float speed=5f;
    public bool isStarted=false ;
}
