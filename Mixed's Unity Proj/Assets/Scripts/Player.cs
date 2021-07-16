using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public Vector2 speed = new Vector2(50, 50);
    public int health = 100;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(speed.x * inputX, speed.y * inputY, 0);

        movement *= Time.deltaTime;

        transform.Translate(movement);
    }

    void OnTriggerEnter2D(Collider2D c2d)
    {
        if (c2d.CompareTag("smallAsteroid"))
        {
            health -= 10;
        }
        else if (c2d.CompareTag("medAsteroid"))
        {
            health -= 25;
        }
        else if (c2d.CompareTag("largeAsteroid"))
        {
            health -= 50;
        }
    }
}
