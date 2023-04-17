using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Friend_Dialogue : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    
    public List<Tuple<string, string, string>> get_dialogue(int completed_games, int whoDunIt)
    {
        if (completed_games == 0)
        {
            return pre_paintings(whoDunIt);
        }
        else if (completed_games == 1)
        {
            return first_painting_done(whoDunIt);
        }
        else if (completed_games == 2)
        {
            return second_painting_done(whoDunIt);
        }
        else if (completed_games == 3)
        {
            return third_painting_done(whoDunIt);

        }
        else if (completed_games == 4)
        {
            return third_painting_done(whoDunIt);
        }
        else
        {
            return leaving(whoDunIt);
        }
    }
    public List<Tuple<string,string, string>> pre_paintings(int whoDunIt)
    {
        List<Tuple<string, string, string>> response = new List<Tuple<string,string, string>>();
        
        response.Add(new Tuple<string,string,string>("main","normal","What's the emerge-"));
        response.Add(new Tuple<string,string,string>("kristen","normal","About time you showed up, come see the state of my living room, quick!"));
        response.Add(new Tuple<string,string,string>("main","normal","You follow behind to see paintings cut up, paintings smudged, and paintings completely stripped from color... This... This is terrible. Who would do something like this?"));
        response.Add(new Tuple<string,string,string>("kristen","normal","See? I told you it was an emergency! Someone broke into my house! "));
        response.Add(new Tuple<string,string,string>("main","normal","Have you called the police?"));
        response.Add(new Tuple<string,string,string>("kristen","normal","The police can't fix my paintings! That's why I called you."));
        response.Add(new Tuple<string,string,string>("main","normal","Righttttt so I'm just your painting fixer now."));
        response.Add(new Tuple<string,string,string>("kristen","normal","No! You are my friend... who just so happens to be amazing at fixing up paintings. In fact you might even make them look better than how they originally look."));
        response.Add(new Tuple<string,string,string>("main","normal","And I assume I'm doing this for free?"));
        response.Add(new Tuple<string,string,string>("kristen","normal","Well... Yes. I don't get paid until next week."));
        response.Add(new Tuple<string,string,string>("main","normal","Ugh alright."));

        return response;
    }

    public List<Tuple<string, string, string>> first_painting_done(int whoDunIt)
    {
        List<Tuple<string, string, string>> response = new List<Tuple<string, string, string>>();
        response.Add(new Tuple<string, string, string>("main", "normal", "So where were you at? I thought you preferred catching up with work at night?"));
        response.Add(new Tuple<string, string, string>("crane", "angry", "Oh I was at that art show Dr. Burch is hosting. I submitted some of my peices but didn't find any of them hanging up. I talked to her and she claimed they weren't good enough! I come back here to take my mind off of things and this mess was here!"));
        response.Add(new Tuple<string, string, string>("main", "normal", "<I> Yeah, because this is so much worse than the disorganised mess you usually have."));
        response.Add(new Tuple<string, string, string>("main", "normal", "Weird, that's where my friend was as well..."));
        return response;
    }

    public List<Tuple<string, string, string>> second_painting_done(int whoDunIt)
    {
        List<Tuple<string, string, string>> response = new List<Tuple<string, string, string>>();
        response.Add(new Tuple<string, string, string>("crane", "happy", "<I> ...mumbling quiety..."));
        response.Add(new Tuple<string, string, string>("main", "normal", "What was that?"));
        response.Add(new Tuple<string, string, string>("crane", "happy","Oh nothing, just saying that she got what she deserved."));
        response.Add(new Tuple<string, string, string>("main", "normal", "Kristen? That's a bit harsh"));
        response.Add(new Tuple<string, string, string>("crane", "normal", "No No! I have nothing against her. The museum curator also got a visit from our new 'Modern artist'. Some of her paintings were destroyed as well."));
        response.Add(new Tuple<string, string, string>("main", "normal", "<I> What is going on!?"));
        response.Add(new Tuple<string, string, string>("main", "normal", "Oh no! I'll go see if she can tell me anything about what happened."));
        return response; 
    }

    public List<Tuple<string, string, string>> third_painting_done(int whoDunIt)
    {
        List<Tuple<string, string, string>> response = new List<Tuple<string, string, string>>();
        if (whoDunIt != 1)
        {
            response.Add(new Tuple<string, string, string>("main", "normal", "<I> There's a blond hair on one of these paintings. it probably would have been knocked off if it was just from a student, right?"));
            //response.Add(new Tuple<string, string>("main"))

        }
        else
        {
            response.Add(new Tuple<string, string, string>("main", "normal", "<I> I think Crane is going bald from stress. I keep finding his hair everywhere."));
            response.Add(new Tuple<string, string, string>("main", "normal", "<I> I guess he does spend most of his time here though ... it's not that weird."));
        }
        return response;
    }

    public List<Tuple<string, string, string>> fourth_painting_done(int whoDunIt)
    {
        List<Tuple<string, string, string>> response = new List<Tuple<string, string, string>>();
        response.Add(new Tuple<string, string, string>("main", "normal", "<I> It's hard to see these paintings, the room is so bright. I know he has always had a fear of the dark but are all of these lights supposed to be on all the time?"));
        return response;
    }

    public List<Tuple<string,string, string>> leaving(int whoDunIt)
    {
        List<Tuple<string, string, string>> response = new List<Tuple<string, string, string>>();
        response.Add(new Tuple<string, string, string>("main", "normal", "Alright Professor Crane,  I'm going to head over to the museum to see if the curator needs any help."));
        response.Add(new Tuple<string, string, string>("Crane", "normal", "Oh...ok. Uh let me know if you find anything out!"));
        response.Add(new Tuple<string, string, string>("main", "normal", "Will do! Call me if you remember something important."));
        response.Add(new Tuple<string, string, string>("main", "normal", "<I> He was acting kind of weird, but I guess all his stuff just got destroyed"));
        return response;
    }


}
