using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bullet_speed;
    public float bullet_lifetime;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(gameObject);
    }
}
