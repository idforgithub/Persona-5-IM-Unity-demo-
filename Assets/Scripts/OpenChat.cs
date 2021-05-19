using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class OpenChat : MonoBehaviour
{
    public GameObject boxSelect;
    public GameObject panelListChat;
    public GameObject apps;
    public void CallAnimation(){
        GameObject box = Instantiate(boxSelect, transform.position, transform.rotation, GameObject.FindGameObjectWithTag("Canvas 3").transform);
        Animation anim = box.GetComponent<Animation>();
        anim.Play();
        
        StartCoroutine("ReenableButton");
        apps.GetComponent<AppsManager>().DestroyGameObject(box, 1f);
        apps.GetComponent<AppsManager>().DeactivateCanvasListChat(.35f);
        apps.GetComponent<AppsManager>().ActivateCanvasChat(0.9f);
        apps.GetComponent<AppsManager>().PassingBoxAnimation(box);
    }

    IEnumerator ReenableButton(){
        this.GetComponent<Button>().enabled = false;
        yield return new WaitForSeconds(.3f);
        this.GetComponent<Button>().enabled = true;
    }
}
