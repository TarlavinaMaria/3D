using UnityEngine;

public class Interact : MonoBehaviour
{
    [SerializeField] private Camera _camera; // Камера
    [SerializeField] private int _sizeAim; // Размер курсора
    [SerializeField] private string _aimChar; // Символ курсора
    [SerializeField] private Transform _startPointRay; // Точка начала луча
    [SerializeField] private float _rayDistance; // Расстояние луча
    [SerializeField] private Transform _itemPosition; // Точка взятия
    [SerializeField] private float _forse; // Точка взятия


    [SerializeField] private float _rotationSpeed; // Скорость вращения
    [SerializeField] private KeyCode _rotateLeftKey; // Клавиша вращения влево
    [SerializeField] private KeyCode _rotateRightKey;   // Клавиша вращения вправо
    [SerializeField] private KeyCode _drop;   // Клавиша броска
    [SerializeField] private KeyCode _check;   // Клавиша нажатия с предметом


    private float _aimPositionDrawX; // Позиция курсора по X
    private float _aimPositionDrawY; // Позиция курсора по Y
    private RaycastHit _raycastHit; // Результат луча
    private Ray _ray; // Луч
    private bool _isHaveItem = false; // Проверка на наличие предмета
    private CubeScene3 _tempCube; // Предмет

    private void Update()
    {
        CastRay(); // Луч
        TakeItem(); // Взятие предмета

        if (_isHaveItem)
        {
            Rotate(); // Вращение
        }
        DropWithForse(); // Бросок 
        CheckDoor(); // Открытие двери
    }

    private void Rotate()
    {
        // Переменная для хранения величины вращения
        float rotationAmount = 0f;
        // Проверяем нажатие клавиши вращения влево (Q)
        if (Input.GetKey(_rotateLeftKey))
        {
            // Если Q зажата - устанавливаем положительное вращение
            rotationAmount = _rotationSpeed * Time.deltaTime;
        }
        // Проверяем нажатие клавиши вращения вправо (E)
        else if (Input.GetKey(_rotateRightKey))
        {
            // Если E зажата - устанавливаем отрицательное вращение
            rotationAmount = -_rotationSpeed * Time.deltaTime;
        }
        //  Если вращение есть (клавиша нажата)
        if (rotationAmount != 0f)
        {
            // Применяем вращение к удерживаемому объекту
            _tempCube.transform.Rotate(Vector3.up, rotationAmount, Space.World);
        }
    }
    private void DropWithForse()
    {
        if (Input.GetKey(_drop) && _isHaveItem == true)
        {
            _tempCube.PrepereDrop();
            _tempCube.DropWithForse(_camera.transform.forward, _forse);
        }
    }
    private void CastRay()
    {
        // Создаем луч
        // Начало: позиция _startPointRay
        // Направление: вперед от камеры
        _ray = new Ray(_startPointRay.position, _camera.transform.forward);
        // Визуализация луча в сцене (видно только в редакторе)
        Debug.DrawRay(_ray.origin, _ray.direction * _rayDistance, Color.red);
    }

    private void TakeItem()
    {
        // Если нажата ЛКМ и предмет не в руке
        if (Input.GetMouseButtonDown(0) && _isHaveItem == false)
        {
            // Проверяем пересечение луча с объектом
            if (Physics.Raycast(_ray, out _raycastHit, _rayDistance))
            {
                // Пытаемся получить компонент куба у объекта
                if (_raycastHit.transform.TryGetComponent<CubeScene3>(out CubeScene3 cube))
                {
                    _tempCube = cube; // Сохраняем куб
                    _tempCube.PrepereDrag(); // Подготавливаем куб
                    Drag(); // Взяли предмет в руку
                }
            }
        }
        // Если нажата ЛКМ и предмет в руке
        else if (Input.GetMouseButtonDown(0) && _isHaveItem == true)
        {
            {
                Drop(); // Отпустили предмет
            }
        }
    }
    private void CheckDoor()
    {
        if (Input.GetKey(_check))
        {
            if (Physics.Raycast(_ray, out _raycastHit, _rayDistance))
            {
                if (_raycastHit.transform.TryGetComponent<Door>(out Door door) && _tempCube != null)
                {
                    Destroy(door.gameObject);
                    Destroy(_tempCube.gameObject, 0.1f);
                    Drop();
                }

            }
        }
    }

    private void Drop()
    {
        _isHaveItem = false; // Флаг наличия предмета в руке, отпущен
        _tempCube.PrepereDrop(); // Возращаем физику предмету
        _tempCube.transform.SetParent(null); // Снимаем родителя
        _tempCube = null; // Снимаем ссылку

    }

    private void Drag()
    {
        _isHaveItem = true; // Флаг наличия предмета в руке, взят
        _tempCube.transform.position = _itemPosition.position;// Перемещает куб в точку _itemPosition, где находится камера
        _tempCube.transform.SetParent(this.transform); // Привязываем куб к игроку, для движения вместе с ним
    }

    private void OnGUI()
    {
        // Вычисляем позицию прицела по центру экрана
        _aimPositionDrawX = _camera.pixelWidth / 2 - _sizeAim / 4;
        _aimPositionDrawY = _camera.pixelHeight / 2 - _sizeAim / 2;
        // Рисуем прицел
        GUI.Label(new Rect(_aimPositionDrawX, _aimPositionDrawY, _sizeAim, _sizeAim), _aimChar);
    }
}