/*******************************************************
 * Project : Persona
 * Filename : DamageInfo.cs
 * Author : SkylightStudio07
 * Contributor :
 * Created Date : 2025-11-19
 * 
 * Description  : 
 *      - 적 피격 관련 정보를 담는 구조체.
 *      - 피격 타입(평타, 치명타)과 피격 위치 정보를 포함.
 *      - 데미지 정보 전달용으로 사용.
 *      - 몬헌식으로 생각해봤는데... hitpoint는 일단 나중에 생각하는 게 나을 듯.
 *
 * Update Log : 
 *      2025-11-19 : 첫 작성
 *******************************************************/

using UnityEngine;

public enum HitType
{
    Normal,
    Critical
}

public struct DamageInfo
{
    public int amount;
    public HitType hitType;
    public Vector2 hitPoint;

    // 보스 특정 부위 피격... 몬헌처럼.
    // hitpoint는 일단 나중에 생각하는 게 나을 듯.
    public DamageInfo(int amount, HitType hitType, Vector2 hitPoint)
    {
        this.amount = amount;
        this.hitType = hitType;
        this.hitPoint = hitPoint;
    }
}