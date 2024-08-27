using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleFollow : MonoBehaviour
{
    Vector3 diff;

    public GameObject target;   //プレイヤー
    public float followSpeed;   //追いつくスピード


    // Start is called before the first frame update
    void Start()
    {
        //元々のカメラとプレイヤーの距離感をそのまま採用※コレいいね
        diff = target.transform.position - transform.position;
    }


    void Update()
    {

    }

    //LateUpdateはUpdateの後に発動（プレーヤーの動きの後になる）
    void LateUpdate()
    {
        transform.position = Vector3.Lerp
            (transform.position,
            target.transform.position - diff,Time.deltaTime * followSpeed);
    }


}
