using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class StartGame : MonoBehaviour
{
    private Text text;
    private Sequence mySequence;
    [SerializeField]
    private CountDownPannel countDownPannel;

    private void Awake()
    {
        text = GetComponent<Text>();
    } 

    private void Start()
    {
        //transform.GetComponent<Text>().material.color = new Color(1, 1, 1, 0);
        mySequence = DOTween.Sequence();
        mySequence.SetDelay(1);
        mySequence.Append(text.DOFade(0, 0.6f));
        mySequence.Append(text.DOFade(1, 0.6f));
        mySequence.SetLoops(-1);
        EventService.Instance.GetEvent<GameActiveEvent>().Subscribe(StopBlink);
    }

    public  void StopBlink()
    {
        mySequence.Kill();
        text.DOFade(0, 0.5f);
        countDownPannel.StartCount();
    }
}
