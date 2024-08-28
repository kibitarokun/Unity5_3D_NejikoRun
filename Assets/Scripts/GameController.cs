using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    public NejikoController nejiko;
    //public Text scoreText; これだと、テキストメッシュプロは入らない
    public TMP_Text scoreText;
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


    }

    int CalcScore()
    {
        return (int)nejiko.transform.position.z;
    }


}
