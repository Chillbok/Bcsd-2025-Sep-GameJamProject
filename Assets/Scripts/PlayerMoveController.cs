using UnityEngine;

public class PlayerMoveController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f; // 점프 힘
    public bool FacingRight { get; private set; } = true; //플레이어가 바라보는 방향이 오른쪽인가?
    public bool canMove = true;


    private Rigidbody2D rb;
    private Animator animator; // Animator 컴포넌트 참조

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>(); // Animator 컴포넌트 초기화
    }

    void Update()
    {
        //플레이어 이동 변수들
        float moveX = Input.GetAxis("Horizontal");

        if (!canMove)
        {
            moveX = 0;
        }

        // isGround와 isSit이 모두 true일 때 좌우 이동을 막음
        if (animator != null && animator.GetBool("isGround") && animator.GetBool("isSit"))
        {
            moveX = 0;
        }
        
        // isMoving 애니메이터 파라미터 설정
        if (animator != null)
        {
            animator.SetBool("isMoving", moveX != 0);

            // 'S' 키를 눌렀을 때 isSit을 true로 설정
            if (Input.GetKeyDown(KeyCode.S))
            {
                animator.SetBool("isSit", true);
            }
            // 'S' 키에서 손을 떼면 isSit을 false로 설정
            if (Input.GetKeyUp(KeyCode.S))
            {
                animator.SetBool("isSit", false);
            }

            // isJumping 애니메이터 파라미터 설정
            if (!animator.GetBool("isGround"))
            {
                if (rb.linearVelocity.y > 0)
                {
                    animator.SetInteger("isJumping", 1);
                }
                else if (rb.linearVelocity.y < 0)
                {
                    animator.SetInteger("isJumping", -1);
                }
            }
            else
            {
                animator.SetInteger("isJumping", 0);
            }
        }
        
        Vector2 movement = new Vector2(moveX, 0); // y축 이동은 점프로 처리하므로 0으로 설정
        transform.position += (Vector3)movement * moveSpeed * Time.deltaTime;

        // 점프
        if (animator.GetBool("isGround") && Input.GetButtonDown("Jump") && canMove)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

        //플레이어 좌우 이동 방향에 따라 플레이어 뒤집기
        if (moveX > 0 && !FacingRight)
        {
            Flip();
        }
        else if (moveX < 0 && FacingRight)
        {
            Flip();
        }
    }

    //플레이어 뒤집는 메서드
    private void Flip()
    {
        FacingRight = !FacingRight;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }

    // 바닥 감지
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground")) // 바닥으로 사용할 오브젝트에 "Ground" 태그를 추가해야 합니다.
        {
            if (animator != null)
            {
                animator.SetBool("isGround", true);
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            if (animator != null)
            {
                animator.SetBool("isGround", false);
            }
        }
    }
}
