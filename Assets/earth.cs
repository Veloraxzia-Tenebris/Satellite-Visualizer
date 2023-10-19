using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class earth : MonoBehaviour {
	public bool startEarth = false;
	public float speed = 1f;
	private Vector3 rot;
	public Transform Earth;

	public void startButton() {
		startEarth = true;
	}

	public void Start() {
		// Start Earth animation using coroutine
		StartCoroutine(animateEarth());
	}

	void Update() {

	}

	IEnumerator animateEarth() {
		while(startEarth) {
			// Want one rotation per 2 minutes ->
			// Rotate 360 degrees per 600 seconds
			// 60 frames per second -> 360 * 10 frames per minute
			// Rotate 1 / 20 degree per frame
			rot += new Vector3(0, speed * 2 * Mathf.PI * Time.deltaTime / 60, 0);
			rot.y  %= 2 * Mathf.PI;
			Earth.eulerAngles = rot * 360 / (2 * Mathf.PI);
			yield return null;
		}
	}
}