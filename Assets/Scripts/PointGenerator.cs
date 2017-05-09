using UnityEngine;
using System.Collections;
using Events;

public class PointGenerator : MonoBehaviour
{

    public GameObject GoodPointsHolder;
    public GameObject BadPointsHolder;

    public GameObject GoodPoint;
    public GameObject BadPoint;

    public int BadPointCount;
    public int GoodPointCount;

    public int ArenaSize;

    // Use this for initialization
    void Start()
    {
        for (int i = 0; i < GoodPointCount; i++)
        {
            AddGoodPoint();
        }

        for (int i = 0; i < BadPointCount; i++)
        {
            AddBadPoint();
        }
    }

    private void AddBadPoint()
    {
        AddPoint(BadPoint);
    }

    private void AddGoodPoint()
    {
        AddPoint(GoodPoint);
    }

    private void AddPoint(GameObject pointType)
    {
        Instantiate(pointType, GetRandomPosition(), Quaternion.identity, GoodPointsHolder.transform);
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


    void OnEnable()
    {
        EventManager.StartListening("GoodPointCollected", AddGoodPoint);
        EventManager.StartListening("BadPointCollected", AddBadPoint);
    }

    void OnDisable()
    {
        EventManager.StopListening("GoodPointCollected", AddGoodPoint);
        EventManager.StopListening("BadPointCollected", AddBadPoint);
    }


}