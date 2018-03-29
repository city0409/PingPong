using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : Singleton<InputManager>
{
    [SerializeField]
    private Rect rectUp, rectDown;

    private Touch[] touches;
    //[SerializeField]
    //private MainBan mainBan;
    //[SerializeField]
    private MainBan banUp;
    //[SerializeField]
    private MainBan banDown;

    public MainBan BanUp { get { return banUp; }set { banUp = value; } }
    public MainBan BanDown { get { return banDown; } set { banDown = value; } }

    private int upBanTouchCount;
    private int downBanTouchCount;

    private float lastX;

	protected override  void Awake () 
	{
        base.Awake();
        EventService.Instance.GetEvent<PlayerCtrlActiveEvent>().Subscribe(PlayerCtrlActive);
	}

    private void PlayerCtrlActive()
    {
        banUp = GameManager.instance.CurrentDirector.UpBan;
        banDown = GameManager.instance.CurrentDirector.DownBan ;
    }

	private void Update () 
	{
        if (AppConst.platform == AppConst.Platform.Android)//多点触控，电脑只有一个鼠标，否则可以通用
        {
            foreach (Touch touch in Input.touches)
            {
                if (!GameManager.Instance.GameActived && touch.phase != TouchPhase.Ended && touch.phase != TouchPhase.Canceled)
                {
                    GameManager.Instance.GameActiveEvent();
                }
                if (GameManager.Instance.GameActived && !GameManager.Instance.PlayerActived && touch.phase != TouchPhase.Ended && touch.phase != TouchPhase.Canceled)
                {
                    GameManager.Instance.PlayerReGoEvent();
                }
                if (banUp && rectUp.Contains(Camera.main.ScreenToWorldPoint(touch.position)))
                {
                    if (touch.phase != TouchPhase.Ended && touch.phase != TouchPhase.Canceled)
                    {
                        banUp.Follow(Camera.main.ScreenToWorldPoint(touch.position));
                    }
                }
                else if (banDown && rectDown.Contains(Camera.main.ScreenToWorldPoint(touch.position)))
                {
                    if (touch.phase != TouchPhase.Ended && touch.phase != TouchPhase.Canceled)
                    {
                        banDown.Follow(Camera.main.ScreenToWorldPoint(touch.position));
                    }
                }
            }
        }
        else if (AppConst.platform == AppConst.Platform.Editor)
        {
            if (!GameManager.Instance.GameActived && Input .GetMouseButton(0))
            {
                GameManager.Instance.GameActiveEvent();
            }
            if (GameManager.Instance.GameActived && !GameManager.Instance.PlayerActived && Input.GetMouseButton(0))
            {
                GameManager.Instance.PlayerReGoEvent();
            }
            if (banUp && rectUp.Contains(Camera.main.ScreenToWorldPoint(Input.mousePosition )))
            {
                if (Input.GetMouseButton(0))
                {
                    banUp.Follow(Camera.main.ScreenToWorldPoint(Input.mousePosition));
                }
            }
            else if (banDown && rectDown.Contains(Camera.main.ScreenToWorldPoint(Input.mousePosition)))
            {
                if (Input.GetMouseButton(0))
                {
                    banDown.Follow(Camera.main.ScreenToWorldPoint(Input.mousePosition));
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
