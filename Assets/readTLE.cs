using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class readTLE : MonoBehaviour {
	public string TLE;
	public string line2;
	public string e;
	public string i;
	public string Omega;
	public string omega;
	public string n_0;

	public void readStringInput(string s) {
		TLE = s;
		Debug.Log(TLE);
		line2 = TLE.Substring(71);
		Debug.Log(line2);
		i = line2.Substring(8, 8);
		Debug.Log(i);
		Omega = line2.Substring(17, 8);
		Debug.Log(Omega);
		e = line2.Substring(26, 7);
		Debug.Log(e);
		omega = line2.Substring(34, 8);
		Debug.Log(omega);
		n_0 = line2.Substring(52, 11);
		Debug.Log(n_0);
	}

	public float getMeanMotion() {
		float N_0 = (float) Convert.ToDouble(n_0.Trim());
		// Need to convert from rev / day to rad / day
		return N_0 * 2 * Mathf.PI;
	}

	public float getSemimajor() {
		float N_0 = (float) Convert.ToDouble(n_0.Trim());
		// Need to convert from rev / day to rad / sec
		N_0 = N_0 * 2 * Mathf.PI / (24 * 60 * 60);
		float A = (float) Math.Cbrt((3.986 * 100000) / Mathf.Pow(N_0, 2));
		// Convert km to "1 unit of Earth radius"
		A /= 6378;
		return A;
	}

	public float getEccentricity() {
		string temp = "0.";
		temp = String.Concat(temp, e.Trim());
		float E = (float) Convert.ToDouble(temp);
		return E * Mathf.Deg2Rad;
	}

	public float getInclination() {
		float I = (float) Convert.ToDouble(i.Trim());
		return I * Mathf.Deg2Rad;
	}

	public float getRAAN() {
		float oMEGA = (float) Convert.ToDouble(Omega.Trim());
		return oMEGA * Mathf.Deg2Rad;
	}

	public float getAoP() {
		float OMEGA = (float) Convert.ToDouble(omega.Trim());
		return OMEGA * Mathf.Deg2Rad;
	}
}