using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothFollow : MonoBehaviour {

	// Target to follow
	public Transform target;

	// Distance in x-z plane to target
	public float distance = 15.0f;

	// Heigh for camera to be above target
	public float height = 5.0f;

	public float heightDamping = 2.0f;
	public float rotationDamping = 3.0f;

	[AddComponentMenu("FpCamera/SmoothFollow")]

	void LateUpdate()
	{
		if (!target)
			return;

		// Calculate current rot angles
		float wantedRotationAngle = target.eulerAngles.y;
		float wantedHeight = target.position.y + height;

		float currentRotationAngle = transform.eulerAngles.y;
		float currentHeight = transform.position.y;

		// Damp rot around y-axis
		currentRotationAngle = Mathf.LerpAngle(currentRotationAngle, wantedRotationAngle, rotationDamping * Time.deltaTime);

		// Damp height
		currentHeight = Mathf.Lerp(currentHeight, wantedHeight, heightDamping * Time.deltaTime);

		// Convert angle into rotation
		var currentRotation = Quaternion.Euler(0, currentRotationAngle, 0);

		// Set pos of camera on x-z plane to: distance meters behind target
		transform.position  = target.position;
		transform.position -= currentRotation * Vector3.forward * distance;

		// Set height for camera
		transform.position = new Vector3(transform.position.x, currentHeight, transform.position.z);

		// Look at target
		transform.LookAt(target);
	}


}