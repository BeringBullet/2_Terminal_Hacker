using System;
using System.Collections.Generic;
using UnityEngine;



public class Hacker : MonoBehaviour
{
    enum Screen { MainMenu, Password, Win }

    private const string menuHint = "You may type menu at any time.";
    private readonly Dictionary<int, string[]> levelPasswords = new Dictionary<int, string[]>()
    {
        { 1, new string[] { "books", "aisle", "shelf", "password", "font", "borrow" } },
        { 2, new string[] { "prisoner", "handcuffs", "holster", "uniform", "arrest" } },
        { 3, new string[] { "starfield", "telescope", "environment", "exploration", "astonauts" } }
    };

    private Screen currentscreen = Screen.MainMenu;
    private int level;
    private string password;

    // Start is called before the first frame update
    void Start()
    {
        ShowMainMenu();
    }

    private void OnUserInput(string input)
    {
        if (input == "menu")
        {
            ShowMainMenu();
            return;
        }
        else if (input == "exit" || input == "close" || input == "quit")
        {
            Terminal.WriteLine($"If on the web close the tab to {input}");
            Application.Quit();
        }
        switch (currentscreen)
        {
            case Screen.MainMenu:
                RunMainMenu(input);
                break;
            case Screen.Password:
                CheckPasword(input);
                break;
            case Screen.Win:
                DisplayWinscreen(input);
                RunMainMenu(input);
                break;
            default:
                break;
        }
    }

    private void ShowMainMenu()
    {
        level = 0;
        currentscreen = Screen.MainMenu;
        Terminal.ClearScreen();

        Terminal.WriteLine("What would you like to hack into?");
        Terminal.WriteLine("Pres 1 for the local library");
        Terminal.WriteLine("Pres 2 for the police station");
        Terminal.WriteLine("Pres 3 for the NASA");
        Terminal.WriteLine("Enter your Selection:");
    }

    private void RunMainMenu(string input)
    {
        bool isValidLevelNumber = (input == "1" || input == "2" || input == "3");
        if (isValidLevelNumber)
        {
            level = int.Parse(input);
            int index = UnityEngine.Random.Range(0, levelPasswords[level].Length);
            password = levelPasswords[level][index];
            currentscreen = Screen.Password;
            AskForPassword();
        }
        else
        {
            Terminal.WriteLine("Please Choose a valid level!");
            Terminal.WriteLine(menuHint);
        }
    }

    private void AskForPassword()
    {
        Terminal.ClearScreen();
        Terminal.WriteLine($"You have chosen level { level }");
        Terminal.WriteLine($"Enter Password, hint: { password.Anagram() }");
    }

    private void CheckPasword(string input)
    {
        if (input == password)
        {
            DisplayWinscreen(input);
        }
        else
        {
            AskForPassword();
        }
    }

    private void DisplayWinscreen(string input)
    {
        currentscreen = Screen.Win;
        Terminal.ClearScreen();
        ShowLevelReward();
    }

    private void ShowLevelReward()
    {
        switch (level)
        {
            case 1:
                Terminal.WriteLine("Have a Book!");
                Terminal.WriteLine("Try level 2 for a harder challange!");
                break;
            case 2:
                Terminal.WriteLine("You got the prison key!");
                Terminal.WriteLine("Try level 3 for a harder challange!");
                break;
            case 3:
                Terminal.WriteLine("wow level 3!");
                break;
            default:
                break;
        }

    }


}
