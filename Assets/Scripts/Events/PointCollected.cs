using Models;
using TinyMessenger;
using UnityEngine;

namespace Events
{
    public class PointCollected : ITinyMessage
    {
        public PointType PointType { get; set; }
        public Vector2 Location { get; set; }
        public object Sender { get; private set; }
        public GameObject PointObject { get; set; }
    }
}