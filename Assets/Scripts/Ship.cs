using UnityEngine;

[RequireComponent (typeof (Rigidbody2D))]
public class Ship : MonoBehaviour 
{
	[SerializeField]
	private Planet connected;
	[SerializeField]
	private string planetTag = "Planet";
	[SerializeField]
	private float distanceFromPlanet = 3f;
	[SerializeField]
	private float throwForce = 100f;

	protected Collider2D Collider
	{
		get; private set;
	}

	private new Rigidbody2D rigidbody;

	private void Awake () 
	{
		rigidbody = GetComponent<Rigidbody2D> ();	
	}
	
	private void Start ()
	{
		Connect (connected);
	}

	private void Update () 
	{
		if (connected != null && connected.Drag.ReleasedDrag)
		{
			Throw ();
			ReleaseConnected ();
		}
		if (connected != null)
			SetPosition ();
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (connected == null && other.CompareTag (planetTag))
			Connect (other.GetComponent<Planet> ());
	}

	private void SetPosition ()
	{
		Vector3 targetPosition = connected.Drag.IsDragging? GetOpposedPosition () : (Vector2)connected.transform.position;
		targetPosition.z = transform.position.z;
		transform.position = (targetPosition);
		if (connected.Drag.IsDragging)
			LookTowards (connected.Drag.Direction);
	}

	private Vector2 GetOpposedPosition ()
	{
		Vector2 planetPosition = connected.transform.position;
		Vector2 opposedPosition = planetPosition + connected.Drag.Direction * distanceFromPlanet;
		return opposedPosition;
	}

	private void LookTowards (Vector2 direction)
	{
		transform.localRotation = Quaternion.LookRotation (Vector3.forward, direction);
	}

	private void Connect (Planet planet)
	{
		connected = planet;
		rigidbody.angularVelocity = 0f;
		rigidbody.velocity = Vector2.zero;
		SetConnected (planet, true);
	}

	private void ReleaseConnected ()
	{
		SetConnected (connected, false);
		connected = null;
	}

	private void SetConnected (Planet planet, bool isConnected)
	{
		connected.SetConnected (gameObject, isConnected);
		Physics2D.IgnoreCollision (Collider, planet.Collider, isConnected);
	}

	private void Throw ()
	{
		Vector2 force = connected.Drag.Direction * (connected.Drag.Value * throwForce);
		rigidbody.AddForce (force, ForceMode2D.Impulse);
	}

	private void OnDestroy ()
	{
		ReleaseConnected ();
	}
}
