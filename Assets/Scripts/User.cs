using UnityEngine;
using System;

[System.Serializable]
public class User {

    public int idUser;

    public string nameUser;

    public string imgUser;

    public DateTime dateTime;

    public Color colorUser;

    public User(int id, string name, DateTime date, string img, Color color){
        this.idUser = id;
        this.nameUser = name;
        this.dateTime = date;
        this.imgUser = img;
        this.colorUser = color;
    }

}
