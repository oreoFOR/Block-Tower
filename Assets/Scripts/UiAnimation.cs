using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UiAnimation : MonoBehaviour
{
    [SerializeField] RectTransform scoreCounter;
    [SerializeField] LeanTweenType easeType;
    [SerializeField] TextMeshProUGUI restartText;
    [SerializeField] TextMeshProUGUI nextLevelText;
    [SerializeField] TextMeshProUGUI tapText;
    [SerializeField] GameObject startCanvas;
    [SerializeField] GameObject GOCanvas;
    [SerializeField] GameObject successCanvas;
    [SerializeField] TextMeshProUGUI lastScoreTxt;
    [SerializeField] TextMeshProUGUI highScoreTxt;
    [SerializeField] TextMeshProUGUI levelNumText;
    [SerializeField] GameObject failText;
    [SerializeField] bool advert;
    private void Start()
    {
        if(highScoreTxt != null){
            lastScoreTxt.text = PlayerPrefs.GetInt("LastScore").ToString();
            highScoreTxt.text = PlayerPrefs.GetInt("HighScore").ToString();
            GameEvents.instance.onPlatformGenerate += AnimateScore;
        }
        else{
            levelNumText.text = "Level "+(PlayerPrefs.GetInt("LevelNum")+1).ToString();
        }

        GameEvents.instance.onGameOver += GameOver;
        GameEvents.instance.onGameStarted += DisableStartUI;
        LeanTween.value(1, 0, 1).setLoopPingPong().setOnUpdate((float f) => { tapText.alpha = f;});
    }
    void DisableStartUI()
    {
        startCanvas.SetActive(false);
        //scoreCounter.gameObject.SetActive(true);
    }
    void GameOver(bool success)
    {
        GOCanvas.SetActive(!success);
        successCanvas.SetActive(success);
        TextMeshProUGUI tempText = success?nextLevelText:restartText;
        LeanTween.moveLocal(tempText.gameObject, new Vector3(0, -200, 0), .5f).setEaseInOutSine();
        LeanTween.value(1, 0, 1).setLoopPingPong().setOnUpdate((float f) => { tempText.alpha = f; });
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
