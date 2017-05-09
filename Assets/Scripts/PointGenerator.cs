using System;
using UnityEngine;
using Events;
using Random = UnityEngine.Random;

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
    private void Start()
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
        AddPoint(PointType.Bad);
    }

    private void AddGoodPoint()
    {
        AddPoint(PointType.Good);
    }

    private void AddPoint(PointType pointType)
    {
        switch (pointType)
        {
            case PointType.Good:
                Instantiate(GoodPoint, GetRandomPosition(), Quaternion.identity, GoodPointsHolder.transform);
                break;
            case PointType.Bad:
                Instantiate(BadPoint, GetRandomPosition(), Quaternion.identity, BadPointsHolder.transform);
                break;
            default:
                throw new ArgumentOutOfRangeException("pointType", pointType, null);
        }
    }

    private Vector3 GetRandomPosition()
    {
        var arenaHalfSize = ArenaSize / 2;
        return new Vector3(Random.Range(-arenaHalfSize, arenaHalfSize), Random.Range(-arenaHalfSize, arenaHalfSize));
    }

    // Update is called once per frame
    private void Update()
    {
    }


    private void OnEnable()
    {
        EventManager.StartListening("GoodPointCollected", AddGoodPoint);
        EventManager.StartListening("BadPointCollected", AddBadPoint);
    }

    private void OnDisable()
    {
        EventManager.StopListening("GoodPointCollected", AddGoodPoint);
        EventManager.StopListening("BadPointCollected", AddBadPoint);
    }

    private enum PointType
    {
        Good,
        Bad,
    }
}