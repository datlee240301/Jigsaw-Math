using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlaySceneUiManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI stepText;
    int step;
    [SerializeField] TextMeshProUGUI levelText;

    // Start is called before the first frame update
    void Start()
    {
        stepText.text = "STEP: " + step.ToString();
        levelText.text = "LEVEL " + PlayerPrefs.GetInt(StringManager.levelId).ToString();
        Debug.Log(PlayerPrefs.GetInt(StringManager.levelId));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CountStep() {
        step++;
        stepText.text = "STEP: " + step.ToString();
    }
}
