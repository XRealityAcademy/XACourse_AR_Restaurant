using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollMenu : MonoBehaviour
{
    [SerializeField] private Transform content;
    [SerializeField] private Transform middle;
    [SerializeField] private Transform centerPos;
    [SerializeField] private PlaceObjectOnPlace objectPlacing;
    private Transform closest;


    //private float middlex;

    private void Awake()
    {
       // middlex = Screen.width * 0.5f;
        //print(middlex);
    }

    // Update is called once per frame
    void Update()
    {
        float minDistance = float.MaxValue;
        Transform closestObject = null;


        for (int i = 0; i< content.childCount; i++)
        {
            
            //check each item from the first one, a list of distance to the screen middle 
            float dist = Mathf.Abs(content.GetChild(i).transform.position.x - middle.position.x);



            //if the item distance is less than screen middle
            if (dist < minDistance)
            {
                //screen middle is equal to the item ditance
                minDistance = dist;

                //closestObject.localScale = Vector3.one;
                // assign the closest object to the qualified one
                closestObject = content.GetChild(i);
            }


        }

        if(closest != closestObject)
        {
            closest = closestObject;

            OnNewClosestObject(closestObject.GetComponent<ItemData>());

        }

        closestObject.localScale = new Vector3(1.5f, 1.5f, 1.5f);


        for (int i = 0; i < content.childCount; i++)
        {
            if (content.GetChild(i) != closestObject)
            {
                content.GetChild(i).localScale = Vector3.one;
            }
        }
       
    }

    public void OnNewClosestObject(ItemData item)
    {
        objectPlacing.UpdateModel(item.itemModel);
    }
}
