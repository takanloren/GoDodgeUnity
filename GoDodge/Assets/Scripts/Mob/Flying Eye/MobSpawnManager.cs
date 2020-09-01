using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobSpawnManager : MonoBehaviour
{
	public GameObject mob;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < 3; i++)
		{
			Instantiate(mob, new Vector3(i * 1.0F, 0, 0), Quaternion.identity);
		}
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
