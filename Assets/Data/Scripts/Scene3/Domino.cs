using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(BoxCollider))]

public class Domino : MonoBehaviour
{
    //Методы аналогичные кубу
    private Rigidbody _rigidbody;
    public bool IsActivated { get; private set; } = true;

    private void Awake()
    {
        // Получаем компонент
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void PickUp()
    {
        //Подбираем 
        IsActivated = false;
        _rigidbody.useGravity = false;
        _rigidbody.isKinematic = true;
        _rigidbody.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
        IsActivated = false;
    }

    public void Place()
    {
        //Ставим
        _rigidbody.useGravity = true;
        _rigidbody.isKinematic = false;
        _rigidbody.collisionDetectionMode = CollisionDetectionMode.Discrete;
        IsActivated = true;
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
