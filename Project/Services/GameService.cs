using System.Collections.Generic;
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
        _game.CurrentRoom = _game.CurrentRoom.Exits[direction];
      }
    }
    public void Help()
    {
      Messages.Add(new Message("This will be a list of commands"));
    }

    public void Inventory()
    {
      throw new System.NotImplementedException();
    }

    public void Look()
    {
      Messages.Add(new Message(_game.CurrentRoom.Description));
    }

    public void Quit()
    {
      _playing = false;
    }
    ///<summary>
    ///Restarts the game 
    ///</summary>
    public void Reset()
    {
      throw new System.NotImplementedException();
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
        Messages.Add(new Message("No Items In This Room"));
      }
      Messages.Add(new Message($"You took the {_game.CurrentRoom.Items[0]}"));
      _game.CurrentPlayer.Inventory.Add(_game.CurrentRoom.Items[0]);
      _game.CurrentRoom.Items.Clear();
    }
    ///<summary>
    ///No need to Pass a room since Items can only be used in the CurrentRoom
    ///Make sure you validate the item is in the room or player inventory before
    ///being able to use the item
    ///</summary>
    public void UseItem(string itemName)
    {
      throw new System.NotImplementedException();
    }
    public void PrintMenu()
    {
      Messages.Add(new Message($"~~~~ Welcome To The Rehearsal Space {_game.CurrentPlayer.Name} ~~~\n"));
      Messages.Add(new Message(_game.CurrentRoom.Description));

    }
  }
}