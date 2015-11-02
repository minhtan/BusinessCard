using UnityEngine;
using System.Collections;

public class SpinWheel : MonoBehaviour
{
    private float baseAngle = 0.0f;

    void OnMouseDown()
    {
        
        Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);
        pos = Input.mousePosition - pos;
        Debug.Log(transform.right + " " + transform.up + " " + transform.forward + " " + pos);
        baseAngle = Mathf.Atan2(pos.y, pos.x) * Mathf.Rad2Deg;
        baseAngle -= Mathf.Atan2(transform.right.y, transform.right.x) * Mathf.Rad2Deg;
    }

    void OnMouseDrag()
    {
        Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);
        pos = Input.mousePosition - pos;
        float ang = Mathf.Atan2(pos.y, pos.x) * Mathf.Rad2Deg + baseAngle;
        //transform.rotation = Quaternion.AngleAxis(ang, Vector3.down);
    }
}

//http://answers.unity3d.com/questions/648778/need-help-rotating-a-wheel-with-touch-or-using-the.html
//http://answers.unity3d.com/questions/952577/advanced-rotation-on-2d-circle-with-mouse-drag.html