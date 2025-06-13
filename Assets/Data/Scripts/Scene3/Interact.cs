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
    [SerializeField] private KeyCode _resetDominoKey; // Клавиша выравнивания



    private float _aimPositionDrawX; // Позиция курсора по X
    private float _aimPositionDrawY; // Позиция курсора по Y
    private RaycastHit _raycastHit; // Результат луча
    private Ray _ray; // Луч
    private bool _isHaveItem = false; // Проверка на наличие предмета
    private CubeScene3 _tempCube; // Предмет

    private Domino _tempDomino; // Временная переменная для домино


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
        Press();
        // Выравнивание
        if (_isHaveItem && _tempDomino != null && Input.GetKeyDown(_resetDominoKey))
        {
            _tempDomino.AlignUpright(_itemPosition.position); // Выравнивание домино
        }

    }
    private void Press()
    {
        // Если нажата ЛКМ
        if (Input.GetMouseButtonDown(0) && _isHaveItem == false)
        {
            if (Physics.Raycast(_ray, out _raycastHit, _rayDistance))
            {
                if (_raycastHit.transform.TryGetComponent<Pry>(out Pry pry))
                {
                    // Подключаем анимацию для рычага
                    pry.Press();
                    Debug.Log("Press");
                }
            }
        }
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
            if (_tempCube != null)
            {
                _tempCube.PrepereDrop();
                _tempCube.DropWithForse(_camera.transform.forward, _forse);
            }
            else if (_tempDomino != null)
            {
                _tempDomino.Place();
                _tempDomino.Push(_camera.transform.forward, _forse);
            }
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
                // Если это Domino — берем его
                else if (_raycastHit.transform.TryGetComponent<Domino>(out Domino domino))
                {
                    _tempDomino = domino; // Сохраняем домино
                    _tempDomino.PickUp(); // Подготавливаем домино
                    Drag();
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
        // Проверяем нажатие клавиши открытия двери
        if (Input.GetKey(_check))
        {
            if (Physics.Raycast(_ray, out _raycastHit, _rayDistance))
            {
                if (_raycastHit.transform.TryGetComponent<Door>(out Door door) && _tempCube != null)
                {
                    // уничтожаем дверь и куб
                    Destroy(door.gameObject);
                    Destroy(_tempCube.gameObject, 0.1f);
                    Drop();
                }

            }
        }
    }

    private void Drop()
    {
        _isHaveItem = false;
        // Если предмет в руке куб
        if (_tempCube != null)
        {
            _tempCube.PrepereDrop();
            _tempCube.transform.SetParent(null);
            _tempCube = null;
        }
        // Если предмет в руке домино
        else if (_tempDomino != null)
        {
            _tempDomino.Place();
            _tempDomino.transform.SetParent(null);
            _tempDomino = null;
        }

    }

    private void Drag()
    {
        _isHaveItem = true; // Флаг наличия предмета в руке, взят
        if (_tempCube != null)
        {
            _tempCube.transform.position = _itemPosition.position;// Перемещает куб в точку _itemPosition, где находится камера
            _tempCube.transform.SetParent(this.transform); // Привязываем куб к игроку, для движения вместе с ним
        }
        else if (_tempDomino != null)
        {
            _tempDomino.transform.position = _itemPosition.position;// Перемещает куб в точку _itemPosition, где находится камера
            _tempDomino.transform.SetParent(this.transform); // Привязываем куб к игроку, для движения вместе с ним
        }
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