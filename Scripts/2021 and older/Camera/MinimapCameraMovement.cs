﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapCameraMovement : MonoBehaviour {

	public Transform player;

	void LateUpdate()
	{
			Vector3 newPosition = player.position;
			newPosition.y = transform.position.y;
			transform.position = newPosition;

			transform.rotation = Quaternion.Euler (90f, player.eulerAngles.y, 0f); // Rotates camera with player
	}
}
