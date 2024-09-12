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

    //�X�R�A�\��
    public void TitleScore()
    {
        highScoreText.text = "High Score : " + PlayerPrefs.GetInt("HighScore") + "m";
    }

    //���Z�b�g����{�^����p�ӂ���ꍇ
    public void ResetScore()
    {
        PlayerPrefs.SetInt("HighScore", 0);
        TitleScore();
    }

    //�Q�[���I��
    public void EndClicked()
    {
        Application.Quit(); //�A�v���I��
    }
}