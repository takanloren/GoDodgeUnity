using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	public float velocity = 2.5f;
	public Rigidbody2D rb;
	private Animator anim;


	// Start is called before the first frame update
	void Start()
    {
		rb = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
		float h = Input.GetAxis("Horizontal");
		float v = Input.GetAxis("Vertical");

		if (v > 0)
		{
			MoveAnimation(MoveDirection.Up);
		}
		else if (v < 0)
		{
			MoveAnimation(MoveDirection.Down);
		}
		else if (h < 0)
		{
			MoveAnimation(MoveDirection.Left);
		}
		else if (h > 0)
		{
			MoveAnimation(MoveDirection.Right);
		}
		else
		{
			MoveAnimation(MoveDirection.Idle);
		}
	}

	void FixedUpdate()
	{
		float h = Input.GetAxis("Horizontal");
		float v = Input.GetAxis("Vertical");
		
		Vector3 tempVect = new Vector3(h, v, 0);
		tempVect = tempVect.normalized * velocity * Time.deltaTime;
		rb.MovePosition(rb.transform.position);
		rb.MovePosition(rb.transform.position + tempVect);
	}

	void MoveAnimation(MoveDirection moveDirection)
	{
		switch (moveDirection)
		{
			case MoveDirection.Up:
				anim.SetBool("MoveUp", true);
				anim.SetBool("MoveDown", false);
				anim.SetBool("MoveLeft", false);
				anim.SetBool("MoveRight", false);
				anim.SetBool("Idle", false);
				break;
			case MoveDirection.Down:
				anim.SetBool("MoveUp", false);
				anim.SetBool("MoveDown", true);
				anim.SetBool("MoveLeft", false);
				anim.SetBool("MoveRight", false);
				anim.SetBool("Idle", false);
				break;
			case MoveDirection.Left:
				anim.SetBool("MoveUp", false);
				anim.SetBool("MoveDown", false);
				anim.SetBool("MoveLeft", true);
				anim.SetBool("MoveRight", false);
				anim.SetBool("Idle", false);
				break;
			case MoveDirection.Right:
				anim.SetBool("MoveUp", false);
				anim.SetBool("MoveDown", false);
				anim.SetBool("MoveLeft", false);
				anim.SetBool("MoveRight", true);
				anim.SetBool("Idle", false);
				break;
			case MoveDirection.Idle:
				anim.SetBool("MoveUp", false);
				anim.SetBool("MoveDown", false);
				anim.SetBool("MoveLeft", false);
				anim.SetBool("MoveRight", false);
				anim.SetBool("Idle", true);
				break;
		}
	}

	public enum MoveDirection
	{
		Up, Down, Left, Right, Idle
	}
}
