using System;
using System.Linq;
using Events;
using NeuralLogic.CarNeuralBechavior;
using NeuralLogic.Genetics;
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
        //AddCar(CarNeuralNet.GetDefaultMutatedNet());
        for (int i = 0; i < CarsCount; i++)
        {
            AddCar(CarNeuralNet.GetDefaultMutatedNet(0.5f),i);
        }
    }

    private void AddCar(CarNeuralNet neuralNet, int playerPrefsIndex=0)
    {
        var instance = Instantiate(CarPrefab, GetRandomPosition(), Quaternion.identity, transform);
        var carMovement = ((GameObject) instance).GetComponent<CarMovement>();

        carMovement.NeuralNet = neuralNet;
        carMovement.BadPoints = BadPoints;
        carMovement.GoodPoints = GoodPoints;
        carMovement.Points = PlayerPrefs.GetInt("car" + playerPrefsIndex + "points");
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


    private void OnDisable()
    {
        EventManager.Instance.Unsubscribe<BatteryDrained>(_eventToken);
    }

    private void CarBatteryDrainedEvent(BatteryDrained batteryDrained)
    {
        Destroy(batteryDrained.Sender);
        var cars = transform.Cast<Transform>()
            .Select(t => t.GetComponent<CarMovement>());
        var carMovements = cars.ToArray();


        var father = carMovements.GetRandomItem(c => c.Points);
        var mother = carMovements.GetRandomItem(c => c.Points);

        var sonNet = CarNeuralNetRecombine.Recombine(father.NeuralNet,mother.NeuralNet,0.05f);
        AddCar(sonNet);
    }

    void OnApplicationQuit()
    {
        int index = 0;
        foreach (Transform car in transform)
        {
            PlayerPrefs.SetInt("car"+index+"points",car.gameObject.GetComponent<CarMovement>().Points);
            index++;
        }
    }
}