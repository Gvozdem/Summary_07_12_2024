using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float minHorizontalAngle = -90f;
    public float maxHorizontalAngle = 90f;

    private Transform cameraTransform;

    void Start()
    {
        cameraTransform = Camera.main.transform;
    }

    void LateUpdate()
    {
        // Получаем текущий поворот камеры
        Vector3 cameraRotation = cameraTransform.eulerAngles;

        // Ограничиваем поворот по горизонтали
        cameraRotation.y = Mathf.Clamp(cameraRotation.y, minHorizontalAngle, maxHorizontalAngle);

        // Применяем новый поворот к камере
        cameraTransform.eulerAngles = cameraRotation;
    }
}