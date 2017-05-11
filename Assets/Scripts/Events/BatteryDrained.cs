using TinyMessenger;
using UnityEngine;

namespace Events
{
    public class BatteryDrained : ITinyMessage
    {
        public GameObject Sender { get; set; }

        object ITinyMessage.Sender
        {
            get { return Sender; }
        }
    }
}