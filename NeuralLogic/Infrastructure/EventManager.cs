using TinyMessenger;

namespace NeuralLogic.Infrastructure
{
    public static class EventManager
    {
        public static TinyMessengerHub Instance { get; } = new TinyMessengerHub();
    }
}