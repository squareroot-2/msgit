using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public int health = 1;
    public GameObject player;
    public float setSpeed = 20f;
    public float movementDelay;
    private Rigidbody2D rb;
    private Vector3 direction;
    private bool destroyed;
    private LogicScript logic;
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
        rb = GetComponent<Rigidbody2D>();

        transform.localScale += Vector3.one * 0.1f * health;
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

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == 6)
        {
            if (destroyed == false)
            {
                destroyed = true;
                logic.GameOver();
                Destroy(gameObject);
            }
        }
    }
}