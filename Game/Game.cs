using System;
using System.Drawing;
using SimpleGPIO.Boards;
using SimpleGPIO.Components;
using SimpleGPIO.GPIO;

namespace LittleExplorers.Game
{
    public class Game
    {
        private Color? _currentColor;

        private readonly IPinInterface _cpuRed;
        private readonly IPinInterface _cpuYellow;
        private readonly IPinInterface _cpuGreen;

        private readonly IPinInterface _redButton;
        private readonly IPinInterface _yellowButton;
        private readonly IPinInterface _greenButton;

        private byte _score;
        private readonly IPinInterface _score1;
        private readonly IPinInterface _score2;
        private readonly IPinInterface _score3;

        private byte _difficulty;
        private readonly RotaryEncoder _difficultyDial;
        private readonly Random _random;

        public Game(RaspberryPi pi)
        {
            _cpuRed = pi.Pin11;
            _cpuYellow = pi.Pin13;
            _cpuGreen = pi.Pin15;

            _redButton = pi.Pin16;
            _yellowButton = pi.Pin18;
            _greenButton = pi.Pin22;

            _score1 = pi.Pin36;
            _score2 = pi.Pin38;
            _score3 = pi.Pin40;

            _difficultyDial = new RotaryEncoder(pi.Pin32, pi.Pin31);
            _random = new Random();
        }

        public void Run()
        {
            _redButton.OnPowerOn(() => Guess(Color.Red));
            _yellowButton.OnPowerOn(() => Guess(Color.Yellow));
            _greenButton.OnPowerOn(() => Guess(Color.Green));

            _difficultyDial.OnIncrease(() =>
            {
                if (_difficulty < 3)
                    _difficulty++;
                StartRound();
            });

            _difficultyDial.OnDecrease(() =>
            {
                if (_difficulty > 1)
                    _difficulty--;
                StartRound();

            });

            _score = 0;
            _difficulty = 1;

            _cpuRed.TurnOnFor(TimeSpan.FromSeconds(0.5));
            _cpuYellow.TurnOnFor(TimeSpan.FromSeconds(0.5));
            _cpuGreen.TurnOnFor(TimeSpan.FromSeconds(0.5));
            _score1.TurnOnFor(TimeSpan.FromSeconds(0.5));
            _score2.TurnOnFor(TimeSpan.FromSeconds(0.5));
            _score3.TurnOnFor(TimeSpan.FromSeconds(0.5));

            StartRound();
        }

        private void StartRound()
        {
            _currentColor = null;
            SetScoreLights();
            PickNextColor();
        }

        private void PickNextColor()
        {
            Color? color = null;
            for (var flash = 0; flash < 5 * _difficulty; flash++)
            {
                color = GetRandomColor();
                ShowColorFor(color, TimeSpan.FromSeconds(1.0 / (3 * _difficulty)));
            }
            _currentColor = color;
        }

        private void ShowColorFor(Color? color, TimeSpan length)
        {
            var pin = GetPinFor(color);
            pin.TurnOnFor(length);
        }

        private IPinInterface GetPinFor(Color? color)
        {
            return color == Color.Red ? _cpuRed
                : (color == Color.Yellow ? _cpuYellow
                : (color == Color.Green ? _cpuGreen
                : null));
        }

        private Color? GetRandomColor()
        {
            switch (_random.Next(0, 3))
            {
                case 0:
                    return Color.Red;
                case 1:
                    return Color.Yellow;
                case 2:
                    return Color.Green;
                default:
                    return null;
            }
        }

        private void Guess(Color color)
        {
            if (_currentColor == null)
                return;

            ShowColorFor(_currentColor, TimeSpan.FromSeconds(2));
            if (color == _currentColor)
                _score++;
            SetScoreLights();
            NextRound();
        }

        private void NextRound()
        {
            if (_score < 3)
                StartRound();
            else
                PlayerWins();
        }

        private void PlayerWins()
        {
            for (var flash = 0; flash < 4; flash++)
            {
                _score1.TurnOnFor(TimeSpan.FromSeconds(0.2));
                _score2.TurnOnFor(TimeSpan.FromSeconds(0.2));
                _score3.TurnOnFor(TimeSpan.FromSeconds(0.2));
            }
            _score = 0;
            SetScoreLights();
            StartRound();
        }

        private void SetScoreLights()
        {
            if (_score >= 1)
                _score1.TurnOn();
            else
                _score1.TurnOff();

            if (_score >= 2)
                _score2.TurnOn();
            else
                _score2.TurnOff();

            if (_score >= 3)
                _score3.TurnOn();
            else
                _score3.TurnOff();
        }
    }
}