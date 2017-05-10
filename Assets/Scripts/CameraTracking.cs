using System.Linq;
using UnityEngine;

public class CameraTracking : MonoBehaviour
{
    public GameObject CarsHolder;
    private GameObject _trackingTarget;
    private Vector3 _offset;

    private float nextActionTime = 0.0f;
    public float period = 0.1f;

    // Use this for initialization
    private void Start()
    {
        _trackingTarget = CarsHolder.transform
            .Cast<Transform>()
            .First()
            .gameObject;

        var z = transform.position.z - _trackingTarget.transform.position.z;
        _offset = new Vector3(0,0,z);
    }

    // Update is called once per frame
    private void Update()
    {
        if (Time.time > nextActionTime)
        {
            nextActionTime += period;
            _trackingTarget = CarsHolder.transform
                .Cast<Transform>()
                .Select(t => t.gameObject)
                .OrderByDescending(o => o.GetComponent<CarMovement>().Points)
                .First();
        }

        transform.position = _trackingTarget.transform.position + _offset;
    }
}