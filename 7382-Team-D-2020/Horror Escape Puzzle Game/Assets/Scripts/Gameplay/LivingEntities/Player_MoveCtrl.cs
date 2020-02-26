using System.Runtime.CompilerServices;
using UnityEngine;
/// <summary>
/// Player Move Behaviour
/// </summary>
public class Player_MoveCtrl : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rd2D;
    [SerializeField] private float _speed=1f;
    [SerializeField] private float _jumpForce = 5;

    private void Update()
    {
        float h = Input.GetAxis("Horizontal");
        Move( h);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }
    public void Move(float h)
    {
        
        //_rd2D.velocity = v * Vector2.up + Vector2.right * h;
        //_rd2D.velocity = Vector2.zero + Vector2.right * h;
        _rd2D.velocity=new Vector2(h * _speed, _rd2D.velocity.y);
    }

    public void Jump()
    {
        _rd2D.AddForce(Vector2.up* _jumpForce, ForceMode2D.Impulse);
    }
}

