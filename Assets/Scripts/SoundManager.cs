using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {
    public AudioSource audioSource;
    [SerializeField] AudioClip winSound;
    [SerializeField] AudioClip exchangeSound;
    [SerializeField] AudioClip moveBoxSound;
    [SerializeField] AudioClip fireworkSound;
    [SerializeField] AudioClip clickSound;
    // Start is called before the first frame update
    void Start() {
        //SetMusicStatus();
    }

    void SetMusicStatus() {
        if (PlayerPrefs.GetInt(StringManager.musicStatus) == 1)
            audioSource.volume = 1;
        else
            audioSource.volume = 0;
    }

    public void PlayWinSound() {
        audioSource.PlayOneShot(winSound);
    }

    public void PlayExchangeSound() {
        audioSource.PlayOneShot(exchangeSound);
    }

    public void PlayMoveBoxSound() {
        audioSource.PlayOneShot(moveBoxSound);
    }

    public void PlayFireworkSound() {
        audioSource.PlayOneShot(fireworkSound);
    }

    public void PlayClickSound() {
        audioSource.PlayOneShot(clickSound);
    }
}
