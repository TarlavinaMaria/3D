using UnityEngine;

public class Interact : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private int _sizeAim;
    [SerializeField] private string _aimChar;
    [SerializeField] private Transform _startPointRay;
    [SerializeField] private float _rayDistance;
    [SerializeField] private Transform _itemPosition;


    private float _aimPositionDrawX;
    private float _aimPositionDrawY;
    private RaycastHit _raycastHit;
    private Ray _ray;
    private bool _isHaveItem = false;
    private CubeScene3 _tempCube;

    private void Update()
    {
        CastRay();
        TakeItem();
    }
    private void CastRay()
    {
        _ray = new Ray(_startPointRay.position, _camera.transform.forward);

        Debug.DrawRay(_ray.origin, _ray.direction * _rayDistance, Color.red);
    }
    private void TakeItem()
    {
        if (Input.GetMouseButtonDown(0) && _isHaveItem == false)
        {
            if (Physics.Raycast(_ray, out _raycastHit, _rayDistance))
            {
                //if (_raycastHit.transform.TryGetComponent<CubeScene3>(out CubeScene3 cube))
                if (_raycastHit.transform.TryGetComponent<CubeScene3>(out CubeScene3 cube))
                {
                    //_nameObj.text = cube.Name; // подготовка куда к взятию
                    _tempCube = cube;
                    _tempCube.PrepereDrag();
                    Drag();
                }
            }
        }
        else if (Input.GetMouseButtonDown(0) && _isHaveItem == true)
        {
            {
                Drop();
            }
        }
    }

    private void Drop()
    {
        _isHaveItem = false;
        _tempCube.PrepereDrop();
        _tempCube.transform.SetParent(null);
        _tempCube = null;
    }

    private void Drag()
    {
        _isHaveItem = true;
        _tempCube.transform.position = _itemPosition.position;
        _tempCube.transform.SetParent(this.transform);
    }

    private void OnGUI()
    {
        _aimPositionDrawX = _camera.pixelWidth / 2 - _sizeAim / 4;
        _aimPositionDrawY = _camera.pixelHeight / 2 - _sizeAim / 2;

        GUI.Label(new Rect(_aimPositionDrawX, _aimPositionDrawY, _sizeAim, _sizeAim), _aimChar);
    }
}
