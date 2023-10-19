using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraMovement : MonoBehaviour {
	public float moveSpeed = 5f;
	public float lookSpeed = 5f;
	private Vector3 motion;
	private Vector3 look;
	private Vector3 direction;
	private float slowLookx;
	private float slowLooky;
	private Rigidbody rb;

	void Start() {
		rb = GetComponent<Rigidbody>();
	}

	void Update() {
		// Update rotation
		slowLookx = Mathf.Lerp(slowLookx, -Input.GetAxis("Mouse Y"), lookSpeed * Time.deltaTime);
		slowLooky = Mathf.Lerp(slowLooky, Input.GetAxis("Mouse X"), lookSpeed * Time.deltaTime);
		look += new Vector3(slowLookx, slowLooky, 0);
		look.x = Mathf.Clamp(look.x, -90, 90);
		rb.transform.eulerAngles = look;
		// Update position
		motion = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Jump"), Input.GetAxisRaw("Vertical"));
		var force = moveSpeed * motion.magnitude;
		direction = new Vector3(0, motion.y, 0);
		direction += motion.z * rb.transform.forward + motion.x * rb.transform.right;
		rb.AddForce(direction * force);
	}
}