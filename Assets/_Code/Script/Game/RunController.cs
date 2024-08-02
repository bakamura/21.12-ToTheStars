using UnityEngine;
using Naka;
using Stars.Player;

namespace Stars.Game {
    public class RunController : Singleton<RunController> {

        public Run CurrentRun { get; private set; }
        public bool IsInRun { get; private set; }
        [SerializeField, Min(0)] private float _scoreGain;

        protected override void Awake() {
            base.Awake();

            DontDestroyOnLoad(this);
            StopRun();
        }

        private void Start() {
            RunStarter.OnPlayerEntryEnd.AddListener(NewRun);
            PlayerHealth.OnDeath.AddListener(StopRun);
        }

        private void Update() {
            if (IsInRun) CurrentRun.AddScore(_scoreGain * PlayerMovement.Instance.SpeedCurrent * Time.deltaTime);
        }

        private void NewRun() {
            IsInRun = true;
        }

        public void StopRun() {
            IsInRun = false;
            CurrentRun = new Run();
            CurrentRun.AddScore(0);
            CurrentRun.AddCurrency(0);
        }

    }
}
