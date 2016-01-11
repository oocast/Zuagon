using UnityEngine;
using System.Collections;
using DG.Tweening;

public class DragonHeart : MonoBehaviour {
	GameController gameController;
	SoundManagerScript soundManager;
	ArrayList balls;
	bool beating;

	// Use this for initialization
	void Start () {
		beating = false;
		soundManager = GameObject.Find ("SoundManager")
			.GetComponent<SoundManagerScript> ();
		balls = GameObject.Find ("SpawnController")
			.GetComponent<SpawnMarbles> ().ballChain;
		gameController = GameObject.Find ("GameController")
			.GetComponent<GameController> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (balls.Count > 0) {
			GameObject ball = balls[0] as GameObject;
			float percent = ball.GetComponent<BallChainMovement>().currPathPercent;
			if (percent > 0.75f) {
				StartBeat();
			}
			else {
				ResetHeart();
			}

			if (percent > 0.99f) {
				gameController.VillainWin();
			}
		}

	}

	IEnumerator HeartBeat () {
		while (beating) {
			soundManager.PlayHeartBeat();
			transform.DOScale(1.1f, 0.3f);
			yield return new WaitForSeconds(0.4f);
			transform.DOScale(0.8f, 0.3f);
			yield return new WaitForSeconds(0.3f);
			yield return new WaitForSeconds(0.5f);
			transform.DOScale(1.1f, 0.3f);
			yield return new WaitForSeconds(0.4f);
			transform.DOScale(0.8f, 0.3f);
			yield return new WaitForSeconds(0.3f);
			yield return new WaitForSeconds(0.5f);
		}
	}

	void StartBeat () {
		if (beating == false) {
			beating = true;
			StartCoroutine("HeartBeat");
		}
	}

	void ResetHeart () {
		if (beating == true) {
			beating = false;
			StopCoroutine("HeartBeat");
			transform.DOScale(0.8f, 0.3f);
		}
	}
}
