using System.Linq;
using System.Runtime.InteropServices;
using Events;
using NeuralLogic.Infrastructure;
using TinyMessenger;
using UnityEngine;

public class CameraTracking : MonoBehaviour
{
    public GameObject CarsHolder;
    public float DampTime = 0.15f;
    public float Period = 0.1f;
    public float SnapDistance = 3f;


    private Transform _target;
    private float _nextActionTime;
    private Vector3 _velocity = Vector3.zero;
    private bool _switchingMode;
    private TinyMessageSubscriptionToken _eventToken;

    // Use this for initialization
    private void Start()
    {
        SetTarget(CarsHolder.transform
            .Cast<Transform>()
            .First());
    }

    // Update is called once per frame
    private void Update()
    {
        if (Time.time > _nextActionTime)
        {
            _nextActionTime += Period;
            var best = CarsHolder.transform
                .Cast<Transform>()
                .OrderByDescending(o => o.GetComponent<CarMovement>().Points)
                .First();

            SetTarget(best);
        }

        if (!_target) return;

        var point = GetComponent<Camera>().WorldToViewportPoint(_target.position);
        var delta = _target.position - GetComponent<Camera>()
                        .ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z)); //(new Vector3(0.5, 0.5, point.z));
        var destination = transform.position + delta;

        if (_switchingMode)
        {
            transform.position = Vector3.SmoothDamp(transform.position, destination, ref _velocity, DampTime);
            if (delta.magnitude < SnapDistance)
            {
                _switchingMode = false;
            }
        }
        else
        {
            transform.position = destination;
        }
    }

    private void SetTarget(Transform target)
    {
        if (target == _target) return;

        Debug.Log(target.GetComponent<CarMovement>().NeuralNet.Weights);

        _target = target;
        _switchingMode = true;
        EventManager.Instance.Publish(new CameraTargetChange
        {
            NewTargetCar = target.gameObject,
        });
    }

    private void OnEnable()
    {
        _eventToken = EventManager.Instance.Subscribe<BatteryDrained>(e =>
        {
            if (e.Sender == _target.gameObject)
            {
                _nextActionTime = Time.time+0.1f;
            }
        });
    }


    private void OnDisable()
    {
        EventManager.Instance.Unsubscribe<BatteryDrained>(_eventToken);
    }
}