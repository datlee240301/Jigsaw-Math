using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    [SerializeField] Image buttonImage;
    [SerializeField] Sprite blue;
    [SerializeField] Sprite red;
    [SerializeField] TextMeshProUGUI levelText;
    public int buttonId;
    int startLevelId;
    void Start()
    {
        SetUpButtonStatus();
    }

    public void LoadScene() {
        if(buttonImage.sprite == red) {
            if (buttonId <= 5) {
                PlayerPrefs.SetInt(StringManager.layoutId, 0);
                PlayerPrefs.SetInt(StringManager.levelId, buttonId);
            } else {
                PlayerPrefs.SetInt(StringManager.layoutId, 1);
            }
            SceneManager.LoadScene("PlayScene");
        } else if(buttonImage.sprite == blue) {

        }
    }

    void SetUpButtonStatus() {
        levelText.text = buttonId.ToString();
        startLevelId = PlayerPrefs.GetInt(StringManager.levelId, 1);
        PlayerPrefs.SetInt(StringManager.levelId, startLevelId);
        if (buttonId == PlayerPrefs.GetInt(StringManager.levelId))
            buttonImage.sprite = red;
        else
            buttonImage.sprite = blue;
        Debug.Log(PlayerPrefs.GetInt(StringManager.levelId));
    }
}
