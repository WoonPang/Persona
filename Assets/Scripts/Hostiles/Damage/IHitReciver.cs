/*******************************************************
 * Project : Persona
 * Filename : IHitReciver.cs
 * Author : SkylightStudio07
 * Contributor :
 * Created Date : 2025-11-19
 * 
 * Description  : 
 *      - 피격받는 객체의 인터페이스.
 *      - ReceiveHit 메서드를 통해 데미지 정보를 전달받음
 *      - 써먹기 힘들어보이네
 *
 * Update Log : 
 *      2025-11-19 : 첫 작성
 *******************************************************/

using UnityEngine;

public interface IHitReceiver
{
    void ReceiveHit(DamageInfo damageInfo);
}

// 차후 사용 예시

/*
 // PlayerAttack에서 쓴다 치면

    - 플레이어 공격이 레이캐스트면
        RaycastHit2D hit = Physics2D.Raycast(...);
        IHitReceiver receiver = hit.collider.GetComponent<IHitReceiver>();
        if (receiver != null)
        {
            DamageInfo info = new DamageInfo(
                20,
                HitType.Normal,
                hit.point
            );
            receiver.ReceiveHit(info);
        }
    - 플레이어 공격이 충돌체(Overlapbox 등) 기반이면
    public int damage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IHitReceiver receiver = collision.GetComponent<IHitReceiver>();

        if (receiver != null)
        {
            var info = new DamageInfo(damage, HitType.Normal, collision.transform.position);
            receiver.ReceiveHit(info);
        }
    }
        이런 식으로..
 */