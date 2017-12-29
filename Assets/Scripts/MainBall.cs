using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MainBall : MonoBehaviour 
{
    [SerializeField]
    private Transform mainBan;
    [SerializeField]
    private float speed=0.01f;
    private Vector3 dis;

    private bool IsChange=false  ;
    private Vector3 direction;
    private BoxCollider2D boxCollider2D;
    private Vector3 pointLeft, pointRight, pointUp, pointDown;
    [SerializeField]
    private LayerMask layerMask;
    private Rigidbody2D rig2D;
    private Vector3[] points = new Vector3[4];
    private Vector2[] rayDirection = new Vector2[4];

    private int damageToGive = 10;

    private void Awake () 
	{
        boxCollider2D = GetComponent<BoxCollider2D>();
        rig2D = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        dis =  transform.position- mainBan.position;
        direction = new Vector3(Random.Range(-1f, 1f), Random.value, 0).normalized;

        pointLeft = new Vector3(-boxCollider2D.bounds.extents.x, 0);
        pointRight = new Vector3(boxCollider2D.bounds.extents.x, 0);
        pointUp = new Vector3(0,boxCollider2D.bounds.extents.y);
        pointDown = new Vector3(0,-boxCollider2D.bounds.extents.y);

        points[0] = pointLeft;
        points[1] = pointRight;
        points[2] = pointUp;
        points[3] = pointDown;
        rayDirection[0] = Vector3.left;
        rayDirection[1] = Vector3.right ;
        rayDirection[2] = Vector3.up ;
        rayDirection[3] = Vector3.down ;


    }

    private void Update () 
	{
        

        if (!IsChange&& Input.GetKeyDown(KeyCode .Space ))
            IsChange = true;
        if (!IsChange)
        {
            transform.position = mainBan.position + dis;
            return;
        }
        //transform.Translate(Time.deltaTime * direction* speed);

        

        DetectRaycasts();

    }

    private void FixedUpdate()
    {
        if (!IsChange)
            return;
        rig2D.velocity = direction * speed;
    } 

    private void DetectRaycasts()
    {
        //RaycastHit2D resultLeft = Physics2D.Raycast(transform.position + pointLeft, Vector2.left, 0.02f, layerMask);
        //RaycastHit2D resultRight = Physics2D.Raycast(transform.position + pointRight, Vector2.right , 0.02f, layerMask);
        //RaycastHit2D resultUp = Physics2D.Raycast(transform.position + pointUp, Vector2.up , 0.02f, layerMask);
        //RaycastHit2D resultDown = Physics2D.Raycast(transform.position + pointDown, Vector2.down , 0.02f, layerMask);
        for (int i = 0; i < points.Length; i++)
        {
            RaycastHit2D result = Physics2D.Raycast(transform.position + points[i], rayDirection[i], 0.03f, layerMask);
            if (result .collider !=null)
            {
                Vector3 fixDirection = Vector3.zero;
                if (result .collider .GetComponent <MainBan >())
                {
                    fixDirection = result.collider.GetComponent<MainBan>().RealSpeed.normalized * 0.5f;
                }
                direction = (Vector3.Reflect(direction, result.normal) + fixDirection).normalized;
                break;

            }
        }

        //if (resultLeft.collider!=null )
        //    direction = Vector3.Reflect(direction, resultLeft.normal);
        //if (resultRight.collider != null)
        //    direction = Vector3.Reflect(direction, resultRight.normal);
        //if (resultUp.collider != null)
        //    direction = Vector3.Reflect(direction, resultUp.normal);
        //if (resultDown.collider != null)
        //    direction = Vector3.Reflect(direction, resultDown.normal);

        Debug.DrawLine(transform.position + pointLeft, transform.position + pointLeft + Vector3.left* 0.01f);
        Debug.DrawLine(transform.position + pointRight, transform.position + pointRight + Vector3.right * 0.01f);
        Debug.DrawLine(transform.position + pointUp, transform.position + pointUp + Vector3.up * 0.01f);
        Debug.DrawLine(transform.position + pointDown, transform.position + pointDown + Vector3.down * 0.01f);
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }

    public   void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Blocker") && collider.GetComponent<ICanTakeDamage>() != null)
        {
            collider.GetComponent<ICanTakeDamage>().TakeDamage(damageToGive, gameObject);
        }
    }
}
