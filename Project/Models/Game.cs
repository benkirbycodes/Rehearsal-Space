using ConsoleAdventure.Project.Interfaces;

namespace ConsoleAdventure.Project.Models
{
  public class Game : IGame
  {
    public IRoom CurrentRoom { get; set; }
    private bool _playing = true;
    public IPlayer CurrentPlayer { get; set; }

    //NOTE Make yo rooms here...
    public void Setup()
    {
      Room StartingRoom = new Room("Starting Room", "You're in a bare concrete room. Every wall is covered in spray-painted pictures and words, a thousand reminders of a thousand bands long dead. A heavy steel door stands open, leading to a hallway beyond. A small table sits in a corner by the door. ");
      Room Hallway = new Room("Hallway", "You stare down a stark concrete hallway, sound echoing from each room and blending into a disorienting cacophony. Three doors open off the passage.");
      Room MetalRoom = new Room("Metal Room", "You're in a large room made to feel small and cramped by the towering monoliths of amplifier stacks lining every wall. The air is thick with the smell of cigarettes, old beer and unwashed leather. A group of long-haired dudes perpare to play their axe-shaped guitars.");
      Room SongwriterRoom = new Room("Songwriter Room", "You're in a medium-sized room, the walls adorned with Tibetan prayer flags and Buddhist mantra posters. The smell of the room is a unique blend of incense and desperation. A guy sits on a stool in the center of the room holding an acoustic guitar. Making eye-contact with you, he asks 'Hey, do you wanna be in my band'?");
      Room CoolJamsRoom = new Room("Cool Jams Room", "You're in a large room, the walls covered in foam and cloth to keep the high-frequencies from murdering your ears. Around the room is an assortment of rad guitars, keyboards and drums, almost as if somebody robbed the 1970's and left them laying about for your enjoyment. A group of people mill around aimlessly, seeming to consider what to do next. Upon seeing you enter the room, they look up and ask, 'What's up?'");

      StartingRoom.Exits.Add("south", Hallway);
      Hallway.Exits.Add("north", StartingRoom);
      Hallway.Exits.Add("west", MetalRoom);
      Hallway.Exits.Add("east", SongwriterRoom);
      Hallway.Exits.Add("south", CoolJamsRoom);
      MetalRoom.Exits.Add("east", Hallway);
      SongwriterRoom.Exits.Add("west", Hallway);
      CoolJamsRoom.Exits.Add("north", Hallway);



      StartingRoom.Items.Add(new Item("Earplugs", "The soft foam variety that are hard to put but better than going deaf."));
      MetalRoom.Items.Add(new Item("Smoke Bomb", "Why would they have a smoke bomb? Who knows, Metal is the music of mysteries."));
      SongwriterRoom.Items.Add(new Item("Beers", "Yes, beers."));
      SongwriterRoom.Items.Add(new Item("His Demo", "What? No. No one wants that."));

      CurrentRoom = StartingRoom;

    }
    public Game()
    {
      Setup();
    }
  }
}