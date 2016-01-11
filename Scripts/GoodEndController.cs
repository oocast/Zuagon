using UnityEngine;
using System.Collections;

public class GoodEndController : MonoBehaviour {
	GameController gameController;
	public Sprite[] endScene;

	bool playLock = true;
	int sceneIdx = 0;

	void Awake () {
		gameController = GameObject.Find ("GameController")
			.GetComponent<GameController>();
		gameController = GameObject.Find ("GameController")
			.GetComponent<GameController>();
		gameController.OnHeroWin += () => {
			playLock = false;
		};
	}

	// Use this for initialization
	void Start () {
		endScene = new Sprite[104];
		for (int i = 0; i < 104; i++) {
			endScene[i] = transform.GetChild(i).GetComponent<SpriteRenderer>().sprite;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (playLock == false && sceneIdx < 104) {
			PlayScenes ();
		}
	}

	void PlayScenes () {
		GetComponent<SpriteRenderer>().sprite = endScene[sceneIdx++];
	}
}
