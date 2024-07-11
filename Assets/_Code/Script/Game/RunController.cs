using UnityEngine;

namespace Stars.Game {
    public class RunController : MonoBehaviour {

        public Run CurrentRun { get; private set; }

        public void NewRun() {
            CurrentRun = new Run();
        }

    }
}
