using System;
using SimpleGPIO.Boards;

namespace Robot.API
{
    public class PiRobot : Robot
    {
        private readonly RaspberryPi _pi;

        public PiRobot(RaspberryPi pi)
        {
            _pi = pi;
        }

        public void GoForward()
        {
            throw new NotImplementedException();
        }

        public void GoBackward()
        {
            throw new NotImplementedException();
        }

        public void SpinFlag()
        {
            throw new NotImplementedException();
        }

        public void TurnLightOn()
        {
            _pi.Pin16.TurnOn();
        }

        public void TurnLightOff()
        {
            _pi.Pin16.TurnOff();
        }

        public void Beep()
        {
            throw new NotImplementedException();
        }
    }
}