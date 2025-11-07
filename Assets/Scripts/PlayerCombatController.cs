using System.Collections;
using UnityEngine;

public class PlayerCombatController : MonoBehaviour
{
    private Animator animator;
    private PlayerMoveController moveController;
    private Rigidbody2D rb;

    public float dashForce = 20f;
    public float dashDuration = 0.5f; // 대시 후 입력 무시 시간
    public float attackDuration = 0.5f; // 공격 후 입력 무시 시간
    public bool isDashing = false;
    private bool isAttacking = false;

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

        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine(Attack());
        }

        if (Input.GetMouseButtonDown(1))
        {
            StartCoroutine(Dash());
        }
    }

    IEnumerator Dash()
    {
        isDashing = true;
        animator.SetTrigger("dash");
        float dashDirection = moveController.FacingRight ? 1 : -1;
        rb.AddForce(new Vector2(dashDirection * dashForce, 0), ForceMode2D.Impulse);

        yield return new WaitForSeconds(dashDuration);

        isDashing = false;
    }

    IEnumerator Attack()
    {
        isAttacking = true;
        moveController.canMove = false;
        animator.SetTrigger("attack");

        yield return new WaitForSeconds(attackDuration);

        moveController.canMove = true;
        isAttacking = false;
    }
}
