using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spines : MonoBehaviour
{
    [SerializeField] private int Damage;
    [SerializeField] private bool EnemiesTakeDamage;
    [SerializeField] private float DelayDamage;
    private bool damageTaken;
    private GameObject player;
    void Start()
    {
       
    }
    private void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if(!damageTaken)
            {
                damageTaken = true;
                player = collision.gameObject;
                StartCoroutine("damage");

            }

        }


    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            if (EnemiesTakeDamage && other.gameObject.GetComponent<Enemies>().failsafe == false)
            {
                other.gameObject.GetComponent<Enemies>().ChangeState(Enemies.States.DEAD);

            }

        }
    }

    IEnumerator damage()
    {
        player.GetComponent<PlayerManager>().GetDamage(Damage);
        yield  return new WaitForSeconds(DelayDamage);
        damageTaken = false;
    }
}
