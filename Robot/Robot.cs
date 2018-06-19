namespace Robot.API
{
    public interface Robot
    {
        void GoForward();
        void GoBackward();
        void SpinFlag();
        void TurnLightOn();
        void TurnLightOff();
        void Beep();
    }
}