using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    public static EnemyAnimation instance;


    public Transform playerTransform;

    public Transform transform;

    private bool isFacingRight;


    private void Awake()
    {
        playerTransform = PlayerController.instance.transform;
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        isFacingRight = (transform.position.x < playerTransform.position.x);
    }

    // Update is called once per frame
    void Update()
    {
        this.Flip();
    }

    void Flip()
    {

        isFacingRight = (transform.position.x < playerTransform.position.x);

        Vector3 vector = transform.localScale;
        if (isFacingRight)
        {
            
            vector.x = 1;
            
        }
        else
        {
            vector.x = -1;
        }
        transform.localScale = vector;
    }

}
