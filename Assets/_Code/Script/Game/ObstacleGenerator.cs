using System.Collections.Generic;
using UnityEngine;
using Stars.Player;

namespace Stars.Game {
    public class ObstacleGenerator : MonoBehaviour {


        [Header("Parameters")]

        [SerializeField] private BoxCollider2D[] _obstacleCoursePrefab;
        private List<BoxCollider2D> _obstacleCourseInstances = new List<BoxCollider2D>();
        [SerializeField, Min(0)] private int _obstacleCourseAmount;

        [Header("Cache")]

        private Vector3 _deltaCache = Vector3.right;

        private void Awake() {
            for(int i = 0; i < _obstacleCourseAmount; i++) GenerateObstacleCourse();
        }

        private void Update() {
            _deltaCache[0] = PlayerMovement.Instance.SpeedCurrent * Time.deltaTime;
            foreach (BoxCollider2D obstacleCourse in _obstacleCourseInstances) obstacleCourse.transform.position -= _deltaCache;
            if (_obstacleCourseInstances[0].transform.position.x + (_obstacleCourseInstances[0].size.x / 2) < -8) {
                Destroy(_obstacleCourseInstances[0]);
                GenerateObstacleCourse();
            }
        }

        private void GenerateObstacleCourse() {
            int seed = Random.Range(0, _obstacleCoursePrefab.Length);
            //while() // Prevent duplicates
            _obstacleCourseInstances.Add(Instantiate(_obstacleCoursePrefab[seed]));
        }

    }
}
