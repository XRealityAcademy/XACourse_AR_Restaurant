using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class ItemPopUpPopulator : MonoBehaviour
{
    [SerializeField] private ScrollMenu itemMenu;
    [SerializeField] private PopUpAnimationBase openBase;

    [SerializeField] private Image itemPreview;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI ingredientText;
    [SerializeField] private TextMeshProUGUI priceText;
    [SerializeField] private string pricePrefix;

    private void OnEnable() => openBase.onGrab += Populate;
    private void OnDisable() => openBase.onGrab -= Populate;

    public void Populate()
    {
        var item = itemMenu.SelectedItem;

        itemPreview.sprite = item.itemPreview;

        if (item.itemPreview != null)
        {
            var text = itemPreview.sprite.texture;
            var rectTrans = itemPreview.GetComponent<RectTransform>();
            float rate = (float)text.width / text.height;
            
            rectTrans.sizeDelta = new Vector2(rectTrans.rect.height * rate, rectTrans.rect.height);
        }
        
        nameText.text = item.itemName;
        ingredientText.text = item.IngredientString();
        priceText.text = pricePrefix + item.PriceString();
    }
}
