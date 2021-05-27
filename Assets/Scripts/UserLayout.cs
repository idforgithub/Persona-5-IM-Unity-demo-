using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class UserLayout : MonoBehaviour {
    public User user;

    private GameObject item;
    private Text lastMessageUser;
    private Image eventMessage;
    private Image colorBacImgkUser;
    private Image imgUser;
    
#region DATE
    private Text monthDate;
    private Text date;
    private Text dateTxt;
#endregion

    public void BuildLayout(User passUser){
        this.user = passUser;
        this.item = this.gameObject; // I HATE NAME GAMEOBJECT
        setAttribute();

        // message -- dummy
        string lastIndexMsg = user.listChat.LastOrDefault().message;
        lastMessageUser.text = lastIndexMsg.Length > 18 ? lastIndexMsg.Substring(0, 18) + "..." : lastIndexMsg;

        // message event
        if(lastIndexMsg.Last() == '?'){
            setupEventChat("question_mark");
        }else if(lastIndexMsg.Last() == '!'){
            setupEventChat("exclamation_mark");
        }

        // image user
        imgUser.overrideSprite = AssetDatabase.LoadAssetAtPath(user.imgUser, typeof(Sprite)) as Sprite;
        imgUser.SetNativeSize();

        // color user
        colorBacImgkUser.color = user.colorUser;

        // user date
        // month
        monthDate.text = user.listChat.LastOrDefault().dateMessage.Month.ToString().PadLeft(2, '0');
        // date
        date.text = user.listChat.LastOrDefault().dateMessage.Day.ToString().PadLeft(2, '0');
        // day
        string day = user.listChat.LastOrDefault().dateMessage.Date.ToString("ddd");
        dateTxt.text = day.Substring(0, day.Length-1);
    }

#region HELPER INTERFACE
    private void setAttribute(){
        lastMessageUser = item.transform.GetChild(1).GetComponent<Text>();
        eventMessage = item.transform.GetChild(2).GetComponent<Image>();
        imgUser = item.transform.GetChild(4).GetChild(0).GetComponent<Image>();
        colorBacImgkUser = item.transform.GetChild(4).GetComponent<Image>();

        monthDate = item.transform.GetChild(5).GetChild(0).GetComponent<Text>();
        date = item.transform.GetChild(5).GetChild(1).GetComponent<Text>();
        dateTxt = item.transform.GetChild(5).GetChild(2).GetComponent<Text>();
    }

    private void setupEventChat(string eventName){
        eventMessage.color = Color.white;
        eventMessage.sprite = AssetDatabase.LoadAssetAtPath($"Assets/Persona 5 IM/{eventName}.png", typeof(Sprite)) as Sprite;
    }
#endregion
}
