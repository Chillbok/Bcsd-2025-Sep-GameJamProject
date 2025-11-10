using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.Animations;

public class PlayerCombatController : MonoBehaviour
{
    private Animator animator;
    private PlayerMoveController moveController;
    private Rigidbody2D rb;

    public float dashForce = 20f;
    public float dashDuration = 0.5f; // 대시 후 입력 무시 시간

    [Header("일반 공격 관련 데이터")]
    public List<PlayerMeleeData> MeleeDatas;
    private int meleeComboCounter = 0;
    private float lastAttackTime = 0f;

    //전투 관련 상태
    public bool isDashing = false;
    private bool isAttacking = false;

    //전투 행동 조절하는 bool 값들
    public bool canDash = true;
    public bool canAttack = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        moveController = GetComponent<PlayerMoveController>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isDashing || isAttacking)
        {
            return; // 대시 또는 공격 중에는 입력을 무시
        }

        if (canAttack == true && Input.GetMouseButtonDown(0))
        {
            if (meleeComboCounter == 0) { StartCoroutine(Attack(MeleeDatas[0])); }
            else if (meleeComboCounter == 1) { StartCoroutine(Attack(MeleeDatas[1])); }
        }

        if (canDash == true && Input.GetMouseButtonDown(1))
        {
            StartCoroutine(Dash());
        }
    }

    //대쉬 행동
    IEnumerator Dash()
    {
        canDash = false;
        animator.SetTrigger("dash");
        float dashDirection = moveController.FacingRight ? 1 : -1;
        rb.AddForce(new Vector2(dashDirection * dashForce, 0), ForceMode2D.Impulse);

        yield return new WaitForSeconds(dashDuration);

        canDash = true;
    }

    //공격 행동
    IEnumerator Attack(PlayerMeleeData meleeData)
    {
        //콤보 인식 시간 동안 공격 버튼을 눌렀는가?
        bool pressedAttack = false;

        canAttack = false;
        moveController.canMove = false;
        animator.SetTrigger("attack");

        //애니메이션 지속시간동안 멈추기
        yield return new WaitForSeconds(meleeData.animationClip.length - meleeData.comboTimeBeforeAnimationEnd);

        //애니메이션 끝나기 전 콤보 입력 가능 시간 동안 실행할 코드들
        if (Input.GetMouseButton(0)) { pressedAttack = true; }

        //애니메이션 끝남, 콤보 입력 가능 시간 기다리기
        yield return new WaitForSeconds(meleeData.comboTimeBeforeAnimationEnd);

        //애니메이션 끝난 후 콤보 입력 가능 시간 동안 실행할 코드들
        if (Input.GetMouseButton(0)) { pressedAttack = true; }

        //콤보 입력 가능 시간 끝난 후 실행할 코드들
        if (pressedAttack == false || (pressedAttack == true && meleeComboCounter == 2)) { meleeComboCounter = 0; }

        //플레이어 다시 움직일 수 있도록 상태 변환
        moveController.canMove = true;
        canAttack = true;
    }
}
