using System.Linq;
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

    // Use this for initialization
    private void Start()
    {
        _target = CarsHolder.transform
            .Cast<Transform>()
            .First();
    }

    // Update is called once per frame
    private void Update()
    {
        if (Time.time > _nextActionTime)
        {
            _nextActionTime += Period;
            _target = CarsHolder.transform
                .Cast<Transform>()
                .OrderByDescending(o => o.GetComponent<CarMovement>().BatteryLevel)
                .First();

            _switchingMode = true;
        }

        if (!_target) return; 

        var point = GetComponent<Camera>().WorldToViewportPoint(_target.position);
        var delta = _target.position - GetComponent<Camera>().ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z)); //(new Vector3(0.5, 0.5, point.z));
        var destination = transform.position + delta;

        if (_switchingMode)
        {
            transform.position = Vector3.SmoothDamp(transform.position, destination, ref _velocity, DampTime);
            if (delta.magnitude<SnapDistance)
            {
                _switchingMode = false;
            }
        }
        else
        {
            transform.position = destination;
        }
    }
}