using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : PersistentSingleton<InputManager>
{
    [SerializeField]
    private Rect rectUp, rectDown;

    private Touch[] touches;
    //[SerializeField]
    //private MainBan mainBan;
    //[SerializeField]
    private MainBan banUp;
    //[SerializeField ]
    private MainBan banDown;

    public MainBan BanUp { get { return banUp; }set { banUp = value; } }
    public MainBan BanDown { get { return banDown; } set { banDown = value; } }


    private float lastX;

	private void Start () 
	{
		
	}
	
	private void Update () 
	{
        foreach (Touch  touch in Input .touches )
        {
            if (banUp&& rectUp.Contains (Camera .main .ScreenToWorldPoint (touch .position )))
            {
                if (touch .phase !=TouchPhase.Ended &&touch .phase !=TouchPhase.Canceled )
                {
                    banUp.Follow(Camera.main.ScreenToWorldPoint(touch.position));
                }
            }
            else if (banDown&&rectDown.Contains(Camera.main.ScreenToWorldPoint(touch.position)))
            {
                if (touch.phase != TouchPhase.Ended && touch.phase != TouchPhase.Canceled)
                {
                    banDown.Follow(Camera.main.ScreenToWorldPoint(touch.position));
                }
            }
        }
	}

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(0, 0, 1, 0.5f);
        Gizmos.DrawCube(new Vector3(rectDown.x + rectDown.width / 2, rectDown.y + rectDown.height / 2, 0), new Vector3(rectDown.width, rectDown.height, 0));
        Gizmos.DrawCube(new Vector3(rectUp.x + rectUp.width / 2, rectUp.y + rectUp.height / 2, 0), new Vector3(rectUp.width, rectUp.height, 0));

    }


}
