using System;
using SimpleGPIO.Boards;
using SimpleGPIO.Components;
using SimpleGPIO.GPIO;

namespace Robot.API
{
    public class PiRobot : IRobot
    {
        private readonly Motor _drive;
        private readonly Motor _flag;
        private readonly IPinInterface _buzzer;

        public PiRobot(RaspberryPi pi)
        {
            _drive = new Motor(pi.Pin11, pi.Pin13);
            _flag = new Motor(pi.Pin33, pi.Pin35);
            _buzzer = pi.Pin40;

            var motorDriver = pi.Pin11;
            motorDriver.TurnOn();
        }

        public void GoForward() => _drive.RunFor(TimeSpan.FromSeconds(1));

        public void SpinFlag() => _flag.RunFor(TimeSpan.FromSeconds(1));

        public void Beep()
        {
            _buzzer.TurnOnFor(TimeSpan.FromSeconds(0.1));
            _buzzer.TurnOffFor(TimeSpan.FromSeconds(0.1));
            _buzzer.TurnOnFor(TimeSpan.FromSeconds(0.3));
        }
    }
}