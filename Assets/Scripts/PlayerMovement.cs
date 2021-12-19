using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public static PlayerMovement Instance { get; private set; } = null;

    private bool _isTrackingTouch = false;
    private Vector3 _touchInitialPos;
    private Vector3 _touchCurrentPos;
    // % of height that defines a drag touch.
    [SerializeField] private float _dragScreenPercentage;
    private float _dragDistance;

    private bool _isMoving;

    private void Awake() {
        if (Instance == null) Instance = this;
        else if (Instance != this) Destroy(gameObject);
    }

    private void Start() {
        _dragDistance = Screen.height * _dragScreenPercentage;
    }

    private void Update() {
        DetectMovement();
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
                        Debug.Log("Swiped Up"); //
                        if (!_isMoving) StartCoroutine(MoveTowardsHeight(2));
                        _isTrackingTouch = false;
                        return;
                    }
                    else if (dragAngle < -45 && dragAngle > -135) {
                        Debug.Log("Swiped Down"); //
                        if (!_isMoving) StartCoroutine(MoveTowardsHeight(-2));
                        _isTrackingTouch = false;
                    }
                }
            }
        }
    }

    private IEnumerator MoveTowardsHeight(float direction) {
        _isMoving = true;

        // Direction should be called with the distance between each lane's pivot
        for (int i = 0; i < 30; i++) {
            transform.position += new Vector3(0, direction / 30, 0);

            yield return new WaitForSeconds(0.01f);
        }

        _isMoving = false;
    }

}
