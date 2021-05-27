using UnityEngine;

public class OpenChat : MonoBehaviour
{
    public GameObject boxSelect;
    public AppsManager apps;

    public void ToChat(){
        GameObject box = Instantiate(boxSelect, transform.position, transform.rotation, GameObject.FindGameObjectWithTag("Canvas 3").transform);
        box.GetComponent<Animation>().Play();
        User user = this.gameObject.transform.parent.GetChild(0).gameObject.GetComponent<UserLayout>().user;

        apps.PreventMultiClick(true);
        apps.DestroyGameObject(box, 1f);
        apps.DeactivateCanvasListChat(.35f);
        apps.ActivateCanvasChat(0.9f);
        apps.PassingBoxAnimation(box);
        apps.ShowCanvasChat(user);
    }
}
