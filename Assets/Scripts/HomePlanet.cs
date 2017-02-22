using UnityEngine;

public class HomePlanet : Planet
{
	public override void SetConnected (GameObject ship, bool isConnected)
	{
		base.SetConnected (ship, isConnected);
		GameManager.Instance.WinGame ();
	}
}
