using System.Collections;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private PlayerCombatController playerCombatController;
    private PlayerMoveController playerMoveController;

    [SerializeField] GameManager gameManager;
    private Animator animator;
    public float currentHP;
    public bool isPlayerDead = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerCombatController = GetComponent<PlayerCombatController>();
        playerMoveController = GetComponent<PlayerMoveController>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (currentHP <= 0) {Death();}
    }

    void GetDamage(float damage)
    {
        bool isDashing = playerCombatController.isDashing;
        if (!isDashing)
        {
            animator.SetTrigger("getDamage");
            currentHP -= damage;
            if (currentHP <= 0)
            {
                currentHP = 0;
                Death();
            }
        }
    }

    void PlayerCantAct()
    {
        playerMoveController.canMove = false;
        
        playerCombatController.canDash = false;
        playerCombatController.canAttack = false;
    }

    //플레이어 죽으면 일어나는 일
    void Death()
    {
        if (isPlayerDead) return;
        gameManager.isGameOver = true;
        isPlayerDead = true;
        StartCoroutine(DeathCoroutine());
    }

    IEnumerator DeathCoroutine()
    {
        PlayerCantAct();
        animator.SetTrigger("death");
        yield return null;
    }
}
