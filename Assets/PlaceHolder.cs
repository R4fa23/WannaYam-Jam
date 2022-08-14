using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlaceHolder : Enemies
{
    private float i;
    [SerializeField] private float TimeOfBulletWave;
    [SerializeField] private float TimeOfCoolDown;
    override public void starter(int wave, EnemySpawner source, GameObject player, NavMeshTriangulation Triang)
    {
        animator = GetComponent<Animator>();
        wichwave = wave;
        spawner = source;
        Player = player;
        Triangulation = Triang;
        
        agent = GetComponent<NavMeshAgent>();

        int VertexIndex = Random.Range(0, Triangulation.vertices.Length);
        NavMeshHit Hit;
        if (NavMesh.SamplePosition(Triangulation.vertices[VertexIndex], out Hit, 2f, -1))
        {
            agent.Warp(Hit.position);
            agent.SetDestination(Hit.position);
        }
        animator.SetTrigger("desce");
        state = States.HIDING;
        remainingLife = Life;

    }

    override public void Iddle()
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
                        ChangeState(States.ATACKING);
                    }
                    else agent.isStopped = true;
                }

                return;
            }

        }
        else
        {
            agent.isStopped = true;
            ChangeState(States.HIDING);
            animator.SetTrigger("desce");
        }
        if (timer > 0)
        { timer -= Time.deltaTime; }
    
}

    override public void Hide()
    {
        if(Vector3.Distance(transform.position, Player.transform.position) < FollowDist)
        {
            Debug.Log("Torreta te viu");
            animator.SetTrigger("sobe");
            ChangeState(States.IDDLE);


        }







    }


    override public void Attack()
    {

        if (Vector3.Distance(transform.position, Player.transform.position) > FollowDist)
        {
           // animator.SetTrigger("desce");
            attacked = false;
            state = States.IDDLE;
            StopCoroutine("Attacking");
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
        while (i <TimeOfBulletWave)
        {
            Vector3 rayDirection = Player.transform.position - transform.position;
            RaycastHit hitInfo;

            Debug.DrawRay(transform.position, rayDirection, Color.blue, FollowDist);
            if (Physics.Raycast(transform.position, rayDirection, out hitInfo, FollowDist))
            {
                if (hitInfo.collider.gameObject.tag == "Wall")
                {
                    Debug.Log("TE VI");
                    ChangeState(States.IDDLE);
                    i = 0;
                }
                else
                {
                    GameObject bullet = ObjectPool.instance.GetBulletFromPool();
                    bullet.transform.position = transform.position;
                    bullet.GetComponent<turretBullet>().Damage = AttackStrenght;
                    bullet.GetComponent<Rigidbody>().velocity = (rayDirection * 500 * Time.deltaTime);
                    i++;
                }
            }








            yield return new WaitForSeconds(AttackDelay);


            
        }
        yield return new WaitForSeconds(TimeOfCoolDown);
        i = 0;
        attacked = false;


    }




 

    public override IEnumerator JumpBack()

    {
       animator.SetTrigger("dano");
       yield return new WaitForSeconds(0.3f);

    }

}
