using UnityEngine;
using System.Collections;

public class EndPoint : MonoBehaviour {
	ArrayList balls;
	GameController controller;

	void Awake () {
		controller = GameObject.Find ("GameController")
			.GetComponent<GameController> ();
		balls = GameObject.Find ("SpawnController")
			.GetComponent<SpawnMarbles> ().ballChain;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (balls.Count > 0) {
			GameObject ball = balls[0] as GameObject;
			//float percent = ball.GetComponent<BallChainMovement>();
		}
	}

	/*
	void OnTriggerEnter (Collider other) {
		if (other.gameObject.GetComponent<BallChainMovement> () != null) {
			controller.VillainWin ();
		}
	}*/
}
