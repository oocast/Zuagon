using UnityEngine;
using System.Collections;

public class GroundController : MonoBehaviour {
	float speed = 30f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey(KeyCode.LeftArrow)) {
			transform.Rotate(-transform.up * Time.deltaTime * speed);
		}
		else if (Input.GetKey(KeyCode.RightArrow)) {
			transform.Rotate(transform.up * Time.deltaTime * speed);
		}
	}
}
