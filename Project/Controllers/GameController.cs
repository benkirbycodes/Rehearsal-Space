using System;
using System.Collections.Generic;
using ConsoleAdventure.Project.Interfaces;
using ConsoleAdventure.Project.Models;

namespace ConsoleAdventure.Project.Controllers
{

  public class GameController : IGameController
  {
    private GameService _gameService = new GameService();

    private bool _playing = true;

    public void Run()
    {
      Console.Clear();


      string title = @"
 ______   _______ __   __ _______ _______ ______   _______ _______ ___       _______ _______ _______ _______ _______   ___ __    _ __   __ _______ ______  _______ ______   
|    _ | |       |  | |  |       |   _   |    _ | |       |   _   |   |     |       |       |   _   |       |       | |   |  |  | |  | |  |   _   |      ||       |    _ |  
|   | || |    ___|  |_|  |    ___|  |_|  |   | || |  _____|  |_|  |   |     |  _____|    _  |  |_|  |       |    ___| |   |   |_| |  |_|  |  |_|  |  _    |    ___|   | ||  
|   |_||_|   |___|       |   |___|       |   |_||_| |_____|       |   |     | |_____|   |_| |       |       |   |___  |   |       |       |       | | |   |   |___|   |_||_ 
|    __  |    ___|       |    ___|       |    __  |_____  |       |   |___  |_____  |    ___|       |      _|    ___| |   |  _    |       |       | |_|   |    ___|    __  |
|   |  | |   |___|   _   |   |___|   _   |   |  | |_____| |   _   |       |  _____| |   |   |   _   |     |_|   |___  |   | | |   ||     ||   _   |       |   |___|   |  | |
|___|  |_|_______|__| |__|_______|__| |__|___|  |_|_______|__| |__|_______| |_______|___|   |__| |__|_______|_______| |___|_|  |__| |___| |__| |__|______||_______|___|  |_|
";
      Console.WriteLine(title);
      Console.WriteLine("What is your name, rogue musician?");
      string name = Console.ReadLine();
      _gameService.Setup(name);
      Console.Clear();
      _gameService.PrintMenu();


      while (_gameService._playing)
      {
        // Console.Clear();
        Console.WriteLine(title);
        Console.WriteLine("Player: " + name);

        Console.WriteLine("Enter HELP to see a list of Commands\n");
        Console.WriteLine("---------------------------------------");

        Console.WriteLine("");
        Console.WriteLine("");

        Print();
        GetUserInput();
      }
      Console.Clear();
      Console.WriteLine("Smell ya later");

    }

    public void GetUserInput()
    {
      Console.WriteLine("");
      Console.WriteLine("What would you like to do?");
      string input = Console.ReadLine().ToLower() + " ";
      string command = input.Substring(0, input.IndexOf(" "));
      string option = input.Substring(input.IndexOf(" ") + 1).Trim();

      switch (command)
      {
        case "help":
          _gameService.Help();
          Print();
          Console.ReadKey();
          Console.Clear();
          _gameService.PrintMenu();
          break;
        case "quit":
        case "q":
        case "exit":
          _gameService.Quit();
          break;
        case "go":
          bool result = _gameService.Go(option);
          if (result)
          {
            Console.Clear();
          }
          else if (!result)
          {
            Console.Clear();
            Print();
            string choice = Console.ReadLine().ToLower();
            if (choice == "q")
            {
              _gameService.Quit();
            }
            else
              Run();
          }
          break;
        case "take":
          Console.Clear();
          _gameService.TakeItem(option);
          break;
        case "look":
          Console.Clear();
          _gameService.Look();
          break;
        case "use":
          bool itemResult = _gameService.UseItem(option);
          if (itemResult)
          {
            Console.Clear();
          }
          else if (!itemResult)
          {
            Console.Clear();
            Print();
            string itemChoice = Console.ReadLine().ToLower();
            if (itemChoice == "q")
            {
              _gameService.Quit();
            }
            else
              Run();
          }
          break;
        case "reset":
          _gameService.Reset();
          Run();
          break;
        case "check":
          _gameService.Check();
          Print();
          Console.ReadKey();
          Console.Clear();
          _gameService.PrintMenu();
          break;
        case "inventory":
          _gameService.Inventory();
          Print();
          Console.ReadKey();
          Console.Clear();
          _gameService.PrintMenu();
          break;
        default:
          Console.Clear();
          _gameService.Messages.Add(new Message("That's not an option I recognize."));
          break;
      }
    }

    private void Print()
    {
      foreach (var message in _gameService.Messages)
      {
        message.Print();
      }
      _gameService.Messages.Clear();
    }

  }
}