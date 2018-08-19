using System;
using SimpleGPIO.Boards;
using SimpleGPIO.GPIO;

namespace Robot.API
{
    public class PiRobot : IRobot
    {
        private readonly IPinInterface _driveMotor;
        private readonly IPinInterface _flagMotor;
        private readonly IPinInterface _buzzer;

        public PiRobot(RaspberryPi pi)
        {
            _driveMotor = pi.Pin13;
            _flagMotor = pi.Pin15;
            _buzzer = pi.Pin40;

            var motorDriver = pi.Pin11;
            motorDriver.TurnOn();
        }

        public void GoForward() => _driveMotor.TurnOnFor(TimeSpan.FromSeconds(1));

        public void SpinFlag() => _flagMotor.TurnOnFor(TimeSpan.FromSeconds(1));

        public void Beep()
        {
            _buzzer.TurnOnFor(TimeSpan.FromSeconds(0.1));
            _buzzer.TurnOffFor(TimeSpan.FromSeconds(0.1));
            _buzzer.TurnOnFor(TimeSpan.FromSeconds(0.3));
        }
    }
}