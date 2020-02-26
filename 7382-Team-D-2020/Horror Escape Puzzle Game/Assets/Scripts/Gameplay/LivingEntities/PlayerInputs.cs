using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputs : MonoBehaviour
{
    private CharacterController2D character;
    private bool jump = false;
    private float h = 0;

    public void OnMove(InputAction.CallbackContext context)
    {
        h = context.ReadValue<Vector2>().x;
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            jump = true;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        character = GetComponent<CharacterController2D>();
    }

    // Update is called once per frame
    void Update()
    {
        character.Move(h, jump);
        jump = false;
    }
}
