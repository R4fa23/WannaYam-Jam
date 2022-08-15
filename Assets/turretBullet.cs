using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turretBullet : MonoBehaviour
{
    public int Damage;
    private bool damaged;
    
    [SerializeField]private float TimeToDie;
    private float timer;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if(!damaged)
            {
                damaged = true;
                other.GetComponent<PlayerManager>().GetDamage(Damage);
                ObjectPool.instance.ReturnBulletToPool(gameObject);
                timer = 0;
                damaged = false;
            }


        }
        if(other.tag == "Wall")
        {
            ObjectPool.instance.ReturnBulletToPool(gameObject);
            timer = 0;

        }    
    }

    private void Update()
    {
        if(timer >= TimeToDie)
        {
            ObjectPool.instance.ReturnBulletToPool(gameObject);
            timer = 0;
        }else
            timer += Time.deltaTime;

        Debug.Log(timer)
           ;
    }

}
