using UnityEngine;

public class Pistol : Weapons
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    [Header("Pistol Settings")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float bullet_speed=12f;
    [SerializeField] private float bullet_lifetime=10f;
    [SerializeField] private float bullet_spread=0f;


    // [SerializeField] public int bullet_count=1;
    // [SerializeField] public float min_fire_delay=.05f;
    // [SerializeField] public float spread=5f;
    // [SerializeField] public float bullet_size=.95f;
    // [SerializeField] public int ammo_count=10;
    // [SerializeField] public int magazine_size=10;
    // [SerializeField] public int ammo_reserve=30;
    // [SerializeField] public int max_ammo_reserve=30;
    // [SerializeField] public float reload_time=2f;
    // [SerializeField] public string FT="Semi";

    // bullet_count=1;
    // min_fire_delay=.05f;
    // spread=5f;
    // bullet_size=.95f;
    // ammo_count=10;
    // magazine_size=10;
    // ammo_reserve=30;
    // max_ammo_reserve=30;
    // reload_time=2f;
    // FT="Semi"

    // public int bullet_count=1;
    // public float min_fire_delay=.05f;
    // public float spread=5f;
    // public float bullet_size=.95f;
    // public int ammo_count=10;
    // public int magazine_size=10;
    // public int ammo_reserve=30;
    // public int max_ammo_reserve=30;
    // public float reload_time=2f;
    // public string FT="Semi"; 

    // Random rand=new Random();

    void Start()
    {
        // bullet_count=1;
        // min_fire_delay=.05f;
        // spread=5f;
        // bullet_size=.95f;
        // ammo_count=10;
        // magazine_size=10;
        // ammo_reserve=30;
        // max_ammo_reserve=30;
        // reload_time=2.0f;
        // // FireType=Semi;
    }

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