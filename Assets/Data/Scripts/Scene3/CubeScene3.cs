using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(QuestItem))]

public class CubeScene3 : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private QuestItem _questItem;
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
        _questItem.enabled = false;
    }
    public void PrepereDrop()
    {
        _rigidbody.useGravity = true;
        _rigidbody.isKinematic = false;
        _rigidbody.collisionDetectionMode = CollisionDetectionMode.Discrete;
        _questItem.enabled = true;
    }
    public void DropWithForse(Vector3 vectorDirection, float force)
    {
        _rigidbody.AddForce(vectorDirection * force, ForceMode.Impulse);
    }
}
