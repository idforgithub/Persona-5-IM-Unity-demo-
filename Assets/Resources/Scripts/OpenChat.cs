using UnityEngine;

public class OpenChat : MonoBehaviour
{
    public GameObject boxSelect;
    public AppsManager apps;

    public void ToChat(){
        GameObject box = Instantiate(boxSelect, transform.position, transform.rotation, GameObject.FindGameObjectWithTag("Canvas 3").transform);
        box.GetComponent<Animation>().Play();

        apps.ToChat(
            this.gameObject.transform.parent.GetChild(0).gameObject.GetComponent<UserLayout>().user, 
            box
        );
    }
}
