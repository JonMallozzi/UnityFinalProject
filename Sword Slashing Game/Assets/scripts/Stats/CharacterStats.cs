using UnityEngine;

public class CharacterStats : MonoBehaviour
{

	public int maxHealth = 100;
	public int currentHealth { get; private set;}

	public Stat damage;

	void Awake () 
	{
		currentHealth = maxHealth;
	}

	//testing damage
	void Update ()
	{
		if (Input.GetKeyDown(KeyCode.T))
			TakeDamage(10);
	}

	public void TakeDamage(int damage)
	{

		//ensures that objects only take positive damage
		//or else they would be healed
		damage = Mathf.Clamp(damage, 0, int.MaxValue);

		Debug.Log("<color=blue>Hello</color>");
		currentHealth -= damage;

		Debug.Log(transform.name + " takes " + damage + " damage.");

		if (currentHealth <= 0)
        {
			Die();
		}
			
	}

	public virtual void Die ()
	{
		// Death code for the given object its being
		// implemented on
		Debug.Log(transform.name + " Died.");
	}

}
