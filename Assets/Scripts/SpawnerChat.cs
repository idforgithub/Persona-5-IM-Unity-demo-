using UnityEngine;
using System.Linq;
using System.Collections;

public class SpawnerChat : MonoBehaviour {
    public GameObject chatItem;
    public GameObject panelChat;

    private User userPasser;

    public void LoadChat(User uPasser){
        this.userPasser = uPasser;
        setupInterface();
        
        IOrderedEnumerable<Chat> listChat = this.userPasser.listChat.OrderBy(e => e.dateMessage.TimeOfDay);
        panelChat.GetComponent<RectTransform>().pivot = new Vector2(0.5f, listChat.Count() > 5 ? 0 : 0.8f);
        StartCoroutine("Spawner", listChat);
    }

    private IEnumerator Spawner(IOrderedEnumerable<Chat> listChat){
        yield return new WaitForSeconds(1f);

        float sizeDeltaX = chatItem.GetComponent<RectTransform>().sizeDelta.x;
        float sizeDeltaY = chatItem.GetComponent<RectTransform>().sizeDelta.y;
        panelChat.GetComponent<RectTransform>().sizeDelta = new Vector2(sizeDeltaX, sizeDeltaY * listChat.Count());
        
        bool activeLinerChat = listChat.Count() > 1;

        int line = 0;
        int currentChild = 1;
        foreach (Chat e in listChat){
            GameObject newChat = Instantiate(chatItem, chatItem.transform.parent);
            newChat.SetActive(true);
            newChat.transform.GetChild(2).GetComponent<ChatLayout>().BuildLayout(e, this.userPasser);

            if(activeLinerChat){
                if(currentChild == 1){
                    newChat.transform.GetChild(0).gameObject.SetActive(true);
                } else if(currentChild == listChat.Count()){
                    // do nothing
                } else {
                    if((currentChild % line) == 0){
                        newChat.transform.GetChild(0).gameObject.SetActive(true);
                    } else {
                        newChat.transform.GetChild(1).gameObject.SetActive(true);
                        line = 0;
                    }
                }
            }
            
            // build transform Canvas 
            newChat.transform.GetChild(2).GetComponent<RectTransform>().sizeDelta = new Vector2(randomX(), sizeDeltaY);
            newChat.name = newChat.name.Replace("(Clone)", $" {currentChild}");

            currentChild++; line++;
        }
    }

#region HELPER INTERFACE

    private void setupInterface(){
        chatItem.transform.GetChild(0).gameObject.SetActive(false);
        chatItem.transform.GetChild(1).gameObject.SetActive(false);
        deletePreviousChat();
        chatItem.SetActive(false);
    }
    private void deletePreviousChat(){
        int totalChild = panelChat.transform.childCount;
        if(totalChild == 1) return;
        for (int i = 1; i < totalChild; i++){
            Destroy(panelChat.transform.GetChild(i).gameObject);
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
