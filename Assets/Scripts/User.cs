using UnityEngine;
using System.Collections.Generic;
using System;
using System.Linq;

[System.Serializable]
public class User {

    public int idUser;

    public string nameUser;

    public string imgUser;

    public Color colorUser;

    public List<Chat> listChat;

    public User(int id, string name, string img, Color color, List<Chat> chats){
        this.idUser = id;
        this.nameUser = name;
        this.imgUser = img;
        this.colorUser = color;
        this.listChat = chats;
    }
}
