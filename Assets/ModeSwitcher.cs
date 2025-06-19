using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModeSwitcher : MonoBehaviour
{
    public float setSpeed = 20f;
    public float movementDelay;
    private Rigidbody2D rb;
    private Vector3 direction;
    private bool destroyed;
    private LogicScript logic;
    private GameObject player;
    private bool movement = false;

    private IEnumerator MovementDelay()
    {
        yield return new WaitForSeconds(movementDelay);
        movement = true;
    }

    void Start()
    {
        StartCoroutine(MovementDelay());
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        direction = player.transform.position - transform.position;
        
        if (movement)
        {
            rb.velocity = new Vector2(direction.x, direction.y).normalized * setSpeed;
            float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, rotZ - 90);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == 6)
        {
            if (destroyed == false)
            {
                destroyed = true;
                
                if (gameObject.name == "Shooter")
                {
                    player.GetComponent<PlayerScript>().DefaultMode();
                }
                else if (gameObject.name == "Guitar")
                {
                    player.GetComponent<PlayerScript>().GoingGuitar();
                }

                Destroy(gameObject);
            }
        }
    }
}