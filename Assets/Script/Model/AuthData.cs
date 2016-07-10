using UnityEngine;
using System.Collections;

[System.Serializable]
public class AuthData  {

    public string email;
    public string password;

    public AuthData(string email, string password)
    {
        this.email = email;
        this.password = password;
    }

    public string toJson()
    {
        return JsonUtility.ToJson(this);
    }
}
