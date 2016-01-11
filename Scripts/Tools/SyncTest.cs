using UnityEngine;
using System.Collections;

public class SyncTest : MonoBehaviour {
	public SiblingTest arrayScript;
	public int index;
	bool once;


	// Use this for initialization
	void Start () {
		once = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (!once) {
			once = true;
			int i = 0;
			foreach(int a in arrayScript.list) {
				i++;
				if (i == index) {
					arrayScript.list.Insert(i, 100 + i);
					break;
				}
			}

			foreach(int a in arrayScript.list) {
				Debug.Log (a + " by " + name);
			}
		}
	}
}
