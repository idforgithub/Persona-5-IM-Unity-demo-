using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using System.IO;
using System;

public class Spawnner : MonoBehaviour {

    public GameObject item;
    public RectTransform panel;
    public int totalSpawnner = 10;

    void Awake() {
        LoadData();
        DestroyImmediate(item);

        print(Application.persistentDataPath);
        Sprite testSprite = AssetDatabase.LoadAssetAtPath("Assets/Persona 5 IM/icons/futaba.png", typeof(Sprite)) as Sprite;
        print(testSprite);
    }

    private void LoadData(){
        string projectPath = Application.persistentDataPath + "Assets/";
        if(Application.platform == RuntimePlatform.Android){ // <<-- FOR ANDROID
            projectPath = Application.persistentDataPath + "/Assets/";
        }

        //string iconDummy = projectPath + "Persona 5 IM/icons/";
        string iconDummy = "Assets/Persona 5 IM/icons/";
        User[] listUser = LoadDummy(iconDummy);
        panel.sizeDelta = new Vector2(720, 160*listUser.Length);
        foreach (User user in listUser){
            GameObject newItem = Instantiate(item, item.transform.parent);
            newItem = renameItem(newItem, user);
            newItem = setupInterfaceChat(newItem, user);
            newItem.GetComponent<RectTransform>().sizeDelta = new Vector2(randomX(), 160f);
        }
    }

    private GameObject setupInterfaceChat(GameObject chatItem, User user){
        // text -- dummy -- 
        chatItem.transform.GetChild(0).GetChild(1).GetComponent<Text>().text = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.";
        
        // image user
        chatItem.transform.GetChild(0).GetChild(3).GetChild(0).GetComponent<Image>().sprite = AssetDatabase.LoadAssetAtPath(user.imgUser, typeof(Sprite)) as Sprite;
        chatItem.transform.GetChild(0).GetChild(3).GetChild(0).GetComponent<Image>().SetNativeSize();

        // color user
        chatItem.transform.GetChild(0).GetChild(3).GetComponent<Image>().color = user.colorUser;

        // user date
        // month
        chatItem.transform.GetChild(0).GetChild(4).GetChild(0).GetComponent<Text>().text = user.dateTime.Month.ToString().PadLeft(2, '0');
        // date
        chatItem.transform.GetChild(0).GetChild(4).GetChild(1).GetComponent<Text>().text = user.dateTime.Day.ToString().PadLeft(2, '0');
        // day
        string day = user.dateTime.Date.ToString("ddd");
        chatItem.transform.GetChild(0).GetChild(4).GetChild(2).GetComponent<Text>().text = day.Substring(0, day.Length-1);

        return chatItem;
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

    private User[] LoadDummy(string iconPath){
        List<User> users = new List<User>();
        User akechi = new User(1, "Akechi", new DateTime(2021, 12, 31, 5, 10, 20) , iconPath + "akechi.png", Color.cyan);
        User futaba = new User(1, "Futaba", new DateTime(2021, 10, 31, 5, 10, 20), iconPath + "futaba.png", Color.green);
        User group = new User(1, "Group",   new DateTime(2020, 10, 31, 5, 10, 20), iconPath + "group.png", Color.grey);
        User hifumi = new User(1, "Hifumi", new DateTime(2020, 10, 31, 5, 10, 20), iconPath + "hifumi.png", Color.magenta);
        User kawakami = new User(1, "Kawakami",new DateTime(2019, 7, 31, 5, 10, 20), iconPath + "kawakami.png", Color.red);
        User makoto = new User(1, "Makoto", new DateTime(2019, 12, 7, 5, 10, 20), iconPath + "makoto.png", Color.magenta);
        User ryuji = new User(1, "Ryuji", new DateTime(2019, 7, 31, 5, 10, 20), iconPath + "ryuji.png",  Color.yellow);
        User sojiro = new User(1, "Sojiro", new DateTime(2019, 7, 31, 5, 10, 20), iconPath + "sojiro.png", Color.green);
        User takemi = new User(1, "Takemi", new DateTime(2019, 7, 31, 5, 10, 20), iconPath + "takemi.png", Color.red);
        User yusuke = new User(1, "Yusuke", new DateTime(2019, 7, 31, 5, 10, 20), iconPath + "yusuke.png", Color.blue);

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
}
