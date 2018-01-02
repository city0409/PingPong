using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockControl : MonoBehaviour 
{
    private int blockAmount;
    public int BlockAmount
    {
        get{return blockAmount;}
        set
        {
            blockAmount = value;
            if (blockAmount<=0)
            {
                YouWin();
            }
        }
    }
	
	private void Start () 
	{
        for (int i = 0; i < transform .childCount ; i++)
        {
            if (transform.GetChild (i).gameObject.activeSelf)
            {
                blockAmount++;
            }
        }
        print(blockAmount);
	}

    public void YouWin()
    {
        print("win");
        GameManager.Instance.GameWin();

    }
}
