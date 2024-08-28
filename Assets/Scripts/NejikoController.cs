using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;




//2024/08/28 Move to Right メソMaxがMinになってたので修正しました　大窪

public class NejikoController : MonoBehaviour
{
    const int MinLane = -2;
    const int MaxLane = 2;
    const float LaneWidth = 1.0f;
    const int DefaultLife = 3;
    const float StunDuration = 0.5f;

    CharacterController controller;
    Animator animator;


    Vector3 moveDirection = Vector3.zero;    //キャラクターの動くべき座標（0,0,0）xyz
    int targetLane;
    int life = DefaultLife;
    float recoverTime = 0.0f;

    public float gravity;   //重力
    public float speedZ;    //キャラクターのスピード
    public float speedX;
    public float speedJump; //キャラクターのジャンプ力
    public float accelerationZ;

    public int Life()
    {
        return life;
    }

    bool IsStun()
    {
        return recoverTime > 0.0f || life <= 0;
    }


    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown("left")) MoveToLeft();
        if (Input.GetKeyDown("right")) MoveToRight();
        if (Input.GetKeyDown("space")) Jump();

        if (IsStun())
        {
            moveDirection.x = 0.0f;
            moveDirection.z = 0.0f;
            recoverTime -= Time.deltaTime;
        }
        else
        {
            float acceleratedZ = moveDirection.z + (accelerationZ * Time.deltaTime);
            moveDirection.z = Mathf.Clamp(accelerationZ, 0, speedZ);

            float ratioX = (targetLane * LaneWidth - transform.position.x) / LaneWidth;
            moveDirection.x = ratioX * speedX;
        }


        //ラジコン操作　抹消
        //--------------------------------------
        //※キャラクターコンポーネントのisGroundedプロパティでは常に接地判定をしている
        //
        //if (controller.isGrounded)
        //{
        //    //上キー（WキーVertical）が押されたら
        //    if (Input.GetAxis("Vertical") > 0.0f)
        //    {

        //        moveDirection.z = Input.GetAxis("Vertical") * speedZ;
        //    }
        //    else
        //    {
        //        moveDirection.z = 0.0f;
        //    }

        //    //左右キー（Horizontal）にて回転させる命令
        //    transform.Rotate(0, Input.GetAxis("Horizontal") * 2, 0);//教科書は(3,0)

        //    if (Input.GetButton("Jump"))
        //    {
        //        moveDirection.y = speedJump;
        //        //Jumpアニメの発動
        //        animator.SetTrigger("jump");
        //    }
        //}
        //--------------------------------------

        //重力分の力を毎フレーム追加(どんどん重くなる)
        //* Time.deltaTimeはどのPCでも同じスピードで動くようにするための補正
        moveDirection.y -= gravity * Time.deltaTime;

        //移動実行
        //* Time.deltaTimeはどのPCでも同じスピードで動くようにするための補正
        //キャラの向きが斜めったときに、ローカル座標をグローバル座標に置き換える
        //これしないと斜め状態で常にZに行く
        Vector3 globalDirection = transform.TransformDirection(moveDirection);
        //Moveメソッド：指定したら引数の座標にプレイヤーを動かす
        controller.Move(globalDirection * Time.deltaTime);

        //移動後接地してたらY方向の速度はリセットする
        if (controller.isGrounded) moveDirection.y = 0;

        //速度が0以上なら0以上なら走っているフラグをtrueにする
        animator.SetBool("run", moveDirection.z > 0.0f);
   

    }

    public void MoveToLeft()
    {
        if (IsStun()) return;
        if (controller.isGrounded && targetLane > MinLane) targetLane--;

    }
    public void MoveToRight()
    {
        if (IsStun()) return;
        if (controller.isGrounded && targetLane < MaxLane) targetLane++;

    }

    public void Jump()
    {
        if (IsStun()) return;
        if (controller.isGrounded)
        {
            moveDirection.y = speedJump;
            animator.SetTrigger("jump");
        }
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (IsStun()) return;
        if(hit.gameObject.tag == "Robo")
        {
            life--;
            recoverTime = StunDuration;

            animator.SetTrigger("damage");

            Destroy(hit.gameObject);


        }
    }





}
