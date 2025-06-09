using UnityEngine;

public class Interact : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private int _sizeAim;
    [SerializeField] private string _aimChar;

    private float _aimPositionDrawX;
    private float _aimPositionDrawY;

    private void OnGUI()
    {
        _aimPositionDrawX = _camera.pixelWidth / 2 - _sizeAim / 4;
        _aimPositionDrawY = _camera.pixelHeight / 2 - _sizeAim / 2;

        GUI.Label(new Rect(_aimPositionDrawX, _aimPositionDrawY, _sizeAim, _sizeAim), _aimChar);
    }
}
