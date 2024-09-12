using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{

    public NejikoController nejiko; //�l�W�R�̎c���C�t�̏�񂪕K�v
    //public Text scoreText; ���ꂾ�ƁA�e�L�X�g���b�V���v���͓���Ȃ�
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

        //�l�W�R�̃��C�t��0�ɂȂ�����Q�[���I�[�o�[
        if (nejiko.Life() <= 0)
        {
            //����ȍ~��Update�͎~�߂�i����Update�͂Ȃ��ɂ���j
            //this.enabled(GameController���g�j���~�߂�
            enabled = false;

            //�n�C�X�R�A�X�V
            if (PlayerPrefs.GetInt("HighScore") < score)
            {
                PlayerPrefs.SetInt("HighScore", score);
            }

            //2�b���ReturnToTitle���\�b�h���Ăяo��
            Invoke("ReturnToTitle", 2.0f);
        }
    }

    int CalcScore()
    {
        //�l�W�R�̑��s�����iZ�̃|�W�V�����j���X�R�A�Ƃ���
        return (int)nejiko.transform.position.z;
    }

    void ReturnToTitle()
    {
        //�^�C�g���V�[���ɐ؂�ւ�
        SceneManager.LoadScene("Title");
    }
}
