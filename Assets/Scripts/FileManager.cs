using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;

public class FileManager : MonoBehaviour
{
    public TextAsset textAssetData;
    public GameObject enemyDataParent;
    public TMP_Text  nameText;
    public TMP_Text healthText;
    public TMP_Text damageText;
    public GameObject errorMsg;
    public TMP_Text errorText;
    public TMP_InputField inputField;


    [System.Serializable]
    public struct Enemy
    {
        public string name;
        public int health;
        public int damage;
    }

    public Dictionary<string, Enemy> enemiesDict = new Dictionary<string, Enemy>(100, StringComparer.OrdinalIgnoreCase);

    void Start()
    {
        ReadCSV();
    }

    void ReadCSV()
{
    string[] lines = textAssetData.text.Split('\n');

    for (int i = 1; i < lines.Length; i++)
    {
        if (string.IsNullOrWhiteSpace(lines[i]))
            continue;

        string[] values = lines[i].Split(',');

        Enemy newEnemy = new Enemy
        {
            name = values[0].Trim(),
            health = int.Parse(values[1].Trim()),
            damage = int.Parse(values[2].Trim())
        };

        enemiesDict[newEnemy.name.ToLower()] = newEnemy;
    }

    foreach (var kvp in enemiesDict)
    {
        Debug.Log($"DICT KEY: '{kvp.Key}'");
    }
}

    public void ParseEnemy()
    {
        string enemy = inputField.text.Trim().ToLower();
        inputField.text = "";
        Debug.Log($"Enemy input: '{enemy}'");
        if (enemiesDict.TryGetValue(enemy, out Enemy enemyData))
        {
            enemyDataParent.SetActive(true);
            errorMsg.SetActive(false);
            nameText.text = $"Name: {enemyData.name}";
            healthText.text = $"Health: {enemyData.health.ToString()}";
            damageText.text = $"Damage: {enemyData.damage.ToString()}";
            return;
        }

        enemyDataParent.SetActive(false);
        errorMsg.SetActive(true);
        errorText.text = $"Couldn't find enemy with name '{enemy}'.";
    }


}
