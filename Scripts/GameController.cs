using UnityEngine;
using System.Collections;
using DG.Tweening;

public class GameController : MonoBehaviour {
	bool gameOver;
	public event System.Action OnIntroEnd;
	public event System.Action OnGameStart;
	public event System.Action OnVillainWin;
	public event System.Action OnHeroWin;

	void Awake () {
		Cursor.visible = false;
		//DOTween.SetTweensCapacity(1, 0);
	}

	// Use this for initialization
	void Start () {
		gameOver = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.M)) {
			HeroWin();
		}
		if (Input.GetKeyDown(KeyCode.N)) {
			VillainWin();
		}
	}

	public void GameStart () {
		if (!gameOver) {
			if (OnGameStart != null) {
				OnGameStart();
			}
		}
	}

	public void VillainWin () {
		if (!gameOver) {
			gameOver = true;
			if (OnVillainWin != null)
				OnVillainWin();
		}
	}

	public void HeroWin () {
		if (!gameOver) {
			gameOver = true;
			if (OnHeroWin != null)
				OnHeroWin();
		}
	}
}
