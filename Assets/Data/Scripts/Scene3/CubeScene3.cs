using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(QuestItem))]

public class CubeScene3 : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private QuestItem _questItem;
    public bool IsActivated { get; private set; } = true;
    public string Name { get; private set; } = "Куб";

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _questItem = GetComponent<QuestItem>();
    }
    public void PrepereDrag()
    {
        _rigidbody.useGravity = false;
        _rigidbody.isKinematic = true;
        _rigidbody.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
        IsActivated = false;
    }
    public void PrepereDrop()
    {
        _rigidbody.useGravity = true;
        _rigidbody.isKinematic = false;
        _rigidbody.collisionDetectionMode = CollisionDetectionMode.Discrete;
        IsActivated = true;
    }
    public void DropWithForse(Vector3 vectorDirection, float force)
    {
        _rigidbody.AddForce(vectorDirection * force, ForceMode.Impulse);
    }
}
