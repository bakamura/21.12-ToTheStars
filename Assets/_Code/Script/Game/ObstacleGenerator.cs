using System.Collections.Generic;
using UnityEngine;
using Stars.Player;

namespace Stars.Game {
    public class ObstacleGenerator : MonoBehaviour {


        [Header("Parameters")]

        [SerializeField] private BoxCollider2D[] _obstacleCoursePrefab;
        private List<BoxCollider2D> _obstacleCourseInstances = new List<BoxCollider2D>();
        private List<int> _obstacleCourseInstanceablesId = new List<int>();
        private Queue<int> _obstacleCourseInstancesId = new Queue<int>();
        [SerializeField, Min(0)] private int _obstacleCourseAmount;

        [Header("Cache")]

        private Vector3 _deltaCache = Vector3.right;

        private void Awake() {
            for (int i = 0; i < _obstacleCoursePrefab.Length; i++) _obstacleCourseInstanceablesId.Add(i);
            for(int i = 0; i < _obstacleCourseAmount; i++) GenerateObstacleCourse();
        }

        private void Update() {
            _deltaCache[0] = PlayerMovement.Instance.SpeedCurrent * Time.deltaTime;
            foreach (BoxCollider2D obstacleCourse in _obstacleCourseInstances) obstacleCourse.transform.position -= _deltaCache;
            if (_obstacleCourseInstances[0].transform.position.x + (_obstacleCourseInstances[0].size.x / 2) < -8) {
                DestroyObstacleCourse();
                GenerateObstacleCourse();
            }
        }

        private void GenerateObstacleCourse() {
            int seed = Random.Range(0, _obstacleCourseInstanceablesId.Count);
            _obstacleCourseInstances.Add(Instantiate(_obstacleCoursePrefab[_obstacleCourseInstanceablesId[seed]], transform));
            _obstacleCourseInstances[_obstacleCourseInstances.Count - 1].transform.position = 
                _obstacleCourseInstances.Count > 1 ? 
                (_obstacleCourseInstances[_obstacleCourseInstances.Count - 2].transform.position.x + (_obstacleCourseInstances[_obstacleCourseInstances.Count - 2].size.x + _obstacleCourseInstances[_obstacleCourseInstances.Count - 1].size.x) / 2f) * Vector3.right :
                (_obstacleCourseInstances[_obstacleCourseInstances.Count - 1].size.x / 2f) * Vector3.right;

            _obstacleCourseInstancesId.Enqueue(_obstacleCourseInstanceablesId[seed]);
            _obstacleCourseInstanceablesId.RemoveAt(seed);
        }

        private void DestroyObstacleCourse() {
            _obstacleCourseInstanceablesId.Add(_obstacleCourseInstancesId.Dequeue());
            Destroy(_obstacleCourseInstances[0].gameObject);
            _obstacleCourseInstances.RemoveAt(0);
        }

    }
}
