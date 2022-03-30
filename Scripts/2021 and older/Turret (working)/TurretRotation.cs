using UnityEngine;

public class TurretRotation : MonoBehaviour {

	public Transform Target;

	void Update()
	{
		Vector3 relativePosition = Target.position - transform.position;
		relativePosition.y = 0;
		Quaternion rotation = Quaternion.LookRotation(relativePosition, new Vector3(0, 1, 0));
		transform.localRotation = Quaternion.Lerp(transform.localRotation, rotation, Time.deltaTime);
	}
}
