using System;
using Microsoft.AspNetCore.Mvc;
using SimpleGPIO.Boards;
using SimpleGPIO.Components;
using SimpleGPIO.GPIO;

namespace Robot.API
{
    [ApiController]
    public class RobotController : ControllerBase
    {
        private readonly Motor _drive;
        private readonly Motor _flag;
        private readonly IPinInterface _buzzer;

        public RobotController(RaspberryPi pi)
        {
            _drive = new Motor(pi.Pin11, pi.Pin13);
            _flag = new Motor(pi.Pin33, pi.Pin35);
            _buzzer = pi.Pin40;
        }

        [HttpPost("/drive")]
        public void GoForward() => _drive.RunFor(TimeSpan.FromSeconds(2));

        [HttpPost("/spin")]
        public void SpinFlag() => _flag.RunFor(TimeSpan.FromSeconds(1));

        [HttpPost("/beep")]
        public void Beep()
        {
            _buzzer.TurnOnFor(TimeSpan.FromSeconds(0.1));
            _buzzer.TurnOffFor(TimeSpan.FromSeconds(0.1));
            _buzzer.TurnOnFor(TimeSpan.FromSeconds(0.3));
        }
    }
}