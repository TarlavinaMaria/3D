using System.Collections.Generic;
using UnityEngine;

//Класс пула кубов, которые будут использоваться в игре
// Пул - это очередь объектов, которые можно взять и вернуть
public class ObjectPool : MonoBehaviour
{
    [SerializeField] private Transform _conteiten;  // Родительский объект для новых кубов
    [SerializeField] private Cube _prefabCube;     // Префаб куба для создания

    private Cube _tempCube;                         // Временная ссылка на куб
    private Queue<Cube> _poolCibes = new Queue<Cube>(); // Очередь для хранения кубов

    public Cube GetCube()
    {
        // Получаем куб из пула:
        if (_poolCibes.Count == 0) // Если пул пуст
        {
            // Создаем новый куб
            Debug.Log(_poolCibes.Count);
            _tempCube = Instantiate(_prefabCube, _conteiten);
        }
        else
        {
            // Берем куб из очереди
            _tempCube = _poolCibes.Dequeue();

            //_tempCube.gameObject.SetActive(true);
        }
        return _tempCube;
    }

    public void PutCube(Cube cube)
    {
        //_tempCube.gameObject.SetActive(false);

        // Возвращаем куб в пул:
        _poolCibes.Enqueue(cube); // Добавляем в очередь
    }
}