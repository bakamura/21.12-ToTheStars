using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FeedbackText : MonoBehaviour{
    public static FeedbackText Instance { get; private set; }

    private Text _feedbackText;
    private Animator _animator;
    private CanvasGroup _canvasGroup;

    private void Awake(){
        if (Instance == null){ 
            Instance = this;
            _feedbackText = GetComponent<Text>();
            _animator = GetComponent<Animator>();
            _canvasGroup = GetComponent<CanvasGroup>();
    }
        else if (Instance != this) Destroy(this.gameObject);
    }

    public void ActivateText(string text){
        _canvasGroup.alpha = 1;
        _feedbackText.text = text;
        _animator.enabled = true;
    }

    public void AnimationEnd(){
        _canvasGroup.alpha = 0;
        _feedbackText.text = "";
        _animator.enabled = false;
    }
}
