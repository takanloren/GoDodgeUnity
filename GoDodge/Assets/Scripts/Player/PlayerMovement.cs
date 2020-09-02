using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	public float velocity = 2.5f;
	public Rigidbody2D rb;
	private Animator anim;
    public Joystick joystick;

	// Start is called before the first frame update
	void Start()
    {
        joystick = FindObjectOfType<Joystick>();
		rb = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float h = joystick.Horizontal * 100f;
        float v = joystick.Vertical * 100f;

        float angle = Mathf.Atan2(joystick.Vertical, joystick.Horizontal) * Mathf.Rad2Deg;

        if (angle <= 150 && angle >= 30)
		{
            MoveAnimation(MoveDirection.Up);
		}
		else if (angle <= -30 && angle >= -150)
		{
            MoveAnimation(MoveDirection.Down);
		}
		else if ((angle <= -150 && angle >= -180) || (angle >= 150 && angle <= 180))
		{
            MoveAnimation(MoveDirection.Left);
		}
		else if ((angle <= 30 && angle > 0) || (angle >= -30 && angle < 0))
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
        float h = joystick.Horizontal * 100f;
        float v = joystick.Vertical * 100f;

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
                anim.SetTrigger("MoveUp");
				break;
			case MoveDirection.Down:
                anim.SetTrigger("MoveDown");
                break;
			case MoveDirection.Left:
                anim.SetTrigger("MoveLeft");
                break;
			case MoveDirection.Right:
                anim.SetTrigger("MoveRight");
                break;
			case MoveDirection.Idle:
                anim.SetTrigger("Idle");
                break;
		}
	}

	public enum MoveDirection
	{
		Up, Down, Left, Right, Idle
	}
}
