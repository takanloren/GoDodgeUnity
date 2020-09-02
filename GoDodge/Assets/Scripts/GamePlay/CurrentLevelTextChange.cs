using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CurrentLevelTextChange : MonoBehaviour
{
    public TextMeshProUGUI currentLevel;
    // Start is called before the first frame update
    void Start()
    {
        UpdateTextToCurrentLevel();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateTextToCurrentLevel()
    {
        currentLevel.text = GameManager.Instance.CurrentLevel.ToString();
    }
}
