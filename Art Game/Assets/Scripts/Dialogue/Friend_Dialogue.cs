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
        
        response.Add(new Tuple<string,string,string>("Player","normal","What's the emerge-"));
        response.Add(new Tuple<string,string,string>("Kristen","angry","About time you showed up, come see the state of my living room, quick!"));
        response.Add(new Tuple<string,string,string>("Player","normal","You follow behind to see paintings cut up, paintings smudged, and paintings completely stripped from color... This... This is terrible. Who would do something like this?"));
        response.Add(new Tuple<string,string,string>("Kristen", "normal","See? I told you it was an emergency! Someone broke into my house! "));
        response.Add(new Tuple<string,string,string>("Player","normal","Have you called the police?"));
        response.Add(new Tuple<string,string,string>("Kristen","normal","The police can't fix my paintings! That's why I called you."));
        response.Add(new Tuple<string,string,string>("Player","normal","Righttttt so I'm just your painting fixer now."));
        response.Add(new Tuple<string,string,string>("Kristen","normal","No! You are my friend... who just so happens to be amazing at fixing up paintings. In fact you might even make them look better than how they originally look."));
        response.Add(new Tuple<string,string,string>("Player","normal","And I assume I'm doing this for free?"));
        response.Add(new Tuple<string,string,string>("Kristen","normal","Well... Yes. I don't get paid until next week."));
        response.Add(new Tuple<string,string,string>("Player","normal","Ugh alright."));

        return response;
    }

    public List<Tuple<string, string, string>> first_painting_done(int whoDunIt)
    {
        List<Tuple<string, string, string>> response = new List<Tuple<string, string, string>>();
        response.Add(new Tuple<string, string, string>("Player", "normal", "So what were you doing before this? How did someone get the opportunity to do this?"));
        response.Add(new Tuple<string, string, string>("Kristen", "normal", "Well I was just at an art gallery like trying to plan out a new purchase ya'know?"));
        response.Add(new Tuple<string, string, string>("Player", "normal", "<I> No wonder she can't pay me.."));
        response.Add(new Tuple<string, string, string>("Kristen", "surprised", "And then I like came home with like the new painting and I find that it was like all like this!!"));
        response.Add(new Tuple<string, string, string>("Player", "normal", "You were at an art gallery this late?"));
        response.Add(new Tuple<string, string, string>("Kristen", "normal", "Of course! Like where else would I be?"));
        response.Add(new Tuple<string, string, string>("Player", "normal", "Right.."));
        return response;
    }

    public List<Tuple<string, string, string>> second_painting_done(int whoDunIt)
    {
        List<Tuple<string, string, string>> response = new List<Tuple<string, string, string>>();
        response.Add(new Tuple<string, string, string>("Kristen", "surprised", "Oh that's like, really weird.."));
        response.Add(new Tuple<string, string, string>("Player", "normal", "What is it?"));
        response.Add(new Tuple<string, string, string>("Kristen", "happy", "It's nothing, just like the worst professor ever also got their art like totally messed up."));
        response.Add(new Tuple<string, string, string>("Player", "normal", "Wait who??"));
        response.Add(new Tuple<string, string, string>("Kristen", "angry", "Oh only like the mean Professor Crane, he failed me for like absolutely no reason."));
        response.Add(new Tuple<string, string, string>("Player", "normal", "Sureeeee..."));
        response.Add(new Tuple<string, string, string>("Kristen", "surprised", "What's that supposed to mean!?"));
        response.Add(new Tuple<string, string, string>("Player", "normal", "Nothing, nothing!"));
        response.Add(new Tuple<string, string, string>("Player", "normal", "<I> I know Micheal Crane... Maybe I should go there next."));
        return response; 
    }

    public List<Tuple<string, string, string>> third_painting_done(int whoDunIt)
    {
        List<Tuple<string, string, string>> response = new List<Tuple<string, string, string>>();
        if (whoDunIt != 0)
        {
            response.Add(new Tuple<string, string, string>("Player", "normal", "<I> The fact that someone hit more than one place is suspicious..."));
            //response.Add(new Tuple<string, string>("Player"))

        }
        else
        {
            response.Add(new Tuple<string, string, string>("Player", "normal", "<I> Nothing looks out of place besides the paintings."));
            response.Add(new Tuple<string, string, string>("Player", "normal", "<I> Who could've done this?"));
        }
        return response;
    }

    public List<Tuple<string, string, string>> fourth_painting_done(int whoDunIt)
    {
        List<Tuple<string, string, string>> response = new List<Tuple<string, string, string>>();
        response.Add(new Tuple<string, string, string>("Player", "normal", "<I> Well that's all of the paintings, I better get going."));
        return response;
    }

    public List<Tuple<string,string, string>> leaving(int whoDunIt)
    {
        List<Tuple<string, string, string>> response = new List<Tuple<string, string, string>>();
        response.Add(new Tuple<string, string, string>("Player", "normal", "Alright Kristen I'm gonna go to Professor Crane's office and see what I can do there."));
        response.Add(new Tuple<string, string, string>("Kristen", "normal", "Okayy,, is there any way you could like change my grade in his computer while you're there?"));
        response.Add(new Tuple<string, string, string>("Player", "normal", "I don't think that's legal."));
        response.Add(new Tuple<string, string, string>("Kristen", "normal", "I'm like totally kidding!"));
        response.Add(new Tuple<string, string, string>("Player", "normal", "<I> Kristy is acting very weird about Micheal.. Oh well."));
        return response;
    }


}
