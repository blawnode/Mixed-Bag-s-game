// Instantiates 10 copies of Prefab each 2 units apart from each other

using UnityEngine;
using System.Collections;

public class spawnAsteroid : MonoBehaviour
{



    //Array of objects to spawn (note I've removed the private goods variable)
    public GameObject[] theGoodies;

    //Time it takes to spawn theGoodies
    [Space(3)]
    public float waitingForNextSpawn = 10;
    public float theCountdown = 10;

    // the range of X
    [Header("X Spawn Range")]
    public float xMin;
    public float xMax;

    // the range of y
    [Header("Y Spawn Range")]
    public float yMin;
    public float yMax;


    void Start()
    {
    }

    public void Update()
    {
        // timer to spawn the next goodie Object
        theCountdown -= Time.deltaTime;
        if (theCountdown <= 0)
        {
            SpawnGoodies();
            theCountdown = waitingForNextSpawn;
        }
    }


    void SpawnGoodies()
    {


        Vector2 pos = new Vector2(0, 0);
        float xThing = Random.Range(xMin, xMax);
        float yThing = Random.Range(yMin, yMax);

       // if (Mathf.Abs(xThing) <= 5) { xThing += (5 * Mathf.Sign(xThing)); }
        if (Mathf.Abs(yThing) <= 5) { yThing += (5 * Mathf.Sign(yThing)); }

        // Defines the min and max ranges for x and y


        pos = new Vector2(xThing, yThing);
        

        Debug.Log(pos);
       

        // Choose a new goods to spawn from the array (note I specifically call it a 'prefab' to avoid confusing myself!)
        GameObject goodsPrefab = theGoodies[Random.Range(0, theGoodies.Length)];

        // Creates the random object at the random 2D position

        GameObject newGoods = (GameObject)Instantiate(goodsPrefab, pos, transform.rotation);

        newGoods.GetComponent<Rigidbody2D>().AddForce((GameObject.FindGameObjectWithTag("Player").transform.position - newGoods.transform.position).normalized * Random.Range(50,200));
        newGoods.GetComponent<Rigidbody2D>().MoveRotation(Quaternion.Euler(0, 0, Random.Range(0, 360)));
        

        // If I wanted to get the result of instantiate and fiddle with it, I might do this instead:
        //GameObject newGoods = (GameObject)Instantiate(goodsPrefab, pos)
        //newgoods.something = somethingelse;
    }
}