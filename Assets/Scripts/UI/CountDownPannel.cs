using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class CountDownPannel : MonoBehaviour 
{
    [SerializeField]
    private GameObject[] numberText;
    private Tweener ter;

    private void Awake()
    {
        
    } 
    private void Start()
    {

            StartCoroutine(Stop());


    }
    private IEnumerator Stop()
    {
        for (int i = 0; i < numberText.Length; i++)
        {
            numberText[i].SetActive(true);
            //numberText[i].transform.GetComponent<Text>().material.color = new Color(1, 1, 1, 1);
            Sequence seq = DOTween.Sequence();
            seq.Append(numberText[i].transform.DOScale(new Vector3(2, 2, 2), 0.5f));
            seq.Append(numberText[i].transform.GetComponent<Text>().DOColor(new Color(1, 1, 1, 0), 0.5f));
            yield return new WaitForSeconds(1.5f);

        }
    }
}
