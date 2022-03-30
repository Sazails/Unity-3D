using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class SimpleCarController : MonoBehaviour {

	[Header("Current Speed")]
	public Vector3 Speed;

	public List<AxleInfo> axleInfos; // the information about each individual axle
	public float maxMotorTorque; // maximum torque the motor can apply to wheel
	public float maxSteeringAngle; // maximum steer angle the wheel can have

	public float brakeSpeed;
	private float Brakes;

	[Header("CarUI")]
	public Text vehicleName;

	void Start()
	{
		vehicleName.text = gameObject.name;
	}

	public void FixedUpdate()
	{
		Brakes = 0;

		float motor = maxMotorTorque * Input.GetAxis("Vertical");
		float steering = maxSteeringAngle * Input.GetAxis("Horizontal");

		foreach (AxleInfo axleInfo in axleInfos) {
			if (axleInfo.steering) {
				axleInfo.leftWheel.steerAngle = steering;
				axleInfo.rightWheel.steerAngle = steering;
			}
			if (axleInfo.motor) {
				axleInfo.leftWheel.motorTorque = motor;
				axleInfo.rightWheel.motorTorque = motor;
			}

			if (Input.GetKey (KeyCode.Space) == true) {
				{
					Brakes = brakeSpeed;
					{
						axleInfo.leftWheel.brakeTorque = Brakes;
						axleInfo.rightWheel.brakeTorque = Brakes;
					}
				}
			}
			else
			{
				axleInfo.leftWheel.brakeTorque = 0f;
				axleInfo.rightWheel.brakeTorque = 0f;
			}
		}
	}
}

[System.Serializable]
public class AxleInfo {
	public WheelCollider leftWheel;
	public WheelCollider rightWheel;
	public bool motor; // is this wheel attached to motor?
	public bool steering; // does this wheel apply steer angle?
}