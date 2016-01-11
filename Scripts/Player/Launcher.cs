using UnityEngine;
using System.Collections;

public class Launcher : MonoBehaviour {
	private JamoDrum jod;
	private JamoDrumKeyVersion jodkey;
	public bool key;

	public int launcherIndex; // 0 or 1
	public float launcherAngle;
	public float degPerTick;
	private float initAngle;
	private int hitIndex;
	private int spinIndex;

	void Awake () {
		jod = GameObject.Find ("JamODrum").GetComponent<JamoDrum>();
		jodkey = GameObject.Find ("JamODrum").GetComponent<JamoDrumKeyVersion>();

		if (key) {
			jodkey.AddHitEvent(HitHandler);
			jodkey.AddSpinEvent(SpinHandler);
		}
		else {
			jod.AddHitEvent(HitHandler);
			jod.AddSpinEvent(SpinHandler);
		}
	}


	// Use this for initialization
	void Start () {
		initAngle = transform.rotation.eulerAngles.y;
		hitIndex = launcherIndex * 2;
		spinIndex = launcherIndex * 2 + 1;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SpinHandler(int controllerID, int delta) {
		if (controllerID == spinIndex + 1) {
			Debug.Log ("Launcher spins");
			launcherAngle += degPerTick * delta;
			launcherAngle = Mathf.Repeat (launcherAngle, 360f);
			Vector3 rot = transform.rotation.eulerAngles;
			rot.y = initAngle + launcherAngle;
			transform.rotation = Quaternion.Euler(rot);
		}
	}
	
	public void HitHandler(int controllerID) {
		if (controllerID == hitIndex) {
			Debug.Log ("Launcher shoots a marble");
		}
	}
}
