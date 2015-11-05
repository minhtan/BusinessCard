using UnityEngine;
using System.Collections;

public class Cube : MonoBehaviour {

    private BackCardControl bcard;
    public GameObject effect;

	void Start () {
        bcard = GameObject.FindObjectOfType<BackCardControl>();
	}

    public void ShowCircle() {
        bcard.ShowCircle();
    }

    public void ShowParticle() {
        effect.SetActive(true);
    }

	void Update () {
	
	}
}
