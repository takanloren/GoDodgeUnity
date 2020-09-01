﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EventTrigger : MonoBehaviour
{
	public GameObject explosionAnimation;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	void OnTriggerEnter2D(Collider2D other)
	{
		Debug.Log("Player OnTriggerEnter2D");
	}

	void OnCollisionEnter2D(Collision2D col)
	{
		Debug.Log("Player OnCollisionEnter2D");

		switch (col.gameObject.tag)
		{
			case "MobEye":
				GameObject aaa = Instantiate(explosionAnimation, this.transform.position, Quaternion.identity).gameObject;
				Destroy(aaa, 1f);
				Destroy(this.gameObject);

				break;

			case "Door":
				GameManager.Instance.CurrentLevel++;

				Debug.Log("Next level :" + GameManager.Instance.CurrentLevel);
				SceneManager.LoadScene("Level" + GameManager.Instance.CurrentLevel);
				break;

		}
		
	}
}