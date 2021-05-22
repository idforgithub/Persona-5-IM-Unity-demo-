using UnityEngine;
using System.Linq;

public class SpawnerChat : MonoBehaviour {
    public GameObject chatItem;
    public GameObject panelChat;

    private User userPasser;

    public void LoadChat(User userPasser){
        chatItem.SetActive(true);
        deletePreviousChat();
        
        IOrderedEnumerable<Chat> listChat = userPasser.listChat.OrderBy(e => e.dateMessage.TimeOfDay);
        panelChat.GetComponent<RectTransform>().sizeDelta = new Vector2(720, 180*listChat.Count());

        foreach (Chat e in listChat){
            GameObject newChat = Instantiate(chatItem, chatItem.transform.parent);
            newChat.GetComponent<ChatLayout>().BuildLayout(e, userPasser);

            // build transform Canvas 
            //newItem.GetComponent<RectTransform>().sizeDelta = new Vector2(randomX(), 160f);
        }
        chatItem.SetActive(false);
    }

    private void deletePreviousChat(){
        int totalChild = panelChat.transform.childCount;
        if(totalChild == 1) return;
        for (int i = 1; i < totalChild; i++){
            Destroy(panelChat.transform.GetChild(i).gameObject);
        }
    }
}
