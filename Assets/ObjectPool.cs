using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool instance;
    [SerializeField] private GameObject bulletPrefab;
    private Queue<GameObject> bulletpool = new Queue<GameObject>();

    [SerializeField] private int bulletPoolSize = 70;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    void Start()
    {
        for (int i = 0; i < bulletPoolSize; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab, transform);
            bulletpool.Enqueue(bullet);
            bullet.SetActive(false);
        }
    }

    public GameObject GetBulletFromPool()
    {
        if (bulletpool.Count >0)
        {
            GameObject bullet = bulletpool.Dequeue();
            bullet.SetActive(true);
            return bullet;
        }
        else
        {
            GameObject bullet = Instantiate(bulletPrefab);
            return bullet;
        }

    }


    public void ReturnBulletToPool(GameObject bullet)
    {
        bulletpool.Enqueue(bullet);
        bullet.SetActive(false);
    }
}
