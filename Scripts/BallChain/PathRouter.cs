using UnityEngine;
using System.Collections;
using DG.Tweening;

public class PathRouter : MonoBehaviour {
	public Tween tween;
	Vector3[] waypointArray;
	GameObject wayPointObj;
	GameObject endPoint;
	bool once;
	public bool updatePath;
	public float pathLength;

	// Use this for initialization
	void Start () {
		tween = null;
		endPoint = GameObject.Find ("EndPoint");
		wayPointObj = GameObject.Find("Waypoints");
		waypointArray = new Vector3[wayPointObj.transform.childCount + 1];
		for (int i = 0; i < waypointArray.Length - 1; i++) {
			waypointArray[i] = wayPointObj.transform.GetChild(i).position;
		}
		waypointArray[waypointArray.Length - 1] = endPoint.transform.position;

		transform.position = waypointArray[0];
		tween = transform.DOPath (waypointArray, 0.0f, PathType.CatmullRom, PathMode.TopDown2D, 10, Color.red);
		tween.SetSpeedBased();
		tween.SetEase(Ease.Linear);
		tween.ForceInit();

		pathLength = tween.PathLength();
	}
	
	// Update is called once per frame
	void Update () {
		InitPath ();
		if (updatePath) {
			//UpdatePath ();
		}
	}

	void InitPath () {
		if (!once) {
			once = true;


		}
	}
}
