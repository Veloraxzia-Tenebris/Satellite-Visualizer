using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class satelliteData {
	public float a;
	[Range(0f, 1f)]
	public float e;
	[Range(0f, Mathf.PI / 2)]
	public float i;
	[Range(0f, 2 * Mathf.PI)]
	public float Omega_big;
	[Range(0f, 2 * Mathf.PI)]
	public float omega_small;

	public satelliteData(float a, float e, float i, float Omega_big, float omega_small) {
		this.a = a;
		this.e = e;
		this.i = i;
		this.Omega_big = Omega_big;
		this.omega_small = omega_small;
	}

	public Vector3 evaluate(float nu) {
		// Set other ellipse parameters
		// Set semi-minor axis
		float b = Mathf.Sqrt(Mathf.Pow(a, 2) - Mathf.Pow((a * e), 2));
		// Set eccentric anomaly
		float E = Mathf.Atan2((Mathf.Sqrt(1 - Mathf.Pow(e, 2)) * Mathf.Sin(nu)), (e + Mathf.Cos(nu)));

		// Create ellipse in orbital plane
		float I = a * Mathf.Cos(E);
		float J = b * Mathf.Sin(E);

		// Shift ellipse to orbit around orbiting center
		I += a * e;

		// Rotate ellipse in orbital plane by argument of periapsis around orbiting center
		float I_omega = I * Mathf.Cos(omega_small) - J * Mathf.Sin(omega_small);
		float J_omega = I * Mathf.Sin(omega_small) + J * Mathf.Cos(omega_small);

		// Rotate orbital plane by inclination around line of nodes
		float J_i = J_omega * Mathf.Cos(i);
		float K_i = J_omega * Mathf.Sin(i);

		// Rotate inclined orbital plane by right ascension of the ascending node around orbiting center
		float I_Omega = I_omega * Mathf.Cos(Omega_big) - J_i * Mathf.Sin(Omega_big);
		float J_Omega = I_omega * Mathf.Sin(Omega_big) + J_i * Mathf.Cos(Omega_big);

		return new Vector3(I_Omega, K_i, J_Omega);
	}
}