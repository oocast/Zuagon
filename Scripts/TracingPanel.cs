using UnityEngine;
using System.Collections;

public class TracingPanel : MonoBehaviour {
	public float degPerTick;
	public float platformAngleLimit;
	private float platformAngle;
	PathRouter router;

	// Use this for initialization
	void Start () {
		platformAngle = 0;
		router = GameObject.Find("PathRouter").GetComponent<PathRouter>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SpinPlatform (int delta) {
		if (delta != 0) {
			router.updatePath = true;
		}
		float angle = platformAngle;
		angle += (float)delta * degPerTick;
		angle = Mathf.Repeat(angle, 360);
		if (!(angle > platformAngleLimit && angle < 360f - platformAngleLimit)) {
			platformAngle = angle;
			Vector3 rot = transform.rotation.eulerAngles;
			rot.y = platformAngle;
			transform.rotation = Quaternion.Euler(rot);
		}
	}
}
