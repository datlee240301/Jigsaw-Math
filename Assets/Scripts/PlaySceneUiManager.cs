using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlaySceneUiManager : MonoBehaviour {
    [SerializeField] TextMeshProUGUI stepText;
    int step;
    [SerializeField] TextMeshProUGUI levelText;
    public GameObject winPanel;

    // Start is called before the first frame update
    void Start() {
        stepText.text = "STEP: " + step.ToString();
        levelText.text = "LEVEL " + PlayerPrefs.GetInt(StringManager.levelId).ToString();
        Debug.Log(PlayerPrefs.GetInt(StringManager.levelId));
        Debug.Log(PlayerPrefs.GetInt(StringManager.layoutId));
    }

    // Update is called once per frame
    void Update() {

    }

    public void ShowObject(GameObject obj) {
        obj.SetActive(true);
    }

    public void CountStep() {
        step++;
        stepText.text = "STEP: " + step.ToString();
    }

    /// Buttons Fucntions
    public void LoadScene(string sceneName) {
        SceneManager.LoadScene(sceneName);
    }
}
