using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class Enemies : MonoBehaviour
{
    [SerializeField] protected int Life;
 
    //[SerializeField] float MoveSpeed;
    [SerializeField] protected float FollowDist;
    [SerializeField] protected float AttackDist;
    [SerializeField] protected float AttackDelay;
    [SerializeField] protected int AttackStrenght;
 

    float timer = 0;
    private bool attacked;
    protected float remainingLife;


    public enum States { IDDLE, FOLLOWING, ATACKING, HIDING, DASHING }
    public States state;
    public GameObject Player;

    protected NavMeshAgent agent;

    void Start()
    {
        remainingLife = Life;
        //Só para teste
        //Remove isso e faz o spawner passar essa info antes de instanciar o inimigo
        Player = GameObject.FindWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
        state = States.IDDLE;



    }
    private void Update()
    {

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

                //  case States.DASHING:
                //      Dash();
                //   break;
        }


    }

    void ChangeState(States newState)
    {
        if (state != newState)
            state = newState;



    }
    void Follow()
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
            timer = 0.3f;
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
    void Iddle()
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

    void Attack()
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

    void Hide()
    {
        if (Vector3.Distance(transform.position, Player.transform.position) > FollowDist)
        {
            ChangeState(States.IDDLE);
        }


    }

    public void Dano(float dano)
    {
        remainingLife -= dano;
        if (remainingLife <= 0)
        {
            StopCoroutine("Attacking");

            Destroy(gameObject);
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

