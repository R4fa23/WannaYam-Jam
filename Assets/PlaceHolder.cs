using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlaceHolder : Enemies
{
    override public void starter(int wave, EnemySpawner source, GameObject player, NavMeshTriangulation Triang)
    {
        wichwave = wave;
        spawner = source;
        Player = player;
        Triangulation = Triang;
        agent = GetComponent<NavMeshAgent>();
        
        int VertexIndex = Random.Range(0, Triangulation.vertices.Length);
        NavMeshHit Hit;
        if(NavMesh.SamplePosition(Triangulation.vertices[VertexIndex], out Hit, 2f, -1))
        {
            agent.Warp(Hit.position);
        }
        state = States.IDDLE;
        remainingLife = Life;

    }

    override public void Iddle()
    {
       
    }

}
