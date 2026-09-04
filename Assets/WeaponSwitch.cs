using UnityEngine;

public class WeaponSwitch : MonoBehaviour
{
    private Weapon w;
    public int current_weapon=0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       SelectWeapon();
    }

    // Update is called once per frame
    void Update()
    {
        int previousSelectedWeapon=current_weapon;

        if (Input.GetAxis("Mouse ScrollWheel")>0f)
        {
            if (current_weapon>= transform.childCount-1 && !GameMaster.is_reloading)
                current_weapon=0;
            else
                current_weapon++;
        }

        if (Input.GetAxis("Mouse ScrollWheel")<0f && !GameMaster.is_reloading)
        {
            if (current_weapon<= 0)
                current_weapon=transform.childCount-1;
            else
                current_weapon--;
        }

        if (Input.GetKeyDown(KeyCode.Alpha1) && !GameMaster.is_reloading)
        {
            current_weapon=0;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2) && transform.childCount>=2 && !GameMaster.is_reloading)
        {
            current_weapon=1;
        }

        if (Input.GetKeyDown(KeyCode.Alpha3) && transform.childCount>=3 && !GameMaster.is_reloading)
        {
            current_weapon=2;
        }

        if (Input.GetKeyDown(KeyCode.Alpha4) && transform.childCount>=4 && !GameMaster.is_reloading)
        {
            current_weapon=3;
        }

        if (Input.GetKeyDown(KeyCode.Alpha5) && transform.childCount>=5 && !GameMaster.is_reloading)
        {
            current_weapon=4;
        }

        if (previousSelectedWeapon!= current_weapon)
        {
            SelectWeapon();
        }
    }

    void SelectWeapon()
    {
        int i=0;
        foreach (Transform weapon in transform)
        {
            if (i==current_weapon)
            {
                weapon.gameObject.SetActive(true);
                // GetComponent<SpriteRenderer>().enabled = true;
            }

            else
                weapon.gameObject.SetActive(false);
                // GetComponent<SpriteRenderer>().enabled = false;
            i++;
        }
    }
}
