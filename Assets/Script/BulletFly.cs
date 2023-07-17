using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletFly : MonoBehaviour
{

    [SerializeField] protected int moveSpeed = 20;
    [SerializeField] protected Vector3 direction = Vector3.right;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(this.direction * moveSpeed * Time.deltaTime);
    }
}
