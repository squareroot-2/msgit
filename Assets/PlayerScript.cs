using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    public string mode;
    public float cooldownTime = 5f;
    public GameObject bullet;
    public GameObject arrow;
    public GameObject cooldownBar;
    public bool onSuper;
    public bool doubleSpeedSet = true;
    public Texture2D guitar;
    private Sprite defaultPlayer;

    private float rotationSpeed = 8f;
    private bool onCooldown = false;
    private float originalCooldown;

    private IEnumerator Shoot()
    {
        gameObject.GetComponent<AudioSource>().Play();
        Instantiate(bullet, transform.position, transform.rotation);
        rotationSpeed = -(rotationSpeed);
        cooldownBar.transform.localScale = new Vector3(0, 1, 1);
        onCooldown = true;

        float time = 0;

        while (time < cooldownTime)
        {
            cooldownBar.transform.localScale += new Vector3((0.1f / cooldownTime), 0, 0);

            if (cooldownBar.transform.localScale.x > 1)
            {
                cooldownBar.transform.localScale = new Vector3(1, 1, 1);
            }
            
            yield return new WaitForSeconds(0.1f);
            time += 0.1f;
        }
        
        onCooldown = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        mode = "Shooter";
        arrow.transform.parent = transform;
        arrow.transform.localPosition = Vector3.zero;
        arrow.transform.position = new Vector3(0, 9, 0); 
        defaultPlayer = gameObject.GetComponent<SpriteRenderer>().sprite;
    }

    public void GoingGuitar()
    {
        mode = "Guitar";
        cooldownTime = 1;
        Sprite guitarSprite = Sprite.Create(guitar, new Rect(0, 0, guitar.width, guitar.height), Vector2.one * 0.5f);
        gameObject.GetComponent<SpriteRenderer>().sprite = guitarSprite;
    }

    public void DefaultMode()
    {
        mode = "Shooter";
        cooldownTime = 0.25f;
        gameObject.GetComponent<SpriteRenderer>().sprite = defaultPlayer;
    }


    // Update is called once per frame
    void Update()
    {
        if (doubleSpeedSet == false)
        {
            doubleSpeedSet = true;

            if (onSuper)
            {
                originalCooldown = cooldownTime;
                cooldownTime = 0.05f;
            }
            else if (!onSuper)
            {
                cooldownTime = originalCooldown;
            }
        }

        if (Input.GetMouseButtonDown(0) && Time.timeScale > 0)
        {
            if (!onCooldown)
            {
                StartCoroutine(Shoot());
            }
        }
    }

    private void FixedUpdate() 
    {
        transform.rotation *= Quaternion.Euler(0f, 0f, rotationSpeed);
        arrow.transform.rotation = transform.rotation;
    }
}
