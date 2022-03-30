using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorESP : MonoBehaviour {

	void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireCube (transform.position, new Vector3 (2, 2, 2));
	}

}