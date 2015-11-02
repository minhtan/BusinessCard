using UnityEngine;
using System.Collections;

public class Rotate : MonoBehaviour {

    public bool rotateConstantly;
    public float constantRotationSpeed;

	// Update is called once per frame
	void Update () {
        if (rotateConstantly) {
            transform.Rotate(Vector3.up * (constantRotationSpeed * Time.deltaTime));
        }
    }

}
