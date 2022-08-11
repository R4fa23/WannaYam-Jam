using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class Enemies : MonoBehaviour
{
    [SerializeField] protected int Life;
    [SerializeField] protected float FollowDist;
    [SerializeField] protected float AttackDist;
    [SerializeField] protected float AttackDelay;
    [SerializeField] protected int AttackStrenght;
    public bool TesteDeMorte;


    protected float timer = 0;
    protected bool attacked;
    protected float remainingLife;


    public enum States { IDDLE, FOLLOWING, ATACKING, HIDING, DASHING, DEAD }
    public States state;
    

    protected NavMeshAgent agent;

    protected int wichwave;
    protected EnemySpawner spawner;
    protected GameObject Player;
    protected NavMeshTriangulation Triangulation;


    private void Update()
    {
        if(TesteDeMorte)
        {
            TesteDeMorte = false;
            Dano(Life);
        
       }
        switch (state)
        {
            case States.IDDLE:
                Iddle();
                break;

            case States.FOLLOWING:
                Follow();
                break;

            case States.ATACKING:
                Attack();
                break;

            case States.HIDING:
                Hide();
                break;
            case States.DEAD:
                Dead();
                break;

                //  case States.DASHING:
                //      Dash();
                //   break;
        }


    }

    public void ChangeState(States newState)
    {
        if (state != newState)
            state = newState;



    }
    virtual public void Follow()
    {


        if (Vector3.Distance(transform.position, Player.transform.position) > FollowDist)
        {
            ChangeState(States.IDDLE);
        }

        if (Vector3.Distance(transform.position, Player.transform.position) < AttackDist)
        {
            ChangeState(States.ATACKING);
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
                    ChangeState(States.IDDLE);
                }
            } }
        agent.isStopped = false;
        agent.SetDestination(Player.transform.position);

        if (timer > 0)
        { timer -= Time.deltaTime; }

    }
    virtual public void Iddle()
    {
        if (Vector3.Distance(transform.position, Player.transform.position) < FollowDist)
        {

            if (timer < 0.1)
            {
                timer = 1;
                Vector3 rayDirection = Player.transform.position - transform.position;
                //Debug.Log("raio " + rayDirection + " posicao player " + Player.transform.position);
                RaycastHit hitInfo;


                Debug.DrawRay(transform.position, rayDirection, Color.black, FollowDist);
                if (Physics.Raycast(transform.position, rayDirection, out hitInfo, FollowDist))
                {
                    if (hitInfo.collider.gameObject.tag != "Wall")
                    {
                        Debug.Log("TE VI");
                        ChangeState(States.FOLLOWING);
                    }
                    else agent.isStopped = true;
                }

                return;
            }

        }
        else agent.isStopped = true;
        if (timer > 0)
        { timer -= Time.deltaTime; }
    }

   virtual public void Attack()
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

    virtual public void Hide()
    {
        if (Vector3.Distance(transform.position, Player.transform.position) > FollowDist)
        {
            ChangeState(States.IDDLE);
        }


    }

    virtual public void Dano(float dano)
    {
        remainingLife -= dano;
        if (remainingLife <= 0)
        {
            StopCoroutine("Attacking");
            ChangeState(States.DEAD);

        }
    }

    virtual public void Dead()
    {
        spawner.killed(wichwave);
        Destroy(gameObject);
    }

    virtual public void starter (int wave, EnemySpawner source, GameObject player, NavMeshTriangulation Triang)
    {
        wichwave = wave;
        spawner = source;
        Player = player;
        Triangulation = Triang;
        agent = GetComponent<NavMeshAgent>();
        state = States.IDDLE;
        remainingLife = Life;

    }


}

