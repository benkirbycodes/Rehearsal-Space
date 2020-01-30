using System.Collections.Generic;
using ConsoleAdventure.Project.Interfaces;

namespace ConsoleAdventure.Project.Models
{
  public class Room : IRoom
  {
    public string Name { get; set; }
    public string Description { get; set; }
    public List<Item> Items { get; set; } = new List<Item>();
    public Dictionary<string, IRoom> Exits { get; set; } = new Dictionary<string, IRoom>();

    public Room(string name, string desc)
    {
      Name = name;
      Description = desc;
    }
  }
}