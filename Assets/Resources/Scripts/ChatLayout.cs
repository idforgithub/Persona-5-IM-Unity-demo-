using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class ChatLayout : MonoBehaviour
{
    private GameObject itemChat;
    private Text txtMessage;
    private Image imgUser;
    private Image colorBackImgUser;
    private Image eventMessage;

    public void BuildLayout(Chat chat, User user){
        this.itemChat = this.gameObject; // I HATE NAME GAMEOBJECT
        setAttribute();

        // message
        txtMessage.text = chat.message;

        // message event
        if(chat.message.Last() == '?'){
            setupEventChat("question_mark");
        }else if(chat.message.Last() == '!'){
            setupEventChat("exclamation_mark");
        }

        // Image User
        //imgUser.sprite = Resources.Load<Sprite>(user.imgUser);
        imgUser.overrideSprite = AssetDatabase.LoadAssetAtPath(user.imgUser, typeof(Sprite)) as Sprite;
        imgUser.SetNativeSize();

        // color user
        colorBackImgUser.color = user.colorUser;
    }

#region HELPER INTERFACE
    private void setAttribute(){
        txtMessage = itemChat.transform.GetChild(3).GetComponent<Text>();
        eventMessage = itemChat.transform.GetChild(2).GetComponent<Image>();
        imgUser = itemChat.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<Image>();
        colorBackImgUser = itemChat.transform.GetChild(0).GetChild(0).GetComponent<Image>();
    }

    private void setupEventChat(string eventName){
        eventMessage.color = Color.white;
        //eventMessage.sprite = Resources.Load<Sprite>($"Assets/Persona 5 IM/{eventName}.png");
        eventMessage.sprite = AssetDatabase.LoadAssetAtPath($"Assets/Resources/Persona 5 IM/{eventName}.png", typeof(Sprite)) as Sprite;
    }
#endregion
}
