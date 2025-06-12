using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(BoxCollider))]

public class Domino : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private bool _isHeld = false;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void PickUp(Transform parent, Vector3 position)
    {
        _isHeld = true;
        _rigidbody.useGravity = false;
        _rigidbody.isKinematic = true;
        transform.SetParent(parent);
        transform.position = position;
    }

    public void Place()
    {
        _isHeld = false;
        _rigidbody.isKinematic = false;
        _rigidbody.useGravity = true;
        transform.SetParent(null);
    }

    public void Push(Vector3 direction, float force)
    {
        if (!_isHeld)
        {
            _rigidbody.AddForce(direction * force, ForceMode.Impulse);
        }
    }
}
