/*
 * @file EntranceScript.cs
 * @brief 이 스크립트는 플레이어(또는 특정 태그를 가진 오브젝트)가 특정 오브젝트의 위치로 이동할 수 있도록 하는 것이 목적임
 */
using UnityEngine;

public class EntranceScript : MonoBehaviour
{
    [SerializeField] GameObject destinationObj;
    /*
    포탈 기능은 어쩌면 여러 가지 용도로 응용할 수 있겠다는 생각이 들어서 따로 지정할 수 있도록 함 
    그래도 일단 현재로서의 목적은 어디까지나 플레이어의 맵 이동이 목적이기 때문에 기본 지정을 플레이어의 태그인 "player"로 지정했음.
    */
    [SerializeField] string objTag = "player";
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(objTag))
        {
            // PortalEnterEffect();
            collision.transform.position = destinationObj.transform.position;
            // PortalExitEffect();
        }
    }

    /*
    포탈에서 나올때, 들어갈 때 사용할 이펙트 함수들은 여기에 나열함.
    이것은 기획 및 에셋이 필요한 부분이므로, 상의 후 추가해야 함.
    */
    //포탈에 입장하면 출력할 이펙트 함수
    private void PortalEnterEffect()
    {

    }

    //포탈에서 나오면 출력할 이펙트 함수
    private void PortalExitEffect()
    {

    }
}
