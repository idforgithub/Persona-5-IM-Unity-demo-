using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using System.IO;
using System;
using System.Linq;

public class Spawnner : MonoBehaviour {
    public GameObject item;
    public RectTransform panel;

    private float sizeDeltaX;
    private float sizeDeltaY;


    private void testPath(){
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
    }

    public void LoadUser(){
        item.SetActive(false);
        this.sizeDeltaX = item.GetComponent<RectTransform>().sizeDelta.x;
        this.sizeDeltaY = item.GetComponent<RectTransform>().sizeDelta.y;

        string debugPathIcons = BuildPath(
            new string[]{
                "Assets",
                "Resources",
                "Persona 5 IM",
                "icons"
            }
        );
        User[] listUser = LoadDummy(debugPathIcons);

        var listUserOrdered = listUser.OrderByDescending(order => order.listChat.LastOrDefault().dateMessage.TimeOfDay);
        
        StartCoroutine("Spawner", listUserOrdered.ToArray<User>());
    }

    private IEnumerator Spawner(User[] listUserOrdered){
        for (int i = 0; i < listUserOrdered.Length; i++){
            panel.sizeDelta = new Vector2(sizeDeltaX, sizeDeltaY * (i+1));
            GameObject newItem = Instantiate(item, item.transform.parent);
            newItem.SetActive(true);
            newItem = renameItem(newItem, listUserOrdered[i]);

            int index = randomAnimationIndex(newItem.transform.GetChild(0).GetComponent<Animation>().GetClipCount());
            newItem.transform.GetChild(0).GetComponent<Animation>().Play($"itemUser{randomAnimationIndex(index)}");
            newItem.transform.GetChild(0).GetComponent<UserLayout>().BuildLayout(listUserOrdered[i]);

            newItem.transform.GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(randomX(), 160f);
            yield return new WaitForSeconds(.1f);
        }
    }

#region HELPER INTERFACE
    private static string BuildPath(string[] path){
        return RecursiveBuild(path, 0, "");
    }

    private static string RecursiveBuild(string[] path, int i, string build){
        if(path.Length == i) 
            return build;
        build += path[i] + Path.DirectorySeparatorChar;
        return RecursiveBuild(path, i + 1, build);
    }

    private GameObject renameItem(GameObject newItem, User user){
        newItem.name = newItem.name.Replace("item(Clone)", char.ToUpper(user.nameUser[0]) + user.nameUser.Substring(1)).Trim();
        return newItem;
    }

    private int randomAnimationIndex(int totalAnimation){
        int animIndex = UnityEngine.Random.Range(1, totalAnimation+1);
        return animIndex;
    }

    private float randomX(){
        int rand = UnityEngine.Random.Range(0, 15);
        float x = 720f;
        if(rand < 5 && rand > 0){
            x = 690f;
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
        chatsFutaba.Add( new Chat("As one of the crew, we’ve got a special gift for you.", new DateTime(2021, 7, 31, 7, 21, 20)) );
        chatsFutaba.Add( new Chat("Like what you see? If fashion is your thing, check out our Instagram as well!", new DateTime(2021, 7, 31, 7, 21, 20)) );
        chatsFutaba.Add( new Chat("Hey Joker, I saw your post. I’d love to feature it on my feed, would that be okay?", new DateTime(2021, 7, 31, 7, 21, 20)) );
        chatsFutaba.Add( new Chat("Monitor your top followers and send them a message.", new DateTime(2021, 7, 31, 7, 21, 20)) );
        chatsFutaba.Add( new Chat("As a reward for being one of our top followers", new DateTime(2021, 7, 31, 7, 21, 20)) );

        List<Chat> chatsHifumi = new List<Chat>();
        chatsHifumi.Add( new Chat("Hello", new DateTime(2021, 7, 31, 6, 10, 20)) );
        chatsHifumi.Add( new Chat("Ren-san ?", new DateTime(2021, 7, 31, 6, 10, 20)) );
        chatsHifumi.Add( new Chat("Amamiya-kun ?", new DateTime(2021, 7, 31, 6, 10, 20)) );
        chatsHifumi.Add( new Chat("Shogi time ?", new DateTime(2021, 7, 31, 7, 21, 20)) );
        chatsHifumi.Add( new Chat("Are you free ?", new DateTime(2021, 7, 31, 7, 21, 20)) );
        chatsHifumi.Add( new Chat("Come and play shogi with me ?", new DateTime(2021, 7, 31, 7, 21, 20)) );

        List<Chat> chatsKawakami = new List<Chat>();
        chatsKawakami.Add( new Chat("Ren..", new DateTime(2021, 7, 31, 6, 10, 20)) );
        chatsKawakami.Add( new Chat("Maid Service ?", new DateTime(2021, 7, 31, 7, 21, 20)) );

        List<Chat> chatsTest = new List<Chat>();
        chatsTest.Add( new Chat("Hello??", new DateTime(2020, 7, 31, 7, 10, 20)) );
        chatsTest.Add( new Chat("Last Message", new DateTime(2021, 9, 29, 3, 10, 20)) );

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
        users.Add(hifumi);
        users.Add(kawakami);
        users.Add(makoto);
        users.Add(ryuji);
        users.Add(sojiro);
        users.Add(takemi);
        users.Add(yusuke);

        users.Add(group);

        return users.ToArray();
    }
#endregion 
}
