using System.Collections;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;
using Naka;

namespace Stars.Save {
    public class SaveSystem : Singleton<SaveSystem> {

        public SaveProgress Progress { get; private set; }
        public SaveOptions Options { get; private set; }

        private string _progressPath;
        private string _optionsPath;

        protected override void Awake() {
            base.Awake();

            _progressPath = $"{Application.persistentDataPath}/Progress";
            _optionsPath = $"{Application.persistentDataPath}/Configs";

            StartCoroutine(LoadData());
        }

        private IEnumerator LoadData() {
            Task<string> readTask;
            if (File.Exists(_progressPath)) {
                readTask = File.ReadAllTextAsync(_progressPath);

                yield return readTask;

                Progress = JsonUtility.FromJson<SaveProgress>(Encryption.Decrypt(readTask.Result));
            }
            if (File.Exists(_optionsPath)) {
                readTask = File.ReadAllTextAsync(_optionsPath);

                yield return readTask;

                Options = JsonUtility.FromJson<SaveOptions>(Encryption.Decrypt(readTask.Result));
            }

        }

        private IEnumerator SaveProgress() {
            yield return File.WriteAllTextAsync(_progressPath, Encryption.Encrypt(JsonUtility.ToJson(Progress)));
        }

        private IEnumerator SaveOptions() {
            yield return File.WriteAllTextAsync(_optionsPath, Encryption.Encrypt(JsonUtility.ToJson(Options)));
        }

    }
}