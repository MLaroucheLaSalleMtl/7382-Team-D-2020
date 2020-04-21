
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class CursorControl : MonoBehaviour
{
    private Vector2 direction = new Vector2();
    private Vector2 currPos = new Vector2();

    private bool moveCursor = false;
    private float controllerCursorSpeed = 1f; //2f was a bit too fast with build 


    private void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        currPos = Mouse.current.position.ReadValue();

        GetCurrentMousePosition();

        direction = Vector2.zero;

        Controls.Instance.UAction_OnCursorNavigate += GetCursorDirection;
        Controls.Instance.UAction_OnClick += Click;
    }

    private void GetCurrentMousePosition()
    {
        currPos += Mouse.current.delta.ReadValue(); // Getting delta helps buts still not accurate. So cursor WARPS haha! Because WarpCursorPosition
        currPos.x = currPos.x > Screen.width ? Screen.width : currPos.x;
        currPos.y = currPos.y > Screen.height ? Screen.height : currPos.y;
    }

    private void Update()
    {
        GetCurrentMousePosition();
        //print("Read: " + currPos
        //    + " | Pointer Position:" + Pointer.current.position.ReadValue()
        //    + " | Mouse Delta: " + Mouse.current.delta.ReadValue());

        if (moveCursor)
        {
            
            currPos.x += direction.x * controllerCursorSpeed;

#if UNITY_EDITOR
            currPos.y += direction.y * controllerCursorSpeed;
#elif UNITY_STANDALONE_WIN
            currPos.y -= direction.y * controllerCursorSpeed;
#endif
            MoveCursor();
        }
    }

    private void GetCursorDirection(bool performing, Vector2 dr)
    {
        direction = dr;
        moveCursor = performing;
    }
    
    private void MoveCursor()
    {
        ClampCursorToScreen();
        Mouse.current.WarpCursorPosition(currPos);
    }

    private void ClampCursorToScreen()
    {
        currPos.x = Mathf.Clamp(currPos.x, 0, Screen.width);
        currPos.y = Mathf.Clamp(currPos.y, 0, Screen.height);
    }

    private void Click()
    {
        //Pointer point = Pointer.current;
        //EventSystem eventSystem;
        //PointerEventData pointerData  = po;
        //GraphicRaycaster raycaster;
        //raycaster.Raycast()

        
    }

}
