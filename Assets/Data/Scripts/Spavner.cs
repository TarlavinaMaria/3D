using System.Collections;
using UnityEngine;

public class Spavner : MonoBehaviour
{
    [SerializeField] private Transform _startPoint;
    [SerializeField] private Transform _envPoint;
    [SerializeField] private float _spavnTime;
    [SerializeField] private float _spavnPositionY;
    [SerializeField] private ObjectPool _objectPool;

    private Cube _tempCube;
    private void Start()
    {
        StartCoroutine(Spavn());
    }
    private IEnumerator Spavn()
    {
        WaitForSeconds wait = new WaitForSeconds(_spavnTime);
        while (enabled)
        {
            _tempCube = _objectPool.GetCube();
            _tempCube.gameObject.SetActive(true);
            _tempCube.transform.position = new Vector3(Random.Range(_envPoint.position.x, _startPoint.position.x), _spavnPositionY, Random.Range(_envPoint.position.z, _startPoint.position.z));
            _tempCube.ReturnedPool += ReturnPool;
            yield return wait;
        }
    }
    private void ReturnPool(Cube cube)
    {
        cube.ReturnedPool -= ReturnPool;
        _objectPool.PutCube(cube);
        _tempCube.gameObject.SetActive(false);
    }
}
