using UnityEngine;
public class RotatingObject : MonoBehaviour {
	public float _Rotatez;
	public float _Rotatey;
	public float _Rotatex;
	void Update () {transform.Rotate (_Rotatex, _Rotatey, _Rotatez);}
}
