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
    public GameService()
    {
      _game = new Game();
      Messages = new List<Message>();
    }
    public void Go(string direction)
    {
      if (_game.CurrentRoom.Exits.ContainsKey(direction))
      {
        if (_game.CurrentRoom.CheckIsTrap())
        {
          Messages.Add(new Message(_game.CurrentRoom.BadOutcome));
          Messages.Add(new Message(""));
          Messages.Add(new Message("Press Enter To Play Again?"));

        }
        _game.CurrentRoom = _game.CurrentRoom.Exits[direction];
        PrintMenu();
        return;

      }
      Messages.Add(new Message("There's no door that direction\n"));

    }
    public void Help()
    {
      Console.Clear();
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
        Messages.Add(new Message("There are no items in this room"));
        return;
      }
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
        Messages.Add(new Message("You have nothing in your inventory"));
        return;
      }
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
    ///<summary>
    ///Restarts the game 
    ///</summary>
    public void Reset()
    { ///TODO Doesn't work
      _game = new Game();
    }

    public void Setup(string playerName)
    {
      Player Player = new Player(playerName);
      _game.CurrentPlayer = Player;
    }
    ///<summary>When taking an item be sure the item is in the current room before adding it to the player inventory, Also don't forget to remove the item from the room it was picked up in</summary>
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
    ///<summary>
    ///No need to Pass a room since Items can only be used in the CurrentRoom
    ///Make sure you validate the item is in the room or player inventory before
    ///being able to use the item
    ///</summary>
    public void UseItem(string itemName)
    {
      if (_game.CurrentPlayer.Inventory.Exists(i => i.Name.ToLower() == itemName))
      {
        if (_game.CurrentRoom.CheckIsTrap() && itemName == _game.CurrentRoom.RelevantItem.Name.ToLower())
        {
          _game.CurrentRoom.IsTrap = false;
          var item = _game.CurrentPlayer.Inventory.Find(i => i.Name == itemName);
          _game.CurrentPlayer.Inventory.Remove(item);
          Messages.Add(new Message(_game.CurrentRoom.GoodOutcome));
          return;

        }
      }
    }
    public void PrintMenu()
    {
      Messages.Add(new Message(_game.CurrentRoom.Description));
      Console.WriteLine("");

    }
  }
}