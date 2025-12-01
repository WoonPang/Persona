/*******************************************************
 * Project : Persona
 * Filename : EnemyBase.cs
 * Author : SkylightStudio07
 * Contributor :
 * Created Date : 2025-11-19
 * 
 * Description  : 
 *      - damage 팝업... 그러니까 적 피격시킬 때 나오는 숫자 처리하는 클래스.\
 *      - 기본적으로 메이플스토리 스타일
 *      - 엄... 치명타일 때 크기 좀 키우고 색깔 바꾸는 정도로?
 * Update Log : 
 *      2025-11-19 : 첫 작성
 *******************************************************/

using UnityEngine;
using TMPro;

public class DamagePopup : MonoBehaviour
{
    [SerializeField] private TextMeshPro textMesh; // 데미지 텍스트
    [SerializeField] private float lifeTime = 0.5f; // 팝업 지속 시간
    [SerializeField] private Vector3 moveSpeed = new Vector3(0f, 2f, 0f); // 팝업 이동 속도
    [SerializeField] private float criticalScale = 1.3f; // 치명타일 때 크기 배율

    private float timer; // 팝업 타이머. 이용시에 주의

    public void Setup(int amount, bool isCritical) 
    {
        if (textMesh == null)
            textMesh = GetComponentInChildren<TextMeshPro>(); // 혹시 몰라서 초기화하는데 퍼포먼스에 문제 생기면 제거하는게 나음

        textMesh.text = amount.ToString();

        if (isCritical)
        {
            textMesh.fontSize *= criticalScale;
            // 색 바꾸고 싶으면 여기서 textMesh.color = ...; 이런 식으로 하믄 될 듯
            // 폰트는 나중에 생각해봅시다
        }

        timer = lifeTime;
    }

    private void Update()
    {
        transform.position += moveSpeed * Time.deltaTime; // 텍스트 자체는 위로 이동시킴. 메이플처럼...

        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            Destroy(gameObject); // 아직 오브젝트 풀링은 미구현
        }
    }
}
