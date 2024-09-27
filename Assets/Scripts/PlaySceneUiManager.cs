using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlaySceneUiManager : MonoBehaviour {
    [SerializeField] TextMeshProUGUI stepText;
    public int step;
    [SerializeField] TextMeshProUGUI levelText;
    public UiPanelDotween winPanel;
    [SerializeField] Image musicButton;
    [SerializeField] Sprite musicOn;
    [SerializeField] Sprite musicOff;

    // Start is called before the first frame update
    void Start() {
        if (SceneManager.GetActiveScene().name == "PlayScene") {
            stepText.text = "STEP: " + step.ToString();
            levelText.text = "LEVEL " + PlayerPrefs.GetInt(StringManager.levelId).ToString();
            Debug.Log(PlayerPrefs.GetInt(StringManager.levelId));
            Debug.Log(PlayerPrefs.GetInt(StringManager.layoutId));
        } else if (SceneManager.GetActiveScene().name == "SelectLevelScene") {

        } else if (SceneManager.GetActiveScene().name == "HomeScene") {
            SetMusicStatus();
        }
        Application.targetFrameRate = 60;
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

    void SetMusicStatus() {
        int musicButtonStatus = PlayerPrefs.GetInt(StringManager.musicStatus,1);
        PlayerPrefs.SetInt(StringManager.musicStatus, musicButtonStatus);

        if (musicButtonStatus == 1) {
            musicButton.sprite = musicOn;
        }
        else {
            musicButton.sprite = musicOff;
        }
    }


    /// Buttons Fucntions

    public void LoadScene(string sceneName) {
        SceneManager.LoadScene(sceneName);
    }

    public void NextLevelButton() {
        int levelId = PlayerPrefs.GetInt(StringManager.levelId);
        PlayerPrefs.SetInt(StringManager.levelId, levelId + 1);
        if (PlayerPrefs.GetInt(StringManager.levelId) <= 5)
            PlayerPrefs.SetInt(StringManager.layoutId, 0);
        else
            PlayerPrefs.SetInt(StringManager.layoutId, 1);
        LoadScene("PlayScene");
    }

    public void LoadLevelScene() {
        int levelId = PlayerPrefs.GetInt(StringManager.levelId);
        PlayerPrefs.SetInt(StringManager.levelId, levelId + 1);
        LoadScene("SelectLevelScene");
    }

    public void MusicButton() {
        if (musicButton.sprite == musicOn) {
            musicButton.sprite = musicOff;
            PlayerPrefs.SetInt(StringManager.musicStatus, 0);
        } else {
            musicButton.sprite = musicOn;
            PlayerPrefs.SetInt(StringManager.musicStatus, 1);
        }
    }
}
