using UnityEngine;

public class weapon_look_at_mouse : MonoBehaviour
{
    private float flipvalue=180f;

    // need to access sprite renderer. This gives us properties to check the angle and flip the model
    private SpriteRenderer spriteRend;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteRend = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		transform.rotation = Quaternion.LookRotation(Vector3.forward, mousePos - transform.position) * Quaternion.Euler(0f,0f,90);

        float zRot = transform.localEulerAngles.z;

        if (zRot > 270f) zRot -= 360f;
        if (zRot >= -90f && zRot <= 90f)
        {
            spriteRend.flipY = false;
        }
        else
        {
            spriteRend.flipY = true;
        }
    }
}
