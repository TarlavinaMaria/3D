using System.Collections;
using UnityEngine;

public class Spavner : MonoBehaviour
{
    [SerializeField] private Transform _startPoint;  // Одна граница спавна
    [SerializeField] private Transform _envPoint;   // Противоположная граница спавна
    [SerializeField] private float _spavnTime;      // Интервал между спавном кубов
    [SerializeField] private float _spavnPositionY; // Фиксированная высота спавна
    [SerializeField] private ObjectPool _objectPool; // Ссылка на пул объектов

    private Cube _tempCube; // Временная ссылка на куб

    private void Start()
    {
        // Запускаем корутину спавна при старте
        StartCoroutine(Spavn());
    }

    private IEnumerator Spavn()
    {
        // Создаем объект ожидания один раз для оптимизации
        WaitForSeconds wait = new WaitForSeconds(_spavnTime);

        // Бесконечный цикл спавна
        while (enabled)
        {
            // Получаем куб из пула
            _tempCube = _objectPool.GetCube();
            // Активируем куб
            _tempCube.gameObject.SetActive(true);
            // Устанавливаем случайную позицию в заданных границах
            _tempCube.transform.position = new Vector3(
                Random.Range(_envPoint.position.x, _startPoint.position.x),
                _spavnPositionY,
                Random.Range(_envPoint.position.z, _startPoint.position.z));
            // Подписываемся на событие возврата куба
            _tempCube.ReturnedPool += ReturnPool;
            // Ждем указанное время
            yield return wait;
        }
    }

    private void ReturnPool(Cube cube)
    {
        // Отписываемся от события
        cube.ReturnedPool -= ReturnPool;
        // Возвращаем куб в пул
        _objectPool.PutCube(cube);
        // Деактивируем куб
        cube.gameObject.SetActive(false);
    }
}