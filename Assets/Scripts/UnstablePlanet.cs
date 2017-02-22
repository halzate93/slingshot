using System.Collections;
using UnityEngine;

public class UnstablePlanet : Planet
{
	[SerializeField]
	private float destroyTime;
	private GameObject connected;

	public override void SetConnected (GameObject ship, bool isConnected)
	{
		base.SetConnected (ship, isConnected);
		connected = isConnected? ship : null;
		if (isConnected)
			StartCoroutine (DestroyRoutine ());
	}

	private IEnumerator DestroyRoutine ()
	{
		yield return new WaitForSeconds (destroyTime);
		if (connected != null)
			Destroy (connected);
		Destroy (gameObject);
	}
	
}
