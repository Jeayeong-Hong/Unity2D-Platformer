using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow2D : MonoBehaviour
{
    public Transform target; // ī�޶� ����ٴ� ��� 
    public float smoothSpeed = 0.125f; // ī�޶��� �̵� �ӵ�
    public Vector3 offset; // ī�޶�� ĳ���� ������ �Ÿ�
    private Vector3 lastTargetPosition; // ���� �����ӿ����� �÷��̾� ��ġ
    public float minBackgroundPositionX;
    public float maxBackgroundPositionX;

    void LateUpdate()
    {
        if (target != null)
        {
            // �÷��̾��� �����ӿ� ���� ī�޶� ��ġ�� ����
            Vector3 desiredPosition = target.position + offset;
            desiredPosition.z = transform.position.z; // ī�޶��� Z ��ġ�� ����

            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;

            // �÷��̾ �������� ���� ����� �̵�
            if (target.position != lastTargetPosition)
            {
                Vector3 moveAmount = target.position - lastTargetPosition;

                // ����� ���۰� �� ������ ����� �ʵ��� ����
                float newX = Mathf.Clamp(transform.position.x + moveAmount.x, minBackgroundPositionX, maxBackgroundPositionX);
                transform.position = new Vector3(newX, transform.position.y, transform.position.z);
            }

            // ���� �����ӿ����� �÷��̾� ��ġ ������Ʈ
            lastTargetPosition = target.position;
        }
    }
}
