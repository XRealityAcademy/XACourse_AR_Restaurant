using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class ItemData : MonoBehaviour
{

    public GameObject itemPrefab;
    public Sprite itemPreview;
    public string itemName;

    public float price;
    public string[] ingredients;

    private void Awake()
    {
        var previewImage = transform.GetChild(0).GetComponent<Image>();
        if (previewImage != null)
        {
            previewImage.sprite = itemPreview;
            previewImage.preserveAspect = true;
        }
    }

    public string PriceString()
    {
        return price.ToString("F", CultureInfo.InvariantCulture);
    }
    
    public string IngredientString()
    {
        var builder = new StringBuilder();

        for (var i = 0; i < ingredients.Length; i++)
        {
            builder.Append(ingredients[i]);
            if (i < ingredients.Length - 1)
                builder.Append(", ");
        }

        return builder.ToString();
    }
}
