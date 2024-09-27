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
    public int buttonId;
    int startLevelId;

    private const string ButtonColorKey = "ButtonColor_"; // Tạo key cho PlayerPrefs

    void Start() {
        SetUpButtonStatus();
        Application.targetFrameRate = 60;
    }

    // Hàm load scene
    public void LoadScene() {
        if (buttonImage.sprite == red) {
            if (buttonId <= 5) {
                PlayerPrefs.SetInt(StringManager.layoutId, 0);
                PlayerPrefs.SetInt(StringManager.levelId, buttonId);
            } else if (buttonId > 5) {
                PlayerPrefs.SetInt(StringManager.layoutId, 1);
            }

            SceneManager.LoadScene("PlayScene");
        } else if (buttonImage.sprite == blue) {
            // Chỉ khi button là màu xanh mới làm gì đó
        }
    }

    // Cài đặt trạng thái ban đầu của button
    void SetUpButtonStatus() {
        levelText.text = buttonId.ToString();
        startLevelId = PlayerPrefs.GetInt(StringManager.levelId, 1);
        PlayerPrefs.SetInt(StringManager.levelId, startLevelId);

        // Kiểm tra xem button này có từng chuyển sang màu đỏ (cam) chưa
        bool isOrange = PlayerPrefs.GetInt(ButtonColorKey + buttonId, 0) == 1;

        if (isOrange) {
            // Nếu đã chuyển sang màu cam, giữ nguyên màu cam (đỏ)
            buttonImage.sprite = red;
        } else {
            // Nếu chưa từng chuyển màu cam, kiểm tra điều kiện hiện tại
            if (buttonId <= PlayerPrefs.GetInt(StringManager.levelId)) {
                // Chuyển sang màu đỏ và lưu lại trạng thái này
                buttonImage.sprite = red;
                PlayerPrefs.SetInt(ButtonColorKey + buttonId, 1); // Lưu lại trạng thái đã chuyển sang cam
            } else {
                // Nếu chưa đạt điều kiện, giữ màu xanh
                buttonImage.sprite = blue;
            }
        }
    }
}
