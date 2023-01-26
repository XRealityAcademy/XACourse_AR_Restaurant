using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class PopUpAnimationBase : MonoBehaviour, IPointerDownHandler
{
    public event Action onGrab; 
    public event Action onGrabCancelled; 

    private bool _isPointerDown;
    
    private void Update()
    {
        #if UNITY_ANDROID && !UNITY_EDITOR
        if(_isPointerDown && Input.touchCount == 0)
            HandlePointerUp();
        #else
        if(_isPointerDown && Input.GetKeyUp(KeyCode.Mouse0))
            HandlePointerUp();
        #endif
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _isPointerDown = true;
        
        onGrab?.Invoke();
    }
    
    void HandlePointerUp()
    {
        _isPointerDown = false;
        
        onGrabCancelled?.Invoke();
    }
}
