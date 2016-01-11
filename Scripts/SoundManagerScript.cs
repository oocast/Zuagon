using UnityEngine;
using System.Collections;

public class SoundManagerScript : MonoBehaviour {
	ArrayList bc;
	GameObject sc,m;
	AudioSource aud1,aud2,aud3;
	GameController gameController;
	public GameObject soundPlayerUnit;

	public AudioClip game_bgm, win_bgm, lose_bgm, ball_rolling, 
	combo_aud, freezeEffect, freezeDialogue, 
	smokeDialogue, fireEffect, shootEffect,
	changeColorEffect, heartBeat, dragonGrowling;

	public bool comboSound=false;
	GameObject jod;
	VillainController vc;
	// 0 ~ intro, 1 ~ game, 2 ~ H win, 3 ~ V win
	int loadedLevel;
	// Use this for initialization

	void Awake()
	{
		DontDestroyOnLoad (gameObject);
		gameController = GameObject.Find ("GameController")
			.GetComponent<GameController> ();

		gameController.OnGameStart += () => loadedLevel = 1;
		gameController.OnHeroWin += () => loadedLevel = 2;
		gameController.OnVillainWin += () => loadedLevel = 3;

	}
	void Start () 
	{
		jod = GameObject.Find ("JamODrum");
		sc = GameObject.Find ("SpawnController");
		bc = sc.GetComponent<SpawnMarbles> ().ballChain;
		AudioSource[] aud = GetComponents<AudioSource> ();
		vc = jod.GetComponent<VillainController> ();
		aud1 = aud [0];
		aud2 = aud [1];
		aud3 = aud [2];
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (loadedLevel == 1 || loadedLevel == 0)
		{
			aud1.clip = game_bgm;
			aud1.volume=1.0f;
			if (!aud1.isPlaying) {
				aud1.Play ();
			}
		}
		if (loadedLevel == 2)
		{
			aud1.clip=win_bgm;
			aud1.volume=1.0f;
			if (!aud1.isPlaying) {
				aud1.Play();
			}
		}
		if (loadedLevel == 3)
		{
			aud1.clip=lose_bgm;
			aud1.volume=1.0f;
			if (!aud1.isPlaying) {
				aud1.Play ();
			}
		}
		//SetGame index scene as 1
		//SetWinIndex as 2
		//SetLose index as 3
		if (bc.Count > 0 && loadedLevel == 1)
		{
			aud2.clip=ball_rolling;
			aud2.volume=0.8f;
			if (!aud2.isPlaying)
				aud2.Play ();
		}
		/*
		if (vc.freezeFlag) 
		{
			aud3.clip=freezeDialogue;
			aud3.volume=0.5f;
			aud3.Play();
		}
		if (vc.smokeFlag)
		{
			aud3.clip=smokeDialogue;
			aud3.volume=0.5f;
			aud3.Play ();
		}*/
	}

	public void PlayFreezeDialogue () {
		aud3.clip=freezeDialogue;
		aud3.volume=0.5f;
		aud3.Play();
	}

	public void PlaySmokeDialogue () {
		aud3.clip=smokeDialogue;
		aud3.volume=0.5f;
		aud3.Play();
	}

	public void PlayFreezeEffect () {
		GameObject soundUnit = Instantiate(soundPlayerUnit);
		AudioSource sound = soundUnit.GetComponent<AudioSource>();
		sound.clip = freezeEffect;
		sound.volume = 0.5f;
		sound.Play();
		Destroy(soundUnit, freezeEffect.length);
	}

	public void PlayComboAudio () {
		GameObject soundUnit = Instantiate(soundPlayerUnit);
		AudioSource sound = soundUnit.GetComponent<AudioSource>();
		sound.clip = combo_aud;
		sound.volume = 0.5f;
		sound.Play();
		Destroy(soundUnit, combo_aud.length);
	} 

	public void PlayShootAudio () {
		GameObject soundUnit = Instantiate(soundPlayerUnit);
		AudioSource sound = soundUnit.GetComponent<AudioSource>();
		sound.clip = shootEffect;
		sound.volume = 0.5f;
		sound.Play();
		Destroy(soundUnit, sound.clip.length);
	}

	public void PlayChangeColorAudio () {
		GameObject soundUnit = Instantiate(soundPlayerUnit);
		AudioSource sound = soundUnit.GetComponent<AudioSource>();
		sound.clip = changeColorEffect;
		sound.volume = 0.5f;
		sound.Play();
		Destroy(soundUnit, sound.clip.length);
	}

	public void PlayHeartBeat () {
		// only happen in gameplay
		if (loadedLevel == 1) {
			GameObject soundUnit = Instantiate(soundPlayerUnit);
			AudioSource sound = soundUnit.GetComponent<AudioSource>();
			sound.clip = heartBeat;
			sound.volume = 1f;
			sound.Play();
			Destroy(soundUnit, sound.clip.length);
		}
	}

	public void PlayDragonGrowling () {
		GameObject soundUnit = Instantiate(soundPlayerUnit);
		AudioSource sound = soundUnit.GetComponent<AudioSource>();
		sound.clip = dragonGrowling;
		sound.volume = 1f;
		sound.Play();
		Destroy(soundUnit, sound.clip.length);
	}
}
