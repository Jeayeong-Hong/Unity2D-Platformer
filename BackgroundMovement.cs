using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMovement : MonoBehaviour
{
    public Transform target; // 카메라가 따라다닐 대상 (주인공 캐릭터)
    public float parallaxEffectMultiplier = 0.5f; // 배경의 움직임에 대한 배율

    private Vector3 lastTargetPosition; // 이전 프레임에서의 플레이어 위치

    void Start()
    {
        lastTargetPosition = target.position;
    }

    void Update()
    {
        if (target != null)
        {
            Vector3 moveAmount = target.position - lastTargetPosition;
            transform.position += new Vector3(moveAmount.x * parallaxEffectMultiplier, moveAmount.y * parallaxEffectMultiplier, 0f);
        }

        lastTargetPosition = target.position;
    }
}
