using System;
using System.Linq;
using Events;
using Models;
using NeuralLogic.CarNeuralBechavior;
using NeuralLogic.Infrastructure;
using UnityEngine;

public class CarMovement : MonoBehaviour
{
    public int Speed;

    public GameObject LeftWheel;
    public GameObject RightWheel;
    public GameObject LeftAntena;
    public GameObject RightAntena;

    public GameObject BadPoints;
    public GameObject GoodPoints;

    private float _batteryLevel = 1;
    public int Points;
    public float BatteryLevel { get { return _batteryLevel; } }

    public float BatteryDrainPerSecond = 0.1f;
    private CarNeuralNet _neuralNet;

    // OnTriggerEnter2D is called when the Collider2D other enters the trigger (2D physics only)
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("GoodPoint"))
        {
            EventManager.Instance.Publish(new PointCollected()
            {
               PointType = PointType.Good,
               Location = collision.gameObject.transform.position,
               PointObject = collision.gameObject
            });
            ChangeBatteryLevel(0.1f);
            Points++;
        }


        if (collision.gameObject.CompareTag("BadPoint"))
        {
            EventManager.Instance.Publish(new PointCollected()
            {
                PointType = PointType.Bad,
                Location = collision.gameObject.transform.position,
                PointObject = collision.gameObject
            });

            ChangeBatteryLevel(-0.1f);
            Points--;
        }
    }

    // Use this for initialization
    private void Start()
    {
        _neuralNet = CarNeuralNet.GetDefaultMutatedNet(0.05f);
    }

    // Update is called once per frame
    private void Update()
    {
        var inputValues = CollectValues();
        var o = _neuralNet.RunNeuralNet(inputValues);


        LeftWheel.GetComponent<Rigidbody2D>().velocity = transform.up * Speed * o.LeftWheelMultiplier;
        RightWheel.GetComponent<Rigidbody2D>().velocity = transform.up * Speed * o.RightWheelMultiplier;

        ChangeBatteryLevel(-BatteryDrainPerSecond * Time.deltaTime);
    }

    private CarInputValues CollectValues()
    {
        return new CarInputValues()
        {
            LeftAntenaGoodSignal = GetAntenaValue(LeftAntena, GoodPoints),
            LeftAntenaBadSignal = GetAntenaValue(LeftAntena, BadPoints),
            RightAntenaGoodSignal = GetAntenaValue(RightAntena, GoodPoints),
            RightAntenaBadSignal = GetAntenaValue(RightAntena, BadPoints),
            BatteryLevel = _batteryLevel,
        };
    }


    private float GetAntenaValue(GameObject antena, GameObject pointsHolder)
    {
        var antenaPosition = antena.transform.position;

        var antenaValue = pointsHolder
            .transform.Cast<Transform>() //foreach
            .Sum(point => GravityForceLike(point.position, antenaPosition));
        return antenaValue;
    }

    private static float GravityForceLike(Vector3 point, Vector3 antenaPosition)
    {
        return 1 / (point - antenaPosition).sqrMagnitude;
    }

    private static float NormDistributionLike(Vector3 point, Vector3 antenaPosition)
    {
        var distance = (point - antenaPosition).sqrMagnitude;
        const int pow = 8; // wyliczenie 2c^2   Math.Pow(2 * 2, 2);
        return (float)Math.Exp(-distance / pow);
    }

    private void ChangeBatteryLevel(float amount)
    {
        _batteryLevel = (_batteryLevel + amount).Clamp(0, 1);
        if (_batteryLevel==0)
        {
            EventManager.Instance.Publish(new BatteryDrained()
            {
                Sender = gameObject,
            });
        }
    }

    
}