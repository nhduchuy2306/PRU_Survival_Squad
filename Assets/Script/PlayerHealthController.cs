using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.VFX;

public class PlayerHealthController : MonoBehaviour
{
    public static PlayerHealthController instance;

    private void Awake()
    {
        instance = this;
    }

    public float currentHealth, maxHealth;

    public Slider healthSlider;

    public GameObject deathEffect;

    // Start is called before the first frame update
    void Start()
    {
        /*maxHealth = PlayerStatController.instance.health[0].value;*/
        maxHealth = PlayerStatController.instance.health[0].value;
        currentHealth = maxHealth;

        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void TakeDamage(float damageToTake)
    {
        currentHealth -= damageToTake;

        if (currentHealth <= 0)
        {
            PlayerController.instance.anim.SetBool("isDead", true);
            gameObject.GetComponent<PlayerController>().enabled = false;
            GameObject.Find("Weapon").active = false;
            /*gameObject.SetActive(false);*/

/*            LevelManager.instance.EndLevel();

            Instantiate(deathEffect, transform.position, transform.rotation);

            SFXManager.instance.PlaySFX(3);*/
        }

        healthSlider.value = currentHealth;
    }
}
