using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {
    public AudioSource audioSource;
    // Start is called before the first frame update
    void Start() {

    }

    void SetMusicStatus() {
        if (PlayerPrefs.GetInt(StringManager.musicStatus) == 1)
            audioSource.volume = 1;
        else
            audioSource.volume = 0;
    }
}
