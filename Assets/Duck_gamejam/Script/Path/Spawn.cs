using UnityEngine;

public class Spawn : MonoBehaviour
{
    [SerializeField]
    private int spawnTime = 60;

    [SerializeField]
    private int randomSpawnTime = 60;

    [SerializeField]
    private SpawnInstance[] ducks;
    [SerializeField]
    private Path path;

    [SerializeField]
    private Transform spawnPoint;

    private int time = 0;
    private int spawnTimeNext = 90;
    private Transform duckHolder;

    void Start()
    {
        duckHolder = new GameObject("duck holder").transform;
        NewSpawnTime();
    }

    void NewSpawnTime()
    {
        spawnTimeNext = (spawnTime + Random.Range(0, randomSpawnTime));
    }

    void FixedUpdate()
    {
        time++;
        if (time > spawnTimeNext)
        {
            SpawnObject();
            NewSpawnTime();
            time = 0;
        }
    }

    void SpawnObject()
    {
        GameObject newDuck = (GameObject)GameObject.Instantiate(GetDuck(), spawnPoint.position, spawnPoint.rotation);
        newDuck.transform.parent = duckHolder;
        DuckMovement newDuckScript = newDuck.GetComponent<DuckMovement>();
        newDuckScript.path = path;
    }

    GameObject GetDuck()
    {
        int totalWeight = 0;
        for (int i = 0; i < ducks.Length; i++)
        {
            totalWeight += ducks[i].spawnWeight;
        }

        int start = 0;
        int spawnInt = Random.Range(start, totalWeight);

        int spawnCounter = 0;
        for (int i = 0; i < ducks.Length; i++){
        
            spawnCounter += ducks[i].spawnWeight;
            if(spawnInt < spawnCounter){
                return ducks[i].spanwObject;
            }
        }
        Debug.LogError("spawn error");
        return ducks[0].spanwObject;
    }
}
