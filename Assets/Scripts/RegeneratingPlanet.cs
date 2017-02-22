using UnityEngine;

public class RegeneratingPlanet : Planet
{
	[SerializeField]
	private float restoredHealthPoints;

	public override void SetConnected (GameObject ship, bool isConnected)
	{
		base.SetConnected (ship, isConnected);
		if (!isConnected) return;
		Health health = ship.GetComponent<Health> ();
		health.Heal (restoredHealthPoints);
	}
}
