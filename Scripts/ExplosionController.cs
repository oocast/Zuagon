using UnityEngine;
using System.Collections;

public class ExplosionController : MonoBehaviour {
	public GameObject particlePrefab;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void CreateExplosion (Vector3 position) {
		GameObject particle = Instantiate(particlePrefab);
		particle.transform.position = position;
		Destroy(particle, particle.GetComponent<ParticleSystem>().duration);
	}
}
