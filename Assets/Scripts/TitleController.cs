using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


public class TitleController : MonoBehaviour
{
    public TextMeshProUGUI highScoreText;

    public void Start()
    {
        TitleScore();
    }
    // Start is called before the first frame update
    public void OnStartButtonClicked()
    {
        SceneManager.LoadScene("Main");
    }

    //スコア表示
    public void TitleScore()
    {
        highScoreText.text = "High Score : " + PlayerPrefs.GetInt("HighScore") + "m";
    }

    //リセットするボタンを用意する場合
    public void ResetScore()
    {
        PlayerPrefs.SetInt("HighScore", 0);
        TitleScore();
    }

    //ゲーム終了
    public void EndClicked()
    {
        Application.Quit(); //アプリ終了
    }
}