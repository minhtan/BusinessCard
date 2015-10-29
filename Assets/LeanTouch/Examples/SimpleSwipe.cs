using UnityEngine;

// This script will push a rigidbody around when you swipe
public class SimpleSwipe : MonoBehaviour
{
	// This stores the layers we want the raycast to hit (make sure this GameObject's layer is included!)
	public LayerMask LayerMask = UnityEngine.Physics.DefaultRaycastLayers;

    // This allows use to set how powerful the swipe will be
    public float speed = 1.0f;
    private float startTime;
    private bool isRotating;
    private float angle;

    protected virtual void OnEnable()
	{
		// Hook into the OnSwipe event
		Lean.LeanTouch.OnFingerSwipe += OnFingerSwipe;
	}
	
	protected virtual void OnDisable()
	{
		// Unhook into the OnSwipe event
		Lean.LeanTouch.OnFingerSwipe -= OnFingerSwipe;
	}
	
	public void OnFingerSwipe(Lean.LeanFinger finger)
	{
		// Raycast information
		var ray = finger.GetStartRay();
		var hit = default(RaycastHit);
		
		// Was this finger pressed down on a collider?
		if (!isRotating && Physics.Raycast(ray, out hit, float.PositiveInfinity, LayerMask) == true)
		{
			// Was that collider this one?
			if (hit.collider.gameObject == gameObject)
			{
                var swipe = finger.SwipeDelta;
                if (swipe.x < -Mathf.Abs(swipe.y))
                {
                    Debug.Log("You swiped left!");
                    
                    startTime = Time.time;
                    isRotating = true;
                    angle = transform.rotation.eulerAngles.y + finger.ScaledSwipeDelta.x;
                    Debug.Log("delta x " + finger.ScaledSwipeDelta.x);
                    Debug.Log("angle " + angle);
                }

                if (swipe.x > Mathf.Abs(swipe.y))
                {
                    Debug.Log("You swiped right!");
                }
            }
		}
	}

    void Update() {
        if (isRotating)
        {
            Debug.Log("rotating");
            float t = Time.time - startTime;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(transform.rotation.eulerAngles.x, angle, transform.rotation.eulerAngles.z), speed * t);
            if (Quaternion.Angle(transform.rotation, Quaternion.Euler(transform.rotation.eulerAngles.x, angle, transform.rotation.eulerAngles.z)) < 1.0f)
            {
                isRotating = false;
                Debug.Log("Stop rotate");
            }
        }
    }
}