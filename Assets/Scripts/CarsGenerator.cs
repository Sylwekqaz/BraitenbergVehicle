using System;
using Events;
using NeuralLogic.Infrastructure;
using UnityEngine;
using TinyMessenger;
using Random = UnityEngine.Random;

public class CarsGenerator : MonoBehaviour
{
    public GameObject CarPrefab;
    public int CarsCount;
    public int ArenaSize;


    public GameObject BadPoints;
    public GameObject GoodPoints;
    private TinyMessageSubscriptionToken _eventToken;

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

    private void OnEnable()
    {
        _eventToken = EventManager.Instance.Subscribe<BatteryDrained>(CarBatteryDrainedEvent);
    }

    private void CarBatteryDrainedEvent(BatteryDrained batteryDrained)
    {
        Destroy(batteryDrained.Sender);
    }

    private void OnDisable()
    {
        EventManager.Instance.Unsubscribe<BatteryDrained>(_eventToken);
    }


}