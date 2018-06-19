using Microsoft.AspNetCore.Mvc;

namespace Robot.API.Controllers
{
    [ApiController]
    public class RobotController : ControllerBase
    {
        private readonly Robot _robot;
        public RobotController(Robot robot) => _robot = robot;

        [HttpPost("/forward")]
        public void GoForward() => _robot.GoForward();

        [HttpPost("/backward")]
        public void GoBackward() => _robot.GoBackward();

        [HttpPost("/flag")]
        public void SpinFlag() => _robot.SpinFlag();

        [HttpPost("/light-on")]
        public void TurnLightOn() => _robot.TurnLightOn();

        [HttpPost("/light-off")]
        public void TurnLightOff() => _robot.TurnLightOff();

        [HttpPost("/beep")]
        public void Beep() => _robot.Beep();
    }
}
