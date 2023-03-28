using System;
using System.IO;
using UnityEngine;

public class UserDatas
{
    // Enter any new field required to manage the player datas
    public bool emailVerified;
    public string objectId;
    public string username;
    public string key;
    public int currency;
    public string email;
    public DateTime createdAt;
    public DateTime updatedAt;

    public override string ToString()
    {
        return $"emailVerified : {emailVerified}\n" +
                $"objectID : {objectId}\n" +
                $"userName : {username}\n" +
                $"email : {email}\n" +
                $"createdAt : {createdAt}\n" +
                $"updatedAt : {updatedAt}";
    }
}