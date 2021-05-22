using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using System.IO;
using System;
using System.Linq;

public class Spawnner : MonoBehaviour {

    public static class FilePathApp {
        public static String Windows { get { return Directory.GetCurrentDirectory() + Path.DirectorySeparatorChar + ASSETS; } } 
        public static String Android { get { return Application.persistentDataPath;} }
    }

    private const string ASSETS = "Assets";

    public GameObject item;
    public RectTransform panel;

    void Awake() {
        LoadData();
        Destroy(item);
    }

    private void LoadData(){
        string projectPath = Directory.GetCurrentDirectory() + ASSETS;
        string persona5Icons = "Persona 5 IM/icons/";

        // DEBUG LINE ------------------------------------------------------------------------------------------------------
        /* 
        string windows = Path.Combine(FilePathApp.Windows, persona5Icons);
        string android = Path.Combine(FilePathApp.Android, persona5Icons);

        print("O -- Windows ==> "+windows);
        print("O -- Android ==> "+android);

        if(Application.platform == RuntimePlatform.Android){ // <<-- FOR ANDROID
            // path = "jar:file://" + Application.dataPath + "!/assets/"
        }

        Sprite testSprite = AssetDatabase.LoadAssetAtPath("Assets/Persona 5 IM/icons/futaba.png", typeof(Sprite)) as Sprite;
        print(testSprite);
        */

        string iconDummy = ASSETS + Path.DirectorySeparatorChar + persona5Icons;
        User[] listUser = LoadDummy(iconDummy);

        panel.sizeDelta = new Vector2(720, 160*listUser.Length);

        IEnumerable<User> orderListUser = listUser.OrderByDescending(order => order.listChat.LastOrDefault().dateMessage.TimeOfDay);
        foreach (User user in orderListUser){
            GameObject newItem = Instantiate(item, item.transform.parent);
            newItem = renameItem(newItem, user);
            newItem = setupInterfaceChat(newItem, user);
            newItem.GetComponent<RectTransform>().sizeDelta = new Vector2(randomX(), 160f);
        }
    }

    private GameObject setupInterfaceChat(GameObject chatItem, User user){
        // message -- dummy -- 
        string lastIndexMsg = user.listChat.LastOrDefault().message;
        chatItem.transform.GetChild(0).GetChild(1).GetComponent<Text>().text = lastIndexMsg.Length > 18 ? lastIndexMsg.Substring(0, 18) + "..." : lastIndexMsg;
        
        // message event
        if(lastIndexMsg.Last() == '?'){
            chatItem = setupEventChat(chatItem, "question_mark");
        }else if(lastIndexMsg.Last() == '!'){
            chatItem = setupEventChat(chatItem, "exclamation_mark");
        }

        // image user
        chatItem.transform.GetChild(0).GetChild(4).GetChild(0).GetComponent<Image>().sprite = AssetDatabase.LoadAssetAtPath(user.imgUser, typeof(Sprite)) as Sprite;
        chatItem.transform.GetChild(0).GetChild(4).GetChild(0).GetComponent<Image>().SetNativeSize();

        // color user
        chatItem.transform.GetChild(0).GetChild(4).GetComponent<Image>().color = user.colorUser;

        // user date
        // month
        chatItem.transform.GetChild(0).GetChild(5).GetChild(0).GetComponent<Text>().text = user.listChat.LastOrDefault().dateMessage.Month.ToString().PadLeft(2, '0');
        // date
        chatItem.transform.GetChild(0).GetChild(5).GetChild(1).GetComponent<Text>().text = user.listChat.LastOrDefault().dateMessage.Day.ToString().PadLeft(2, '0');
        // day
        string day = user.listChat.LastOrDefault().dateMessage.Date.ToString("ddd");
        chatItem.transform.GetChild(0).GetChild(5).GetChild(2).GetComponent<Text>().text = day.Substring(0, day.Length-1);

        return chatItem;
    }

#region HELPER INTERFACE
    private GameObject setupEventChat(GameObject chat, string eventName){
        chat.transform.GetChild(0).GetChild(2).GetComponent<Image>().color = Color.white;
        chat.transform.GetChild(0).GetChild(2).GetComponent<Image>().sprite = AssetDatabase.LoadAssetAtPath($"Assets/Persona 5 IM/{eventName}.png", typeof(Sprite)) as Sprite;
        return chat;
    }

    private GameObject renameItem(GameObject newItem, User user){
        newItem.name = newItem.name.Replace("item(Clone)", char.ToUpper(user.nameUser[0]) + user.nameUser.Substring(1)).Trim();
        return newItem;
    }

    private float randomX(){
        int rand = UnityEngine.Random.Range(0, 15);
        float x = 720f;
        if(rand < 5 && rand > 0){
            x = 680f;
        }else if(rand < 10 && rand > 6){
            x = 770f;
        }
        return x;
    }
#endregion

#region SETUP DUMMY DATA
    private User[] LoadDummy(string iconPath){
        List<Chat> chatsAkechi = new List<Chat>();
        chatsAkechi.Add( new Chat("Hello", new DateTime(2019, 7, 31, 5, 10, 20)) );
        chatsAkechi.Add( new Chat("Fuking Joker?!", new DateTime(2019, 7, 31, 5, 10, 20)) );

        List<Chat> chatsFutaba = new List<Chat>();
        chatsFutaba.Add( new Chat("Hello", new DateTime(2021, 7, 31, 6, 10, 20)) );
        chatsFutaba.Add( new Chat("Ren?!", new DateTime(2021, 7, 31, 7, 10, 20)) );
        chatsFutaba.Add( new Chat("Moshi-moshi?!", new DateTime(2021, 7, 31, 7, 10, 20)) );
        chatsFutaba.Add( new Chat("Oiiiii!!!!!!!!", new DateTime(2021, 7, 31, 7, 21, 20)) );

        List<Chat> chatsHifumi = new List<Chat>();
        chatsHifumi.Add( new Chat("Hello", new DateTime(2021, 7, 31, 6, 10, 20)) );
        chatsHifumi.Add( new Chat("Come and play shogi with me ?", new DateTime(2021, 7, 31, 7, 21, 20)) );

        List<Chat> chatsKawakami = new List<Chat>();
        chatsKawakami.Add( new Chat("Ren..", new DateTime(2021, 7, 31, 6, 10, 20)) );
        chatsKawakami.Add( new Chat("Maid Service ?", new DateTime(2021, 7, 31, 7, 21, 20)) );

        List<Chat> chatsTest = new List<Chat>();
        chatsTest.Add( new Chat("Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.", new DateTime(2021, 7, 31, 7, 10, 20)) );
        chatsTest.Add( new Chat("Message Last Index", new DateTime(2020, 9, 29, 3, 10, 20)) );
        
        List<User> users = new List<User>();
        User akechi = new User(1, "Akechi", iconPath + "akechi.png", Color.cyan, chatsAkechi);
        User futaba = new User(2, "Futaba", iconPath + "futaba.png", Color.green, chatsFutaba);
        User group = new User(3, "Group", iconPath + "group.png", Color.grey, chatsTest);
        User hifumi = new User(4, "Hifumi", iconPath + "hifumi.png", Color.magenta, chatsHifumi);
        User kawakami = new User(5, "Kawakami", iconPath + "kawakami.png", Color.red, chatsKawakami);
        User makoto = new User(6, "Makoto", iconPath + "makoto.png", Color.magenta, chatsTest);
        User ryuji = new User(7, "Ryuji", iconPath + "ryuji.png",  Color.yellow, chatsTest);
        User sojiro = new User(8, "Sojiro", iconPath + "sojiro.png", Color.green, chatsTest);
        User takemi = new User(9, "Takemi", iconPath + "takemi.png", Color.red, chatsTest);
        User yusuke = new User(10, "Yusuke", iconPath + "yusuke.png", Color.blue, chatsTest);

        users.Add(akechi);
        users.Add(futaba);
        users.Add(group);
        users.Add(hifumi);
        users.Add(kawakami);
        users.Add(makoto);
        users.Add(ryuji);
        users.Add(sojiro);
        users.Add(takemi);
        users.Add(yusuke);

        return users.ToArray();
    }
#endregion 
}
