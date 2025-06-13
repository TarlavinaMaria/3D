using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(MeshRenderer))]

public class Cube4 : MonoBehaviour, IPointerClickHandler
{

    private int _maxCube = 7; // максимальное количество кубов
    private int _minCube = 2; // минимальное количество кубов
    private int _maxChanceDevine = 101; // шанс разделения 
    private int _countDevide = 0; // количество разделенных кубов
    private Cube4 _tempCube; // временный куб
    private Material _materialCube; // материал
    private MeshRenderer _meshRenderer;

    public int _chanceDevine { get; private set; } = 100; // шанс разделения

    public void OnPointerClick(PointerEventData eventData)
    {
        if (Random.Range(0, _maxChanceDevine) < _chanceDevine)
        {
            _countDevide = Random.Range(_minCube, _maxCube);
            for (int i = 0; i < _countDevide; i++)
            {
                _tempCube = Instantiate(this);
            }
        }
        Destroy(this.gameObject);
    }
    private void Start()
    {
        _meshRenderer = GetComponent<MeshRenderer>(); //
        _materialCube = _meshRenderer.material; // 
        _materialCube.color = Random.ColorHSV(); //
        _meshRenderer.material = _materialCube;
    }


}
