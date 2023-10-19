using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class displaySemimajorAxis : MonoBehaviour {
	public TMP_Text val;
	public string s;
	// Start is called before the first frame update
	void Awake() {
		val = GetComponent<TMP_Text>();
	}

	// Update is called once per frame
	void Update() {
		s = "a = {0} km";
		s = string.Format(s, GameObject.Find("Satellite").GetComponent<readTLE>().getSemimajor() * 6378);
		val.text = s;
	}
}