using UnityEngine;
using System.Collections;

public class BulletBall : MonoBehaviour {
	ExplosionController explosion;
	int maxCollisionCount;
	int collisionCount;
	ChainMovement chainMovement;
	bool registered;
	float birthTime;
	float lifeTime;

	void Awake () {
		chainMovement = GameObject.Find ("SpawnController")
			.GetComponent <ChainMovement>();
		explosion = GameObject.Find ("ExplosionController")
			.GetComponent<ExplosionController>();
	}

	// Use this for initialization
	void Start () {
		collisionCount = 0;
		maxCollisionCount = 2;
		registered = false;
		birthTime = Time.time;
		lifeTime = 15f;
	}
	
	// Update is called once per frame
	void Update () {
		if (!registered && (collisionCount > maxCollisionCount || Time.time > birthTime + lifeTime)) {
			explosion.CreateExplosion(transform.position);
			Destroy(gameObject);
		}
	}

	void OnCollisionEnter (Collision collision) {
		// if the collision object has BallChainMovement
		// then it is in the chain
		BallChainMovement ballChainMovement = collision.gameObject
			.GetComponent<BallChainMovement>();

		if (ballChainMovement != null && !registered && GetComponent<Rigidbody> ().isKinematic == false) {
			MovementRegistrationEntry entry = new MovementRegistrationEntry();
			entry.bulletBall = gameObject;
			entry.chainBall = collision.gameObject;
			entry.proceedTime = 0f;
			entry.registerFrame = Time.frameCount;
			entry.repeatChainBall = 0;
			chainMovement.RegisterInsertion (entry);
			registered = true;

			GetComponent<Rigidbody> ().isKinematic = true;
			GetComponent<Rigidbody> ().velocity = Vector3.zero;
		}
		else if (ballChainMovement == null && !registered){
			collisionCount++;
		}
	}

	public void CheckColor () {
		
	}
}
