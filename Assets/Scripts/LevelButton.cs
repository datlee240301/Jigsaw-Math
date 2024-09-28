using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour {
    [SerializeField] Image buttonImage;
    [SerializeField] Sprite blue;
    [SerializeField] Sprite red;
    [SerializeField] TextMeshProUGUI levelText;
    [SerializeField] GameObject[] stars;
    public int buttonId;
    int startLevelId;

    private const string ButtonColorKey = "ButtonColor_"; // Tạo key cho PlayerPrefs

    void Start() {
        SetUpButtonStatus();
        Application.targetFrameRate = 60;
        SetStar();
    }

    // Hàm load scene
    public void LoadScene() {
        if (buttonImage.sprite == red) {
            if (buttonId <= 5) {
                PlayerPrefs.SetInt(StringManager.layoutId, 0);
                PlayerPrefs.SetInt(StringManager.levelId, buttonId);
            } else {
                PlayerPrefs.SetInt(StringManager.layoutId, 1);
            }
            FindObjectOfType<SoundManager>().PlayClickSound();
            FindObjectOfType<PlaySceneUiManager>().LoadScene("PlayScene");
        }
        else
            FindObjectOfType<PlaySceneUiManager>().ShowCompleteLevelPanel();
    }

    // Thiết lập trạng thái màu của button
    void SetUpButtonStatus() {
        levelText.text = buttonId.ToString();
        startLevelId = PlayerPrefs.GetInt(StringManager.levelId, 1);
        PlayerPrefs.SetInt(StringManager.levelId, startLevelId);

        bool isOrange = PlayerPrefs.GetInt(ButtonColorKey + buttonId, 0) == 1;

        if (isOrange) {
            buttonImage.sprite = red;
        } else {
            if (buttonId <= PlayerPrefs.GetInt(StringManager.levelId)) {
                buttonImage.sprite = red;
                PlayerPrefs.SetInt(ButtonColorKey + buttonId, 1);
            } else {
                buttonImage.sprite = blue;
            }
        }
    }

    // Thiết lập các sao cho mỗi level dựa trên buttonId
    void SetStar() {
        string starKey = GetStarKey(buttonId);
        int starCount = PlayerPrefs.GetInt(starKey, 0);

        for (int i = 0; i < stars.Length; i++) {
            stars[i].SetActive(i < starCount);
        }
    }

    // Hàm trả về key của số sao dựa trên buttonId
    string GetStarKey(int id) {
        return id switch {
            1 => StringManager.level1Star,
            2 => StringManager.level2Star,
            3 => StringManager.level3Star,
            4 => StringManager.level4Star,
            5 => StringManager.level5Star,
            6 => StringManager.level6Star,
            7 => StringManager.level7Star,
            8 => StringManager.level8Star,
            9 => StringManager.level9Star,
            10 => StringManager.level10Star,
            _ => ""
        };
    }
}
