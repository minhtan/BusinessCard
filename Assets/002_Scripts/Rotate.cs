using UnityEngine;
using System.Collections;

public class Rotate : MonoBehaviour {

    float rotationRate = 1.0f;
    float time;
    RaycastHit hit;
    public bool rotateX;

    public bool rotateConstantly;
    public float constantRotationSpeed;

    bool isRotating;
    float startTime;
    float angleY;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (rotateConstantly) {
            transform.Rotate(Vector3.up * (constantRotationSpeed * Time.deltaTime));
        }
    }

    void FixedUpdate()
    {

        if (Input.touchCount > 0){
            Touch theTouch = Input.GetTouch(0);

            Ray ray = Camera.main.ScreenPointToRay(theTouch.position);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity)){

                if (Input.touchCount == 1){

                    float deltaY = 0;
                    float deltaX = 0;
                    float absY = 0;
                    float absX = 0;

                    if (theTouch.phase == TouchPhase.Began){
                        time = Time.time;
                    }

                    if (theTouch.phase == TouchPhase.Moved){
                        deltaY = theTouch.deltaPosition.y;
                        deltaX = theTouch.deltaPosition.x;
                        absY = Mathf.Abs(deltaY);
                        absX = Mathf.Abs(deltaX);

                        if (absY > absX && rotateX){
                            if (deltaY > 0){
                                transform.Rotate(absY * rotationRate, 0, 0, Space.World);
                            }
                            else{
                                transform.Rotate(absY * rotationRate * -1, 0, 0, Space.World);
                            }
                        }
                        else{
                            if (deltaX > 0){
                                transform.Rotate(0, absX * rotationRate * -1, 0, Space.World);
                            }
                            else{
                                transform.Rotate(0, absX * rotationRate, 0, Space.World);
                            }
                        }
                    }

                    if (theTouch.phase == TouchPhase.Ended) {
                        float deltaTime = Time.time - time;
                        if (deltaTime < 2.0f) {
                            startTime = Time.time;
                            angleY = transform.rotation.eulerAngles.y + absX * (1/deltaTime);
                            Debug.Log(angleY);
                            isRotating = true;
                        }
                    }
                }
            }
        }

        if (isRotating) {
            SlerpAroundY();
        }
    }

    void SlerpAroundY() {
        Debug.Log("slerping");
        float t = Time.time - startTime;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.AngleAxis(angleY, Vector3.up), t * rotationRate);
        if (Quaternion.Angle(transform.rotation, Quaternion.AngleAxis(angleY, Vector3.up)) < 1.0f)
        {
            isRotating = false;
        }
    }
}
