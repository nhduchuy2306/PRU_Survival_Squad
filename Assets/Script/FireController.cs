using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireController : MonoBehaviour
{

    [SerializeField] protected bool isShooting = false;
    [SerializeField] protected Transform bulletPrefab;
    [SerializeField] protected float shootDelay = 0.5f;
    [SerializeField] protected float shootTimer = 0f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.IsShooting();
        
    }

    private void FixedUpdate()
    {
        this.Shooting();
    }

    protected virtual void Shooting()
    {
        
        if((this.shootTimer >= this.shootDelay) && !isShooting) { return; }

        this.shootTimer += Time.fixedDeltaTime;

/*        if ((this.shootTimer >= this.shootDelay) && !isShooting) { return; }*/

        if (this.shootTimer < this.shootDelay) { return; }
        

        if (!isShooting) { return; }

        Vector3 spawnPos =transform.position;
        Quaternion rotation = transform.rotation;
        /*Instantiate(this.bulletPrefab, spawnPos, rotation);*/
        BulletSpawner.instance.BulletsSpawn(bulletPrefab, spawnPos, rotation);
        this.shootTimer = 0f;
    }

    protected virtual bool IsShooting()
    {
        this.isShooting = Input.GetAxis("Fire1") == 1;
        return this.isShooting; 
    }
}
