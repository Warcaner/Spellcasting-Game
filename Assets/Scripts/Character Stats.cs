using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public float currHealth;
    public float maxHealth;

    public float currMana;
    public float maxMana;

    bool enoughMana;

    public bool isDead = false;

    public virtual void CheckHealth()
    {
        if (currHealth >= maxHealth)
        {
            currHealth = maxHealth;
        }
            
        if (currHealth <= 0)
        {
            currHealth = 0;
            isDead = true;
            Die();
        }

    }

    public virtual void CheckMana()
    {
        if (currMana >= maxMana)
        {
            currMana = maxMana;
        }

        if (currMana <= 0)
        {
            currMana = 0;
        }


    }

    public virtual void Die()
    {
        //Override
        Debug.Log("You Died LAWL");
    }
    
    public void TakeDamage(float damage)
    {
        currHealth -= damage;
    }

    public void SpendMana(float loss)
    {
        if(currMana >= loss)
        {
            currMana -= loss;
        }
        
    }
}
