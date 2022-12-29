using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemData : MonoBehaviour
{

    public GameObject itemModel;
    public Sprite itemPreview;
    public string itemName;

    private void Awake()
    {
        var previewImage = transform.GetChild(0).GetComponent<Image>();
        if (previewImage != null)
            previewImage.sprite = itemPreview;
    }
}
