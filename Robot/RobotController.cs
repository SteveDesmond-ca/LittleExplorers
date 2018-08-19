using Microsoft.AspNetCore.Mvc;

namespace Robot.API
{
    [ApiController]
    public class RobotController : ControllerBase
    {
        private readonly IRobot _robot;

        public RobotController(IRobot robot) => _robot = robot;

        [HttpPost("/drive")]
        public void GoForward() => _robot.GoForward();

        [HttpPost("/spin")]
        public void SpinFlag() => _robot.SpinFlag();

        [HttpPost("/beep")]
        public void Beep() => _robot.Beep();
    }
}
