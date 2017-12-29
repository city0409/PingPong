using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveDamageToPlayer : MonoBehaviour 
{
    [SerializeField]
    private int damageToGive = 10;

    public virtual void OnTriggerEnter2D (Collider2D collider) 
	{
        if (collider .CompareTag ("Player")&&collider .GetComponent <ICanTakeDamage >()!=null)
        {
            collider.GetComponent<ICanTakeDamage>().TakeDamage(damageToGive, gameObject);
        }
	}
	
}
