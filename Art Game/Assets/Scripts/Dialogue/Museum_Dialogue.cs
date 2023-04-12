using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Museum_Dialogue : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    

    public List<Tuple<string, string>> pre_paintings(int whoDunIt)
    {
        List<Tuple<string, string>> response = new List<Tuple<string, string>>();
        response.Add(new Tuple<string, string>("main", "Hey Doc, what's going on? Thi s is my third call of the night and there seems to be a pattern."));
        response.Add(new Tuple<string, string>("burch", "Three you say? That's just awful. yes, please come, they're right over here. Please do the best you can! These things are priceslless you know."));
        response.Add(new Tuple<string, string>("main", "Of course, I'll do my best. I'll let you know if I find anything over here."));
        return response;
    }

    public List<Tuple<string, string>> first_painting_done(int whoDunIt)
    {
        List<Tuple<string, string>> response = new List<Tuple<string, string>>();
        response.Add(new Tuple<string, string>("main", "So how did this happen? I thought you were hosting a silent auction tonight."));
        response.Add(new Tuple<string, string>("crane", "I was, but the night had ended and I had cleaned up and was going to go through the sales after I got some coffee from down the street."));
        response.Add(new Tuple<string, string>("main", "<I> Doesn't surprise me, she's the biggest clean freak I've met. She came by my office once and threw away a soda I was only half done with."));
        return response;
    }

    public List<Tuple<string, string>> second_painting_done(int whoDunIt)
    {
        List<Tuple<string, string>> response = new List<Tuple<string, string>>();
        response.Add(new Tuple<string, string>("crane", "<I> ...mumbling quiety..."));
        response.Add(new Tuple<string, string>("main", "What was that?"));
        response.Add(new Tuple<string, string>("crane", "Oh nothing, just saying that she got what she deserved."));
        response.Add(new Tuple<string, string>("main", "Kristen? That's a bit harsh"));
        response.Add(new Tuple<string, string>("crane", "No No! I have nothing against her. The museum curator also got a visit from our new 'Modern artist'. Some of her paintings were destroyed as well."));
        response.Add(new Tuple<string, string>("main", "<I> What is going on!?"));
        response.Add(new Tuple<string, string>("main", "Oh no! I'll go see if she can tell me anything about what happened."));
        return response;
    }
}
