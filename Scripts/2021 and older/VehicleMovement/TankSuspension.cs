using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankSuspension : MonoBehaviour {

	Transform[] models;
	Transform[] tracks;
	WheelCollider[] colliders;

	WheelHit hit;

	public float trackHitPointY;
	public float wheelHitPointY;

	void Start()
	{
		models = new Transform[8];
		tracks = new Transform[8];
		colliders = new WheelCollider[8];

		for (int i = 0; i < 4; i++) {
			models [i] = transform.Find ("SuspensionL/WheelModels/Wheel" + i);
			models [i + 4] = transform.Find ("SuspensionR/WheelModels/Wheel" + i);

			tracks [i] = transform.Find ("SuspensionL/Tracks/Bone" + i);
			tracks [i + 4] = transform.Find ("SuspensionR/Tracks/Bone" + i);

			colliders [i] = transform.Find ("SuspensionL/WheelColliders/Wheel" + i).gameObject.GetComponent<WheelCollider> ();
			colliders [i + 4] = transform.Find ("SuspensionR/WheelColliders/Wheel" + i).gameObject.GetComponent<WheelCollider> ();
		}
	}

	void Update()
	{
		for (int i = 0; i < 8; i++) {
			if (colliders [i].GetGroundHit (out hit)) {
				models[i].localPosition = new Vector3(
					models[i].localPosition.x,
					transform.InverseTransformPoint(hit.point).y + wheelHitPointY,
					models[i].localPosition.z);

				tracks[i].localPosition = new Vector3(
					tracks[i].localPosition.x,
					tracks[i].localPosition.y,
					transform.InverseTransformPoint(hit.point).y + trackHitPointY);
			}
		}
	}
}
