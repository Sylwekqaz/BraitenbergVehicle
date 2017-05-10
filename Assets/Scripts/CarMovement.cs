using System;
using System.Linq;
using Events;
using NeuralLogic;
using NeuralLogic.CarNeuralBechavior;
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

    public int Points;

    private CarNeuralNet _neuralNet; 

    // OnTriggerEnter2D is called when the Collider2D other enters the trigger (2D physics only)
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("GoodPoint"))
        {
            EventManager.TriggerEvent("GoodPointCollected");
            Points++;
            Destroy(collision.gameObject);
        }


        if (collision.gameObject.CompareTag("BadPoint"))
        {
            EventManager.TriggerEvent("BadPointCollected");
            Points--;
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.CompareTag("Wall"))
        {
            transform.position = Vector3.zero;
        }
    }

    // Use this for initialization
    private void Start()
    {
        _neuralNet = CarNeuralNet.GetDefaultNet();
    }

    // Update is called once per frame
    private void Update()
    {
        var inputValues = CollectValues();
        var o = _neuralNet.RunNeuralNet(inputValues);


        Debug.LogFormat("{0}   :    {1}", o.LeftWheelMultiplier, o.RightWheelMultiplier);

        LeftWheel.GetComponent<Rigidbody2D>().velocity = transform.up * Speed * o.LeftWheelMultiplier;
        RightWheel.GetComponent<Rigidbody2D>().velocity = transform.up * Speed * o.RightWheelMultiplier;
    }

    private CarInputValues CollectValues()
    {
        return new CarInputValues()
        {
            LeftAntenaGoodSignal = GetAntenaValue(LeftAntena, GoodPoints),
            LeftAntenaBadSignal = GetAntenaValue(LeftAntena, BadPoints),
            RightAntenaGoodSignal = GetAntenaValue(RightAntena, GoodPoints),
            RightAntenaBadSignal = GetAntenaValue(RightAntena, BadPoints),
            BatteryLevel = Points,
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
}