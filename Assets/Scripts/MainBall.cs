﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class MainBall : MonoBehaviour
{
    public enum State { Idle,Running}
    [Serializable]
    public struct PingPongInitData
    {
        public State state;
        public Vector3 initPosition;
        public float speed;
        //public bool isStarted;
    }

    [SerializeField]
    private TrailRenderer fx;
    private Transform mainBan;
    [SerializeField]
    private LayerMask layerMask;
    [SerializeField]
    private PingPongInitData pingPongInitData;

    private Vector3 dis;

    private Vector3 direction;
    private BoxCollider2D boxCollider2D;
    //private Vector3 pointLeft, pointRight, pointUp, pointDown;

    private Rigidbody2D rig2D;
    //private Vector3[] points = new Vector3[4];
    //private Vector2[] rayDirection = new Vector2[4];

    //private int damageToGive = 10;

    private PingPongInitData currentPingPongData;
    private MainBan currentBan;
    private StateMachine<State> stateMachine;


    private void Awake()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
        rig2D = GetComponent<Rigidbody2D>();
        fx = GetComponent<TrailRenderer>();
        EventService.Instance.GetEvent<PlayerRunEvent>().Subscribe(Run);
    }

    private void Start()
    {
        currentPingPongData = pingPongInitData;
        fx.enabled = false;
        mainBan = GameManager.Instance.CurrentDirector .InitBan .transform ;
        currentBan = mainBan.GetComponent<MainBan>();
        //transform.position = mainBan.position + new Vector3(0, 0.3f, 0);

        dis = GameManager.Instance.CurrentDirector.PongRelativePos;
        //direction = new Vector3(Random.Range(-1f, 1f), Random.value, 0).normalized;
        stateMachine = new StateMachine<State>();
        stateMachine.AddState(State.Idle, () => { fx.enabled = false; });
        stateMachine.AddState(State.Running ,() => { fx.enabled = true ; });
        stateMachine.CurrentState = State.Idle;

        //pointLeft = new Vector3(-boxCollider2D.bounds.extents.x, 0);
        //pointRight = new Vector3(boxCollider2D.bounds.extents.x, 0);
        //pointUp = new Vector3(0,boxCollider2D.bounds.extents.y);
        //pointDown = new Vector3(0,-boxCollider2D.bounds.extents.y);

        //points[0] = pointLeft;
        //points[1] = pointRight;
        //points[2] = pointUp;
        //points[3] = pointDown;
        //rayDirection[0] = Vector3.left;
        //rayDirection[1] = Vector3.right ;
        //rayDirection[2] = Vector3.up ;
        //rayDirection[3] = Vector3.down ;
        //fx.Play();

    }

    private void Update()
    {
        if (stateMachine.CurrentState == State.Idle)
        {
            transform.position = mainBan.position + dis;
            return;
        }
        //transform.Translate(Time.deltaTime * direction* speed);
        DetectRaycast2();
    }

    private void FixedUpdate()
    {
        if (stateMachine.CurrentState == State.Idle)
            return;
        rig2D.velocity = direction * currentPingPongData.speed;
    }

    public void Run()
    {
        stateMachine.CurrentState = State.Running;
        direction = (currentBan.RealSpeed/10 + Vector3.up).normalized;
        print(currentBan.RealSpeed.normalized);
    }
    //private void DetectRaycasts()
    //{
    //    //RaycastHit2D resultLeft = Physics2D.Raycast(transform.position + pointLeft, Vector2.left, 0.02f, layerMask);
    //    //RaycastHit2D resultRight = Physics2D.Raycast(transform.position + pointRight, Vector2.right , 0.02f, layerMask);
    //    //RaycastHit2D resultUp = Physics2D.Raycast(transform.position + pointUp, Vector2.up , 0.02f, layerMask);
    //    //RaycastHit2D resultDown = Physics2D.Raycast(transform.position + pointDown, Vector2.down , 0.02f, layerMask);
    //    for (int i = 0; i < points.Length; i++)
    //    {
    //        RaycastHit2D result = Physics2D.Raycast(transform.position + points[i], rayDirection[i], 0.03f, layerMask);
    //        if (result .collider !=null)
    //        {
    //            Vector3 fixDirection = Vector3.zero;
    //            if (result .collider .GetComponent <MainBan >())
    //            {
    //                fixDirection = result.collider.GetComponent<MainBan>().RealSpeed.normalized * 0.5f;
    //            }
    //            direction = (Vector3.Reflect(direction, result.normal) + fixDirection).normalized;
    //            break;

    //        }
    //    }

    //    //if (resultLeft.collider!=null )
    //    //    direction = Vector3.Reflect(direction, resultLeft.normal);
    //    //if (resultRight.collider != null)
    //    //    direction = Vector3.Reflect(direction, resultRight.normal);
    //    //if (resultUp.collider != null)
    //    //    direction = Vector3.Reflect(direction, resultUp.normal);
    //    //if (resultDown.collider != null)
    //    //    direction = Vector3.Reflect(direction, resultDown.normal);

    //    Debug.DrawLine(transform.position + pointLeft, transform.position + pointLeft + Vector3.left* 0.01f);
    //    Debug.DrawLine(transform.position + pointRight, transform.position + pointRight + Vector3.right * 0.01f);
    //    Debug.DrawLine(transform.position + pointUp, transform.position + pointUp + Vector3.up * 0.01f);
    //    Debug.DrawLine(transform.position + pointDown, transform.position + pointDown + Vector3.down * 0.01f);
    //}

    public void DestroySelf()
    {
        GameManager.Instance.Life -= 1;
        ResetPingPongData();
        GameManager.Instance.PlayerDeadEvent();
    }

    public void ResetPingPongData()
    {
        currentPingPongData = pingPongInitData;
        transform.rotation = Quaternion.identity;
        rig2D.velocity = Vector2.zero;
        rig2D.angularVelocity = 0f;
        stateMachine.CurrentState = pingPongInitData.state;

        //playerDeadEvent();
    }

    private void OnDrawGizmosSelected()//看方块射线的大小
    {
        Gizmos.color = Color.yellow;
        boxCollider2D = GetComponent<BoxCollider2D>();
        Gizmos.DrawCube(transform.position, boxCollider2D.bounds.extents * 2.5f);
    }

    private void DetectRaycast2()
    {
        RaycastHit2D results = Physics2D.BoxCast(transform.position, boxCollider2D.bounds.extents * 2.5f, 0f, Vector2.zero, 0f, layerMask);
        if (results.collider != null)
        {
            Vector3 fixDirection = Vector3.zero;
            if (results.collider.GetComponent<MainBan>())
            {
                fixDirection = results.collider.GetComponent<MainBan>().RealSpeed.normalized * 0.5f;
            }
            direction = (Vector3.Reflect(direction, results.normal) + fixDirection).normalized;
            ICanTakeDamage CanTakeDamage = results.collider.GetComponent<ICanTakeDamage>();
            if (CanTakeDamage != null)
            {
                CanTakeDamage.TakeDamage(1, gameObject);
            }
        }
    }

    //public void OnTriggerEnter2D(Collider2D collider)
    //{
    //    if (collider.CompareTag("Blocker") && collider.GetComponent<ICanTakeDamage>() != null)
    //    {
    //        collider.GetComponent<ICanTakeDamage>().TakeDamage(damageToGive, gameObject);
    //    }
    //}
}
