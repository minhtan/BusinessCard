using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class CubePart : MonoBehaviour {

    public UnityEvent cubePartClickEvent;
    public LayerMask myLayerMask;
    private bool clicked;
    private float time;
	
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            Ray m_Ray;
            m_Ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit m_RayCastHit;
            if (Physics.Raycast(m_Ray.origin, m_Ray.direction, out m_RayCastHit, Mathf.Infinity, myLayerMask)) {
                if (m_RayCastHit.collider.tag == gameObject.tag)
                {
                    Debug.Log(m_RayCastHit.collider.name);
                    clicked = true;
                    time = Time.time;
                }
            }
        }

        if (Input.GetMouseButtonUp(0)) {
            time = Time.time - time;
            if (clicked = true && time <0.5f) {
                Ray m_Ray;
                m_Ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit m_RayCastHit;
                if (Physics.Raycast(m_Ray.origin, m_Ray.direction, out m_RayCastHit, Mathf.Infinity, myLayerMask))
                {
                    if (clicked = true && m_RayCastHit.collider.tag == gameObject.tag)
                    {
                        Debug.Log(m_RayCastHit.collider.name);
                        cubePartClickEvent.Invoke();
                    }
                }
            }
        }
    }

}
