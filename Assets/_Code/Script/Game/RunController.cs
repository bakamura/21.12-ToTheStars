using UnityEngine;
using Naka;
using Stars.Player;

namespace Stars.Game {
    public class RunController : Singleton<RunController> {

        public Run CurrentRun { get; private set; }
        private bool _isInRun;
        [SerializeField, Min(0)] private float _scoreGain;

        protected override void Awake() {
            base.Awake();

            DontDestroyOnLoad(this);
        }

        private void Update() {
            if (_isInRun) CurrentRun.AddScore(_scoreGain * PlayerMovement.Instance.SpeedCurrent * Time.deltaTime);
        }

        public void NewRun() {
            CurrentRun = new Run();
            _isInRun = true;
        }

        public void StopRun() {
            _isInRun = false;
        }

    }
}
