using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyController : MonoBehaviour
{
    public Rigidbody2D theRB;
    public float moveSpeed;
    private Transform target;
    private Animator animator;
    private CircleCollider2D circleCollider;

    public float damage;

    public float hitWaitTime = 1f;
    private float hitCounter;

    public float destroyWaitTime = 2f;
    private float destroyCounter;

    public float health = 10f;

    public float knockBackTime = .1f;
    private float knockBackCounter;

    public int expToGive = 1;

    public int coinValue = 1;
    public float coinDropRate = .5f;

    // Start is called before the first frame update
    void Start()
    {
        target = FindAnyObjectByType<PlayerController>().transform;
        animator = gameObject.GetComponentInChildren<Animator>();
        circleCollider = gameObject.GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(knockBackCounter > 0)
        {
            knockBackCounter -= Time.deltaTime;

            if(moveSpeed > 0)
            {
                moveSpeed = -moveSpeed * 2;

            }

            if(knockBackCounter <= 0)
            {
                moveSpeed = Mathf.Abs(moveSpeed * .5f);
            }
        }


        theRB.velocity = (target.position - transform.position).normalized * moveSpeed;

        if (hitCounter > 0f)
        {
            hitCounter -= Time.deltaTime;
        }   

        if(destroyCounter > 0)
        {
            destroyCounter -= Time.deltaTime;
            if (destroyCounter <= 0)
            {
                Destroy(gameObject);
                ExperienceLevelController.instance.SpawnExp(transform.position, expToGive);
                if(Random.value <= coinDropRate)
                {
                    CoinController.instance.DropCoin(transform.position, coinValue);
                }
            }
            
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && hitCounter <= 0f)
        {
            PlayerHealthController.instance.TakeDamage(damage);

            hitCounter = hitWaitTime;


        }
    }

    public void TakeDamage(float damageToTake)
    {
        health -= damageToTake;
        animator.SetTrigger("Hit");

        if(health <= 0f)
        {
            animator.SetBool("Dead", true);
            destroyCounter = destroyWaitTime;
            circleCollider.enabled = false;
            moveSpeed = 0;
        }
    }

    public void TakeDamage(float damageToTake, bool shouldKnockBack)
    {
        TakeDamage(damageToTake);

        if (shouldKnockBack)
        {
            knockBackCounter = knockBackTime;
        }
    }
}
