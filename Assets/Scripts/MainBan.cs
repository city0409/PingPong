using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainBan : MonoBehaviour
{
    [SerializeField]
    private Transform mainBall;
    //[SerializeField]
    //private Transform pointLeft;
    //[SerializeField]
    //private Transform pointRight;
    [SerializeField]
    private float Speed = 5f;
    

    private float directionX;
    private Rigidbody2D rig2D;
    private Vector3 pointLeft, pointRight;
    private BoxCollider2D boxCollider2D;
    private Vector3 lastPos;
    public  Vector3 RealSpeed;

    private void Awake()
    {
        mainBall = GetComponent<Transform>();
        rig2D = GetComponent<Rigidbody2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();


    }

    private void Start()
    {
        pointLeft = new Vector3(-boxCollider2D.bounds.extents.x-0.01f, 0);
        pointRight = new Vector3(boxCollider2D.bounds.extents.x+0.01f, 0);
        lastPos = transform.position;

    }

    private void Update()
    {
        
        RaycastHit2D resultLeft = Physics2D.Raycast(transform.position + pointLeft, Vector2.left, 0.02f);
        RaycastHit2D resultRight = Physics2D.Raycast(transform.position + pointRight, Vector2.right, 0.02f);
        directionX = Input.GetAxisRaw("Horizontal");
        if (resultLeft.collider == null && directionX < 0)
        {
            transform.position += new Vector3(directionX * Time.deltaTime * Speed, 0, 0);
        }
        else if (resultRight.collider == null && directionX > 0)
        {
            transform.position += new Vector3(directionX * Time.deltaTime * Speed, 0, 0);
        }

        RealSpeed = ((transform.position - lastPos) / Time.deltaTime).normalized;
        lastPos = transform.position;

        Debug.DrawLine(transform.position + pointLeft, transform.position + pointLeft + Vector3.left * 0.02f);
        Debug.DrawLine(transform.position + pointRight, transform.position + pointRight + Vector3.right * 0.02f);
    }
   
}
