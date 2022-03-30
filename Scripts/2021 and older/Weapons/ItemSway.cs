using UnityEngine;

public class ItemSway : MonoBehaviour {

	Vector3 _InitialPos;
	Vector3 _FinalPos;

	public float _Amount;
	public float _SmoothAmount;
	public float _MaxAmount;

	float MoveX;
	float MoveY;

	void Start () {
		_InitialPos = transform.localPosition;
	}
	
	void Update () {
		MoveX = -Input.GetAxis("Mouse X") * _Amount;
		MoveY = -Input.GetAxis("Mouse Y") * _Amount;
		MoveX = Mathf.Clamp(MoveX, -_MaxAmount, _MaxAmount);
		MoveY = Mathf.Clamp(MoveY, -_MaxAmount, _MaxAmount);
		_FinalPos = new Vector3(MoveX, MoveY, 0);
		transform.localPosition = Vector3.Lerp(transform.localPosition, _FinalPos + _InitialPos, Time.deltaTime * _SmoothAmount);
	}
}
