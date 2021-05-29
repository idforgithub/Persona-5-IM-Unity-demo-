using UnityEngine;
using System.Linq;
using System.Collections;

public class SpawnerChat : MonoBehaviour {
    public GameObject chatItem;
    public GameObject topContact;
    public GameObject panelChat;
    private User userPasser;
    private float sizeDeltaX;
    private float sizeDeltaY;
    
    public void LoadChat(User uPasser){
        this.userPasser = uPasser;
        setupInterface();
        
        this.sizeDeltaX = chatItem.GetComponent<RectTransform>().sizeDelta.x;
        this.sizeDeltaY = chatItem.GetComponent<RectTransform>().sizeDelta.y;

        IOrderedEnumerable<Chat> listChat = this.userPasser.listChat.OrderBy(e => e.dateMessage.TimeOfDay);

        panelChat.GetComponent<RectTransform>().pivot = new Vector2(0.5f, listChat.Count() > 5 ? 0 : 0.8f);
        panelChat.GetComponent<RectTransform>().sizeDelta = new Vector2(sizeDeltaX, sizeDeltaY * listChat.Count());
        StartCoroutine("Spawner", listChat.ToArray());
    }

    private IEnumerator Spawner(Chat[] listChat){
        yield return new WaitForSeconds(1f);
        RecursiveSpawnerTopContact();
        RecursiveSpawner(listChat, 0, 1, 0, listChat.Count() > 1);
    }

    // -----------------------------------------------------------
    // TO DO if group must change to loop
    private void RecursiveSpawnerTopContact(){
        GameObject tContact = Instantiate(topContact, topContact.transform.parent);
        BuildTopContactLayout(tContact);
        tContact.SetActive(true);

        tContact = Instantiate(topContact, topContact.transform.parent);
        BuildTopContactLayout(tContact);
        tContact.SetActive(true);

        tContact = Instantiate(topContact, topContact.transform.parent);
        BuildTopContactLayout(tContact);
        tContact.SetActive(true);

        tContact = Instantiate(topContact, topContact.transform.parent);
        BuildTopContactLayout(tContact);
        tContact.SetActive(true);
    }

    private void RecursiveSpawner(Chat[] listChat, int i, int currentChild, int line, bool activeLinerChat){
        if(i < listChat.Length){
            GameObject newChat = Instantiate(chatItem, chatItem.transform.parent);
            newChat.SetActive(true);
            newChat.transform.GetChild(2).GetComponent<ChatLayout>().BuildLayout(listChat[i], this.userPasser);
            if(activeLinerChat){
                if(currentChild == 1){
                    newChat.transform.GetChild(0).gameObject.SetActive(true);
                } else if(currentChild != listChat.Count()){
                    if((currentChild % line) == 0){
                        newChat.transform.GetChild(0).gameObject.SetActive(true);
                    } else {
                        newChat.transform.GetChild(1).gameObject.SetActive(true);
                        line = 0;
                    }
                }
            }
            
            newChat.transform.GetChild(2).GetComponent<RectTransform>().sizeDelta = new Vector2(randomX(), sizeDeltaY);
            newChat.name = newChat.name.Replace("(Clone)", $" {currentChild}");

            RecursiveSpawner(listChat, i+1, currentChild+1, line+1, activeLinerChat);
        }
        return;
    }

    

#region HELPER INTERFACE
    private void BuildTopContactLayout(GameObject tContact){
        int indexAnim = UnityEngine.Random.Range(1, tContact.transform.GetChild(0).GetComponent<Animation>().GetClipCount()+1);

        tContact.transform.GetChild(0).GetComponent<Animation>().Play("topContact"+indexAnim);
        tContact.transform.GetChild(0).GetChild(1).GetComponent<UnityEngine.UI.Image>().color = UnityEngine.Random.Range(1, 3) == 1 ? Color.black : Color.white;
        tContact.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = UnityEditor.AssetDatabase.LoadAssetAtPath(this.userPasser.imgUser, typeof(Sprite)) as Sprite;
        tContact.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<UnityEngine.UI.Image>().SetNativeSize();
    }

    private void setupInterface(){
        chatItem.transform.GetChild(0).gameObject.SetActive(false);
        chatItem.transform.GetChild(1).gameObject.SetActive(false);
        deletePreviousChat();
        chatItem.SetActive(false);
        topContact.SetActive(false);
    }
    private void deletePreviousChat(){
        // destroy list chat
        int totalChild = panelChat.transform.childCount;
        if(totalChild != 1) {
            for (int i = 1; i < totalChild; i++){
                Destroy(panelChat.transform.GetChild(i).gameObject);
            }
        }

        // destroy top contact item
        totalChild = topContact.transform.parent.childCount;
        if(totalChild != 0) {
            for (int i = 1; i < totalChild; i++){
                Destroy(topContact.transform.parent.GetChild(i).gameObject);
            }
        }
    }

    private float randomX(){
        int rand = UnityEngine.Random.Range(0, 15);
        float x = 720f;
        if(rand < 5 && rand > 0){
            x = 690f;
        }else if(rand < 10 && rand > 6){
            x = 750f;
        }
        return x;
    }
}
#endregion 
