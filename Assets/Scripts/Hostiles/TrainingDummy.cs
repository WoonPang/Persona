/*******************************************************
 * Project : Persona
 * Filename : TrainingDummy.cs
 * Author : SkylightStudio07
 * Contributor :
 * Created Date : 2025-11-19
 * 
 * Description  : 
 *      - 프로토타입 공격 및 피격 구현용 더미
 *      - 무한 체력 및 위치 고정 옵션 제공
 * Update Log : 
 *      2025-11-19 : 첫 작성
 *******************************************************/

using UnityEngine;

public class TrainingDummy : EnemyBase
{
    [SerializeField] private bool infiniteHealth = true; // 무한 체력 옵션
    [SerializeField] private bool freezePosition = true; // 위치 고정 옵션

    private Rigidbody2D rb;

    protected override void Awake()
    {
        base.Awake();

        rb = GetComponent<Rigidbody2D>();

        if (freezePosition && rb != null)
            rb.constraints = RigidbodyConstraints2D.FreezeAll; 

        if (infiniteHealth)
        {
            currentHp = maxHp;
            isDead = false;
        }
    }

    protected override void ApplyDamage(int damage)
    {
        if (infiniteHealth)
        {
            currentHp -= damage;
            if (currentHp <= 0)
                currentHp = maxHp;
        }
        else
        {
            base.ApplyDamage(damage);
        }
    }

    protected override bool IsDead()
    {
        if (infiniteHealth) return false;
        return base.IsDead();
    }

    protected override void Die()
    {
        if (infiniteHealth) return;
        base.Die();
    }
}
