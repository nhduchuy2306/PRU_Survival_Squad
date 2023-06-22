using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    public static BulletSpawner instance;

    public GameObject bulletToSpawn;

    public Transform minSpawn, maxSpawn;

    private float despawnDistant;

    private List<GameObject> spawnBullets = new List<GameObject>();

    public int checkPerFrame;
    private int bulletToCheck;

    private Transform target;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {

        target = PlayerController.instance.transform;

        despawnDistant = Vector3.Distance(transform.position, maxSpawn.position) + 4f;
    }

    // Update is called once per frame
    void Update()
    {
        CheckBullet();
    }


    protected virtual void CheckBullet()
    {
        int checkTarget = bulletToCheck + checkPerFrame;

        while (bulletToCheck < checkTarget)
        {
            if (bulletToCheck < spawnBullets.Count)
            {
                if (spawnBullets[bulletToCheck] != null)
                {
                    if (Vector3.Distance(transform.position, spawnBullets[bulletToCheck].transform.position) > despawnDistant)
                    {
                        Destroy(spawnBullets[bulletToCheck]);

                        spawnBullets.RemoveAt(bulletToCheck);
                        checkTarget--;
                    }
                    else
                    {
                        bulletToCheck++;
                    }

                }
                else
                {
                    spawnBullets.RemoveAt(bulletToCheck);
                    checkTarget--;
                }
            }
            else
            {
                bulletToCheck = 0;
                checkTarget = 0;
            }
        }
    }

    public virtual void BulletsSpawn(Transform bulletPrefab, Vector3 spawnPos, Quaternion rotation)
    {

        GameObject newBullet = Instantiate(bulletToSpawn, spawnPos, rotation);
        spawnBullets.Add(newBullet);
        transform.position = target.position;

        
    }

}
