using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Health : MonoBehaviour
{
    [SerializeReference] private int health = 100;

    private int MAX_HEALTH =100;
    private int previousHealth;
    private Animator animator;

    public static Action OnPlayerDeath;
    public static Action OnEnemyDeath;

    // Update is called once per frame

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if(Input.GetKeyUp(KeyCode.D))
        {
            //Damage(10);
        }
        if(Input.GetKeyUp(KeyCode.H))
        {
            Heal(10);
        }    
    }

    public void SetHealth(int maxHealth, int health)
    {
        this.MAX_HEALTH = maxHealth;
        this.health = health;
    }

    private IEnumerator VisualIndicator (Color color )
    {
        GetComponent<SpriteRenderer>().color = color;
        yield return new WaitForSeconds(0.15f);
        GetComponent<SpriteRenderer>().color = Color.white;
    }

    public void Damage(int amount)
    {
        if (amount < 0)
        {
            throw new System.ArgumentException("cannot have negative damage");
        }
        this.health -= amount;
            StartCoroutine(VisualIndicator(Color.red));

        if (health <= 0)
        {
            Die();
        }
        int damageTaken = previousHealth - health;
        Debug.Log("Damage taken: " + damageTaken);
    }

    public void Heal(int amount)
    {
        if (amount < 0)
        {
            throw new System.ArgumentException("cannot have negative healing ");
        }

        bool wouldBeOverMaxHealth = health + amount > MAX_HEALTH;
        StartCoroutine(VisualIndicator(Color.green));

        if (wouldBeOverMaxHealth)
        {
            this.health = MAX_HEALTH;
        }
        else
        {
            this.health += amount;
        }
    }
     
    private void Die()
    {
        Debug.Log("I am Dead!");
        animator.SetTrigger("death");
        Destroy(gameObject);

        if (this.CompareTag("Player"))
        {
            Time.timeScale = 0;
            OnPlayerDeath?.Invoke();
        }
        else
        {
            OnEnemyDeath?.Invoke();
        }


    }





}
