using UnityEngine;
using System.Collections;

public class NavPlayerController : MonoBehaviour {
	NavMeshAgent nav;
	public Transform[] wayPoints;
	int wayPointIndex;

	// Use this for initialization
	void Start () {
		nav = GetComponent<NavMeshAgent> ();
		wayPointIndex = 0;

	}
	
	// Update is called once per frame
	void Update () {
		nav.destination = wayPoints[wayPointIndex].position;
		nav.Resume();
		nav.Stop ();
		GetComponent<Rigidbody>().velocity = Vector3.zero;
		if (nav.remainingDistance < nav.stoppingDistance) {
			wayPointIndex++;
			if (wayPointIndex >= wayPoints.Length) {
				wayPointIndex = 0;
			}
		}
	}
}
