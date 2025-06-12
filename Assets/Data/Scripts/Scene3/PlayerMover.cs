using UnityEngine;

// Обязательные компоненты для объекта
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(CharacterController))]


public class PlayerMover : MonoBehaviour
{
    // Константы
    private const string Horizontal = "Horizontal";
    private const string Vertical = "Vertical";

    // Переменные
    private float _speed = 10; // Скорость
    private CharacterController _characterController; // Контроллер
    private float _directionHorizontal; // Направление по горизонтали
    private float _directionVertical; // Направление по вертикали
    private Vector3 _move;// Вектор движения

    private void Awake()
    {
        // Подтягиваем компонент контроллера движения персонажа
        _characterController = GetComponent<CharacterController>();
    }

    private void FixedUpdate()
    {
        // Получаем направление по вертикали, которое вводит игрок
        _directionVertical = Input.GetAxis(Vertical);
        _directionHorizontal = Input.GetAxis(Horizontal);
        // Вычисляем вектор движения
        _move = transform.forward * _directionVertical + transform.right * _directionHorizontal;
        // Передаем вектор движения контроллеру, двигаем персонажа
        _characterController.Move(_move * _speed * Time.deltaTime);
    }
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        // Если столкнулись с объектом Domino
        if (hit.collider.TryGetComponent<Domino>(out Domino domino))
        {
            Vector3 pushDirection = hit.moveDirection;
            domino.Push(pushDirection, 3f); // Сила 3
        }
    }

}
