using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
	public Rigidbody2D rb2D;
	public float velocityX = 2f;
	public float velocityY = 2f;
	public float speed = 1f;
	private float objectWidth;
	private float objectHeight;

	// Start is called before the first frame update
	void Start()
    {
		rb2D = GetComponent<Rigidbody2D>();
		objectWidth = transform.GetComponent<SpriteRenderer>().bounds.extents.x; //extents = size of width / 2
		objectHeight = transform.GetComponent<SpriteRenderer>().bounds.extents.y; //extents = size of height / 2
	}

    // Update is called once per frame
    void FixedUpdate()
    {
		Vector3 screenPos = Camera.main.WorldToScreenPoint(this.transform.position);

		if (screenPos.x - objectWidth <= 0 || screenPos.x + objectWidth >= Screen.width)
		{
			velocityX *= -1;
		}
		else if (screenPos.y - objectHeight <= 0 || screenPos.y + objectHeight >= Screen.height)
		{
			velocityY *= -1;
		}

		MoveObject(velocityX, velocityY);
	}

	void MoveObject(float x, float y)
	{
		Vector3 tempVect = new Vector3(x, y, 0);
		tempVect = tempVect.normalized * speed * Time.deltaTime;
		rb2D.MovePosition(rb2D.transform.position + tempVect);
	}
}
