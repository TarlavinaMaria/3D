using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]

public class Cube4 : MonoBehaviour
{
    private int _maxCube = 7; // максимальное количество создаваемых кубов
    private int _minCube = 2; // минимальное количество создаваемых кубов
    private int _maxChanceDevine = 101; // максимальный шанс разделения
    private int _countDevide = 0; // количество создаваемых новых кубов
    private Cube4 _tempCube; // временный объект нового куба
    private Material _materialCube; // материал куба (для цвета)
    private MeshRenderer _meshRenderer; // компонент рендеринга куба

    public int _chanceDevine { get; private set; } = 100; // начальный шанс разделения (100%)


    private void OnMouseDown()
    {
        if (Random.Range(0, _maxChanceDevine) < _chanceDevine)
        {
            _countDevide = Random.Range(_minCube, _maxCube);// случайное количество кубов
            for (int i = 0; i < _countDevide; i++)
            {
                _tempCube = Instantiate(this, transform.position, Quaternion.identity); // создаем новый куб на месте текущего
                _tempCube.transform.localScale = transform.localScale * 0.5f; // Уменьшаем размер
                _tempCube._chanceDevine = _chanceDevine / 2; // Уменьшаем шанс разделения
            }
        }
        Destroy(this.gameObject); // Удаляем текущий куб
    }
    private void Start()
    {
        _meshRenderer = GetComponent<MeshRenderer>(); // Получаем компонент рендеринга
        _materialCube = _meshRenderer.material; // Получаем материал куба
        _materialCube.color = Random.ColorHSV(); //Случайный цвет
        _meshRenderer.material = _materialCube; // Применяем материал к рендереру
        // Добовляем физику к кубу
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.useGravity = true;
    }


}
