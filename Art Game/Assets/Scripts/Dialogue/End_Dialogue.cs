using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class End_Dialogue : MonoBehaviour
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
            return start_of_end(whoDunIt);
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
    public List<Tuple<string,string, string>> start_of_end(int whoDunIt)
    {
        List<Tuple<string, string, string>> response = new List<Tuple<string,string, string>>();
        
        response.Add(new Tuple<string,string,string>("Player","normal","Thank you all for coming down. It seems like you are the only three to be effected by this vandalizer, and I want to let you know that I believe I have figured out who the criminal is."));
        response.Add(new Tuple<string, string, string>("Player", "normal", "It is one of you."));
        response.Add(new Tuple<string, string, string>("Kristen", "shocked", "What?"));
        response.Add(new Tuple<string, string, string>("Crane","shocked","How can..."));
        response.Add(new Tuple<string, string, string>("Burch", "shocked","There is no way..."));
        response.Add(new Tuple<string, string, string>("Everyone", "angry", "<I> They all stare at one another accusingly"));
        response.Add(new Tuple<string, string, string>("Kristen", "shocked", "Well you know it can't be me! We've been friends for years you know me!"));
        response.Add(new Tuple<string, string, string>("Crane", "angry", "Why would I do it? Risk my career?!"));
        response.Add(new Tuple<string, string, string>("Burch", "shocked", "My business already isn't doing well. Why would I sneak off into the night to destroy others and my paintings?"));
        response.Add(new Tuple<string, string, string>("Player", "normal", "Listen. You all have your reasons why you wouldn't and why you would."));
        // May want to add more here, but pick after

        return response;
    }

    public List<Tuple<string, string, string>> picked_kristen(int whoDunIt)
    {
        List<Tuple<string, string, string>> response = new List<Tuple<string, string, string>>();
        if (whoDunIt > 0)
        {
            response.Add(new Tuple<string, string, string>("Kristen", "angry", "Really? You genuinely think I could do that? I said I went out to dinner, why didn't you believe me? I thought we were friends"));
            response.Add(new Tuple<string, string, string>("Kristen", "normal", "<L> She storms out of the building before you can say anything, and you feel in your heart you got it wrong."));
        }
        else
        {
            response.Add(new Tuple<string, string, string>("Kristen", "shocked", "What! Me? But I... I couldn't... *sigh*"));
            response.Add(new Tuple<string, string, string>("Kristen", "angry", "Ugh. Fine. Yeah, it was me. I did it, way to go. Crane got what he deserved. Just because you hate your job and aren't good at your hobbies doesn't give you the right to fail students for no good reason."));
            response.Add(new Tuple<string, string, string>("Crane", "shocked", "I don't think that's fai..."));
            response.Add(new Tuple<string, string, string>("Burch", "angry", "WHY DID YOU DESTROY MY ART!?"));
            response.Add(new Tuple<string, string, string>("Kristen", "happy", "You made it too easy. Leaving the doors unlocked while you go get a coffee every night? You didn't deserve it like he did, but I'm not heartbroken about it."));

        }

        return response;
    }

    public List<Tuple<string, string, string>> picked_crane(int whoDunIt)
    {
        List<Tuple<string, string, string>> response = new List<Tuple<string, string, string>>();
        if (whoDunIt == 1)
        {
            response.Add(new Tuple<string, string, string>("Crane", "angry", "Okay okay okay, I admit it. I did it. I was so mad at what you had said about my work, Dr. Burch, that I went back and destroyed them all. I realized then that I shouldn't be the only one to suffer. Kristen, I do apologize to you. I thought I could get away with it with a third victim, and you made it obvious that you weren't home with that ridiculous blog of yours."));
            response.Add(new Tuple<string, string, string>("Burch", "normal", "I guess I could have been nicer about it..."));
            response.Add(new Tuple<string, string, string>("Kristen", "angry", "Uh screw you. My blog is perfect and so was my art until you ruined it! What a sad man you are."));

        }
        else
        {
            response.Add(new Tuple<string, string, string>("Crane", "shocked", "I really took you as a better person. I went to my night class for art lessons. Burch's words hurt me but I wanted to prove her wrong with my art, not malicious actions."));
            response.Add(new Tuple<string, string, string>("Crane", "normal", "I'm disappointed in you. Thank you for fixing my art, but please don't come back by."));
            response.Add(new Tuple<string, string, string>("Crane", "normal", "<L> He walks out of the building, hurt you would accuse him of that."));



        }

        return response;
    }

    public List<Tuple<string, string, string>> picked_burch(int whoDunIt)
    {
        List<Tuple<string, string, string>> response = new List<Tuple<string, string, string>>();
        if (whoDunIt ==2)
        {
            response.Add(new Tuple<string, string, string>("Burch", "happy", "And I would gladly do it again. Kristen, you've cost me thousands with your pretentious blog. I invited you tonight because I know you always go to eat out after an art show for the 'vibes'. What does that even mean? It was almost perfect. I destroyed yours because I hate you, mine for an alibi and insurance, and Crane your art is just bad so I don't really feel guilty about that."));
            response.Add(new Tuple<string, string, string>("Kristen", "shocked", "Wow. You are a monster..."));
            response.Add(new Tuple<string, string, string>("Crane", "shocked", "How can you possibly be so cruel?"));



        }
        else
        {
            response.Add(new Tuple<string, string, string>("Burch", "shocked", "I said she was bad for business, but that is not enough for you to accuse me of mass vandalism and insurance fraud!"));
            response.Add(new Tuple<string, string, string>("Burch", "angry", "I can't believe you would accuse me of something like this in my own museum. You can try again to figure out who it is, but I'm not staying anymore."));
            response.Add(new Tuple<string, string, string>("Burch", "normal", "<L> She walks out of the room, reasonably annoyed given the situation."));
        }

        return response;
    }

    public List<Tuple<string, string, string>> third_painting_done(int whoDunIt)
    {
        List<Tuple<string, string, string>> response = new List<Tuple<string, string, string>>();
        if (whoDunIt != 1)
        {
            response.Add(new Tuple<string, string, string>("Player", "normal", "<I> There's a blond hair on one of these paintings. it probably would have been knocked off if it was just from a student, right?"));
            //response.Add(new Tuple<string, string>("main"))

        }
        else
        {
            response.Add(new Tuple<string, string, string>("Player", "normal", "<I> I think Crane is going bald from stress. I keep finding his hair everywhere."));
            response.Add(new Tuple<string, string, string>("Player", "normal", "<I> I guess he does spend most of his time here though ... it's not that weird."));
        }
        return response;
    }

    public List<Tuple<string, string, string>> fourth_painting_done(int whoDunIt)
    {
        List<Tuple<string, string, string>> response = new List<Tuple<string, string, string>>();
        response.Add(new Tuple<string, string, string>("Player", "normal", "<I> It's hard to see these paintings, the room is so bright. I know he has always had a fear of the dark but are all of these lights supposed to be on all the time?"));
        return response;
    }

    public List<Tuple<string,string, string>> leaving(int whoDunIt)
    {
        List<Tuple<string, string, string>> response = new List<Tuple<string, string, string>>();
        response.Add(new Tuple<string, string, string>("Player", "normal", "Alright Professor Crane,  I'm going to head over to the museum to see if the curator needs any help."));
        response.Add(new Tuple<string, string, string>("Crane", "normal", "Oh...ok. Uh let me know if you find anything out!"));
        response.Add(new Tuple<string, string, string>("Player", "normal", "Will do! Call me if you remember something important."));
        response.Add(new Tuple<string, string, string>("Player", "normal", "<I> He was acting kind of weird, but I guess all his stuff just got destroyed"));
        return response;
    }


}
