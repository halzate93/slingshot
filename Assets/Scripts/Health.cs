using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour 
{
	[SerializeField]
	private float healtPoints;

	public void ApplyDamage (float damage)
	{
		healtPoints -= damage;
		if (healtPoints <= 0f)
			Die ();
	}

	public void Heal (float lifePoints)
	{
		healtPoints += lifePoints;
	}

	private void Die ()
	{
		Debug.Log ("Player died");
		Destroy (gameObject);
	}
}
