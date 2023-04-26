using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Start_Dialogue : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    
    public List<Tuple<string, string, string>> get_dialogue(int completed_games, int whoDunIt)
    {
        return intro(whoDunIt);
    }
    public List<Tuple<string,string, string>> intro(int whoDunIt)
    {
        List<Tuple<string, string, string>> response = new List<Tuple<string,string, string>>();
        string s = "";
        for (int i = 0; i < 250; i++)
        {
            s += i + " ";
        }
        response.Add(new Tuple<string,string,string>("Phone","normal","<I>phone buzzing"));
        response.Add(new Tuple<string,string,string>("Player","normal","Ugh right as I'm falling asleep, of course..."));
        response.Add(new Tuple<string,string,string>("Player","normal","<I>Huh, it's Kristen... I wonder what she wants."));
        response.Add(new Tuple<string,string,string>("Player","normal", "Hello?"));
        response.Add(new Tuple<string,string,string>("Kristen","normal","Come over! Quick! It's an emergency! *click*"));
        response.Add(new Tuple<string,string,string>("Player","normal","What's the problem?"));
        response.Add(new Tuple<string,string,string>("Player","normal","Oh.. She hung up on me."));
        response.Add(new Tuple<string,string,string>("Player","normal","Well I guess I'll be a good friend this time.. Ugh."));
        response.Add(new Tuple<string,string,string>("Narrator","normal","<B>You drive over to Kristen's house to see her waiting at the door."));
        // From here we can jump to the next Location in the scene (aka actually show a door and her avatar) as the prepainting dialogue
        
        return response;
    }

}
