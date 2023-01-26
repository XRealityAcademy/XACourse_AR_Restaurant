using System;
using UnityEngine;
using UnityEngine.UI;

public class IntroCanvas : MonoBehaviour
{
    [SerializeField] private Button confirmButton;
    [SerializeField] private GameObject mainCanvas;

    private void Awake()
    {
        mainCanvas.SetActive(false);
    }

    private void OnEnable()
    {
        confirmButton.onClick.AddListener(OnConfirm);
    }
    
    private void OnDisable()
    {
        confirmButton.onClick.RemoveListener(OnConfirm);
    }
    
    void OnConfirm()
    {
        Destroy(gameObject);
        mainCanvas.gameObject.SetActive(true);
    }
}
