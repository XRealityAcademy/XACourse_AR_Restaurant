using System;
using UnityEngine;

public class ItemPopUpAnimator : MonoBehaviour
{
    [SerializeField] private PopUpAnimationBase animBase;
    [SerializeField] private RectTransform startPos;
    [SerializeField] private RectTransform endPos;
    [SerializeField] private float minMovement;
    [SerializeField] private float minAnimVel;
    [SerializeField] private float maxAnimVel = 8f;
    [SerializeField] private float velocitySmoothing = 10f;
    [SerializeField] private float referenceHeight = 1080f;


    private float _pixelMultipl;
    private Vector3 _offset;
    private float _currentYVel;
    private float _totalDistance;
    private float _lastMousePosY;

    private bool _isGrabbed;

    private void Awake()
    {
        _totalDistance = Vector2.Distance(startPos.localPosition, endPos.localPosition);
        _pixelMultipl = Screen.currentResolution.height / referenceHeight;
    }

    private void OnEnable()
    {
        animBase.onGrab += Grab;
        animBase.onGrabCancelled += CancelGrab;
    }

    private void OnDisable()
    {
        animBase.onGrab -= Grab;
        animBase.onGrabCancelled -= CancelGrab;
    }

    private void Update()
    {
        if (_isGrabbed) HandleGrab();
        _lastMousePosY = Input.mousePosition.y;
    }

    void HandleGrab()
    {
        float vel;
        
        #if UNITY_ANDROID && !UNITY_EDITOR
        var touch = Input.GetTouch(0);
        vel = Mathf.Abs(touch.deltaPosition.y);
        
        transform.position = new Vector3(transform.position.x, touch.position.y - _offset.y, 0f);
        #else
        vel = Mathf.Abs(_lastMousePosY - Input.mousePosition.y);

        transform.position = new Vector3(transform.position.x, Input.mousePosition.y - _offset.y, 0f);
#endif

        _currentYVel = Mathf.Lerp(vel, _currentYVel, Time.deltaTime * velocitySmoothing);
    }
    
    void Grab()
    {
        LeanTween.cancel(gameObject);
        _offset = Input.mousePosition - transform.position;
        _isGrabbed = true;
    }

    void CancelGrab()
    {
        _isGrabbed = false;

        var movedDist = Vector2.Distance(transform.localPosition, startPos.localPosition);
        
        if (movedDist > minMovement)
        {
            var time = (_totalDistance - movedDist) / GetVel();
            time *= Time.deltaTime;
            LeanTween.moveLocalY(gameObject, endPos.localPosition.y, time).setEaseOutQuint();
        }
        else
        {
            var time = movedDist / GetVel();
            time *= Time.deltaTime;
            LeanTween.moveLocalY(gameObject, startPos.localPosition.y, time).setEaseOutQuint();
        }

        _currentYVel = 0f;
    }

    public void Open()
    {
        var distToMove = Vector2.Distance(transform.localPosition, endPos.localPosition);
        
        var time = distToMove / maxAnimVel;
        time *= Time.deltaTime;
        LeanTween.moveLocalY(gameObject, endPos.localPosition.y, time).setEaseOutCirc();
    }

    public void Close()
    {
        var distToMove = Vector2.Distance(transform.localPosition, startPos.localPosition);
        
        var time = distToMove / minAnimVel;
        time *= Time.deltaTime;
        LeanTween.moveLocalY(gameObject, startPos.localPosition.y, time).setEaseOutCirc();
    }

    float GetVel()
    {
        return Mathf.Min(Mathf.Max(_currentYVel, minAnimVel), maxAnimVel) * _pixelMultipl;
    }
}
