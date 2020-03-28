using System;

namespace Events
{
    public interface IObservableMainEvents
    {
        void SubscribeCompletedGame(Action observer);
        void UnSubscribeCompletedGame(Action observer);
        void SubscribeGameOver(Action observer);
        void UnSubscribeGameOver(Action observer);
    }
}

