using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UiAnimation : MonoBehaviour
{
    [SerializeField] RectTransform scoreCounter;
    [SerializeField] LeanTweenType easeType;
    [SerializeField] TextMeshProUGUI restartText;
    [SerializeField] TextMeshProUGUI tapText;
    [SerializeField] GameObject startCanvas;
    [SerializeField] GameObject GOCanvas;
    [SerializeField] TextMeshProUGUI lastScoreTxt;
    [SerializeField] TextMeshProUGUI highScoreTxt;
    [SerializeField] GameObject failText;
    [SerializeField] bool advert;
    private void Start()
    {
        lastScoreTxt.text = PlayerPrefs.GetInt("LastScore").ToString();
        highScoreTxt.text = PlayerPrefs.GetInt("HighScore").ToString();
        GameEvents.instance.onPlatformGenerate += AnimateScore;
        GameEvents.instance.onGameOver += GameOver;
        GameEvents.instance.onGameStarted += DisableStartUI;
        LeanTween.value(1, 0, 1).setLoopPingPong().setOnUpdate((float f) => { tapText.alpha = f;});
    }
    void DisableStartUI()
    {
        startCanvas.SetActive(false);
        scoreCounter.gameObject.SetActive(true);
    }
    void GameOver()
    {
        GOCanvas.SetActive(true);
        LeanTween.moveLocal(restartText.gameObject, new Vector3(0, -200, 0), .5f).setEaseInOutSine();
        LeanTween.value(1, 0, 1).setLoopPingPong().setOnUpdate((float f) => { restartText.alpha = f; });
        if (advert)
        {
            failText.SetActive(true);
            LeanTween.scale(failText, new Vector3(.7f, .7f, .7f), .075f);
        }
    }
    void AnimateScore()
    {
        LeanTween.scale(scoreCounter, new Vector3(2f, 2f, 2f), .5f).setEase(easeType);
    }
}
