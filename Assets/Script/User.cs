using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class User
{
    // Start is called before the first frame update
    public string userName;
    public float userScore;

    public User()
    {
    }

    public User(string userName, float userScore)
    {
        this.userName = userName;
        this.userScore = userScore;
    }
}
