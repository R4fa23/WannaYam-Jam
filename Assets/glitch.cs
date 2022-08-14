using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class glitch : Enemies
{
    [SerializeField]private float HidingTime;
    [SerializeField]private float ShowingTime;
    [SerializeField] private float ShowTimer;
    [SerializeField] private float HideTimer;
    [SerializeField] private GameObject texture;
    private bool CantakeDamage;


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
                ChangeState(States.HIDING);
        }
        else
        {
            if (!attacked)
                StartCoroutine("Attacking");

        }



    }


    override public void Follow()
    {


        if (Vector3.Distance(transform.position, Player.transform.position) > FollowDist)
        {
            ChangeState(States.HIDING);
        }

        if (Vector3.Distance(transform.position, Player.transform.position) < AttackDist)
        {
            animator.SetTrigger("sobe");
            ChangeState(States.ATACKING);
        }
        if(transform.position.x > Player.transform.position.x)
        {
            float x = texture.transform.rotation.x;

            texture.transform.localRotation = Quaternion.Euler(0, 180, 0);
        }else
        {
            float x = texture.transform.rotation.x;
            texture.transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
            

        if (HideTimer <= 0)
        {
            if (!CantakeDamage)
            {
                animator.SetTrigger("sobe");
                ShowTimer = Random.Range(ShowingTime - (0.5f * ShowingTime), ShowingTime + (0.5f * ShowingTime));
                CantakeDamage = true;

            }


        } else
        {
            HideTimer -= Time.deltaTime;
        }

        if(ShowTimer <= 0)
        {
            if(CantakeDamage)
            {
                animator.SetTrigger("desce");
                HideTimer = Random.Range(HidingTime - (0.5f * HidingTime), HidingTime + (0.5f * HidingTime));
                CantakeDamage = false;
            }
        }   else
        {
            ShowTimer -= Time.deltaTime;
        }


        if (timer < 0.05)
        {
            timer = 0.5f;
            Vector3 rayDirection = Player.transform.position - transform.position;
            RaycastHit hitInfo;
            Debug.DrawRay(transform.position, rayDirection, Color.red, FollowDist);
            if (Physics.Raycast(transform.position, rayDirection, out hitInfo, FollowDist))
            {
                if (hitInfo.collider.gameObject.tag == "Wall")
                {
                    Debug.Log("Parede");
                    ChangeState(States.HIDING);
                }
            }
        }
        agent.isStopped = false;
        agent.SetDestination(Player.transform.position);

        if (timer > 0)
        { timer -= Time.deltaTime; }

    }

    public IEnumerator Attacking()

    {

        attacked = true;



        yield return new WaitForSeconds(AttackDelay);
        Player.GetComponent<PlayerManager>().GetDamage(AttackStrenght);
        attacked = false;


    }

    override public void Dano(float dano)
    {
        if(CantakeDamage)
        {
            remainingLife -= dano;
            if (remainingLife <= 0)
            {
                StopCoroutine("Attacking");
                ChangeState(States.DEAD);

            }
            else
            {
                StartCoroutine("JumpBack");
            }

        }
        return;

    }


    override public void Hide()
    {
        if (Vector3.Distance(transform.position, Player.transform.position) < FollowDist)
        {
            Debug.Log("Glitch te viu");
            animator.SetTrigger("sobe");
            ChangeState(States.FOLLOWING);


        }







    }
}
