using System;
using System.Collections;
using UnityEngine;


[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(Rigidbody))]

public class Cube : MonoBehaviour
{
    [SerializeField] private Color _standartColor;
    [SerializeField] private float _minTimeDie;
    [SerializeField] private float _maxTimeDie;

    private MeshRenderer _meshRenderer;
    private Rigidbody _rigidbody;
    private bool _isColorChanged = false;
    private Coroutine _coroutine = null;
    private WaitForSeconds _sleepTime;

    public event Action<Cube> ReturnedPool;

    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _rigidbody = GetComponent<Rigidbody>();
    }
    private void OnEnable()
    {
        _meshRenderer.material.color = _standartColor;
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
            _coroutine = null;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (_isColorChanged == false)
        {
            if (collision.gameObject.TryGetComponent<Platform>(out Platform platform))
            {
                _meshRenderer.material.color = UnityEngine.Random.ColorHSV();
                _isColorChanged = true;
                _sleepTime = new WaitForSeconds(UnityEngine.Random.Range(_minTimeDie, _maxTimeDie));

                if (_coroutine == null)
                {
                    _coroutine = StartCoroutine(DeleteDelay());
                }
            }
        }
    }
    private IEnumerator DeleteDelay()
    {
        yield return _sleepTime;
        ReturnedPool?.Invoke(this);
    }
}
