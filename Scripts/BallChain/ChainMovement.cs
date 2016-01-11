using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MovementRegistrationEntry {
	public GameObject bulletBall;
	public GameObject chainBall;
	public int registerFrame;
	public float proceedTime;
	public int repeatChainBall;
}

public class CheckColorTableEntry {
	public int indexReference;
	public bool covered;
}

// Control the movement from higher level view
public class ChainMovement : MonoBehaviour {
	ExplosionController explosion;
	SoundManagerScript soundManager;
	GameObject ballsObject;
	SpawnMarbles spawn;
	ArrayList balls;
	VillainHealth health;
	public float insetionSmoothTime;
	float diameterPercentPerSecond;
	float diameterPercent;
	Queue <MovementRegistrationEntry> insertionQueue;
	Dictionary <GameObject, int> hitChainBallTable;
	Dictionary <GameObject, CheckColorTableEntry> checkColorTable;
	HashSet <GameObject> brokenChainNodeSet;
	bool brokenChainNodeDirty;

	public float checkColorDelay;
	float checkColorRegisterTime;
	int combo;

	void Awake () {
		spawn = GetComponent<SpawnMarbles> ();
		health = GameObject.Find ("VillainHealth")
			.GetComponent<VillainHealth>();
		soundManager = GameObject.Find ("SoundManager")
			.GetComponent<SoundManagerScript>();
		explosion = GameObject.Find ("ExplosionController")
			.GetComponent<ExplosionController>();
	}

	// Use this for initialization
	void Start () {
		brokenChainNodeDirty = false;
		balls = spawn.ballChain;
		ballsObject = GameObject.Find ("Balls");
		insertionQueue = new Queue <MovementRegistrationEntry>();
		hitChainBallTable = new Dictionary <GameObject, int>();
		checkColorTable = new Dictionary <GameObject, CheckColorTableEntry>();
		brokenChainNodeSet = new HashSet <GameObject>();
		combo = 0;
	}
	
	// Update is called once per frame
	void Update () {
		UpdateInsertion ();
		CheckColor ();
		ControlBrokenChain ();
	}

	public void SetDiameterPPS (float diameterPercentage) {
		diameterPercent = diameterPercentage;
		diameterPercentPerSecond = diameterPercentage / insetionSmoothTime;
	}

	public void RegisterInsertion (MovementRegistrationEntry data) {
		// TODO: will have mismatch when 2 balls hit same chain ball
		if (balls.Contains (data.chainBall)) {
			if (hitChainBallTable.ContainsKey(data.chainBall)) {
				data.repeatChainBall = hitChainBallTable[data.chainBall] + 1;
				hitChainBallTable[data.chainBall] = data.repeatChainBall;
			}
			else {
				data.repeatChainBall = 1;
				hitChainBallTable.Add (data.chainBall, 1);
			}

			if (brokenChainNodeSet.Contains(data.chainBall)) {
				brokenChainNodeSet.Remove(data.chainBall);
				brokenChainNodeSet.Add (data.bulletBall);
				brokenChainNodeDirty = true;
			}
			insertionQueue.Enqueue (data);
		}
		else {
			Debug.Log ("Ball Hit Chain Ball Being Deleted");
		}
	}

	public void RegisterRemove () {
	}

	void UpdateInsertion () {
		// Move chain and Add bullet to chain
		int dequeSize = 0;
		foreach (MovementRegistrationEntry entry in insertionQueue) {
			if (Time.frameCount > entry.registerFrame) {
				// move all the leading balls
				foreach (GameObject ball in balls) {
					float deltaTime = Time.deltaTime;
					if (entry.proceedTime + deltaTime > insetionSmoothTime) {
						deltaTime = insetionSmoothTime - entry.proceedTime;
					}
					ball.GetComponent<BallChainMovement>()
						.PushFoward (diameterPercentPerSecond * deltaTime);
					if (ball == entry.chainBall) {
						break;
					}
				}

				entry.proceedTime += Time.deltaTime;

				// TODO: Fix the bug of position mismatch when multiple bullets
				// hit same chainBall
				if (entry.proceedTime > insetionSmoothTime) {
					dequeSize++;
					int idx = balls.IndexOf (entry.chainBall);
					if (entry.repeatChainBall > 1) {
						Debug.Log ("Same Time");
					}
					if (idx + entry.repeatChainBall >= balls.Count) {
						Debug.LogWarning ("Insertion idx Larger than chain size");
					}
					balls.Insert(idx + entry.repeatChainBall, entry.bulletBall);
					// Synchronizing the remove
					CheckColorTableEntry setEntry = new CheckColorTableEntry();
					setEntry.indexReference = idx + entry.repeatChainBall;
					setEntry.covered = false;
					if (!checkColorTable.ContainsKey(entry.bulletBall)) {
						checkColorTable.Add(entry.bulletBall, setEntry);
					}
					else {
						Debug.LogError ("Same bulletBall registered twice " + entry.bulletBall);
					}
					combo = 0;
					checkColorRegisterTime = Time.time;
					//Debug.Log (entry.bulletBall.name + " " + entry.chainBall.name + " " + (idx));
					entry.bulletBall.GetComponent<Rigidbody> ().isKinematic = false;
					entry.bulletBall.transform.parent = ballsObject.transform;
					BallChainMovement ballScript = entry.bulletBall
						.AddComponent <BallChainMovement>();
					ballScript.currPathPercent = entry.chainBall.GetComponent<BallChainMovement> ().currPathPercent
						- (entry.repeatChainBall) * diameterPercent;
					spawn.SetPPSForBallScript (ballScript);

				}
			}
			else {
				// as it is enqued in time order
				break;
			}
		}

		DequeHeadElements (dequeSize);

		// Move bullet
	}

	void DequeHeadElements (int dequeSize) {
		for (int i = 0; i < dequeSize; i++) {
			MovementRegistrationEntry entry = insertionQueue.Dequeue ();
			int chainBallHitCount = hitChainBallTable[entry.chainBall] - 1;
			if (hitChainBallTable.ContainsKey(entry.chainBall)) {
				if (hitChainBallTable[entry.chainBall] > 1) {
					hitChainBallTable[entry.chainBall] = chainBallHitCount;
				}
				else if (hitChainBallTable[entry.chainBall] == 1) {
					hitChainBallTable.Remove(entry.chainBall);
				}
			}
			else {
				Debug.LogWarning ("Data Mismatch, table entry missing");
			}
		}
	}

	void CheckColor () {
		if (insertionQueue.Count == 0 && checkColorTable.Count > 0 && Time.time > checkColorRegisterTime + checkColorDelay) {
			foreach (KeyValuePair<GameObject, CheckColorTableEntry> kvpair in checkColorTable) {
				if (kvpair.Value.covered == false) {
					int idx = balls.IndexOf(kvpair.Key);
					if (idx < 0) {
						Debug.LogWarning ("Ball not in Chain " + kvpair.Key.name + "ID: " + kvpair.Key.GetInstanceID());
						// continue seems to avoid the bug
						// value.cover not work well
						continue;
					}
					int prevCount = 0;
					int nextCount = 0;
					int counterMagicCount = 0;

					if (!brokenChainNodeSet.Contains(kvpair.Key)) {
						foreach (GameObject ball in balls.GetRange(idx + 1, balls.Count - (idx + 1))) {
							if (ball.tag == kvpair.Key.tag || ball.tag == "CounterMagic") {
								nextCount++;
								if (checkColorTable.ContainsKey(kvpair.Key)) {
									kvpair.Value.covered = true;
								}
								if (brokenChainNodeSet.Contains(ball)) {
									break;
								}
							}
							else {
								break;
							}
						}
					}

					ArrayList reverseBalls = new ArrayList(balls.GetRange(0, idx));
					reverseBalls.Reverse();
					foreach (GameObject ball in reverseBalls) {
						if ((ball.tag == kvpair.Key.tag || ball.tag == "CounterMagic")
						    && (!brokenChainNodeSet.Contains(ball))) {
							prevCount++;
							if (checkColorTable.ContainsKey(kvpair.Key)) {
								Debug.Log (kvpair.Key.name + " ID: " + kvpair.Key.GetInstanceID() + " is covered in combo");
								kvpair.Value.covered = true;
							}
							if (ball.tag == "CounterMagic") {
								counterMagicCount++;
							}
						}
						else {
							break;
						}
					}

					if (prevCount + nextCount + 1 > 2) {
						GameObject head, tail;
						head = (GameObject)balls[idx - prevCount];
						tail = (GameObject)balls[idx + nextCount];
						combo++;
						health.GetDamage (prevCount + nextCount + 1 - counterMagicCount, counterMagicCount, combo);
						soundManager.PlayComboAudio();

						if (brokenChainNodeSet.Contains(head)) {
							Debug.LogError ("Chain with same color crosses gap");
						}
						if (brokenChainNodeSet.Contains(tail)) {
							brokenChainNodeSet.Remove(tail);
						}
						if (idx - prevCount > 0) {
							GameObject headHead = (GameObject)balls[idx - prevCount - 1];
							if (!brokenChainNodeSet.Contains(headHead)) {
								brokenChainNodeSet.Add (headHead);
								brokenChainNodeDirty = true;
							}
						}

						foreach (GameObject ball in balls.GetRange(idx - prevCount, prevCount + nextCount + 1)){
							explosion.CreateExplosion(ball.transform.position);
							Destroy (ball);
						}
						balls.RemoveRange(idx - prevCount, prevCount + nextCount + 1);

					}

				}
			}
			checkColorTable.Clear();
		}
	}

	void ControlBrokenChain () {
		if (brokenChainNodeDirty) {
			//brokenChainNodeDirty = false;
			if (brokenChainNodeSet.Count > 0) {
				int maxIdx = -1;
				int max2ndIdx = -1;
				GameObject tailNode = null;
				foreach (GameObject brokenNode in brokenChainNodeSet) {
					int idx = balls.IndexOf(brokenNode);
					if (idx > maxIdx) {
						tailNode = brokenNode;
						max2ndIdx = maxIdx;
						maxIdx = idx;
					}
				}

				if (tailNode != null) {
					float tailPercent = tailNode.GetComponent<BallChainMovement>().currPathPercent;
					if (maxIdx + 1 >= balls.Count) 
						return;
					GameObject tailBack = (GameObject)balls[maxIdx + 1];

					float tailBackPercent = tailBack.GetComponent<BallChainMovement>().currPathPercent;
					if (tailPercent - tailBackPercent < diameterPercent) {
						foreach (GameObject ball in balls.GetRange(max2ndIdx + 1, maxIdx - max2ndIdx)) {
							ball.GetComponent<BallChainMovement>().moveFlag = true;
						}
						tailNode.GetComponent<BallChainMovement>().currPathPercent = tailBackPercent + diameterPercent;
						brokenChainNodeSet.Remove(tailNode);
						// TODO: register color check
						CheckColorTableEntry entry = new CheckColorTableEntry();
						entry.indexReference = maxIdx;
						entry.covered = false;
						if (!checkColorTable.ContainsKey(tailNode))
							checkColorTable.Add (tailNode, entry);

					}
					else {
						foreach (GameObject ball in balls.GetRange(max2ndIdx + 1, maxIdx - max2ndIdx)) {
							ball.GetComponent<BallChainMovement>().moveFlag = false;
						}
					}
				}
			}
		}
	}
}
