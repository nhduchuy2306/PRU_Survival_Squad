using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunMovement : MonoBehaviour
{

    public static GunMovement instance;

    [SerializeField] protected Vector3 targetPosition;
    [SerializeField] public bool isFacingRight;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        isFacingRight = true;
    }

    // Update is called once per frame
    void Update()
    {
        this.Facing();
        this.GetMousePos();
        this.LookAtTarget();
        this.FlipGun();
    }

    protected virtual void GetMousePos()
    {
        targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
    }

    protected virtual void LookAtTarget()
    {
        Vector3 diff = this.targetPosition - transform.position;
        diff.Normalize();
        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rot_z);
    }

    protected virtual void Facing()
    {
        isFacingRight = (targetPosition.x - transform.position.x) > 0;
    }

    protected virtual void FlipGun()
    {
        if(isFacingRight) {
            Vector3 vector = transform.localScale;
            //vector.x = vector.x * -1;
            vector.y = 1;
            vector.x = 1;

            transform.localScale = vector;
        }
        else
        {
            Vector3 vector = transform.localScale;
            //vector.x = vector.x * -1;
            vector.y = -1;
            vector.x = -1;
            transform.localScale = vector;
        }
    }
}
