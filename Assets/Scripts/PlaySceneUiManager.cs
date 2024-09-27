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

    // Hiển thị object
    public void ShowObject(GameObject obj) {
        obj.SetActive(true);
    }

    // Tăng số bước di chuyển và cập nhật text
    public void CountStep() {
        step++;
        stepText.text = "STEP: " + step.ToString();
    }

    // Cài đặt trạng thái nhạc
    void SetMusicStatus() {
        int musicButtonStatus = PlayerPrefs.GetInt(StringManager.musicStatus, 1);
        PlayerPrefs.SetInt(StringManager.musicStatus, musicButtonStatus);

        if (musicButtonStatus == 1) {
            musicButton.sprite = musicOn;
        } else {
            musicButton.sprite = musicOff;
        }
    }

    // Hiển thị bảng thông báo
    public void ShowNoTicePanel() {
        notificationPanel.PanelFadeIn();
    }

    // Cộng thêm 10 coin vào tổng số coin và cập nhật text
    public void RewardCoin() {
        int newCoinNumber = PlayerPrefs.GetInt(StringManager.coinNumber) + 10;
        PlayerPrefs.SetInt(StringManager.coinNumber, newCoinNumber);
        coinNumberText.text = newCoinNumber.ToString();
    }


    ///   Button



    // Chuyển cảnh với hiệu ứng fade
    public void LoadScene(string sceneName) {
        StartCoroutine(FadeAndLoadScene(sceneName)); // Bắt đầu coroutine fade
    }

    // Coroutine để fade màn hình và sau đó load scene
    private IEnumerator FadeAndLoadScene(string sceneName) {
        fadeImage.gameObject.SetActive(true);
       // fadeDuration = 1f; // Thời gian fade (tính bằng giây)
        float currentTime = 0f;

        // Dần dần tăng alpha của fadeImage từ 0 đến 1
        while (currentTime < fadeDuration) {
            currentTime += Time.deltaTime;
            float alpha = Mathf.Lerp(0, 1, currentTime / fadeDuration);
            fadeImage.color = new Color(0, 0, 0, alpha); // Đặt alpha cho Image
            yield return null; // Đợi frame tiếp theo
        }

        // Khi fade hoàn tất, load scene mới
        SceneManager.LoadScene(sceneName);
    }

    // Chuyển tới level tiếp theo
    public void NextLevelButton() {
        int levelId = PlayerPrefs.GetInt(StringManager.levelId);
        PlayerPrefs.SetInt(StringManager.levelId, levelId + 1);
        if (PlayerPrefs.GetInt(StringManager.levelId) <= 5)
            PlayerPrefs.SetInt(StringManager.layoutId, 0);
        else
            PlayerPrefs.SetInt(StringManager.layoutId, 1);
        LoadScene("PlayScene");
    }

    // Chuyển tới màn hình chọn level
    public void LoadLevelScene() {
        int levelId = PlayerPrefs.GetInt(StringManager.levelId);
        PlayerPrefs.SetInt(StringManager.levelId, levelId + 1);
        LoadScene("SelectLevelScene");
    }

    // Bật/tắt nhạc khi ấn vào nút
    public void MusicButton() {
        if (musicButton.sprite == musicOn) {
            musicButton.sprite = musicOff;
            PlayerPrefs.SetInt(StringManager.musicStatus, 0);
        } else {
            musicButton.sprite = musicOn;
            PlayerPrefs.SetInt(StringManager.musicStatus, 1);
        }
    }

    // Mua thêm coin
    public void BuyCoin(int number) {
        int newCoinNumber = PlayerPrefs.GetInt(StringManager.coinNumber) + number;
        PlayerPrefs.SetInt(StringManager.coinNumber, newCoinNumber);
        coinNumberText.text = newCoinNumber.ToString();
    }

    // Trừ coin
    public void MinusCoin() {
        int newCoinNumber = PlayerPrefs.GetInt(StringManager.coinNumber) - 5;
        PlayerPrefs.SetInt(StringManager.coinNumber, newCoinNumber);
        coinNumberText.text = newCoinNumber.ToString();
    }
}
