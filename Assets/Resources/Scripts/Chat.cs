using System;

public class Chat {

    public string message;
    public DateTime dateMessage;

    public Chat(){
    }

    public Chat(string msg, DateTime dtMsg){
        this.message = msg;
        this.dateMessage = dtMsg;
    }

}
