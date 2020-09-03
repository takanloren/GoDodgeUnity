using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
	public Transform player;
	public Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		var xVector = player.position.x;
		var yVector = player.position.y;

		if(xVector < -1 || xVector > 1)
		{
			xVector = transform.position.x;
		}

		if(yVector < 0 || yVector > 11)
		{
			yVector = transform.position.y;
		}

		transform.position = new Vector3(xVector, yVector, transform.position.z); 
	}
}
