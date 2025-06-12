using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(BoxCollider))]

public class Domino : MonoBehaviour
{
    private Rigidbody _rigidbody;
    public bool IsActivated { get; private set; } = true;

    private void Awake()
    {
        // Получаем компонент
        _rigidbody = GetComponent<Rigidbody>();
    }


    public void PickUp(Transform parent, Vector3 position)
    {
        //
        IsActivated = false;
        _rigidbody.useGravity = false;
        _rigidbody.isKinematic = true;
        transform.SetParent(parent);
        transform.position = position;
    }

    public void Place()
    {
        IsActivated = true;
        _rigidbody.isKinematic = false;
        _rigidbody.useGravity = true;
        transform.SetParent(null);
    }

    public void Push(Vector3 direction, float force)
    {
        // Добавляем силу. Импульс
        _rigidbody.AddForce(direction * force, ForceMode.Impulse);
    }
    public void AlignUpright(Vector3 position)
    {
        // Для выравнивания домино
        transform.position = position;
        transform.rotation = Quaternion.identity;
    }

}
