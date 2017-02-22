using UnityEngine;

[RequireComponent (typeof (Collider2D))]
public class RadiationZone : MonoBehaviour 
{
	[SerializeField]
	private float damagePerSecond;
	[SerializeField]
	private string shipTag = "Player";

	private void OnTriggerStay2D (Collider2D other)
	{
		if (other.CompareTag (shipTag))
			other.GetComponent<Health> ().ApplyDamage (damagePerSecond * Time.fixedDeltaTime);
	}
}
