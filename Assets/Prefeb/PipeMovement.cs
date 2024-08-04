using UnityEngine;

public class PipeMovement : MonoBehaviour
{
    private float speed;

    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
        GetComponent<Rigidbody2D>().velocity = new Vector2(-speed, 0);
    }
}
