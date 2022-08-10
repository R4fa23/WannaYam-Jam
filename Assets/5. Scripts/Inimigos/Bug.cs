using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Bug : Enemies
{
    void Start()
    {
        remainingLife = Life;
        //Só para teste
        //Remove isso e faz o spawner passar essa info antes de instanciar o inimigo
        Player = GameObject.FindWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
        state = States.IDDLE;
        agent.updateRotation = false;


    }


}
