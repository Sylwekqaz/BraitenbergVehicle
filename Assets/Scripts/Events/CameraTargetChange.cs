using TinyMessenger;
using UnityEngine;

namespace Events
{
    public class CameraTargetChange : ITinyMessage
    {
        public GameObject NewTargetCar { get; set; }

        public object Sender
        {
            get { return null; }
        }
    }
}