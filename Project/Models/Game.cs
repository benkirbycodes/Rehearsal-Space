using System.Collections.Generic;
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
      Room StartingRoom = new Room("Starting Room", "You enter a bare concrete room. Every wall is covered in spray-painted pictures and words, a thousand reminders of a thousand bands long dead. A heavy steel door stands open, leading to a hallway beyond. A small table sits in a corner by the door.");
      Room Hallway = new Room("Hallway", "You enter a stark concrete hallway, sound echoing from each room and blending into a disorienting cacophony. Three doors open off the passage.");
      Room MetalRoom = new Room("Metal Room", "You enter a large room made to feel small and cramped by the towering monoliths of amplifier stacks lining every wall. The air is thick with the smell of cigarettes, old beer and unwashed leather. A group of long-haired dudes perpare to play their axe-shaped guitars.");
      Room SongwriterRoom = new Room("Songwriter Room", "You enter a medium-sized room, the walls adorned with Tibetan prayer flags and Buddhist mantra posters. The smell of the room is a unique blend of incense and desperation. A guy sits on a stool in the center of the room holding an acoustic guitar. Making eye-contact with you, he asks 'Hey, do you wanna be in my band'?");
      Room CoolJamsRoom = new Room("Cool Jams Room", "You enter a large room, the walls covered in foam and cloth to keep the high-frequencies from murdering your ears. Around the room is an assortment of rad guitars, keyboards and drums, almost as if somebody robbed the 1970's and left them laying about for your enjoyment. A group of people mill around aimlessly, seeming to consider what to do next. Upon seeing you enter the room, they look up and ask, 'What's up?'");

      StartingRoom.Exits.Add("south", Hallway);
      Hallway.Exits.Add("north", StartingRoom);
      Hallway.Exits.Add("west", MetalRoom);
      Hallway.Exits.Add("east", SongwriterRoom);
      Hallway.Exits.Add("south", CoolJamsRoom);
      MetalRoom.Exits.Add("east", Hallway);
      SongwriterRoom.Exits.Add("west", Hallway);
      CoolJamsRoom.Exits.Add("north", Hallway);

      StartingRoom.IsTrap = false;
      Hallway.IsTrap = false;
      MetalRoom.IsTrap = true;
      SongwriterRoom.IsTrap = true;
      CoolJamsRoom.IsTrap = true;

      Item Earplugs = new Item("Earplugs", "The soft foam variety that are hard to put in but better than going deaf.");
      Item SmokeBomb = new Item("Smoke Bomb", "Why would they have a smoke bomb? Who knows, but I hear they're great for distractions.");
      Item Beers = new Item("Beers", "Yes, beers.");
      Item HisDemo = new Item("His Demo", "What? No. No one wants that.");







      MetalRoom.RelevantItem = Earplugs;
      SongwriterRoom.RelevantItem = SmokeBomb;
      CoolJamsRoom.RelevantItem = Beers;

      // BadOutcomes.Add(MetalRoom, "You turn to leave but it is too late: A wall of noise hits you with the force of a thousand winds and you're knocked to the ground, your vision closing to black as blood trickles from your ears.\n GAME OVER");
      // GoodOutcomes.Add(MetalRoom, "You manage to shove the earplugs in your ears just in time! The wall of noise hits and you flinch, but your brain is not turned to mush!");
      // BadOutcomes.Add(SongwriterRoom, "You turn to leave, but it is too late: He has engaged you in a amazingly pointless conversation with the force of a futuristic Tractor Beam and you are unable to leave, or even speak. He's talking about his 'revolutionary perspective on music' and how all he wants is some 'sick collabs. You feel time begin to stretch, each second longer than the last, and soon you are sinking into a fully-formed singularity from which you will never escape.\n GAME OVER");
      // GoodOutcomes.Add(SongwriterRoom, "You pull out the smoke bomb and throw it at the ground in front of you! He's stunned and you slip out of the room behind a curtain of blessed smoke.");
      // BadOutcomes.Add(CoolJamsRoom, "You stand there awkwardly as they stare at you, wondering why you're in their room. Eventually, overcome with the uncomfortably questioning looks on their faces, you melt into a puddle of embarrassment. \n GAME OVER");
      // GoodOutcomes.Add(CoolJamsRoom, "You hold up the beers, offering them as a gift. Immediately understanding, they invite you in and together, you have an excellent evening of jamming.\n YOU WON!");

      MetalRoom.BadOutcome = "You turn to leave but it is too late: A wall of noise hits you with the force of a thousand winds and you're knocked to the ground, your vision closing to black as blood trickles from your ears.\n GAME OVER";
      MetalRoom.GoodOutcome = "You manage to shove the earplugs in your ears just in time! The wall of noise hits and you flinch, but your brain is not turned to mush!";
      SongwriterRoom.BadOutcome = "You turn to leave, but it is too late: He has engaged you in a amazingly pointless conversation with the force of a futuristic Tractor Beam and you are unable to leave, or even speak. He's talking about his 'revolutionary perspective on music' and how all he wants is some 'sick collabs. You feel time begin to stretch, each second longer than the last, and soon you are sinking into a fully-formed singularity from which you will never escape.\n GAME OVER";
      SongwriterRoom.GoodOutcome = "You pull out the smoke bomb and throw it at the ground in front of you! He's stunned and you slip out of the room behind a curtain of blessed smoke.";
      CoolJamsRoom.BadOutcome = "You stand there awkwardly as they stare at you, wondering why you're in their room. Eventually, overcome with the uncomfortably questioning looks on their faces, you melt into a puddle of embarrassment. \n GAME OVER";
      CoolJamsRoom.GoodOutcome = "You hold up the beers, offering them as a gift. Immediately understanding, they invite you in and together, you have an excellent evening of jamming.\n YOU WON!";




      StartingRoom.Items.Add(Earplugs);
      MetalRoom.Items.Add(SmokeBomb);
      SongwriterRoom.Items.Add(Beers);
      SongwriterRoom.Items.Add(HisDemo);

      CurrentRoom = StartingRoom;

    }
    public Game()
    {
      Setup();
    }
  }
}