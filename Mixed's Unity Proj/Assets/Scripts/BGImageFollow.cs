using UnityEngine;

public class BGImageFollow : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private float imgWidth, imgHeight;  // in Unity units

    void Update()
    {
        if(playerTransform.position.x - transform.position.x >= imgWidth / 2)
            transform.position = new Vector2(transform.position.x + imgWidth, transform.position.y);

        else if(playerTransform.position.x - transform.position.x <= -imgWidth / 2)
            transform.position = new Vector2(transform.position.x - imgWidth, transform.position.y);

        else if(playerTransform.position.y - transform.position.y >= imgHeight / 2)
            transform.position = new Vector2(transform.position.x, transform.position.y + imgHeight);

        else if(playerTransform.position.y - transform.position.y <= -imgHeight / 2)
            transform.position = new Vector2(transform.position.x, transform.position.y - imgHeight);
    }
}
