

using System.Text;
using UnityEngine;
using UnityEngine.UI;
public class Death_Panel_Behavior : MonoBehaviour
{
    [SerializeField] private Text deathTxt = null;
    [SerializeField] private float timeBeforeClosing = 1f;
    StringBuilder builder = new StringBuilder();

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

    private void OnEnable()
    {
        GenerateShittySentence();
        deathTxt.text = builder.ToString();
        Invoke("ClosePanel", timeBeforeClosing);
        builder.Clear();
    }

    private void ClosePanel()
    {
        gameObject.SetActive(false);
    }

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

}

