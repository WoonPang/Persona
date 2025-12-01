/*******************************************************
 * Project : Persona
 * Filename : EnemyBase.cs
 * Author : SkylightStudio07
 * Contributor :
 * Created Date : 2025-11-19
 * 
 * Description  : 
 *      - 적 관련 기본 클래스 및 최상위 클래스.
 *      - 기본적으로 적대적 행동을 하는 모든 적은 여기서부터 상속받도록 설계
 *      - 피격 관련도 여기서 처리
 *      - 피격 이펙트, 데미지 팝업 등도 여기서 처리
 *
 * Update Log : 
 *      2025-11-19 : 첫 작성
 *******************************************************/

using UnityEngine;

public abstract class EnemyBase : MonoBehaviour, IHitReceiver
{
    [Header("Stats")]
    [SerializeField] protected int maxHp = 100;
    [SerializeField] protected int currentHp;

    /* 컴포넌트 */
    /* 피격 이펙트도 일단 스프라이트렌더러로 처리하는 방향으로 생각해봐야 될 듯 */

    [Header("Components")]
    [SerializeField] protected Animator animator;
    [SerializeField] protected SpriteRenderer spriteRenderer;

    /* 피격 이펙트 */
    /* 치명타로 들어왔을 때 피격 이펙트도 달라지나? */

    [Header("Effects")]
    [SerializeField] protected GameObject hitEffectPrefab;
    [SerializeField] protected GameObject critHitEffectPrefab; 
    [SerializeField] protected DamagePopup damageNumPopupPrefab; // 텍스트 팝업용

    /* 사망처리 */

    protected bool isDead;

    /*
     * 
     * 고민을 좀 해 봤는데, 미리 해시로 만들어두는 게 나을 듯
     * 특히 피격 같은 경우 자주 발생하니까...
     * Animator.SetTrigger(string) 쓸 때마다 코스트가 조금 들긴 하니까 미리 바꿔놓기...
     * 문자열 파라미터 이름을 정수(int) 값으로 바꿔 저장하는 거
     * 
     */

    protected static readonly int HashHit = Animator.StringToHash("Hit"); 
    protected static readonly int HashDie = Animator.StringToHash("Die");

    protected virtual void Awake()
    {
        currentHp = maxHp;
        if (animator == null) animator = GetComponentInChildren<Animator>();
        if (spriteRenderer == null) spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    public virtual void ReceiveHit(DamageInfo damageInfo)
    {
        if (isDead) return;

        // 데미지 적용
        ApplyDamage(damageInfo.amount);

        // 피격 연출
        PlayHitAnimation();
        SpawnHitEffect(damageInfo);
        SpawnDamagePopup(damageInfo);

        // 사망 체크
        if (IsDead())
        {
            Die();
        }
    }

    protected virtual void ApplyDamage(int damage)
    {
        currentHp -= damage;
        if (currentHp < 0) currentHp = 0;
    }

    protected virtual bool IsDead()
    {
        return currentHp <= 0;
    }

    protected virtual void Die()
    {
        isDead = true;
        if (animator != null)
        {
            animator.SetTrigger(HashDie);
        }

        // 필요하다면 여기서 콜라이더 비활성화 처리 같은 거 하면 됨
        // GetComponent<Collider2D>().enabled = false;
    }

    // 피격 애니메이션 재생
    protected virtual void PlayHitAnimation()
    {
        if (animator != null)
        {
            animator.SetTrigger(HashHit);
        }
    }

    protected virtual void SpawnHitEffect(DamageInfo damageInfo)
    {
        if (damageInfo.hitType == HitType.Critical && critHitEffectPrefab != null) // 치명타 이펙트 우선
        {
            SpawnEffect(critHitEffectPrefab, damageInfo.hitPoint);
        }
        else if (hitEffectPrefab != null)
        {
            SpawnEffect(hitEffectPrefab, damageInfo.hitPoint);
        }
    }

    protected virtual void SpawnEffect(GameObject prefab, Vector2 position)
    {
        // 나중에 풀링으로 교체하려고 분리해놓은거
        if (prefab == null) return;
        Instantiate(prefab, position, Quaternion.identity);
    }

    protected virtual void SpawnDamagePopup(DamageInfo damageInfo) // 이건 좀 더 고민해 볼거
                                                                   // 데미지 팝업 생성
                                                                   // 치명타 여부에 따라 다르게 처리 가능하게
    {
        if (damageNumPopupPrefab == null) return;

        var popup = Instantiate(
            damageNumPopupPrefab,
            damageInfo.hitPoint,
            Quaternion.identity
        );
        popup.Setup(damageInfo.amount, damageInfo.hitType == HitType.Critical);
    }
}
