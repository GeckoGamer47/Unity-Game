using UnityEngine;

public class Pistol : Weapons
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    [Header("Pistol Settings")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float bullet_speed=12f;
    [SerializeField] private float bullet_lifetime=10f;
    [SerializeField] private float bullet_spread=0f;

    protected override void Fire()
    {
        Debug.Log("FIRE");
        if (!bulletPrefab || !firePoint)
        {
            Debug.Log("no prefab or firepoint");
            return;
        }
        Vector2 direction=GetMouseDirection(bullet_spread);


        float angle = Mathf.Atan2(direction.y,direction.x)*Mathf.Rad2Deg-90f;
        GameObject bullet=Instantiate(bulletPrefab,firePoint.position, Quaternion.Euler(0f,0f,angle));
        if (bullet.TryGetComponent<Rigidbody2D>(out var rb))
            rb.linearVelocity=direction*bullet_speed;
        Debug.Log("destroyed");
        Destroy(bullet,bullet_lifetime);
    }
}

// to do:
// make shoot work with multiple bullets at once
// make shotgun reloading work