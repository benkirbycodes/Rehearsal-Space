using System;

namespace ConsoleAdventure.Project.Models
{
  public class Message
  {
    public string Body { get; set; }
    public void Print()
    {
      Console.WriteLine(Body);
    }
    public Message(string body)
    {
      Body = body;
    }
  }
}