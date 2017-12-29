﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DataPannel : MonoBehaviour 
{
    [SerializeField]
    private Text score, life;
    private GameManager gm;
    private void Start()
    {
        gm = GameManager.Instance;
    }
    private void Update()
    {
        life.text = gm.Life.ToString();
    }

}
