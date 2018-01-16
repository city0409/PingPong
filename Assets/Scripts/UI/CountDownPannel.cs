using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class CountDownPannel : MonoBehaviour
{
    private Text text;

    private void Awake()
    {
        text = GetComponent<Text>();
        text.enabled = false;
        EventService.Instance.GetEvent<PlayerReGoEvent >().Subscribe(ReGoCount);
        EventService.Instance.GetEvent<PlayerDeadEvent >().Subscribe(PlayerDead);
    }

    public void StartCount()
    {
        StartCoroutine(DoStartCount());
    }

    private void ReGoCount()
    {
        StartCoroutine(DoReGoCount());
    }

    private void PlayerDead()
    {
        text.enabled = true;
        transform.localScale = Vector3.one;
        text.DOFade(1, 0f);
        text.text = "Ready";
    }

    public void DeadCount()
    {
        StartCoroutine(DoDeadCount());
    }
    //public void StartCount()
    //{
    //    StartCoroutine(DoStartCount());
    //}
    //private void Start()
    //{
    //    StartCoroutine(StartCount());
    //}
    private IEnumerator DoReGoCount()
    {
        text.enabled = true ;
        yield return StartCoroutine(Fade("Ready"));
        GameManager.Instance.PlayerRunEvent();

        yield return StartCoroutine(Fade("GO"));

        text.enabled = false;
    }

    private IEnumerator DoDeadCount()
    {
        text.enabled = true;
        yield return StartCoroutine(Fade("Ready"));
        yield return StartCoroutine(Fade("GO"));

        text.enabled = false;
        GameManager.Instance.GameStartEvent();
    }

    private IEnumerator DoStartCount()
    {
        text.enabled = true;
        int index = 3;
        for (int i = index; i >= 0; i--)
        {
            yield return StartCoroutine(Fade(i.ToString ()));
        }
        yield return StartCoroutine(Fade("GO"));
        text.enabled = false;
        GameManager.Instance.PlayerRunEvent();

    }
    private IEnumerator Fade(string  index)
    {
        transform.localScale = Vector3.one;
        transform.DOScale(1.3f, 1f);
        text.text = index;
        text.DOFade(1, 0f);
        yield return new WaitForSeconds(1f);
        text.DOFade(0, 1f);
        yield return new WaitForSeconds(1f);
    }
}
