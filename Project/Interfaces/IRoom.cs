using System.Collections.Generic;
using ConsoleAdventure.Project.Models;

namespace ConsoleAdventure.Project.Interfaces
{
  public interface IRoom
  {
    string Name { get; set; }
    string Description { get; set; }
    List<Item> Items { get; set; }
    Dictionary<string, IRoom> Exits { get; set; }
    bool IsTrap { get; set; }
    string BadOutcome { get; set; }
    string GoodOutcome { get; set; }
    Item RelevantItem { get; set; }
    bool LastRoom { get; set; }
  }
}
