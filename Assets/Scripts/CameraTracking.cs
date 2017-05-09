using UnityEngine;

public class CameraTracking : MonoBehaviour
{
    public GameObject Target;
    private Vector3 _offset;

    // Use this for initialization
    private void Start()
    {
        _offset = transform.position - Target.transform.position;
    }

    // Update is called once per frame
    private void Update()
    {
        transform.position = Target.transform.position + _offset;
    }
}