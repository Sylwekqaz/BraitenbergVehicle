using System;
using Events;
using Models;
using NeuralLogic.Infrastructure;
using TinyMessenger;
using UnityEngine;
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
    private TinyMessageSubscriptionToken _eventToken;

    // Use this for initialization
    private void Start()
    {
        for (int i = 0; i < GoodPointCount; i++)
        {
            AddPoint(PointType.Good);
        }

        for (int i = 0; i < BadPointCount; i++)
        {
            AddPoint(PointType.Bad);
        }
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
        _eventToken = EventManager.Instance.Subscribe<PointCollected>((e) =>
        {
            Destroy(e.PointObject);
            AddPoint(e.PointType);
        });
    }

    private void OnDisable()
    {
        EventManager.Instance.Unsubscribe<PointCollected>(_eventToken);
    }
}