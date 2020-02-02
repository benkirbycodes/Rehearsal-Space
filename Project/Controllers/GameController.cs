using System;
using System.Collections.Generic;
using ConsoleAdventure.Project.Interfaces;
using ConsoleAdventure.Project.Models;

namespace ConsoleAdventure.Project.Controllers
{

  public class GameController : IGameController
  {
    private GameService _gameService = new GameService();
    private Game _game = new Game();

    private bool _playing = true;

    //NOTE Makes sure everything is called to finish Setup and Starts the Game loop
    public void Run()
    {
      Console.Clear();
      Console.WriteLine("What is your name, rogue musician?");
      string name = Console.ReadLine();
      _gameService.Setup(name);
      Console.Clear();
      Console.WriteLine($"~~~~ Welcome To The Rehearsal Space {name} ~~~\n");
      Console.WriteLine("Enter HELP to see a list of Commands\n");
      _gameService.PrintMenu();


      while (_gameService._playing)
      {
        Print();
        GetUserInput();
      }
      Console.Clear();
      Console.WriteLine("Smell ya later");

    }

    //NOTE Gets the user input, calls the appropriate command, and passes on the option if needed.
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
          _gameService.TakeItem(option);
          break;
        case "look":
          _gameService.Look();
          break;
        case "use":
          bool itemResult = _gameService.UseItem(option);
          if (itemResult)
          {
            Console.Clear();
            Print();
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
          Console.WriteLine("That's not an option I recognize.");
          break;
      }





      //NOTE this will take the user input and parse it into a command and option.
      //IE: take silver key => command = "take" option = "silver key"

    }

    //NOTE this should print your messages for the game.
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