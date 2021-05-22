using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class OpenChat : MonoBehaviour
{
    public GameObject boxSelect;
    public AppsManager apps;

    public void CallAnimation(){
        GameObject box = Instantiate(boxSelect, transform.position, transform.rotation, GameObject.FindGameObjectWithTag("Canvas 3").transform);
        Animation anim = box.GetComponent<Animation>();
        anim.Play();

        apps.PreventMultiClick(true);
        apps.DestroyGameObject(box, 1f);
        apps.DeactivateCanvasListChat(.35f);
        apps.ActivateCanvasChat(0.9f);
        apps.PassingBoxAnimation(box);
    }
}
