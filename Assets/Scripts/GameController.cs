using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{

    public NejikoController nejiko; //ネジコの残ライフの情報が必要
    //public Text scoreText; これだと、テキストメッシュプロは入らない
    public TMP_Text scoreText; //TextMeshProUGUI
    public LifePanel lifePanel;

    // Start is called before the first frame update
    void Start()
    {

    }



    // Update is called once per frame
    void Update()
    {

        int score = CalcScore();
        scoreText.text = "Score :" + score + "m";

        lifePanel.UpdateLife(nejiko.Life());

        //ネジコのライフが0になったらゲームオーバー
        if (nejiko.Life() <= 0)
        {
            //これ以降のUpdateは止める（次のUpdateはなしにする）
            //this.enabled(GameController自身）を止める
            enabled = false;

            //ハイスコア更新
            if (PlayerPrefs.GetInt("HighScore") < score)
            {
                PlayerPrefs.SetInt("HighScore", score);
            }

            //2秒後にReturnToTitleメソッドを呼び出す
            Invoke("ReturnToTitle", 2.0f);
        }
    }

    int CalcScore()
    {
        //ネジコの走行距離（Zのポジション）をスコアとする
        return (int)nejiko.transform.position.z;
    }

    void ReturnToTitle()
    {
        //タイトルシーンに切り替え
        SceneManager.LoadScene("Title");
    }
}
