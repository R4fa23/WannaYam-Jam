using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Bug : Enemies
{
    void Start()
    {
       
        agent.updateRotation = false;


    }

   override public void Attack()
    {
        if (Vector3.Distance(transform.position, Player.transform.position) > AttackDist)
        {
            StopCoroutine("Attacking");
            attacked = false;
            if (Vector3.Distance(transform.position, Player.transform.position) < FollowDist)
                ChangeState(States.FOLLOWING);
            else
                ChangeState(States.IDDLE);
        }
        else
        {
            if (!attacked)
                StartCoroutine("Attacking");

        }



    }




    public IEnumerator Attacking()

    {

        attacked = true;



        yield return new WaitForSeconds(AttackDelay);
        Player.GetComponent<PlayerManager>().GetDamage(AttackStrenght);
        attacked = false;


    }


}
