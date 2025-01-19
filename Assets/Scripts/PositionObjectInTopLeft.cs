using UnityEngine;

public class PositionObjectInTopLeft : MonoBehaviour
{
    public Camera orthoCamera;     // 正交摄像机
    public Vector2 offset;         // 偏移量（单位为世界坐标）

    void Start()
    {
        if (orthoCamera == null)
        {
            orthoCamera = Camera.main;
        }

        // 获取摄像机视口右上角的世界坐标
        Vector3 topRightCorner = orthoCamera.ViewportToWorldPoint(new Vector3(0, 1, orthoCamera.nearClipPlane));

        // 加上偏移量
        topRightCorner += new Vector3(offset.x, offset.y, 0);
        topRightCorner.z = 0;

        // 设置物体位置
        transform.position = topRightCorner;
    }
}
