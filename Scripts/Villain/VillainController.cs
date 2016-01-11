using UnityEngine;
using System.Collections;
using DG.Tweening;

public class VillainController : MonoBehaviour {
	// Script references
	SingleLauncher launcher;
	JamoDrum jod;
	JamoDrumKeyVersion jodkey;
	// platform spin
	TracingPanel platform;
	// chain
	SpawnMarbles spawn;

	ParticleSystem smokeParticle;
	SoundManagerScript soundManager;
	GameObject[] freezeEffects;
	// 0 ~ inactive, 1 ~ active
	GameObject[] skillPads;


	public int villainIndex;
	public float optionHalfAngle;
	public float villainMarbleCoolDown;
	public Cannon cannon;
	public int skillThreshold;

	public int numColor;
	public string[] colorTags;
	public int numSkill;
	// skill choosing, shoot, spin platform, accelerate
	public float[] stateResetTime;
	public int numEscapeFreezeTap;
	public int numClearFogDelta;
	int heroTapSum;
	int heroSpinSum;

	private float[] colorAngle;
	private float colorSegAngle;
	private float[] skillAngle;
	private float skillSegAngle;
	public int spawnCount;
	private int spawnCountThresh;
	private System.Action skillAction;
	private System.Action stateStartAction;
	private System.Action stateEndAction;
	// "Magic" is the skill which doesn't cause state change
	// freeze, smoke
	private event System.Action magicStartAction;
	private event System.Action magicEndAction;
	// 0 = normal, 1 = choose skill, > 1 = skills
	// skills:
	// 2 = shoot
	// 3 = freeze
	// 4 = smoke
	// 5 = spin platform
	// 6 = accelerate chain
	public int state;
	private float stateTimer;

	void Awake () {
		jod = GetComponent<JamoDrum> ();
		jodkey = GetComponent<JamoDrumKeyVersion> ();
		launcher = GetComponent<SingleLauncher> ();
		platform = GameObject.Find("TracingPanel")
			.GetComponent<TracingPanel>();
		spawn = GameObject.FindGameObjectWithTag("SpawnController")
			.GetComponent<SpawnMarbles>();
		soundManager = GameObject.Find ("SoundManager")
			.GetComponent<SoundManagerScript>();

		if (launcher.key) {
			jodkey.AddHitEvent(HitHandler);
			jodkey.AddSpinEvent(SpinHandler);
		}
		else {
			jod.AddHitEvent(HitHandler);
			jod.AddSpinEvent(SpinHandler);
		}

		stateStartAction = () => {
			stateTimer = 0f;
			heroTapSum = 0;
			heroSpinSum = 0;
		};
	}


	// Use this for initialization
	void Start () {
		spawnCount = 0;
		smokeParticle = GameObject.Find ("Smoke Particle System")
			.GetComponent<ParticleSystem>();
		state = 0;
		skillAction = null;
		stateResetTime[0] = Mathf.Infinity;

		skillAngle = new float[numSkill];
		skillSegAngle = optionHalfAngle * 2 / numSkill;
		for (int i = 0; i < numSkill; i++) {
			skillAngle[i] = -optionHalfAngle + skillSegAngle * i;
			skillAngle[i] = Mathf.Repeat(skillAngle[i], 360f);
		}

		colorAngle = new float[numColor];
		colorSegAngle = optionHalfAngle * 2 / numColor;
		for (int i = 0; i < numColor; i++) {
			colorAngle[i] = -optionHalfAngle + colorSegAngle * i;
			colorAngle[i] = Mathf.Repeat(colorAngle[i], 360f);
		}

		freezeEffects = new GameObject[4];
		for (int i = 0; i < 4; i++) {
			freezeEffects[i] = GameObject.Find ("ToolTip" + (i+1) + "/Freeze");
		}

		skillPads = new GameObject[2];
		skillPads[0] = GameObject.Find ("SkillPadInactive");
		skillPads[1] = GameObject.Find ("SkillPadActive");

		spawnCount = 0;
		heroTapSum = 0;
		heroSpinSum = 0;
		spawnCountThresh = 5;

	}
	
	// Update is called once per frame
	void Update () {
		TryResetState ();
		KeyBoardBackdoor ();
	}

	void TryResetState () {
		if (state >= stateResetTime.Length) return;
		stateTimer += Time.deltaTime;
		if (stateTimer > stateResetTime[state]) {
			Debug.Log ("State Time Up. Return to state 0 from state " + state);
			ResetState ();
		}
		else {
			switch(state) {
			case 1:
				if (heroSpinSum > numClearFogDelta) {
					Debug.Log ("Clear the Fog!");
					ResetState ();
				}
				break;
			}
		}
	}

	void ResetState () {
		state = 0;
		Debug.Log ("Back to normal");
		if (stateEndAction != null)
			stateEndAction();
	}

	public void VillainTap (float angle) {
		switch (state) {
		// normal
		case 0:
			if (spawnCount == spawnCountThresh + 1) {
				ActivateSkill();
			}
			ChooseSkill (angle);
			break;
		// skill
		case 1:
			//ChooseSkill (angle);
			break;
		// shoot
		case 2:
			break;
		// freeze
		case 3:
			ChooseColor (angle);
			break;
		// smoke
		case 4:
			ChooseColor (angle);
			break;
		case 5:
		case 6:
			break;
		}
	}

	void ChooseColor (float angle) {
		// check the selected color
		if (angle > optionHalfAngle && angle < 360f - optionHalfAngle) {
			// Villain choose to use skill
			state = 1;
			if (stateStartAction != null) 
				stateStartAction();
			Debug.Log ("Skill selection");
		}
		else {
			for (int i = 0; i < numColor; i++) {
				if ((angle > colorAngle[i] && angle < colorAngle[i] + colorSegAngle) ||
				    (i == numColor / 2 && angle < colorSegAngle * 0.5f)){
					// Add a marble of certain color
					if (i < numColor - 1) {
						Debug.Log ("Add Marble with Color " + colorTags[i]);
						spawn.ChangeNextColor(colorTags[i]);
					}
					switch (i) {
					}
					if (i == numColor - 1) {
						state = 1;
						if (stateStartAction != null) 
							stateStartAction();
						Debug.Log ("Skill selection");
					}
					break;
				}
			}
		}
	}

	void ActivateSkill () {
		skillPads[0].GetComponent<SpriteRenderer>().DOFade(0f, 0.5f);
		skillPads[1].GetComponent<SpriteRenderer>().DOFade(1f, 0.5f);
	}

	void DeactivateSkill () {
		skillPads[0].GetComponent<SpriteRenderer>().DOFade(1f, 0.5f);
		skillPads[1].GetComponent<SpriteRenderer>().DOFade(0f, 0.5f);
	}

	void ChooseSkill (float angle) {
		if (angle > optionHalfAngle && angle < 360f - optionHalfAngle && spawnCount > spawnCountThresh) {
			// Villain choose to not use skill
			state = 1;
			spawnCount = 0;
			DeactivateSkill();
			StartSmoke();
			spawn.ChangeNextColor("CounterMagic");
		}
		else {
			/*
			for (int i = 0; i < numSkill; i++) {
				if ((angle > skillAngle[i] && angle < skillAngle[i] + skillSegAngle) ||
				    (i == numSkill / 2 && angle < skillSegAngle * 0.5f)){
					state = i + 2;
					switch (i) {
					case 0:
						StartShoot();
						break;
					case 1:
						StartFreeze();
						break;
					case 2:
						StartSmoke();
						break;
					case 3:
						StartSpinPlatform();
						break;
					case 4:
						StartAccelerate();
						break;
					}
					if (i == numSkill - 1) {
						ResetState();
					}
					// Put a countercharge ball

					break;
				}
			}
			*/
		}
	}

	void StartShoot () {
		cannon.cannonLock = false;
		cannon.marbleCoolDown = villainMarbleCoolDown;
		stateEndAction += EndShoot;
		if (stateStartAction != null) 
			stateStartAction();
	}

	void EndShoot () {
		// e.g change UI
		cannon.cannonLock = true;
		stateEndAction -= EndShoot;
	}

	void StartFreeze () {
		Debug.Log ("Magic::Freeze!");
		for (int i = 0; i < 4; i++) {
			if (i == villainIndex)
				continue;
			launcher.ChangeSpin(i, 0f);
			launcher.SetCannonLock(i, true);
			freezeEffects[i].GetComponent<SpriteRenderer>().DOFade(1f, 0.5f);
			freezeEffects[i].transform.GetChild(0).gameObject.GetComponent<ParticleSystem>().Play();
		}
		soundManager.PlayFreezeEffect ();
		soundManager.PlayFreezeDialogue ();
		if (stateStartAction != null) 
			stateStartAction();
		stateEndAction += EndFreeze;
	}

	void EndFreeze () {
		for (int i = 0; i < 4; i++) {
			if (i == villainIndex)
				continue;
			launcher.ChangeSpin(i, 1f);
			launcher.SetCannonLock(i, false);
			freezeEffects[i].GetComponent<SpriteRenderer>().DOFade(0f, 0.5f);
			//freezeEffects[i].transform.GetChild(0).gameObject.GetComponent<ParticleSystem>().Stop();
		}
		stateEndAction -= EndFreeze;
		if (magicEndAction != null)
			magicEndAction();
	}

	void StartSmoke () {
		Debug.Log ("Magic::Smoke!");
		smokeParticle.Play();
		soundManager.PlaySmokeDialogue ();
		stateEndAction += EndSmoke;
		if (stateStartAction != null) 
			stateStartAction();
		if (magicStartAction != null)
			magicStartAction();
	}

	void EndSmoke () {
		smokeParticle.Stop();
		stateEndAction -= EndSmoke;
		if (magicEndAction != null)
			magicEndAction();

	}


	void StartSpinPlatform () {
		Debug.Log ("Skill::SpinPlatform!");
		if (stateStartAction != null) 
			stateStartAction();
		stateEndAction += EndSpinPlatform;
	}

	void EndSpinPlatform () {

		stateEndAction -= EndSpinPlatform;
	}

	void StartAccelerate () {
		Debug.Log ("Skill::Accelerate!");
		stateEndAction += EndAccelerate;
		if (stateStartAction != null) 
			stateStartAction();
	}

	void EndAccelerate () {

		stateEndAction -= EndAccelerate;
	}

	public void RegisterMagicStartAction (System.Action func) {
		magicStartAction += func;
	}

	public void RegisterMagicEndAction (System.Action func) {
		magicEndAction += func;
	}

	public void SpinHandler(int controllerID, int delta) {
		if (controllerID - 1 == villainIndex) {
			switch (state) {
			case 5:
				platform.SpinPlatform(delta);
				break;
			case 6:
				// Change ball chain speed
				break;
			}
		}
		else {
			heroSpinSum += (delta < 0) ? -delta : delta;
		}
	}
	
	public void HitHandler(int controllerID) {
		if (controllerID - 1 != villainIndex) {
			heroTapSum++;
		}
	}

	void KeyBoardBackdoor () {
		if (Input.GetKeyDown(KeyCode.P)) {
			if (!smokeParticle.isPlaying) {

				smokeParticle.Play();
			}
			else {
				smokeParticle.Stop();
			}
		}
		if (Input.GetKeyDown(KeyCode.E)) {
			
			smokeParticle.enableEmission = !smokeParticle.enableEmission;
		}
	}

}
