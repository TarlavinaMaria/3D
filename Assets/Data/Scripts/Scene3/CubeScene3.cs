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
        // Получаем компоненты
        _rigidbody = GetComponent<Rigidbody>();
        _questItem = GetComponent<QuestItem>();
    }
    public void PrepereDrag()
    {
        // Отключаем гравитацию
        _rigidbody.useGravity = false;
        // Коллизии отключаем
        _rigidbody.isKinematic = true;
        _rigidbody.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
        IsActivated = false; // Сбрасываем флаг, временно отключен
    }
    public void PrepereDrop()
    {
        // Включаем гравитацию
        _rigidbody.useGravity = true;
        // Коллизии включаем
        _rigidbody.isKinematic = false;
        _rigidbody.collisionDetectionMode = CollisionDetectionMode.Discrete;
        IsActivated = true; // Включаем флаг
    }
    public void DropWithForse(Vector3 vectorDirection, float force)
    {
        // Добавляем силу, импульс
        _rigidbody.AddForce(vectorDirection * force, ForceMode.Impulse);
    }
}
