using UnityEngine;

[RequireComponent (typeof (Rigidbody2D))]
[RequireComponent (typeof (DragHandler))]
public class Planet : MonoBehaviour 
{
	[SerializeField]
	private float maxTorque;
	private new Rigidbody2D rigidbody;
	public DragHandler Drag
	{
		get; private set;
	}

	public Collider2D Collider
	{
		get; private set;
	}

	private void Awake ()
	{
		rigidbody = GetComponent<Rigidbody2D> ();
		Drag = GetComponent<DragHandler> ();
		Collider = GetComponent<Collider2D> ();
	}

	private void Start () 
	{
		AddRandomTorque ();	
	}

	public virtual void SetConnected (GameObject player, bool isConnected)
	{
		Drag.enabled = isConnected;
	}

	private void AddRandomTorque ()
	{
		float torque = Random.Range (-maxTorque, maxTorque);
		rigidbody.AddTorque (torque);
	}
}
