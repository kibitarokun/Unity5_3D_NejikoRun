using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;




//2024/08/28 Move to Right ���\Max��Min�ɂȂ��Ă��̂ŏC�����܂����@��E

public class NejikoController : MonoBehaviour
{
    const int MinLane = -2;
    const int MaxLane = 2;
    const float LaneWidth = 1.0f;
    const int DefaultLife = 3;
    const float StunDuration = 0.5f;

    CharacterController controller;
    Animator animator;


    Vector3 moveDirection = Vector3.zero;    //�L�����N�^�[�̓����ׂ����W�i0,0,0�jxyz
    int targetLane;
    int life = DefaultLife;
    float recoverTime = 0.0f;

    public float gravity;   //�d��
    public float speedZ;    //�L�����N�^�[�̃X�s�[�h
    public float speedX;
    public float speedJump; //�L�����N�^�[�̃W�����v��
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


        //���W�R������@����
        //--------------------------------------
        //���L�����N�^�[�R���|�[�l���g��isGrounded�v���p�e�B�ł͏�ɐڒn��������Ă���
        //
        //if (controller.isGrounded)
        //{
        //    //��L�[�iW�L�[Vertical�j�������ꂽ��
        //    if (Input.GetAxis("Vertical") > 0.0f)
        //    {

        //        moveDirection.z = Input.GetAxis("Vertical") * speedZ;
        //    }
        //    else
        //    {
        //        moveDirection.z = 0.0f;
        //    }

        //    //���E�L�[�iHorizontal�j�ɂĉ�]�����閽��
        //    transform.Rotate(0, Input.GetAxis("Horizontal") * 2, 0);//���ȏ���(3,0)

        //    if (Input.GetButton("Jump"))
        //    {
        //        moveDirection.y = speedJump;
        //        //Jump�A�j���̔���
        //        animator.SetTrigger("jump");
        //    }
        //}
        //--------------------------------------

        //�d�͕��̗͂𖈃t���[���ǉ�(�ǂ�ǂ�d���Ȃ�)
        //* Time.deltaTime�͂ǂ�PC�ł������X�s�[�h�œ����悤�ɂ��邽�߂̕␳
        moveDirection.y -= gravity * Time.deltaTime;

        //�ړ����s
        //* Time.deltaTime�͂ǂ�PC�ł������X�s�[�h�œ����悤�ɂ��邽�߂̕␳
        //�L�����̌������΂߂����Ƃ��ɁA���[�J�����W���O���[�o�����W�ɒu��������
        //���ꂵ�Ȃ��Ǝ΂ߏ�Ԃŏ��Z�ɍs��
        Vector3 globalDirection = transform.TransformDirection(moveDirection);
        //Move���\�b�h�F�w�肵��������̍��W�Ƀv���C���[�𓮂���
        controller.Move(globalDirection * Time.deltaTime);

        //�ړ���ڒn���Ă���Y�����̑��x�̓��Z�b�g����
        if (controller.isGrounded) moveDirection.y = 0;

        //���x��0�ȏ�Ȃ�0�ȏ�Ȃ瑖���Ă���t���O��true�ɂ���
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
