using UnityEngine;
using UnityEngine.InputSystem;

public abstract class Weapons : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public int bullet_count;
    public int fire_rate;
    public float min_fire_delay;
    public int accuracy;
    public int spread; //will use separately from accuracy when shooting multiple bullets
    // public int bullet_speed;
    public int bullet_size;
    public int ammo_count;
    // public int ammo_modifier;
    public int ammo_reserve; //might use
    public int reload_time;
    // public int reload_speed; //a modifier for reload time
    private bool is_reloading;
    private bool is_firing;

    private float time_shot=0f;
    private int current_burst_shot_number;
    private bool fire_successful;

    //variables for how many shots are fired per burst and how much time is in between each burst, respectively
    public int burst_count=3;
    public float burst_delay=0.05f;

    public enum FireType
    {
        Semi,
        Auto,
        Burst
    }

    [SerializeField] protected FireType FT = FireType.Semi;

    protected abstract void Fire();

    [SerializeField] protected Transform firePoint;
    // [SerializeField] protected Transform Square
    protected Camera mainCamera;

    protected Vector2 GetMouseDirection(float bullet_spread)
    {
        float spread_angle=Random.Range(-spread-bullet_spread,spread+bullet_spread);
        Vector2 mouseWorld=mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        Vector2 aim_direction=(mouseWorld-(Vector2)firePoint.position).normalized;
        return Quaternion.Euler(0f,0f,spread_angle)*aim_direction;
    }

    void Awake()
    {
        is_reloading=false;
        mainCamera=Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (FT==FireType.Auto && Mouse.current.leftButton.isPressed)
        {
            TryFire();
        }

        if (FT==FireType.Burst && Input.GetMouseButtonDown(0))
        {
            // burst_start_time=Time.time
            current_burst_shot_number=1;
            while(current_burst_shot_number<burst_count+1)
            {
                fire_successful=false;
                TryFire();
                if (fire_successful)
                {
                    current_burst_shot_number+=1;
                }
            }
        }
        
    }

    public void OnShoot(InputValue value)
    {
        Debug.Log("im shooting");
        TryFire();
    }

    public void TryFire()
    {
        if(is_reloading==false && ammo_count<=0)
        {
            Debug.Log("reload");
            is_reloading=true;
        }
        else if(is_reloading==false && ammo_count>0)
        {
            if(Time.time-time_shot>=min_fire_delay)
            {
                Debug.Log("shoot");
                ammo_count-=1;
                Debug.Log(ammo_count);
                time_shot=Time.time;

                switch(FT)
                {
                    case FireType.Semi:
                        //semiautomatic Fire
                        Debug.Log("semiauto");
                        Fire();
                        break;
                    case FireType.Auto:
                        //automatic Fire
                        //the rest of the auto function is in Update()
                        Fire();
                        Debug.Log("auto");
                        break;
                    case FireType.Burst:
                        //burst Fire
                        if(Time.time-time_shot>=burst_delay)
                        {
                            Fire();
                            fire_successful=true;
                            Debug.Log("burst");
                        }
                        break;
                }
            }
        }
        else
        {
            Debug.Log("cant shoot");
        }
    }
}




