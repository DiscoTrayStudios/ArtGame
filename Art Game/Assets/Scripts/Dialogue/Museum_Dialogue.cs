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
            return fourth_painting_done(whoDunIt);
        }
        else if (completed_games == 5)
        {
            return fifth_painting_done(whoDunIt);
        }
        else
        {
            return leaving(whoDunIt);
        }
    }
    public List<Tuple<string, string, string>> pre_paintings(int whoDunIt)
    {
        List<Tuple<string, string, string>> response = new List<Tuple<string, string, string>>();
        response.Add(new Tuple<string, string, string>("Player", "normal","Hey Doc, what's going on? Thi s is my third call of the night and there seems to be a pattern."));
        response.Add(new Tuple<string, string, string>("Burch", "shocked", "Three you say? That's just awful. yes, please come, they're right over here. Please do the best you can! These things are priceless you know."));
        response.Add(new Tuple<string, string, string>("Player", "normal", "Of course, I'll do my best. I'll let you know if I find anything over here."));
        return response;
    }

    public List<Tuple<string, string, string>> first_painting_done(int whoDunIt)
    {
        List<Tuple<string, string, string>> response = new List<Tuple<string, string, string>>();
        response.Add(new Tuple<string, string, string>("Player", "normal", "So how did this happen? I thought you were hosting a silent auction tonight."));
        response.Add(new Tuple<string, string, string>("Burch", "normal", "I was, but the night had ended and I had cleaned up and was going to go through the sales after I got some coffee from down the street."));
        response.Add(new Tuple<string, string, string>("Player", "normal", "<I> Doesn't surprise me, she's the biggest clean freak I've met. She came by my office once and threw away a soda I was only half done with."));
        return response;
    }


    // TODO: Everything below is copied from the office. Do different stuff.
    public List<Tuple<string, string, string>> second_painting_done(int whoDunIt)
    {
        List<Tuple<string, string, string>> response = new List<Tuple<string, string, string>>();
        response.Add(new Tuple<string, string, string>("Player", "normal", "Do you know why you were targeted, or why anyone else was?"));
        response.Add(new Tuple<string, string, string>("Burch", "normal", "No, I have no idea. Maybe someone trying to destroy my business. Who else got their paintings destroyed?"));
        response.Add(new Tuple<string, string, string>("Player", "normal", "Professor Michael Crane and Kristen Walker. Do you know them?"));
        response.Add(new Tuple<string, string, string>("Burch", "angry", "No idea who the professor is, but I do know Kristen. She is an awful person, her and that horrible blog of hers. She has cost me os much business with her 'Critiques' that I had to ban her! A shame really, she used to be a great customer."));
        response.Add(new Tuple<string, string, string>("Player", "normal", "Wasn't she just here earlier tonight?"));
        response.Add(new Tuple<string, string, string>("Burch", "normal", "Yes, I let her in on the condition she leaves her phone in her car. To be honest, business hasn't been great so a sale even to her would be awesome."));
        return response;
    }

    public List<Tuple<string, string, string>> third_painting_done(int whoDunIt)
    {
        List<Tuple<string, string, string>> response = new List<Tuple<string, string, string>>();
        response.Add(new Tuple<string, string, string>("Player", "normal", "<I> I don't understand... it's been hours since the crime happened but these seem to be the only people affected. They were all here but so were other collectors. It doesn't make any sense..."));
        response.Add(new Tuple<string, string, string>("Player", "normal", "<I> ...unless..."));
        response.Add(new Tuple<string, string, string>("Player", "normal", "<I> I mean, they all hate each other, or one other person specifically. If they attacked that person, it would be obvious even if they had self sabotaged. If they involved a third, 'innocent' party though it wouldn't be clear!"));


        return response;




        
    }

    public List<Tuple<string, string, string>> fourth_painting_done(int whoDunIt)
    {
        List<Tuple<string, string, string>> response = new List<Tuple<string, string, string>>();
        response.Add(new Tuple<string, string, string>("Burch", "normal", "Is there anything I can do to help?"));
        response.Add(new Tuple<string, string, string>("Player", "normal", "Yes, I think I've just about figured out who did this. I need you to call the other two victims though and meet us here. I need to talk to them once more to make sure."));
        return response;
    }


    public List<Tuple<string, string, string>> fifth_painting_done(int whoDunIt)
    {
        List<Tuple<string, string, string>> response = new List<Tuple<string, string, string>>();
        if (whoDunIt != 0)
        {
            response.Add(new Tuple<string, string, string>("Player", "normal", "<I> This room reeks of coffee, even more so than usual."));

        }
        else
        {
            response.Add(new Tuple<string, string, string>("Player", "normal", "<I> I think Kristen visited this room earlier during the auction. Her perfume has been stuck in my nose all night."));
        }
        return response;
    }
    public List<Tuple<string, string, string>> leaving(int whoDunIt)
    {
        List<Tuple<string, string, string>> response = new List<Tuple<string, string, string>>();
        response.Add(new Tuple<string, string, string>("Player", "normal", "I guess that's it. Time to question everybody. I can't believe it's one of these three..."));
        return response;
    }
}
