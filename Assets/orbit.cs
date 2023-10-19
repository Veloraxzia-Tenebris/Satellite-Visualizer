using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class orbit : MonoBehaviour {
	// Main satellite object
	public Transform satellite;
	// Line renderer component
	LineRenderer lr;
	// Orbit line segments
	[Range(4, 64)]
	public int segments;
	// Satellite data and math
	public satelliteData satelliteOrbit;
	// TLE data
	public readTLE TLE;
	// True anomaly of satellite
	[Range(0f, 2 * Mathf.PI)]
	public float trueAnomaly = 0f;
	// Period of satellite
	public float meanMotion = 1f;
	public bool startOrbit = false;
	public float TLE_a, TLE_e, TLE_i, TLE_Omega, TLE_omega;
	public float orbitSpeed = 1f;

	public void startButton() {
		startOrbit = true;
	}

	void Awake() {
		lr = GetComponent<LineRenderer>();
		CalculateOrbit();
	}

	public void Start() {
		// If there isn't any orbiting object, stop orbit
		if(satellite == null) {
			startOrbit = false;
			return;
		}
		// Get all TLE data
		TLE = GetComponent<readTLE>();
		TLE_a = TLE.getSemimajor();
		TLE_e = TLE.getEccentricity();
		TLE_i = TLE.getInclination();
		TLE_Omega = TLE.getRAAN();
		TLE_omega = TLE.getAoP();
		satelliteOrbit = new satelliteData(TLE_a * 50, TLE_e, TLE_i, TLE_Omega, TLE_omega);
		meanMotion = TLE.getMeanMotion();
		// Set orbiting object initial position
		setPosition();
		CalculateOrbit();
		// Start orbit animation using coroutine
		StartCoroutine(animateOrbit());
	}

	void CalculateOrbit() {
		Vector3[] points = new Vector3[segments];
		for(int i = 0; i < segments; i++) {
			// Slowly approach max segment amount for smoothest orbit
			Vector3 position = satelliteOrbit.evaluate((((float) i) / ((float) segments)) * 360 * Mathf.Deg2Rad);
			points[i] = position;
		}
		lr.positionCount = segments;
		lr.SetPositions(points);
	}

	void OnValidate() {
		CalculateOrbit();
	}

	void setPosition() {
		Vector3 orbitPosition = satelliteOrbit.evaluate(trueAnomaly);
		satellite.localPosition = orbitPosition;
	}

	IEnumerator animateOrbit() {
		while(startOrbit) {
			// Want mean motion revolutions per 10 minutes (One Earth rotation, one day)
			// Thus, need to rotate mean motion / 10 degrees per frame
			// Minutes per frame => Time.deltaTime / 60
			// Radians per minute => 2 * Mathf.PI * n_0
			trueAnomaly += meanMotion * orbitSpeed * Time.deltaTime / 60;
			trueAnomaly %= (2 * Mathf.PI);
			setPosition();
			yield return null;
		}
	}
}