using UnityEngine;
using System.Collections;

public class Cannon : MonoBehaviour {
	SoundManagerScript soundManager;

	public bool cannonLock;
	public bool villain;
	private bool marbleReady;
	public float marbleCoolDown;
	private float shootTime;
	private Transform shotMarble;
	private Transform marbleLibrary;
	private float marbleSpeed;
	private int numNonBulletMarble;

	public Transform ballHint;

	void Awake () {
		soundManager = GameObject.Find ("SoundManager")
			.GetComponent<SoundManagerScript> ();
	}

	// Use this for initialization
	void Start () {
		if (marbleCoolDown == 0f) marbleCoolDown = 1f;
		marbleReady = false;
		shootTime = 0.5f;
		shotMarble = GameObject.Find ("ShotMarble").transform;
		marbleLibrary = GameObject.Find ("MarbleLibrary").transform;
		marbleSpeed = 65f;
		numNonBulletMarble = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (marbleReady == false && cannonLock == false) {
			Reload ();
		}
		else if (marbleReady == true && cannonLock == true) {
			Unload ();
		}
	}

	void Reload () {
		if (Time.time > shootTime + marbleCoolDown) {
			GameObject bulletMarble;
			if (!villain) {
				int idx = Random.Range (0, 4);
				bulletMarble = Instantiate(marbleLibrary.GetChild(idx).gameObject);
				//bulletMarble.transform.localScale = 2f * Vector3.one;
				bulletMarble.transform.parent = transform;
				bulletMarble.transform.localPosition = Vector3.zero;
				bulletMarble.transform.localRotation = Quaternion.identity;
				bulletMarble.GetComponent<Rigidbody>().isKinematic = true;

				// Set Hint
				GameObject hintColor = Instantiate(bulletMarble);
				hintColor.transform.parent = ballHint;
				hintColor.transform.localPosition = Vector3.zero;
				hintColor.transform.localRotation = Quaternion.identity;
				hintColor.transform.localScale = Vector3.one;
			}
			else {
				bulletMarble = Instantiate(marbleLibrary.GetChild(4).gameObject);
				//bulletMarble.transform.localScale = 2f * Vector3.one;
				bulletMarble.transform.parent = transform;
				bulletMarble.transform.localPosition = Vector3.zero;
				bulletMarble.transform.localRotation = Quaternion.identity;
				bulletMarble.GetComponent<Rigidbody>().isKinematic = true;
				bulletMarble.GetComponent<ChangeColorBallScriptCanonball> ().ballsToBeChanged = 4;
			}
			marbleReady = true;
		}
	}

	void Unload () {
		marbleReady = false;
		Destroy(transform.GetChild(0).gameObject);
		Destroy(ballHint.GetChild(0).gameObject);
	}

	public void Shoot () {
		if (marbleReady == true && cannonLock == false) {
			marbleReady = false;
			shootTime = Time.time;
			GameObject bulletMarble = transform.GetChild(0).gameObject;
			bulletMarble.transform.parent = shotMarble;
			bulletMarble.GetComponent<Rigidbody>().isKinematic = false;
			bulletMarble.GetComponent<Rigidbody>().AddForce(bulletMarble.transform.forward * marbleSpeed, ForceMode.VelocityChange);
			soundManager.PlayShootAudio();
			if (!villain) {
				bulletMarble.AddComponent<BulletBall>();
				Destroy(ballHint.GetChild(0).gameObject);
			}
		}
	}

	public void ChangeBulletSpeed (float speed) {
		marbleSpeed = speed;
	}
}
