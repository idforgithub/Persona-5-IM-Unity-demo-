using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawnner : MonoBehaviour
{

    public GameObject item;
    public RectTransform panel;
    public int totalSpawnner = 10;

    void Awake()
    {
        panel.sizeDelta = new Vector2(720, 160*totalSpawnner);
        for(int i=0; i<totalSpawnner; i++){
            int rand = Random.Range(0, 15);
            float x = 720f;
            if(rand < 5 && rand > 0){
                x = 680f;
            }else if(rand < 10 && rand > 6){
                x = 770f;
            }
            item.GetComponent<RectTransform>().sizeDelta = new Vector2(x, 160f);
            Instantiate(item, item.transform.parent);
        }

        Destroy(item);
    }
}
