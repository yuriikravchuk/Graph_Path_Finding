using UnityEngine;
using UnityEngine.Events;

public class PlayerInputs : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private RectTransform _canvas;

    public ClickEvent LeftClick;
    public ClickEvent RightClick;
    public ClickEvent ShiftAndLeftClick;
    public ClickEvent ShiftAndRightClick;

    private void Update()
    {
        Debug.DrawRay(_camera.ScreenToWorldPoint(Input.mousePosition), Vector3.forward * 4);

        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
                ShiftAndLeftClick.Invoke(GetMouseWorldPosition());
            else if (Input.GetKeyDown(KeyCode.Mouse1))
                ShiftAndRightClick.Invoke(GetMouseWorldPosition());
        }
        else if (Input.GetKeyDown(KeyCode.Mouse0))
            LeftClick.Invoke(GetMouseWorldPosition());
        else if (Input.GetKeyDown(KeyCode.Mouse1))
            RightClick.Invoke(GetMouseWorldPosition());

    }

    private Vector3 GetMouseWorldPosition() 
        => _camera.ScreenToWorldPoint(Input.mousePosition);
}