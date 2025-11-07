using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private PlayerCombatController playerCombatController;
    private Animator animator;
    public float currentHP;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerCombatController = GetComponent<PlayerCombatController>();
        animator = GetComponent<Animator>();
    }

    void GetDamage(float damage)
    {
        bool isDashing = playerCombatController.isDashing;
        if (!isDashing)
        {
            animator.SetTrigger("getDamage");
            currentHP -= damage;
            if (currentHP <= 0) {currentHP = 0;}
        }
    }
}
