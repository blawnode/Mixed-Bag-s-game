using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGImageFollow : MonoBehaviour
{
    [SerializeField] Transform toFollow;
    [SerializeField] float imgWidth;  // in Unity units
    [SerializeField] float imgHeight;  // in Unity units

    void Update()
    {
        if(toFollow.position.x - transform.position.x >= imgWidth / 2)
        {
            transform.position = new Vector2(transform.position.x + imgWidth, transform.position.y);
        }
        else if(toFollow.position.x - transform.position.x <= -imgWidth / 2)
        {
            transform.position = new Vector2(transform.position.x - imgWidth, transform.position.y);
        }
        else if(toFollow.position.y - transform.position.y >= imgHeight / 2)
        {
            transform.position = new Vector2(transform.position.x, transform.position.y + imgHeight);
        }
        if(toFollow.position.y - transform.position.y <= -imgHeight / 2)
        {
            transform.position = new Vector2(transform.position.x, transform.position.y - imgHeight);
        }
    }
}
