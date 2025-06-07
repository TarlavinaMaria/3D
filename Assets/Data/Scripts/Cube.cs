using System;
using System.Collections;
using UnityEngine;

// Класс куба в игре
[RequireComponent(typeof(MeshRenderer))] // Обязательный компонент для отображения куба
[RequireComponent(typeof(Rigidbody))]    // Обязательный компонент для физического поведения

public class Cube : MonoBehaviour
{
    [SerializeField] private Color _standartColor;  // Стандартный цвет куба
    [SerializeField] private float _minTimeDie;    // Минимальное время до исчезновения
    [SerializeField] private float _maxTimeDie;    // Максимальное время до исчезновения

    private MeshRenderer _meshRenderer;  // Компонент для изменения внешнего вида
    private Rigidbody _rigidbody;       // Компонент физики
    private bool _isColorChanged = false; // Флаг, менял ли куб цвет
    private Coroutine _coroutine = null;  // Ссылка на запущенную корутину
    private WaitForSeconds _sleepTime;    // Время ожидания перед исчезновением

    // Событие, вызываемое при возврате куба в пул
    public event Action<Cube> ReturnedPool;

    private void Awake()
    {
        // Получаем ссылки на компоненты при создании объекта
        _meshRenderer = GetComponent<MeshRenderer>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        // При активации объекта:
        // Устанавливаем случайное время жизни
        _sleepTime = new WaitForSeconds(UnityEngine.Random.Range(_minTimeDie, _maxTimeDie));
        // Сбрасываем флаг изменения цвета
        _isColorChanged = false;
        // Возвращаем стандартный цвет
        _meshRenderer.material.color = _standartColor;
        // Останавливаем предыдущую корутину, если она была запущена
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
            _coroutine = null;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // При столкновении:
        if (_isColorChanged == false) // Если цвет еще не менялся
        {
            if (collision.gameObject.TryGetComponent<Platform>(out Platform platform))
            {
                // Меняем цвет на случайный
                _meshRenderer.material.color = UnityEngine.Random.ColorHSV();
                // Устанавливаем флаг, что цвет изменен
                _isColorChanged = true;
                // Запускаем корутину исчезновения
                //if (_coroutine == null)
                //{
                _coroutine = StartCoroutine(DeleteDelay());
                //}
            }
        }
    }

    private IEnumerator DeleteDelay()
    {
        // Ждем установленное время
        yield return _sleepTime;
        // Сообщаем подписчикам (спавнеру), что куб можно вернуть в пул
        ReturnedPool?.Invoke(this);
    }
}