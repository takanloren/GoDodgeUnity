﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StayInSide : MonoBehaviour
{
	public Camera MainCamera; //be sure to assign this in the inspector to your main camera
	private Vector2 screenBounds;
	private float objectWidth;
	private float objectHeight;
	// Start is called before the first frame update
	void Start()
    {
		screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
		objectWidth = transform.GetComponent<SpriteRenderer>().bounds.extents.x; //extents = size of width / 2
		objectHeight = transform.GetComponent<SpriteRenderer>().bounds.extents.y; //extents = size of height / 2
	}

    // Update is called once per frame
    void LateUpdate()
    {
		screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
		Vector3 screenPos = Camera.main.WorldToScreenPoint(this.transform.position);

		Vector3 viewPos = transform.position;
		viewPos.x = Mathf.Clamp(viewPos.x, screenBounds.x * -1 + objectWidth, screenBounds.x - objectWidth);
		viewPos.y = Mathf.Clamp(viewPos.y, screenBounds.y * -1 + objectHeight, screenBounds.y - objectHeight);
		transform.position = viewPos;
	}
}
