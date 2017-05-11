using UnityEngine;
using TinyMessenger;

public class CarsGenerator : MonoBehaviour
{
    public GameObject CarPrefab;
    public int CarsCount;
    public int ArenaSize;


    public GameObject BadPoints;
    public GameObject GoodPoints;

    // Use this for initialization
    void Start()
    {
        for (int i = 0; i < CarsCount; i++)
        {
            AddPoint();
        }
    }

    private void AddPoint()
    {
        var instance = Instantiate(CarPrefab, GetRandomPosition(), Quaternion.identity, transform);
        var carMovement = ((GameObject) instance).GetComponent<CarMovement>();
        carMovement.BadPoints = BadPoints;
        carMovement.GoodPoints = GoodPoints;
    }

    private Vector3 GetRandomPosition()
    {
        var arenaHalfSize = ArenaSize / 2;
        return new Vector3(Random.Range(-arenaHalfSize, arenaHalfSize), Random.Range(-arenaHalfSize, arenaHalfSize));
    }

    // Update is called once per frame
    void Update()
    {
    }

    
}