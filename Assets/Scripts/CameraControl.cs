using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 摄像机控制类
/// </summary>
public class CameraControl : MonoBehaviour
{
    #region Private Variable

    /// <summary>
    /// 世界坐标X轴与本地坐标X轴之间的夹角
    /// </summary>
    float angleOfWorldToLocalX;

    /// <summary>
    /// 世界坐标系Y轴与本地坐标系Y轴之间的夹角
    /// </summary>
    float angleOfWorldToLocalY;

    /// <summary>
    /// 摄像机距离观察点的目标距离
    /// </summary>
    float targetDistance;

    /// <summary>
    /// 摄像机距离观察点的当前距离
    /// </summary>
    float currentDistance;

    /// <summary>
    /// 摄像机的目标朝向
    /// </summary>
    Quaternion targetRotation;

	#endregion

	#region Public Variable

	/// <summary>
	/// 鼠标在X轴方向移动的灵敏度
	/// </summary>
	public float sensitivityOfX = 5f;

	/// <summary>
	/// 鼠标在Y轴方向移动的灵敏度
	/// </summary>
	public float sensitivityOfY = 5f;

	/// <summary>
	/// 滑轮转动的灵敏度
	/// </summary>
	public float sensitivityOfScrollWheel = 5f;

	/// <summary>
	/// 摄像机的旋转速度
	/// </summary>
	public float rotateSpeed = 10f;

	/// <summary>
	/// 摄像机的平移速度
	/// </summary>
	public float translateSpeed = 10f;

	/// <summary>
	/// 摄像机观察的目标
	/// </summary>
	public Transform lookTarget;

	/// <summary>
	/// X轴最小限位角度
	/// </summary>
	public float minLimitAngleX = -360f;

	/// <summary>
	/// X轴最大限位角度
	/// </summary>
	public float maxLimitAngleX = 360f;

	/// <summary>
	/// Y轴最小限位角度
	/// </summary>
	public float minLimitAngleY = -360f;

	/// <summary>
	/// Y轴最大限位角度
	/// </summary>
	public float maxLimitAngleY = 360f;

	/// <summary>
	/// 最小观察距离
	/// </summary>
	public float minLimitDistance = 2f;

	/// <summary>
	/// 最大观察距离
	/// </summary>
	public float maxLimitDistance = 10f;

	#endregion

	#region Private Method

	private void Start()
    {
        //计算世界坐标系与本地坐标系轴向的夹角
        angleOfWorldToLocalX = Vector3.Angle(Vector3.right, transform.right);
        angleOfWorldToLocalY = Vector3.Angle(Vector3.up, transform.up);
        //计算观察目标与摄像机的距离
        currentDistance = Vector3.Distance(lookTarget.position, transform.position);
        targetDistance = currentDistance;
    }

    //在Update方法之后，在每一帧被调用(防止摄像机比要移动的物体先移动，造成穿帮)
    private void LateUpdate()
    {
        if (Input.GetMouseButton(1))
        {
            //在鼠标移动与夹角变化之间建立映射关系
            angleOfWorldToLocalX += Input.GetAxis("Mouse X") * sensitivityOfX;
            angleOfWorldToLocalY -= Input.GetAxis("Mouse Y") * sensitivityOfY;
            //处理摄像机旋转角度的限位
            angleOfWorldToLocalX = Mathf.Clamp(angleOfWorldToLocalX,minLimitAngleX, maxLimitAngleX);
            angleOfWorldToLocalY = Mathf.Clamp(angleOfWorldToLocalY,minLimitAngleY, maxLimitAngleY);
            //计算目标朝向，将欧拉角转换为四元数
            targetRotation = Quaternion.Euler(angleOfWorldToLocalY, angleOfWorldToLocalX, 0);
            //将摄像机向目标朝向进行移动
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);
        }
        //建立滑轮移动与摄像机移动之间的映射关系
        targetDistance += Input.GetAxis("Mouse ScrollWheel")* sensitivityOfScrollWheel;
        //处理摄像机移动距离的限位
        targetDistance = Mathf.Clamp(targetDistance,minLimitDistance, maxLimitDistance);
        //计算摄像机与观察点的当前距离
        currentDistance = Mathf.Lerp(currentDistance,targetDistance, translateSpeed * Time.deltaTime);
        //更新摄像机的位置
        transform.position =lookTarget.position - transform.forward * currentDistance;
    }

    #endregion

  
}