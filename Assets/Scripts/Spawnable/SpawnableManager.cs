using UnityEngine;

public class SpawnableManager : MonoBehaviour
{
    // Player
    [SerializeField] private Transform playerTransform;

    // Spawn
    [SerializeField] private GameObject[] spawnables;

    [SerializeField] private float spawnInterval = 10;
    private float spawnTimer = 0f;

    [SerializeField] private float minSpawnDistance, maxSpawnDistance;

    private void Update()
    {
        spawnTimer += Time.deltaTime;
        if (spawnTimer >= spawnInterval)
        {
            spawnTimer = 0f;
            Spawn();
        }
    }

    private void Spawn()
    {
        // figure out spawn position
        Vector2 randUnitCircle = Random.insideUnitCircle.normalized;

        float xPos = randUnitCircle.x * Random.Range(minSpawnDistance, maxSpawnDistance);
        float yPos = randUnitCircle.y * Random.Range(minSpawnDistance, maxSpawnDistance);

        Vector2 position = new Vector2(xPos, yPos);

        // spawn and shoot
        GameObject spawnable = Instantiate(spawnables[Random.Range(0, spawnables.Length)], position, Quaternion.Euler(0, 0, Random.Range(0, 360)));
        spawnable.GetComponent<Spawnable>().ShootTowards(playerTransform.position);

        if (spawnable.CompareTag("Battery"))
            AudioManager.i.Play(AudioManager.AudioName.BatterySpawn);
    }
}