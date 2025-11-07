using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public float enemyHP;
    void Start()
    {
        
    }

    void Update()
    {
        if (enemyHP <= 0) {Death();}
    }

    void GetDamage(float damage)
    {

    }

    void Death()
    {
        Destroy(gameObject);
    }
}
