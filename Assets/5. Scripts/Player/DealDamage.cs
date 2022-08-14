using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealDamage : MonoBehaviour
{
    [SerializeField] PlayerStats PlayerStats;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy")
        {
            other.GetComponent<Enemies>().Dano(PlayerStats.damage);
            if (gameObject.tag == "Bullet") gameObject.SetActive(false);
        }

        if(other.tag == "Wall" && gameObject.tag == "Bullet")
        {
            gameObject.SetActive(false);
        }
    }
}
