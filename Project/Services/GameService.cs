using System.Collections.Generic;
using System;
using ConsoleAdventure.Project.Interfaces;
using ConsoleAdventure.Project.Models;

namespace ConsoleAdventure.Project
{
  public class GameService : IGameService
  {
    private IGame _game { get; set; }
    public bool _playing = true;
    public List<Message> Messages { get; set; }
    string title = @"
 ______   _______ __   __ _______ _______ ______   _______ _______ ___       _______ _______ _______ _______ _______   ___ __    _ __   __ _______ ______  _______ ______   
|    _ | |       |  | |  |       |   _   |    _ | |       |   _   |   |     |       |       |   _   |       |       | |   |  |  | |  | |  |   _   |      ||       |    _ |  
|   | || |    ___|  |_|  |    ___|  |_|  |   | || |  _____|  |_|  |   |     |  _____|    _  |  |_|  |       |    ___| |   |   |_| |  |_|  |  |_|  |  _    |    ___|   | ||  
|   |_||_|   |___|       |   |___|       |   |_||_| |_____|       |   |     | |_____|   |_| |       |       |   |___  |   |       |       |       | | |   |   |___|   |_||_ 
|    __  |    ___|       |    ___|       |    __  |_____  |       |   |___  |_____  |    ___|       |      _|    ___| |   |  _    |       |       | |_|   |    ___|    __  |
|   |  | |   |___|   _   |   |___|   _   |   |  | |_____| |   _   |       |  _____| |   |   |   _   |     |_|   |___  |   | | |   ||     ||   _   |       |   |___|   |  | |
|___|  |_|_______|__| |__|_______|__| |__|___|  |_|_______|__| |__|_______| |_______|___|   |__| |__|_______|_______| |___|_|  |__| |___| |__| |__|______||_______|___|  |_|
";
    public GameService()
    {
      _game = new Game();
      Messages = new List<Message>();
    }
    public bool Go(string direction)
    {
      if (_game.CurrentRoom.Exits.ContainsKey(direction) && !_game.CurrentRoom.IsTrap)
      {
        _game.CurrentRoom = _game.CurrentRoom.Exits[direction];
        PrintMenu();
        return true;
      }
      else if (_game.CurrentRoom.IsTrap)
      {
        Messages.Add(new Message(title));
        Messages.Add(new Message(_game.CurrentRoom.BadOutcome));
        Messages.Add(new Message(""));
        Messages.Add(new Message("(Q)uit or press any key to play again"));
        _game.Setup();
        return false;
      }
      else Messages.Add(new Message("There's no door that direction\n"));
      return true;
    }
    public void Help()
    {
      Console.Clear();
      Messages.Add(new Message(title));
      Messages.Add(new Message("---COMMANDS---\n"));
      Messages.Add(new Message("Go ----------- Move In A Direction"));
      Messages.Add(new Message("Check --------Check The Room for Items"));
      Messages.Add(new Message("Take --------- Take an Item"));
      Messages.Add(new Message("Use ---------- Use an Item"));
      Messages.Add(new Message("Look --------- Look Around At The Room"));
      Messages.Add(new Message("Quit --------- Quit the Game"));
      Messages.Add(new Message("Inventory ---- See your inventory\n"));

      Messages.Add(new Message("Press ENTER to return to Game"));


    }

    public void Check()
    {
      Console.Clear();

      if (_game.CurrentRoom.Items.Count == 0)
      {
        Messages.Add(new Message(title));
        Messages.Add(new Message("There are no items in this room"));
        return;
      }
      Messages.Add(new Message(title));
      Messages.Add(new Message("--- ITEMS IN THIS ROOM---\n"));
      foreach (var item in _game.CurrentRoom.Items)
      {
        Messages.Add(new Message($"{item.Name} ---- {item.Description}"));
      }
      Messages.Add(new Message(""));
      Messages.Add(new Message("Press ENTER to return to Game"));


    }

    public void Inventory()
    {
      Console.Clear();

      if (_game.CurrentPlayer.Inventory.Count == 0)
      {
        Messages.Add(new Message(title));
        Messages.Add(new Message("You have nothing in your inventory"));
        return;
      }
      Messages.Add(new Message(title));
      Messages.Add(new Message("--- INVENTORY ---\n"));
      foreach (var item in _game.CurrentPlayer.Inventory)
      {
        Messages.Add(new Message($"{item.Name} -- {item.Description}"));
      }
      Messages.Add(new Message(""));

      Messages.Add(new Message("Press ENTER to return to Game"));

    }

    public void Look()
    {
      Messages.Add(new Message(_game.CurrentRoom.Description));
      Messages.Add(new Message(""));
    }

    public void Quit()
    {
      _playing = false;
    }

    public void Reset()
    {
      _game.Setup();
    }

    public void Setup(string playerName)
    {
      Player Player = new Player(playerName);
      _game.CurrentPlayer = Player;
    }

    public void TakeItem(string itemName)
    {
      if (_game.CurrentRoom.Items.Count == 0)
      {
        Messages.Add(new Message("No Items In This Room\n"));
      }
      else if (_game.CurrentRoom.Items.Exists(i => i.Name.ToLower() == itemName))
      {
        var item = _game.CurrentRoom.Items.Find(i => i.Name.ToLower() == itemName);
        Messages.Add(new Message($"You took the {item.Name}\n"));
        _game.CurrentPlayer.Inventory.Add(item);
        _game.CurrentRoom.Items.Remove(item);
      }
      else
      {
        Messages.Add(new Message("No item by that name in this room\n"));
      }
    }

    public bool UseItem(string itemName)
    {
      if (_game.CurrentPlayer.Inventory.Exists(i => i.Name.ToLower() == itemName) && _game.CurrentRoom.IsTrap && itemName == _game.CurrentRoom.RelevantItem.Name.ToLower())
      {
        Messages.Add(new Message(title));
        _game.CurrentRoom.IsTrap = false;
        var item = _game.CurrentPlayer.Inventory.Find(i => i.Name == itemName);
        _game.CurrentPlayer.Inventory.Remove(item);
        Messages.Add(new Message(_game.CurrentRoom.GoodOutcome));
        if (_game.CurrentRoom.LastRoom)
        {
          Messages.Add(new Message(""));
          Messages.Add(new Message("(Q)uit or press any key to play again"));
          _game.Setup();
          return false;
        }
        return true;


      }
      else if (_game.CurrentPlayer.Inventory.Exists(i => i.Name.ToLower() == itemName) && _game.CurrentRoom.IsTrap && itemName != _game.CurrentRoom.RelevantItem.Name.ToLower())
      {
        Messages.Add(new Message(title));
        Messages.Add(new Message("oh no, That item doesn't seem to have any effect!"));
        Messages.Add(new Message(""));
        Messages.Add(new Message(_game.CurrentRoom.BadOutcome));
        Messages.Add(new Message(""));
        Messages.Add(new Message("(Q)uit or press any key to play again"));
        _game.Setup();
        return false;
      }
      else if (_game.CurrentPlayer.Inventory.Exists(i => i.Name.ToLower() == itemName) && !_game.CurrentRoom.IsTrap)

      {
        Messages.Add(new Message(title));
        Messages.Add(new Message("Maybe you should save that item, doesn't seem to have much use in this room."));
        return true;
      }
      else
        Messages.Add(new Message(title));
      Messages.Add(new Message("No item by that name in your Inventory."));
      return true;
    }




    public void PrintMenu()
    {
      Messages.Add(new Message(_game.CurrentRoom.Description));
      Console.WriteLine("");

    }
  }

}