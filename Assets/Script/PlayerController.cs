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

    //public Weapon activeWeapon;

    public List<Weapon> unassignedWeapons, assignedWeapons;

    void Start()
    {
        isFacingRight = true;

        AddWeapon(unassignedWeapons[0]);
        assignedWeapons[0].gameObject.SetActive(true);
        moveSpeed = PlayerStatController.instance.moveSpeed[0].value;
        pickupRange= PlayerStatController.instance.pickupRange[0].value;

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
        this.ChangeWeapon();
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
        else
        {

            Vector3 vector = transform.localScale;
            //vector.x = vector.x * -1;
            vector.x = -1;
            transform.localScale = vector;
            Vector3 canvasVector = canvas.transform.localScale;
            canvasVector.x = -0.01f;
            canvas.transform.localScale = canvasVector;
        }
    }

    public void AddWeapon(int weaponNumber)
    {
        if (weaponNumber < unassignedWeapons.Count)
        {
            assignedWeapons.Add(unassignedWeapons[weaponNumber]);

            //unassignedWeapons[weaponNumber].gameObject.SetActive(true);
            unassignedWeapons.RemoveAt(weaponNumber);
        }
    }

    public void AddWeapon(Weapon weaponToAdd)
    {
        //weaponToAdd.gameObject.SetActive(true);
        assignedWeapons.Add(weaponToAdd);

        unassignedWeapons.Remove(weaponToAdd);
    }

    public void ChangeWeapon()
    {

        if (Input.GetKey(KeyCode.Alpha1))
        {
            assignedWeapons[0].gameObject.SetActive(true);
            if (assignedWeapons.Count > 1)
            {
                assignedWeapons[1].gameObject.SetActive(false);
            }
            if (assignedWeapons.Count > 2)
            {
                assignedWeapons[2].gameObject.SetActive(false);
            }
        }
        if (Input.GetKey(KeyCode.Alpha2))
        {
            if (assignedWeapons.Count < 2) { return; }
            assignedWeapons[1].gameObject.SetActive(true);

            assignedWeapons[0].gameObject.SetActive(false);

            if (assignedWeapons.Count > 2)
            {
                assignedWeapons[2].gameObject.SetActive(false);
            }
        }
        if (Input.GetKey(KeyCode.Alpha3))
        {
            if (assignedWeapons.Count < 3) { return; }
            assignedWeapons[2].gameObject.SetActive(true);
            if (assignedWeapons[0] != null)
            {
                assignedWeapons[0].gameObject.SetActive(false);
            }
            if (assignedWeapons[1] != null)
            {
                assignedWeapons[1].gameObject.SetActive(false);
            }
        }
    }
}
