using UnityEngine;
using System.Collections;

public class SpinWheel : MonoBehaviour
{
    private float touchAngle = 0.0f;

    void OnMouseDown()
    {
        Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);
        pos = Input.mousePosition - pos;
        touchAngle = Mathf.Atan2(pos.y, pos.x) * Mathf.Rad2Deg;
        //float originAngle = Mathf.Atan2(transform.forward.y, transform.forward.x) * Mathf.Rad2Deg;
        float originAngle = transform.rotation.eulerAngles.y * -1 + 360;
        touchAngle = touchAngle - originAngle;
    }

    void OnMouseDrag()
    {
        Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);
        pos = Input.mousePosition - pos;
        float ang = Mathf.Atan2(pos.y, pos.x) * Mathf.Rad2Deg - touchAngle;
        transform.rotation = Quaternion.AngleAxis(ang, Vector3.down);
    }
}

//http://answers.unity3d.com/questions/648778/need-help-rotating-a-wheel-with-touch-or-using-the.html
//http://answers.unity3d.com/questions/952577/advanced-rotation-on-2d-circle-with-mouse-drag.html