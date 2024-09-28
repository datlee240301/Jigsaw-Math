using System.Collections;
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
    [SerializeField] TextMeshProUGUI coinNumberText;
    int coinNumber;
    [SerializeField] Camera camera;
    [SerializeField] UiPanelDotween notificationPanel;
    [SerializeField] UiPanelDotween completeLevelNotificationPanel;
    [SerializeField] Image fadeImage; // Image màu đen dùng để fade
    float fadeDuration = .5f;

    // Start is called before the first frame update
    void Start() {
        if (SceneManager.GetActiveScene().name == "PlayScene") {
            stepText.text = "STEP: " + step.ToString();
            levelText.text = "LEVEL " + PlayerPrefs.GetInt(StringManager.levelId).ToString();
            coinNumber = PlayerPrefs.GetInt(StringManager.coinNumber);
            coinNumberText.text = coinNumber.ToString();
            if (PlayerPrefs.GetInt(StringManager.layoutId) == 1) {
                camera.orthographicSize = 6.16f;
                camera.transform.position = new Vector3(2.02f, 2.27f, -10f);
            }
        } else if (SceneManager.GetActiveScene().name == "HomeScene") {
            SetMusicStatus();
            coinNumber = PlayerPrefs.GetInt(StringManager.coinNumber);
            coinNumberText.text = coinNumber.ToString();
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
        int musicButtonStatus = PlayerPrefs.GetInt(StringManager.musicStatus, 1);
        PlayerPrefs.SetInt(StringManager.musicStatus, musicButtonStatus);

        if (musicButtonStatus == 1) {
            musicButton.sprite = musicOn;
            FindObjectOfType<MusicManager>().audioSource.volume = 1;
        } else {
            musicButton.sprite = musicOff;
            FindObjectOfType<MusicManager>().audioSource.volume = 0;
        }
    }

    public void ShowNoTicePanel() {
        notificationPanel.PanelFadeIn();
    }

    public void ShowCompleteLevelPanel() {
        completeLevelNotificationPanel.PanelFadeIn();
    }

    public void RewardCoin() {
        int newCoinNumber = PlayerPrefs.GetInt(StringManager.coinNumber) + 10;
        PlayerPrefs.SetInt(StringManager.coinNumber, newCoinNumber);
        coinNumberText.text = newCoinNumber.ToString();
    }


    ///   Button



    // Chuyển cảnh với hiệu ứng fade
    public void LoadScene(string sceneName) {
        FindObjectOfType<SoundManager>().PlayClickSound();
        StartCoroutine(FadeAndLoadScene(sceneName)); 
    }

    private IEnumerator FadeAndLoadScene(string sceneName) {
        fadeImage.gameObject.SetActive(true);
        float currentTime = 0f;

        while (currentTime < fadeDuration) {
            currentTime += Time.deltaTime;
            float alpha = Mathf.Lerp(0, 1, currentTime / fadeDuration);
            fadeImage.color = new Color(0, 0, 0, alpha); 
            yield return null; 
        }

        SceneManager.LoadScene(sceneName);
    }

    public void NextLevelButton() {
        int levelId = PlayerPrefs.GetInt(StringManager.levelId);
        PlayerPrefs.SetInt(StringManager.levelId, levelId + 1);
        if (PlayerPrefs.GetInt(StringManager.levelId) <= 5)
            PlayerPrefs.SetInt(StringManager.layoutId, 0);
        else
            PlayerPrefs.SetInt(StringManager.layoutId, 1);
        FindObjectOfType<SoundManager>().PlayClickSound();
        LoadScene("PlayScene");
    }

    public void LoadLevelScene() {
        int levelId = PlayerPrefs.GetInt(StringManager.levelId);
        PlayerPrefs.SetInt(StringManager.levelId, levelId + 1);
        FindObjectOfType<SoundManager>().PlayClickSound();
        LoadScene("SelectLevelScene");
    }

    public void MusicButton() {
        if (musicButton.sprite == musicOn) {
            musicButton.sprite = musicOff;
            PlayerPrefs.SetInt(StringManager.musicStatus, 0);
            FindObjectOfType<MusicManager>().audioSource.volume = 0;
        } else {
            musicButton.sprite = musicOn;
            PlayerPrefs.SetInt(StringManager.musicStatus, 1);
            FindObjectOfType<MusicManager>().audioSource.volume = 1;
        }
        FindObjectOfType<SoundManager>().PlayClickSound();
    }

    public void BuyCoin(int number) {
        int newCoinNumber = PlayerPrefs.GetInt(StringManager.coinNumber) + number;
        PlayerPrefs.SetInt(StringManager.coinNumber, newCoinNumber);
        coinNumberText.text = newCoinNumber.ToString();
        FindObjectOfType<SoundManager>().PlayClickSound();
    }

    public void MinusCoin() {
        int newCoinNumber = PlayerPrefs.GetInt(StringManager.coinNumber) - 5;
        PlayerPrefs.SetInt(StringManager.coinNumber, newCoinNumber);
        coinNumberText.text = newCoinNumber.ToString();
    }
}
