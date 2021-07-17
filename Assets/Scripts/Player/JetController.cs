using UnityEngine;

public class JetController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;

    [SerializeField] private float flyForce = 15f;

    const float MAX_FLY_SPEED = 3.5f;
    const float MAX_FALL_SPEED = 2;

    private void Update()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            rb.AddForce(Vector2.up * flyForce);
        }

        if(rb.velocity.magnitude >= MAX_FLY_SPEED && IsFlyingUp())
        {
            rb.velocity = new Vector2(rb.velocity.x, MAX_FLY_SPEED);
        }
        else if(rb.velocity.magnitude >= MAX_FALL_SPEED && IsFallingDown())
        {
            rb.velocity = new Vector2(rb.velocity.x, -MAX_FALL_SPEED);
        }
    }

    private bool IsFlyingUp()
    {
        return rb.velocity.y > 0;
    }

    private bool IsFallingDown()
    {
        return rb.velocity.y < 0;
    }
}
