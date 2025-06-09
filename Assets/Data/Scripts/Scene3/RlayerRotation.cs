using UnityEngine;

public class RlayerRotation : MonoBehaviour
{
    private const string LineRotationY = "Mouse Y";
    private const string LineRotationX = "Mouse X";

    private float _sensitivity = 15; // чувствительность мыши
    // максимальный и минимальный угол поворота камеры
    private float _minRotation = -45;
    private float _maxRotation = 45;
    private float _mouseY;
    private float _mouseX;
    private float _rotationX;
    private void Start()
    {
        Cursor.visible = false; // скрываем курсор
        Cursor.lockState = CursorLockMode.Locked;
    }
    private void Update()
    {
        // Получаем направление по вертикали, которое вводит игрок
        _mouseX = Input.GetAxis(LineRotationX);
        _mouseY = Input.GetAxis(LineRotationY);

        // Поворачиваем камеру по горизонтали
        transform.parent.Rotate(Vector3.up * _mouseX * _sensitivity * Time.deltaTime);

        // Поворачиваем камеру по вертикали
        _rotationX -= _mouseY * _sensitivity * Time.deltaTime;
        _rotationX = Mathf.Clamp(_rotationX, _minRotation, _maxRotation);
        transform.localRotation = Quaternion.Euler(_rotationX, 0f, 0f);
    }
}
