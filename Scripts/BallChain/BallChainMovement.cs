using UnityEngine;
using System.Collections;
using DG.Tweening;

public class BallChainMovement : MonoBehaviour {
	GameController gameController;

	float percentPerSecond=0.01f;
	float percentPerSecondBack;
	public float currPathPercent = 0f;
	GameObject sc;
	ArrayList bc;
	int collided_index;
	int ind=0,i=0;
	GameObject endPoint;
	Vector3[] wayPoints;
	public int wayPointIndex;
	Tween tween;
	VillainController villain;

	public bool moveFlag = true;
	public bool moveLock;

	void Awake () {
		villain = GameObject.Find ("JamODrum")
			.GetComponent<VillainController>();
		
		villain.RegisterMagicStartAction (() => {
			percentPerSecond *= 0.02f;
		});
		villain.RegisterMagicEndAction (() => {
			percentPerSecond *= 50f;
		});

		gameController = GameObject.Find ("GameController")
			.GetComponent<GameController>();
		gameController.OnVillainWin += () => {
			moveLock = true;
		};
		gameController.OnHeroWin += () => {
			moveLock = true;
		};
	}
	// Use this for initialization
	void Start ()
	{
		moveLock = false;
		sc = GameObject.Find ("SpawnController");
		bc = sc.GetComponent<SpawnMarbles> ().ballChain;
		endPoint=GameObject.Find ("EndPoint");
		wayPoints = sc.GetComponent<SpawnMarbles> ().waypointArray;
		wayPointIndex = 0;

		tween = GameObject.Find ("PathRouter")
			.GetComponent<PathRouter>().tween;


	}

	public void SetPPS (float forward, float backward) {
		percentPerSecond = forward;
		percentPerSecondBack = backward;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (moveFlag) {
			currPathPercent += percentPerSecond * Time.deltaTime;
		}
		else {
			currPathPercent -= percentPerSecondBack * Time.deltaTime;
			// TODO: check collide condition
		}

		if (!moveLock) {
			Vector3 pos = tween.PathGetPoint(currPathPercent);
			transform.position = pos;
		}
		//ChangeNavigation ();
	}

	void OnCollisionEnter(Collision col)
	{
		/*
		foreach (GameObject b in bc) {
			ind++;
			if (col.gameObject.name == b.gameObject.name) {
				//Debug.Log (b.name + " collided " + name);
				collided_index = ind;
				b.GetComponent<BallChainMovement>().moveFlag=true;
				//MoveInFront ();
			}
		}*/
		if(col.gameObject.name==endPoint.gameObject.name)
		{
			Debug.Log ("GameOver");

		}
	}

	

	void MoveInFront()
	{
		foreach (GameObject ba in bc) 
		{
			i++;
			if(i < ind)
			{
				ba.GetComponent<BallChainMovement>().moveFlag=true;

			}
			else {
				break;
			}
		}
	}

	public void PushFoward (float percentageDelta) {
		currPathPercent += percentageDelta;
	}

}

