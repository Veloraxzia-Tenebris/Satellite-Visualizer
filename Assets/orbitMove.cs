using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class orbitMove : MonoBehaviour {
	public Transform satellite;
	public satelliteData orbitPath;
	[Range(0f, 2 * Mathf.PI)]
	public float trueAnomaly = 0f;
	public float period = 3f;
	public bool startOrbit = true;

	void Start() {
		// If there isn't any orbiting object, stop orbit
		if(satellite == null) {
			startOrbit = false;
			return;
		}
		// Set orbiting object initial position
		setPosition();
		// Start orbit animation using coroutine
		StartCoroutine(animateOrbit());
	}

	void setPosition() {
		Vector3 orbitPosition = orbitPath.evaluate(trueAnomaly);
		satellite.localPosition = orbitPosition;
	}

	IEnumerator animateOrbit() {
		float orbitSpeed = 1f / period;
		while(startOrbit) {
			trueAnomaly += Time.deltaTime * orbitSpeed;
			trueAnomaly %= (2 * Mathf.PI);
			setPosition();
			yield return null;
		}
	}
}