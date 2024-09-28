using UnityEngine;
using DG.Tweening;
using System.Collections;

public class UiPanelDotween : MonoBehaviour {
    public float fadeTime = 0.5f;
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private RectTransform rectTransform;
    public GameObject panelblack;
    [SerializeField] GameObject[] stars;
    int starCount;

    private void Awake() {
        panelblack.SetActive(false);
    }
    public void PanelFadeIn() {
        canvasGroup.alpha = 0;
        //rectTransform.transform.localPosition = new Vector3(0, -1000f, 0);
        rectTransform.DOAnchorPos(new Vector2(0, 0), fadeTime, false).SetEase(Ease.InOutBack);
        canvasGroup.DOFade(1, fadeTime);
        panelblack.SetActive(true);
        SetStarNumber();
        FindObjectOfType<SoundManager>().PlayClickSound();
    }

    public void PanelFadeOut() {
        canvasGroup.alpha = 1;
        //rectTransform.transform.localPosition = new Vector3(0, 0, 0);
        rectTransform.DOAnchorPos(new Vector2(0, -2500f), fadeTime, false).SetEase(Ease.InOutBack);
        canvasGroup.DOFade(1, fadeTime);
        StartCoroutine(HidePanel());
        FindObjectOfType<SoundManager>().PlayClickSound();
    }

    IEnumerator HidePanel() {
        yield return new WaitForSeconds(fadeTime);
        panelblack.SetActive(false);
    }

    void SetStarNumber() {
        if (gameObject.tag == "WinPanel" && panelblack.activeSelf) {
            int currenLevelId = PlayerPrefs.GetInt(StringManager.levelId);
            if (currenLevelId == 1) {
                CountStar();
                PlayerPrefs.SetInt(StringManager.level1Star, starCount);
            } else if (currenLevelId == 2) {
                CountStar();
                PlayerPrefs.SetInt(StringManager.level2Star, starCount);
            } else if (currenLevelId == 3) {
                CountStar();
                PlayerPrefs.SetInt(StringManager.level3Star, starCount);
            } else if (currenLevelId == 4) {
                CountStar();
                PlayerPrefs.SetInt(StringManager.level4Star, starCount);
            } else if (currenLevelId == 5) {
                CountStar();
                PlayerPrefs.SetInt(StringManager.level5Star, starCount);
            } else if (currenLevelId == 6) {
                CountStar();
                PlayerPrefs.SetInt(StringManager.level6Star, starCount);
            } else if (currenLevelId == 7) {
                CountStar();
                PlayerPrefs.SetInt(StringManager.level7Star, starCount);
            } else if (currenLevelId == 8) {
                CountStar();
                PlayerPrefs.SetInt(StringManager.level8Star, starCount);
            } else if (currenLevelId == 9) {
                CountStar();
                PlayerPrefs.SetInt(StringManager.level9Star, starCount);
            } else if (currenLevelId == 10) { 
                CountStar();
                PlayerPrefs.SetInt(StringManager.level10Star, starCount);
            }
        }
    }

    void CountStar() {
        if (FindObjectOfType<PlaySceneUiManager>().step <= 50) {
            stars[0].SetActive(true);
            stars[1].SetActive(true);
            stars[2].SetActive(true);
        } else if (FindObjectOfType<PlaySceneUiManager>().step > 50 &&
            FindObjectOfType<PlaySceneUiManager>().step <= 70) {
            stars[0].SetActive(true);
            stars[1].SetActive(true);
        }
        GameObject[] star = GameObject.FindGameObjectsWithTag("Star");
        starCount = star.Length;
    }
}
