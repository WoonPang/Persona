using System.Collections;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerCombatController : MonoBehaviour
{
    private PlayerMoveController moveController;
    private Animator anim;

    [Header("Attack Settings")]
    public float attackRange = 1.5f;   // 공격 레이 범위
    public LayerMask enemyLayer;       // Enemy Layer 설정

    [Header("Direction")]
    public Transform[] attackPoint; // 캐릭터 앞 위치에 빈 오브젝트 하나 넣기
    private Transform attackStartPoint;

    public bool isDashing = false;
    public float dashForce = 20f;
    public float dashDuration = 0.5f;

    private void Start()
    {
        anim = GetComponent<Animator>();
        moveController = GetComponent<PlayerMoveController>();
    }

    void Update()
    {
        SetAttackDirec();

        if (Input.GetButtonDown("Fire1")) // 공격 키
        {
            anim.SetTrigger("Attack");
        }

        DebugRay();
    }

    void DebugRay()
    {
       Debug.DrawRay(attackStartPoint.position, transform.right* moveController.lastDirection * attackRange, Color.red);
    }

    void SetAttackDirec()
    {
        if (moveController.lastDirection == 1) attackStartPoint = attackPoint[0];
        else attackStartPoint = attackPoint[1];
    }

    void Attack()
    {
        RaycastHit2D hit = Physics2D.Raycast(attackStartPoint.position, transform.right * moveController.lastDirection, attackRange, enemyLayer);

        if (hit.collider != null)
        {
            Debug.Log("Attack Sucess");
        }
    }
}
