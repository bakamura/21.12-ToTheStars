using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementOld : MonoBehaviour {

    public static PlayerMovementOld Instance { get; private set; } = null;

    [System.NonSerialized] public Rigidbody2D rbPlayer;

    private bool _isTrackingTouch = false;
    private Vector3 _touchInitialPos;
    private Vector3 _touchCurrentPos;
    // % of height that defines a drag touch.
    [SerializeField] private float _dragScreenPercentage;
    private float _dragDistance;

    [SerializeField] private float _baseTimeToSwitchLane;
    private bool _isMoving;
    private float _targetHeight;

    [SerializeField] private float[] _laneHeights;
    private int currentLane = 1;

    private void Awake() {
        if (Instance == null) Instance = this;
        else if (Instance != this) Destroy(gameObject);
    }

    private void Start() {
        rbPlayer = GetComponent<Rigidbody2D>();
        _dragDistance = Screen.height * _dragScreenPercentage;
    }

    private void Update() {
        DetectMovement();
#if UNITY_EDITOR
        if (!_isMoving && Input.GetKeyDown(KeyCode.UpArrow) && currentLane > 0) StartCoroutine(MoveTowardsHeight(true));
        if (!_isMoving && Input.GetKeyDown(KeyCode.DownArrow) && currentLane < 2) StartCoroutine(MoveTowardsHeight(false));
#endif
    }

    private void DetectMovement() {
        if (Input.touchCount > 0) {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began) {
                _touchInitialPos = touch.position;
                _isTrackingTouch = true;
            }
            else if (touch.phase == TouchPhase.Ended) {
                _isTrackingTouch = false;
                return;
            }

            if (_isTrackingTouch) {
                _touchCurrentPos = touch.position;

                if (Vector3.Distance(_touchInitialPos, _touchCurrentPos) > _dragDistance) {
                    Vector3 dragDirection = _touchCurrentPos - _touchInitialPos;
                    float dragAngle = Mathf.Atan2(dragDirection.y, dragDirection.x) * Mathf.Rad2Deg;
                    if (dragAngle > 45 && dragAngle < 135) {
                        if (!_isMoving && currentLane > 0) StartCoroutine(MoveTowardsHeight(true));
                        _isTrackingTouch = false;
                        return;
                    }
                    else if (dragAngle < -45 && dragAngle > -135) {
                        if (!_isMoving && currentLane < 2) StartCoroutine(MoveTowardsHeight(false));
                        _isTrackingTouch = false;
                    }
                }
            }
        }
    }

    private IEnumerator MoveTowardsHeight(bool isUp) {
        _isMoving = true;

        Vector3 initialHeight = Vector3.up * _laneHeights[currentLane];
        initialHeight[0] = transform.position.x;
        currentLane += isUp ? -1 : 1;
        Vector3 finalHeight = Vector3.up * _laneHeights[currentLane];
        finalHeight[0] = transform.position.x;
        float currentTransition = 0;
        while (true) {
            currentTransition += Time.deltaTime * MapGenerator.baseSpeed * MapGenerator.Instance.VelocityCalc() / _baseTimeToSwitchLane;
            if (currentTransition > 1) currentTransition = 1;
            transform.position = Vector3.Lerp(initialHeight, finalHeight, currentTransition);
            if (currentTransition >= 1) break;
            yield return null;
        }

        _isMoving = false;
    }

    private void OnEnable() {
        transform.position = new Vector3(transform.position.x, _laneHeights[1], 0);
        currentLane = 1;
    }
}
