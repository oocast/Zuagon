using UnityEngine;
using System.Collections;

public class SiblingTest : MonoBehaviour {
	GameObject jod;
	GameObject drum3;
	public Transform parent;
	public ArrayList list;
	public int[] list_values;
	bool once;

	// Use this for initialization
	void Start () {
		list = new ArrayList();
		jod = GameObject.Find("JamODrum");
		list.Add(jod.transform.GetChild(0).gameObject);
		list.Add(jod.transform.GetChild(1).gameObject);
		list.Add(jod.transform.GetChild(2).gameObject);
		list.Add(jod.transform.GetChild(3).gameObject);
		once = false;

		drum3 = jod.transform.GetChild(2).gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		if (!once) {
			once = true;
			Debug.Log (list.IndexOf(drum3));
		}
		/*
		if (Input.GetKeyDown(KeyCode.Equals)) {
			transform.SetParent(parent);
			transform.SetSiblingIndex(2);
		}*/

	}
}
