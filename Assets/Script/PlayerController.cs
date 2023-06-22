using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    public Canvas canvas;

    private void Awake()
    {
        instance = this;
    }
    public float move;
    public float moveSpeed;

    public Animator anim;

    public float pickupRange = 1.5f;

    public int maxWeapons = 3;

    private bool isFacingRight;



    void Start()
    {
        isFacingRight = true;
    }


    void Update()
    {
        isFacingRight = GunMovement.instance.isFacingRight;

        Vector3 moveInput = new Vector3(0f, 0f, 0f);
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");

        //Debug.Log(moveInput);

        moveInput.Normalize();

        //Debug.Log(moveInput);

        transform.position += moveInput * moveSpeed * Time.deltaTime;

        if (moveInput != Vector3.zero)
        {
            anim.SetBool("isMoving", true);
        }
        else
        {
            anim.SetBool("isMoving", false);
        }
        this.Flip();
    }

    void Flip()
    {
        if (isFacingRight)
        {

            Vector3 vector = transform.localScale;
            //vector.x = vector.x * -1;
            vector.x = 1;
            transform.localScale = vector;
            Vector3 canvasVector = canvas.transform.localScale;
            canvasVector.x = 0.01f;
            canvas.transform.localScale = canvasVector;
        }
        else {

            Vector3 vector = transform.localScale;
            //vector.x = vector.x * -1;
            vector.x = -1;
            transform.localScale = vector;
            Vector3 canvasVector = canvas.transform.localScale;
            canvasVector.x = -0.01f;
            canvas.transform.localScale = canvasVector;
        }
    }

}
