using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class StartGame : MonoBehaviour
{
    private Tweener ter;

    private void Start()
    {
        //transform.GetComponent<Text>().material.color = new Color(1, 1, 1, 0);
        Sequence seq = DOTween.Sequence();
        seq.Append(transform.GetComponent<Text>().DOColor(new Color(1, 1, 1, 1), 0.6f));
        seq.Append(transform.GetComponent<Text>().DOColor(new Color(1, 1, 1, 0), 0.6f));
        seq.SetLoops(-1);
        //seq.Play();
    }

    private void Update()
    {

    }
}
