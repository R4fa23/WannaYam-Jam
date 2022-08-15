using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawner : MonoBehaviour
{

    
    public Transform[] SpawnPositions;
    public List<Waves> waves = new List<Waves>();
    public List<int> DeadEnemies = new List<int>();
    public float IntervalOfSpawn;
    private float timer;
    private bool enableSpawn;
    public int i;
    private NavMeshTriangulation Triangulation;
   [SerializeField]private GameObject Player;

    void Start()
    {
        timer = IntervalOfSpawn;
        Triangulation = NavMesh.CalculateTriangulation();
        foreach (Waves wavequant in waves)
        {
            DeadEnemies.Add(waves.Count);
            DeadEnemies[waves.IndexOf(wavequant)] = wavequant.Wave.Count;
        }
      enableSpawn = true;
        foreach (Transform anima in SpawnPositions)
        {
            anima.gameObject.GetComponent<Animator>().SetTrigger("Open");
        }
        

    }

    // Update is called once per frame
    void Update()
    {
        if (enableSpawn)
        {

            if (IntervalOfSpawn <= 0)
            {
                Spawn();
                IntervalOfSpawn = timer;
            }
            else
                IntervalOfSpawn -= Time.deltaTime;

        }
      
    }

    public void killed(int wavecount)
    {
        DeadEnemies[wavecount]--;
        if (DeadEnemies[wavecount] <= 0)
        {
            nextwave();
        }

    }

    void nextwave()
    {
        i++;
    }

    void Spawn()
    {
        if(i < waves.Count)
        {
            if (waves[i].Wave.Count != 0 && waves[i].Wave[0] != null)
            {
                int n = Random.Range(0, waves[i].Wave.Count);
               // Debug.Log(n);
                GameObject enemy = Instantiate(waves[i].Wave[n], SpawnPositions[Random.Range(0, SpawnPositions.Length)].position, Quaternion.identity);
                enemy.GetComponent<Enemies>().starter(i, this, Player,Triangulation);
                waves[i].Wave.RemoveAt(n);


            }
            else { 
                
                
                
                Debug.Log("Aguardando proxima onda"); }


        }
        
        else
        { Debug.Log("Acabou a fase");


            foreach (Transform anima in SpawnPositions)
            {
                anima.gameObject.GetComponent<Animator>().SetTrigger("Close");
            }
            Player.GetComponent<PlayerManager>().playerStats.LevelCompletedTrigger();
        }
       
                   

          




    
    }
    
}

[System.Serializable]
public class Waves
{
    public List<GameObject> Wave;
}

