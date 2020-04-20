
using UnityEngine;
using UnityEngine.UI;
public class Death_Panel_Behavior : MonoBehaviour
{
    [SerializeField] private Text deathTxt = null;

    private string[] positiveSentences =
    {
        "You can do this!",
        "You will conquer this game!",
        "Blame the developer!",
        "Try again!",
        "You are beautiful <3",
        "You are not alone!",
        "Master your timing!",
        "Master control of your emtions!",
        "You are the chosen one!",
        "Failure is 1 step closer to success!",
        "Learning is only part of the process!",
        "All journeys starts with a first step!",
        "Try swearing at the developer?",
        "Remember. We are all one!",
        "Good timing is also part of the process!",
        "Don't hesitate; Just go for it!",
        "GET UP!",
        "Just do it!",
        "Keep it up!",
        "You are almost there!",
        "Yell it: \"F**k the developer!\""
    };

    private void OnEnable()
    {
        deathTxt.text = positiveSentences[Random.Range(0, positiveSentences.Length)];
        Invoke(nameof(ClosePanel), Settings.DeathWaitTimer);
    }

    private void ClosePanel()
    {
        gameObject.SetActive(false);
    }
}

// This is just me screwing around
/*
 * 
 *  Keeping it for personal use
 * 
 *  StringBuilder builder = new StringBuilder();
 *     
    string[] determinants = { "a", "an", "the", "it", "this",
                              "that", "these", "those", "my", "many", "few",
                              "some", "them" };
    string[] subjects = { "Jeremy", "cat", "game", "closet", "school", "pencil"
                        , "appricot", "police officer", "stool", "potato", "carrot"
                        , "banana", "keyboard", "teacher", "manager", "administrator"
                        , "garbage can", "apples", "watch", "soap", "acid", "bleach"
                        , "poison", "pope", "children" };
    string[] verbs = { "hangs", "shoots", "burns", "fights", "traps", "harasses"
                    , "cooks", "imprisons", "butters up", "steals", "shreds"
                    , "nags", "touches", "cuddles", "belittles", "jumps" };

    string[] adjectives = { "ugly", "beautiful", "aggressive", "depressed", "disturbed"
                            , "repulsive", "smelly", "fat", "silly", "bad", "good", "goofy"
                            , "charming", "cruel", "poor", "dead" };

    private void GenerateShittySentence()
    {
        builder.Append(determinants[Random.Range(0, determinants.Length)]);
        builder.Append(" ");
        if (Random.Range(0, 2) == 0) builder.Append(adjectives[Random.Range(0, adjectives.Length)]);
        builder.Append(" ");
        builder.Append(subjects[Random.Range(0, subjects.Length)]);
        builder.Append(" ");
        builder.Append(verbs[Random.Range(0, verbs.Length)]);
        builder.Append(" ");
        builder.Append(subjects[Random.Range(0, subjects.Length)]);
        builder.Append(".");
    }
*/