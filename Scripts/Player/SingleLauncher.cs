using UnityEngine;
using System.Collections;

public class SingleLauncher : MonoBehaviour {
	
	public JamoDrum jod;
	public JamoDrumKeyVersion jodkey;
	public VillainController villainController;
	public bool key;

	public bool[] evil = new bool[4];
	public GameObject[] spinners = new GameObject[4];
	public float[] degPerTick = new float[4];
	public float[] spinnerModifer = new float[4];
	public float[] spinnerAngle = new float[4];
	private float[] initAngle = new float[4];
	private Cannon[] cannon = new Cannon[4];
	public bool[] drumLock = new bool[4];
	
	private bool once;

	void Awake () {
		jod = GetComponent<JamoDrum>();
		jodkey = GetComponent<JamoDrumKeyVersion>();
		villainController = GetComponent<VillainController>();
		villainController.RegisterMagicEndAction(ResetAllSpinners);
		villainController.RegisterMagicEndAction(UnlockAllDrums);
		//villainController.RegisterMagicEndAction(UnlockAllCannons);

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
		for(int i=0; i<4; i++) {
			initAngle[i] = spinners[i].transform.rotation.eulerAngles.y;
			cannon[i] = spinners[i].transform.GetChild(0)
				.gameObject.GetComponent<Cannon>();
			if (evil[i] == true) {
				villainController.villainIndex = i;
				cannon[i].villain = true;
				//SetCannonLock(i, true);
				villainController.cannon = cannon[i];
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (!key) {
			for(int i=0; i<4; i++) {
				//spin
				if(Mathf.Abs(jod.spinDelta[i]) > 0) {
					//Debug.Log("EXAMPLE SPIN "+i);
					spinnerAngle[i] += jod.spinDelta[i] * degPerTick[i] * spinnerModifer[i];
					//Debug.Log("Spin "+i+" : "+jod.spinDelta[i]);
					spinnerAngle[i] = Mathf.Repeat(spinnerAngle[i], 360);
					Vector3 rot = spinners[i].transform.rotation.eulerAngles;
					rot.y = initAngle[i] + spinnerAngle[i];
					spinners[i].transform.rotation = Quaternion.Euler(rot);
				}
				//hit
				if(jod.hit[i]) {
					if (!drumLock[i]) {
						//Debug.Log("EXAMPLE HIT "+i);
						cannon[i].Shoot ();
						if (evil[i]) {
							villainController.VillainTap (spinnerAngle[i]);
						}
					}
				}
			}
		}
		else {
			for(int i=0; i<4; i++) {
				//spin
				if(Mathf.Abs(jodkey.spinDelta[i]) > 0) {
					//Debug.Log("EXAMPLE SPIN "+i);
					spinnerAngle[i] += jodkey.spinDelta[i] * degPerTick[i] * spinnerModifer[i];
					//Debug.Log("Spin "+i+" : "+jodkey.spinDelta[i]);
					spinnerAngle[i] = Mathf.Repeat(spinnerAngle[i], 360);
					Vector3 rot = spinners[i].transform.rotation.eulerAngles;
					rot.y = initAngle[i] + spinnerAngle[i];
					spinners[i].transform.rotation = Quaternion.Euler(rot);
				}
				//hit
				if(jodkey.hit[i]) {
					if (!drumLock[i]) {
						//Debug.Log("EXAMPLE HIT "+i);
						cannon[i].Shoot ();
						if (evil[i]) {
							villainController.VillainTap (spinnerAngle[i]);
						}
					}
					else {
						
					}
				}
			}
		}
		
		if(Input.GetKeyUp(KeyCode.Escape)){
			Application.Quit();
		}
	}
	
	public void SpinHandler(int controllerID, int delta) {
		//Debug.Log("SPIN EVENT "+(controllerID-1));
	}
	
	public void HitHandler(int controllerID) {
		//Debug.Log("HIT EVENT "+(controllerID-1));
	}

	public void SetDrumLock (int index, bool value) {
		drumLock[index] = value;
	}

	public void ChangeSpin (int index, float value) {
		spinnerModifer[index] = value;
	}

	public void SetCannonLock (int index, bool value) {
		cannon[index].cannonLock = value;
	}

	void LockAllDrums () {
		for (int i = 0; i < 4; i++) {
			drumLock[i] = true;
		}
	}

	void UnlockAllDrums () {
		for (int i = 0; i < 4; i++) {
			drumLock[i] = false;
		}
	}

	void ResetAllSpinners () {
		for (int i = 0; i < 4; i++) {
			spinnerModifer[i] = 1f;
		}
	}

	void UnlockAllCannons () {
		for (int i = 0; i < 4; i++) {
			cannon[i].cannonLock = false;
		}
	}
}
