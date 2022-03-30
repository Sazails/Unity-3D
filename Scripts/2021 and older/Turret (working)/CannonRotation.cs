using UnityEngine;

public class CannonRotation : MonoBehaviour {

	public Transform Target;
	public float RotSpeed = 1;
	Vector3 relPos;
	Quaternion rot;

	void Update()
	{
		// Position of target relative to my position
		relPos = (Target.position - transform.position).normalized;

		rot = Quaternion.LookRotation(relPos, new Vector3(0, 1, 0));

		// Lerping localRotatiot > rot with speed: Time.deltaTime * speed (set to 1 by default)
		transform.localRotation = Quaternion.Lerp(transform.localRotation, rot, Time.deltaTime * RotSpeed);

		// Resetting y- and z-EulerAngles to 0; This way only the x-EulerAngle will be affected. 
		// The y rotation is inherited from the parent (turret_turret)
		transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, 0, 0);
	}
}
