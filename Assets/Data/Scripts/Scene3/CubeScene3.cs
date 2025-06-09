using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class CubeScene3 : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }
    public void PrepereDrag()
    {
        _rigidbody.useGravity = false;
        _rigidbody.isKinematic = true;
        _rigidbody.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
    }
    public void PrepereDrop()
    {
        _rigidbody.useGravity = true;
        _rigidbody.isKinematic = false;
        _rigidbody.collisionDetectionMode = CollisionDetectionMode.Discrete;
    }
}
