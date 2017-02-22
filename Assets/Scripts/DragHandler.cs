using UnityEngine;

[RequireComponent (typeof (LineRenderer))]
public class DragHandler: MonoBehaviour
{
    [SerializeField]
    private float maxLength;
    private Vector2 rawDrag;
    private LineRenderer lineRenderer;

    public bool IsDragging
    {
        get; private set;
    }

    public bool ReleasedDrag
    {
        get; private set;
    }

    public Vector2 Direction 
    { 
        get
        {
            return (IsDragging || ReleasedDrag)? -rawDrag.normalized : Vector2.zero;
        }
    }

    public float Value 
    { 
        get
        {
            return (IsDragging || ReleasedDrag)? ClampMagnitude () : 0f;
        }
    }

    public Vector3 DragPosition
    {
        get
        {
            Vector2 screenPosition = Camera.main.WorldToScreenPoint (transform.position);
            Vector3 dragPosition = screenPosition - Direction * (maxLength * Value);
            dragPosition = Camera.main.ScreenToWorldPoint (dragPosition);
            dragPosition.z = transform.position.z;
            return dragPosition;
        }
    }

    private void Awake ()
    {
        lineRenderer = GetComponent<LineRenderer> ();
    }

    private void Update ()
    {
        ChangeDragState ();
        if (IsDragging)
            UpdateRawDrag ();
        HandleLine ();
    }

    private void OnEnable ()
    {
        lineRenderer.enabled = true;
    }

    private void OnDisable ()
    {
        rawDrag = Vector2.zero;
        lineRenderer.enabled = false;
        IsDragging = false;
    }

    private void ChangeDragState ()
    {
        bool wasDragging = IsDragging;
        IsDragging = Input.GetMouseButton (0);
        ReleasedDrag = wasDragging && !IsDragging;
    }

    private void UpdateRawDrag ()
    {
        Vector2 screenPosition = Camera.main.WorldToScreenPoint (transform.position);
        rawDrag = (Vector2) Input.mousePosition - screenPosition;
    }

    private float ClampMagnitude ()
    {
        float percentage = rawDrag.sqrMagnitude / (maxLength * maxLength);
        return Mathf.Clamp (percentage, 0f, 1f);
    }

    private void HandleLine ()
    {
        lineRenderer.enabled = IsDragging;
        if (IsDragging)
            SetLinePositions ();
    }

    private void SetLinePositions ()
    {
        lineRenderer.numPositions = 2;
        lineRenderer.SetPosition (0, transform.position);
        lineRenderer.SetPosition (1, DragPosition);
    }
}