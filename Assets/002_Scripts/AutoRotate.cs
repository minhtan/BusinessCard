using UnityEngine;
using System.Collections;

public class AutoRotate : MonoBehaviour {

    public bool rotateClockwise;
    public float constantRotationSpeed;
    private Vector3 axis;

    void Start() {
        if (rotateClockwise)
        {
            axis = Vector3.up;
        }
        else {
            axis = Vector3.down;
        }
    }

	// Update is called once per frame
	void Update () {
        transform.Rotate(axis * (constantRotationSpeed * Time.deltaTime));
    }

}
