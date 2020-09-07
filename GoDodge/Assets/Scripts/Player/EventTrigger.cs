using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EventTrigger : MonoBehaviour
{
	public GameObject explosionAnimation;
    public AudioSource audioSource;
    public AudioClip explosionAC;
    public AudioClip enterGateAC;

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
            case "Goblin":
				audioSource.PlayOneShot(explosionAC, 1);
                GameObject mobObject = Instantiate(explosionAnimation, this.transform.position, Quaternion.identity).gameObject;
				mobObject.GetComponent<SpriteRenderer>().sortingOrder = 5;
                GameManager.Instance.SetGameOverWithDelayTime(true, 1000);
                Destroy(mobObject, 1f);
				Destroy(this.gameObject);
				break;

			case "Door":
                audioSource.PlayOneShot(enterGateAC, 1);
                GameManager.Instance.ActiveRunAttemp.FinishedLevel++;
				SceneManager.LoadScene(GetMapScenePrefix(GameManager.Instance.ActiveRunAttemp.Map) + GameManager.Instance.ActiveRunAttemp.FinishedLevel);
				break;

		}
		
	}    

    private string GetMapScenePrefix(GameManager.Map map)
    {
        switch (map)
        {
            case GameManager.Map.DUNGEON:
                return Constants.DUNGEON_LEVEL_PREFIX;

            default:
                return "";
        }
    }
}
