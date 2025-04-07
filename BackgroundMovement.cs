using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMovement : MonoBehaviour
{
    public Transform target; // ī�޶� ����ٴ� ��� (���ΰ� ĳ����)
    public float parallaxEffectMultiplier = 0.5f; // ����� �����ӿ� ���� ����

    private Vector3 lastTargetPosition; // ���� �����ӿ����� �÷��̾� ��ġ

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
