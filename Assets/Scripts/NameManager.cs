using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Linq;

public class NameManager : MonoBehaviour
{
    public GameObject nameHolder;
    public GameObject startButton;
    public GameObject ingameButtonParent;
    public GameObject correctDisplay;
    public GameObject incorrectDisplay;
    public GameObject endGameDisplay;
    public TMP_Text endGameAccuracy;
    public TMP_Text nameText;
    public HashSet<string> usedNames = new();
    public HashSet<string> randomDisplayedNames = new();
    public List<string> firstNames = new()
    {
        "Olivia",
        "John",
        "Jane",
        "James",
        "Mary",
        "Patricia",
        "Linda",
        "Robert",
        "David",
        "Barbara",
        "William",
        "Abraham",
        "Erin",
        "Karen",
        "Sarah",
        "Lisa",
        "Charles",
        "Donna",
        "Brian",
        "Sharon",
        "Laura",
        "Eric",
    };

    public List<string> lastNames = new()
    {
        "Smith",
        "Doe",
        "Baker",
        "Williams",
        "Jones",
        "Davis",
        "Jones",
        "Miller",
        "Wilson",
        "Harris",
        "Martin",
        "Garcia",
        "Martinez",
        "Wright",
        "Hill",
        "Lopez",
        "Turner",
        "Edwards",
        "Kelly",
    };

    List<string> firstNameHolder;
    int numbGuesses;
    int totalGuesses = 0;
    int correctGuesses = 0;

    void Awake()
    {
        firstNameHolder = new List<string>(firstNames);
        numbGuesses = firstNameHolder.Count;
    }


    public void StartGame()
    {
        nameHolder.SetActive(true);
        StartCoroutine(GenerateNamePairs());
    }

    IEnumerator GenerateNamePairs()
    {
        while (firstNames.Count > 0)
        {
            int rand = Random.Range(0, firstNames.Count);
            int rand2 = Random.Range(0, lastNames.Count);

            string fullName = $"{firstNames[rand]} {lastNames[rand2]}";
            usedNames.Add(fullName);
            nameText.text = fullName;
            Debug.Log($"Full name: '{fullName}'");

            firstNames.RemoveAt(rand);

            yield return new WaitForSeconds(0.33f);
        }
        nameHolder.SetActive(false);
        startButton.SetActive(false);
        StartCoroutine(DisplayRandomName());
    }

    IEnumerator DisplayRandomName()
    {
        yield return new WaitForSeconds(2f);

        ingameButtonParent.SetActive(true);
        nameHolder.SetActive(true);

        // 50-50 to give a real name or a fake one
        int randRoll = Random.Range(0, 2);
        Debug.Log($"rand val: {randRoll}");
        if (randRoll == 0){
            // Real name
            string randomName;
            do {
            // Real name from the set
            int index = Random.Range(0, usedNames.Count);
            randomName = usedNames.ElementAt(index);
            nameText.text = randomName;
            } while (randomDisplayedNames.Contains(randomName));
        }
        else
        {
            string fullName;
            // Random name
            do{
            int rand = Random.Range(0, firstNameHolder.Count);
            int rand2 = Random.Range(0, lastNames.Count);

            fullName = $"{firstNameHolder[rand]} {lastNames[rand2]}";
            nameText.text = fullName;
            } while (usedNames.Contains(fullName) || randomDisplayedNames.Contains(fullName));
        }

    }

    public void VerifySteal(bool didISteal)
    {
        nameHolder.SetActive(false);
        ingameButtonParent.SetActive(false);
        numbGuesses--;
        totalGuesses++;

        if (didISteal == usedNames.Contains(nameText.text))
        {
            StartCoroutine(DisplayCorrect());
            correctGuesses++;
        }
        else
        {
            StartCoroutine(DisplayIncorrect());
        }
    }

    IEnumerator DisplayCorrect()
    {
        Debug.Log("Correct!");
        correctDisplay.SetActive(true);

        yield return new WaitForSeconds(2f);

        correctDisplay.SetActive(false);
        if (numbGuesses == 0)
        {
            EndGameDisplay();
        }
        else
        {
            StartCoroutine(DisplayRandomName());
        }
    }

    IEnumerator DisplayIncorrect()
    {
        Debug.Log("Incorrect!");
        incorrectDisplay.SetActive(true);

        yield return new WaitForSeconds(2f);


        incorrectDisplay.SetActive(false);
        if (numbGuesses == 0)
        {
            EndGameDisplay();
        }
        else
        {
            StartCoroutine(DisplayRandomName());
        }
    }

    void EndGameDisplay()
    {
        endGameDisplay.SetActive(true);
        endGameAccuracy.text = $"{correctGuesses}/{totalGuesses}";
    }
}
