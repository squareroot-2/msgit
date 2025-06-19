using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private Vector3 direction;
    private float projectileSpeed = 40f;
    private bool collided = false;
    private LogicScript logic;
    private PlayerScript player;

    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.velocity = transform.up * projectileSpeed;
        
        transform.position += new Vector3(transform.rotation.x, transform.rotation.y, 0f).normalized * 7.5f;

        if (player.mode == "Guitar")
        {
            transform.localScale *= 2;
        }

        StartCoroutine(TimePeriodToDelete());
    }

    private IEnumerator TimePeriodToDelete()
    {
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (!other.gameObject.CompareTag("Player"))
        {
            if (collided == false)
            {
                if (player.mode != "Guitar")
                {
                    collided = true;
                }
                
                if (other.gameObject.CompareTag("Enemy"))
                {
                    other.gameObject.GetComponent<EnemyScript>().health -= 1;
                    other.gameObject.transform.localScale -= Vector3.one * 0.1f;
                    logic.IncreaseSuperMeter();

                    if (player.mode != "Guitar")
                    {
                        Destroy(gameObject);
                    }
                }
                else if (other.gameObject.CompareTag("Wall"))
                {
                    logic.LoseCombo();
                    Destroy(gameObject);
                }
            }
        }
    }
}