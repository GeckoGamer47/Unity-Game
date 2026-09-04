using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public abstract class Weapons : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public int bullet_count;
    public float min_fire_delay;
    public float spread; //will use separately from accuracy when shooting multiple bullets
    // public int bullet_speed;
    public int ammo_count;
    public int magazine_size;
    // public int ammo_modifier;
    public int ammo_reserve;
    public int max_ammo_reserve;
    public float reload_time;
    // public int reload_speed; //a modifier for reload time

    private float time_shot=0f;
    private bool is_bursting=false;
    // private int current_burst_shot_number;
    // private int burst_attempt_number;

    //variables for how many shots are fired per burst and how much time is in between each burst, respectively
    public int burst_count=3;
    public float burst_delay=0.05f;

    // so far unused
    public float bullet_size;
    private float fire_rate;
    private int accuracy;

    public int slot_number;
    public bool is_active;

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

    private IEnumerator BurstRoutine()
    {
        is_bursting=true;
        GameMaster.can_shoot=false;
        for (int i=0; i<burst_count;i++)
        {
            // if i<burst_count--;
            if (ammo_count<=0)
            {
                break;
            }
            ammo_count--;
            Fire();
            yield return new WaitForSeconds(burst_delay);
        }
        time_shot=Time.time+fire_rate;
        is_bursting=false;
        GameMaster.can_shoot=true;
    }

    private IEnumerator ReloadRoutine()
    {
        GameMaster.is_reloading=true;
        GameMaster.can_shoot=false;
        yield return new WaitForSeconds(reload_time);
        if (magazine_size>ammo_reserve)
        {
            ammo_reserve=0;
            ammo_count=ammo_reserve;
        }
        else
        {
            ammo_reserve-=magazine_size;
            ammo_count=magazine_size;
        }
        GameMaster.is_reloading=false;
        GameMaster.can_shoot=true;
    }

    void Awake()
    {
        GameMaster.is_reloading=false;
        mainCamera=Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (FT==FireType.Auto && Mouse.current.leftButton.isPressed)
        {
            TryFire();
        }

        if (is_active==false)
        {
            GetComponent<SpriteRenderer>().enabled = false;
        }
        else
        {
            GetComponent<SpriteRenderer>().enabled = true;
        }
    }
        
    

    public void OnShoot(InputValue value)
    {
        // Debug.Log("im shooting");
        TryFire();
    }


    public void TryFire()
    {
        if(GameMaster.is_reloading==false && ammo_count<=0)
        {
            Debug.Log("reload");
            StartCoroutine(ReloadRoutine());

        }
        else if(GameMaster.is_reloading==false && ammo_count>0 && is_active==true)
        {
            // Debug.Log("test");
            if(Time.time-time_shot>=min_fire_delay)
            {
                // Debug.Log("shoot");
                Debug.Log(ammo_count);
                GameMaster.can_shoot=true;
                

                switch(FT)
                {
                    case FireType.Semi:
                        //semiautomatic Fire
                        ammo_count-=1;
                        Fire();
                        time_shot=Time.time;
                        break;
                    case FireType.Auto:
                        //automatic Fire
                        //the rest of the auto function is in Update()
                        Fire();
                        ammo_count-=1;
                        time_shot=Time.time;
                        break;
                    case FireType.Burst:
                        //burst Fire
                            if (!is_bursting)
                            {
                                StartCoroutine(BurstRoutine());
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


