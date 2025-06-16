using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]

public class Cube4 : MonoBehaviour
{
    private int _maxCube = 7; // максимальное количество кубов
    private int _minCube = 2; // минимальное количество кубов
    private int _maxChanceDevine = 101; // шанс разделения 
    private int _countDevide = 0; // количество разделенных кубов
    private Cube4 _tempCube; // временный куб
    private Material _materialCube; // материал
    private MeshRenderer _meshRenderer;

    public int _chanceDevine { get; private set; } = 100; // шанс разделения

    private void OnMouseDown()
    {
        if (Random.Range(0, _maxChanceDevine) < _chanceDevine)
        {
            _countDevide = Random.Range(_minCube, _maxCube);
            for (int i = 0; i < _countDevide; i++)
            {
                //_tempCube = Instantiate(this);

                _tempCube = Instantiate(this, transform.position, Quaternion.identity);
                _tempCube.transform.localScale = transform.localScale * 0.5f; // Уменьшаем размер
                _tempCube._chanceDevine = _chanceDevine / 2; // Уменьшаем шанс разделения
            }
        }
        Destroy(this.gameObject);
    }
    private void Start()
    {
        _meshRenderer = GetComponent<MeshRenderer>(); //
        _materialCube = _meshRenderer.material; // 
        _materialCube.color = Random.ColorHSV(); //Случайный цвет
        _meshRenderer.material = _materialCube;

        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody>();
        }
        rb.useGravity = true;
    }


}
