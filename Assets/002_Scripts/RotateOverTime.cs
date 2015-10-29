using UnityEngine;
using System.Collections;

public class RotateOverTime : MonoBehaviour {

    public float RotationSpeed;

    // Use this for initialization
    void Start () {
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up * (RotationSpeed * Time.deltaTime));
    }


}
