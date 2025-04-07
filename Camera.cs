using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow2D : MonoBehaviour
{
    public Transform target; // 카메라가 따라다닐 대상 
    public float smoothSpeed = 0.125f; // 카메라의 이동 속도
    public Vector3 offset; // 카메라와 캐릭터 사이의 거리
    private Vector3 lastTargetPosition; // 이전 프레임에서의 플레이어 위치
    public float minBackgroundPositionX;
    public float maxBackgroundPositionX;

    void LateUpdate()
    {
        if (target != null)
        {
            // 플레이어의 움직임에 따라 카메라 위치를 조정
            Vector3 desiredPosition = target.position + offset;
            desiredPosition.z = transform.position.z; // 카메라의 Z 위치를 유지

            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;

            // 플레이어가 움직였을 때만 배경을 이동
            if (target.position != lastTargetPosition)
            {
                Vector3 moveAmount = target.position - lastTargetPosition;

                // 배경의 시작과 끝 지점을 벗어나지 않도록 제한
                float newX = Mathf.Clamp(transform.position.x + moveAmount.x, minBackgroundPositionX, maxBackgroundPositionX);
                transform.position = new Vector3(newX, transform.position.y, transform.position.z);
            }

            // 이전 프레임에서의 플레이어 위치 업데이트
            lastTargetPosition = target.position;
        }
    }
}
