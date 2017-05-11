using UnityEngine;
using System.Collections;
using System.Linq;
using System.Text;
using Events;
using NeuralLogic.Infrastructure;
using TinyMessenger;
using UnityEngine.UI;

public class RankingGenerator : MonoBehaviour
{
    public GameObject CarHolder;
    private TinyMessageSubscriptionToken _eventToken;
    private GameObject _currentlyTrackingCar;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        var builder = new StringBuilder();
        builder.AppendLine("Ranking:");

        foreach (var m in CarHolder.transform
            .Cast<Transform>()
            .Select(t => t.gameObject.GetComponent<CarMovement>())
            .OrderByDescending(m => m.Points))
        {
            var icon = m.gameObject == _currentlyTrackingCar ? "[x]" : "[ ]";
            builder.AppendFormat("{0} B: {1:P0} P: {2} \n", icon, m.BatteryLevel,m.Points);
        }

        GetComponent<Text>().text = builder.ToString();
    }

    private void OnEnable()
    {
        _eventToken = EventManager.Instance.Subscribe<CameraTargetChange>(e => _currentlyTrackingCar = e.NewTargetCar);
    }


    private void OnDisable()
    {
        EventManager.Instance.Unsubscribe<CameraTargetChange>(_eventToken);
    }
}