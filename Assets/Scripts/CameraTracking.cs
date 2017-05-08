using UnityEngine;
using System.Collections;

public class CameraTracking : MonoBehaviour
{
    public GameObject Target;
    private Vector3 _offset;

    // Use this for initialization
    void Start()
    {
        _offset = transform.position - Target.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Target.transform.position + _offset;
    }
}