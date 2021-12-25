using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour {

    public static MapGenerator instance { get; private set; } = null;

    [SerializeField] private GameObject[] _presetArea;
    [SerializeField] private Vector3 _spawnLocation;

    [System.NonSerialized] public bool isMoving = true;
    [SerializeField] private float _baseSpeed;
    public float speedMultiplier;
    private List<GameObject> _currentAreas = new List<GameObject>();
    [SerializeField] private float _distanceToNewArea;

    private void Awake() {
        if (instance == null) instance = this;
        else if (instance != this) Destroy(gameObject);
    }

    private void Start() {
        // Generates the first terrain and the upcoming one.
        GenerateArea().transform.position = Vector3.zero;
        GenerateArea();
    }

    private void FixedUpdate() {
        if (isMoving) {
            // Speed multiplier is in sqrt to assure a "caping" nature to the game
            Vector3 displacement = Vector3.left * _baseSpeed * Mathf.Sqrt(speedMultiplier) * Time.deltaTime;
            foreach (GameObject area in _currentAreas) area.transform.position += displacement;

            if (_currentAreas[0].transform.position.x < -20) {
                GameObject areaToRemove = _currentAreas[0];
                _currentAreas.Remove(areaToRemove);
                Destroy(areaToRemove);
            }
            if (_currentAreas[_currentAreas.Count - 1].transform.position.x < _distanceToNewArea) GenerateArea().transform.position = _currentAreas[_currentAreas.Count - 2].transform.position + new Vector3(_distanceToNewArea, 0, 0);
        }
    }

    private GameObject GenerateArea() {
        GameObject instantiatedArea = Instantiate(_presetArea[Random.Range(0, (_presetArea.Length - 1))], _spawnLocation, Quaternion.identity);
        _currentAreas.Add(instantiatedArea);
        return instantiatedArea;
    }

}
