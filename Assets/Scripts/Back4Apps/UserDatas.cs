using System;

public class UserDatas
{
    // Enter any new field required to manage the player datas
    public bool emailVerified;
    public string objectId;
    public string username;
    public string key;
    public string email;
    public DateTime createdAt;
    public DateTime updatedAt;
    public bool hasCompletedTutorial = false;

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