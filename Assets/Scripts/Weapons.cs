using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public abstract class Weapons : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public int bullet_count;
<<<<<<< HEAD
=======
<<<<<<< HEAD
=======
    public float fire_rate;
>>>>>>> db06938b4f2c884d445d161d8ed9cd10d7c01287
>>>>>>> df1f70b38748027a4ec830bcd031dd660be887e0
    public float min_fire_delay;
    public float spread; //will use separately from accuracy when shooting multiple bullets
    // public int bullet_speed;
    public int ammo_count;
    public int magazine_size;
    // public int ammo_modifier;
    public int ammo_reserve;
    public int max_ammo_reserve;
<<<<<<< HEAD
    public float reload_time;
=======
<<<<<<< HEAD
    public float reload_time;
=======
    public int reload_time;
>>>>>>> db06938b4f2c884d445d161d8ed9cd10d7c01287
>>>>>>> df1f70b38748027a4ec830bcd031dd660be887e0
    // public int reload_speed; //a modifier for reload time

    private float time_shot=0f;
    private bool is_bursting=false;
    // private int current_burst_shot_number;
<<<<<<< HEAD
=======
    private bool can_shoot;
>>>>>>> df1f70b38748027a4ec830bcd031dd660be887e0
    // private int burst_attempt_number;

    //variables for how many shots are fired per burst and how much time is in between each burst, respectively
    public int burst_count=3;
    public float burst_delay=0.05f;

    // so far unused
    public float bullet_size;
    private float fire_rate;
    private int accuracy;

<<<<<<< HEAD
    public int slot_number;
    public bool is_active;

=======
>>>>>>> df1f70b38748027a4ec830bcd031dd660be887e0
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
<<<<<<< HEAD
        GameMaster.can_shoot=false;
=======
        can_shoot=false;
>>>>>>> df1f70b38748027a4ec830bcd031dd660be887e0
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
<<<<<<< HEAD
        time_shot=Time.time+fire_rate;
        is_bursting=false;
        GameMaster.can_shoot=true;
=======
        min_fire_delay=Time.time+fire_rate;
        is_bursting=false;
        can_shoot=true;
>>>>>>> df1f70b38748027a4ec830bcd031dd660be887e0
    }

    private IEnumerator ReloadRoutine()
    {
<<<<<<< HEAD
        GameMaster.is_reloading=true;
        GameMaster.can_shoot=false;
=======
        is_reloading=true;
        can_shoot=false;
>>>>>>> df1f70b38748027a4ec830bcd031dd660be887e0
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
<<<<<<< HEAD
        GameMaster.is_reloading=false;
        GameMaster.can_shoot=true;
=======
        is_reloading=false;
        can_shoot=true;
>>>>>>> df1f70b38748027a4ec830bcd031dd660be887e0
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
<<<<<<< HEAD

        if (is_active==false)
        {
            GetComponent<SpriteRenderer>().enabled = false;
        }
        else
        {
            GetComponent<SpriteRenderer>().enabled = true;
        }
=======
>>>>>>> df1f70b38748027a4ec830bcd031dd660be887e0
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
<<<<<<< HEAD
            // Debug.Log("test");
            if(Time.time-time_shot>=min_fire_delay)
            {
                // Debug.Log("shoot");
                Debug.Log(ammo_count);
                GameMaster.can_shoot=true;
=======
            Debug.Log("test");
            if(Time.time-time_shot>=min_fire_delay)
            {
                Debug.Log("shoot");
                Debug.Log(ammo_count);
                can_shoot=true;
>>>>>>> df1f70b38748027a4ec830bcd031dd660be887e0
                

                switch(FT)
                {
                    case FireType.Semi:
                        //semiautomatic Fire
<<<<<<< HEAD
=======
                        Debug.Log("semiauto");
>>>>>>> df1f70b38748027a4ec830bcd031dd660be887e0
                        ammo_count-=1;
                        Fire();
                        time_shot=Time.time;
                        break;
                    case FireType.Auto:
                        //automatic Fire
                        //the rest of the auto function is in Update()
                        Fire();
<<<<<<< HEAD
=======
                        Debug.Log("auto");
>>>>>>> df1f70b38748027a4ec830bcd031dd660be887e0
                        ammo_count-=1;
                        time_shot=Time.time;
                        break;
                    case FireType.Burst:
                        //burst Fire
                            if (!is_bursting)
                            {
                                StartCoroutine(BurstRoutine());
                            }
<<<<<<< HEAD
=======
                            Debug.Log("burst");
>>>>>>> df1f70b38748027a4ec830bcd031dd660be887e0
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


<<<<<<< HEAD
=======
<<<<<<< HEAD
=======


// fix burst only shooting once
// declutter debug logs
>>>>>>> db06938b4f2c884d445d161d8ed9cd10d7c01287
>>>>>>> df1f70b38748027a4ec830bcd031dd660be887e0
