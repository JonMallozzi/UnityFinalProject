using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    public UnityEvent deathEvent;
    public UnityEvent damageEvent;
    public UnityEvent healEvent;

    public int maxHealth = 10;
    public int currentHealth = 10;

    [SerializeField] float invlunerabilityTime = 1;
    float lastTimeHit;
    bool dead = false;

    public void ModifyHealth(int amount)
    {
        if (dead)
        {
            return; //skip if you die
        }
        //run events
        if(amount > 0)
        {
            healEvent?.Invoke();
        }
        else if (amount < 0)
        {
            if(lastTimeHit + invlunerabilityTime < Time.time)
            {
                lastTimeHit = Time.time;
                damageEvent?.Invoke();
            }
            else
            {
                print("cancelling damage");
                return;
            }
        }
        //add or subtract health
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        if(currentHealth == 0)
        {
            Die();
        }
    }

    public void Die()
    {
        //remove character
        dead = true;
        if(deathEvent != null)
        {
            deathEvent?.Invoke();
        }
        else
        {
            print("ERROR NO DEATH EFFECT");
        }
    }

    void Ping()
    {
        Debug.Log("Ping");
    }
}