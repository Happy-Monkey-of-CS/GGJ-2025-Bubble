using UnityEngine;

public class PositionObjectInTopLeft : MonoBehaviour
{
    public Camera orthoCamera;     // ���������
    public Vector2 offset;         // ƫ��������λΪ�������꣩

    void Start()
    {
        if (orthoCamera == null)
        {
            orthoCamera = Camera.main;
        }

        // ��ȡ������ӿ����Ͻǵ���������
        Vector3 topRightCorner = orthoCamera.ViewportToWorldPoint(new Vector3(0, 1, orthoCamera.nearClipPlane));

        // ����ƫ����
        topRightCorner += new Vector3(offset.x, offset.y, 0);
        topRightCorner.z = 0;

        // ��������λ��
        transform.position = topRightCorner;
    }
}
