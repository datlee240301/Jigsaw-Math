using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HomeSceneUiManager : MonoBehaviour
{
    [SerializeField] Image musicButton;
    [SerializeField] Sprite musicOn;
    [SerializeField] Sprite musicOff;

    private void Awake() {
        Application.targetFrameRate = 60;
    }
    // Start is called before the first frame update
    void Start()
    {
        SetMusicStatus();
    }

    void SetMusicStatus() {
        int musicButtonStatus = PlayerPrefs.GetInt(StringManager.musicStatus, 1);
        PlayerPrefs.SetInt(StringManager.musicStatus, musicButtonStatus);

        if (musicButtonStatus == 1) {
            musicButton.sprite = musicOn;
        } else {
            musicButton.sprite = musicOff;
        }
    }

    /// Button Functions zone

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
