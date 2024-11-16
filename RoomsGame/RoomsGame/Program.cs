using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text.Json.Serialization;

namespace RoomsGame
{
    internal class Program
    {
        //global variables
        public static bool hasHorse = false;
        public static bool knowKingsSecret = false;
        public static bool hasBackpack = false;
        public static bool hasMap = false;
        //venal means ready to betray someone for money
        public static bool knowVenalGuards = false;

        public static string[,] rooms =
        {
            { "                   ", "Armorer    ", "   City hall", "\n\n"}, 
            { "Weapons shop  ", " Market district  ", " City square  ", " Castle"},
            { "\n\n                    ", "Stables  ", "    Entry area", ""}
        };
        public static string activeRoom = "Entry area";

        //list of directions for each room
        public static List<string> startOptions = new List<string> { "Start Game", "Back to Introduction", "Exit" };
        public static List<string> entryAreaDirections = new List<string> { "City square" };
        public static List<string> citySquareDirections = new List<string> { "Entry area", "Market district", "Castle", "City hall" };
        public static List<string> castleDirections = new List<string> { "City square" };
        public static List<string> cityHallDirections = new List<string> { "City square" };
        public static List<string> marketDistrictDirections = new List<string> { "City square", "Armorer", "Weapons shop", "Stables" };
        public static List<string> armorerDirections = new List<string> { "Market district" };
        public static List<string> weaponsShopDirections = new List<string> { "Market district" };
        public static List<string> stablesDirections = new List<string> { "Market district" };

        //lists of actions for each room
        public static List<string> entryAreaActions = new List<string> { "Buy a handmade map from a stranger for 3 coins", "Buy a certified map for 7 coins", "Change room" };
        public static List<string> castleActions = new List<string> { "Try to negotiate with the king’s guards to betray him", "Enter a direct battle with the king’s guard", "Change room" };
        public static List<string> citySquareActions = new List<string> { "Try to steal an unattended horse", "Try to steal a backpack left on a nearby bench", "Donate 3 coins to a beggar", "Change room" };
        public static List<string> marketDistrictActions = new List<string> { "Try to sell the stones you found in the backpack", "Buy an energy booster potion for 10 coins", "Change room" };
        public static List<string> weaponsShopActions = new List<string> { "Buy a sword for 15 coins", "Try to sell the old knife you found in the backpack", "Change room" };
        public static List<string> stablesActions = new List<string> { "Buy food for the horse for 3 coins", "Join a battle contest for the grand prize of 100 coins", "Change the horse’s horseshoes for 5 coins", "Change room" };
        public static List<string> armorerActions = new List<string> { "Buy armor for the horse for 7 coins", "Buy a high-quality silver armor for 25 coins", "Buy an old, used armor for 10 coins", "Change room" };
        public static List<string> cityHallActions = new List<string> { "Send a present to the mayor worth 10 coins", "Tell the king’s secret to the mayor", "Change room" };


        //player characteristics
        public static int coins = 50;
        public static int health = 50;
        public static int strength = 50;
        public static int knowledge = 50;

        static void Main(string[] args)
        { 
            IntroduceTheGame();
        }

        static void RunMenu(List<string> options)
        {
            int selectedIndex = 0;

            bool running = true;

            while (running)
            {
                Console.Clear();
                ShowPlayerCharacteristics();
                Console.WriteLine("\n");
                if (hasMap)
                {
                    ShowMap();
                    Console.WriteLine("\n\n\n\n");
                }
                
                PrintMenu(options, selectedIndex);

                ConsoleKeyInfo keyInfo = Console.ReadKey();
                switch (keyInfo.Key)
                {
                    //moving up
                    case ConsoleKey.UpArrow:
                        if (selectedIndex == 0)
                        {
                            selectedIndex = options.Count - 1;
                        }
                        else
                        {
                            selectedIndex--;
                        }
                        break;

                    //moving down
                    case ConsoleKey.DownArrow:
                        if (selectedIndex == options.Count - 1)
                        {
                            selectedIndex = 0;
                        }
                        else
                        {
                            selectedIndex++;
                        }
                        break;

                    //if the option is selected
                    case ConsoleKey.Enter:
                        if (options[selectedIndex] == "Change room")
                        {
                            return;
                        }
                        HandleSelection(options[selectedIndex]);
                        break;
                }
            }
        }

        static void PrintMenu(List<string> options, int selectedIndex)
        {
            for (int i = 0; i < options.Count; i++)
            {
                if (i == selectedIndex)
                {
                    // Highlight the selected option
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"> {options[i]}");
                }
                else
                {
                    Console.ResetColor();
                    Console.WriteLine($"  {options[i]}");
                }
            }
            // Reset color back to the default
            Console.ResetColor();
        }

        static void HandleSelection(string selectedOption)
        {
            Console.Clear();
            if (selectedOption == "Change room") return;
            switch (selectedOption)
            {
                //Start menu options
                case "Exit":
                    Environment.Exit(0);
                    break;
                case "Start Game":
                    EntryArea();
                    break;
                case "Back to Introduction":
                    IntroduceTheGame();
                    break;
                //Direction options
                case "Entry area":
                    EntryArea();
                    break;
                case "City square":
                    CitySquare();
                    break;
                case "Castle":
                    Castle();
                    break;
                case "Market district":
                    MarketDistrict();
                    break;
                case "Weapons shop":
                    WeaponsShop();
                    break;
                case "Stables":
                    Stables();
                    break;
                case "Armorer":
                    Armorer();
                    break;
                case "City hall":
                    CityHall();
                    break; 
                //Entry area actions
                case "Buy a handmade map from a stranger for 3 coins":
                    Console.WriteLine("You cautiously hand 3 precious coins to a shifty-eyed stranger, only to find that the map's lines twist and blur as if inked by a trickster's hand. The parchment crumbles in your grasp, leaving you poorer and wiser for the deceit. You lost 3 coins.");
                    coins -= 3;
                    entryAreaActions.Remove("Buy a handmade map from a stranger for 3 coins");
                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey();
                    break;
                case "Buy a certified map for 7 coins":
                    Console.WriteLine("You part with 7 hard-earned coins for a map bearing the seal of the royal cartographer. Its detailed routes and landmarks become an invaluable ally, guiding your journey through unfamiliar paths. You feel a surge of confidence as newfound understanding seeps into your mind. You lost 7 coins and earned 15 knowledge.");
                    hasMap = true;
                    coins -= 7;
                    knowledge += 15;
                    entryAreaActions.Remove("Buy a certified map for 7 coins");
                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey();
                    break;
                case "Donate 3 coins to a beggar":
                    Console.WriteLine("The beggar’s eyes gleam with gratitude as he clasps your hands and leans in, voice lowered to a whisper. He shares a rumor about the king's hidden dealings, a revelation that could shift the tides of power. You walk away with a mind brimming with this vital secret. You lost 3 coins but gained 10 knowledge.");
                    coins -= 3;
                    knowledge += 10;
                    knowKingsSecret = true;
                    citySquareActions.Remove("Donate 3 coins to a beggar");
                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey();
                    break;
                //City square actions
                case "Try to steal an unattended horse":
                    Console.WriteLine("Your heart hammers as you approach the noble steed, eyes darting to catch any witness. The horse whinnies softly, sensing your nerves, but you press on, driven by sheer resolve. With a swift leap, you mount and grip the reins, feeling the surge of power beneath you as you gallop into the unknown. Your daring pays off; the thrill of triumph hardens your spirit. You gained 10 strength.");
                    strength += 10;
                    hasHorse = true;
                    citySquareActions.Remove("Try to steal an unattended horse");
                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey();
                    break;
                case "Try to steal a backpack left on a nearby bench":
                    Console.WriteLine("The act is risky, and your heart pounds as you take the leap. Miraculously, you succeed, securing the backpack and vanishing into the crowd. Inside, you find somee stones, an old knife, and a small pouch brimming with coins. You gained 15 coins and 5 strength.");
                    hasBackpack = true;
                    strength += 5;
                    coins += 15;
                    citySquareActions.Remove("Try to steal a backpack left on a nearby bench");
                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey();
                    break;
                //Market district actions
                case "Try to sell the stones you found in the backpack":
                    if (!hasBackpack)
                    {
                        Console.WriteLine("Seems like you do not have a backpack.");
                        Console.WriteLine("\nPress any key to continue...");
                        Console.ReadKey();
                        break;
                    }
                    Console.WriteLine("The merchant narrows his eyes, studying the stones intently. Most are worthless, but his expression shifts when he finds one of rare quality. You leave the market with a heavier purse and a satisfied grin. You gained 30 coins.");
                    coins += 30;
                    marketDistrictActions.Remove("Try to sell the stones you found in the backpack");
                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey();
                    break;
                case "Buy an energy booster potion for 10 coins":
                    Console.WriteLine("You sip the ancient elixir, its taste a blend of bitterness and power. It invigorates your muscles and bolsters your spirit. You lost 10 coins but gained 10 strength and 15 health.");
                    coins -= 10;
                    strength += 10;
                    health += 15;
                    marketDistrictActions.Remove("Buy an energy booster potion for 10 coins");
                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey();
                    break;
                //Stables actions
                case "Buy food for the horse for 3 coins":
                    if (!hasHorse)
                    {
                        Console.WriteLine("Seems like you do not have a horse.");
                        Console.WriteLine("\nPress any key to continue...");
                        Console.ReadKey();
                        break;
                    }
                    Console.WriteLine("Your steed’s eyes brighten as it devours the meal, its strength visibly renewed. You sense it could now carry you to the very ends of the realm. You lost 3 coins and gained 5 strength.");
                    coins -= 3;
                    strength += 5;
                    stablesActions.Remove("Buy food for the horse for 3 coins");
                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey();
                    break;
                case "Change the horse’s horseshoes for 5 coins":
                    if (!hasHorse)
                    {
                        Console.WriteLine("Seems like you do not have a horse.");
                        Console.WriteLine("\nPress any key to continue...");
                        Console.ReadKey();
                        break;
                    }
                    Console.WriteLine("The blacksmith inspects the old horseshoes, his brow furrowing when he finds the royal insignia etched within. “This horse belongs to the king,” he mutters. With a sharp gaze, he offers a choice: 10 coins for his silence or your steed will be seized. The shadow of danger looms.");
                    Console.WriteLine("Press Enter to pay 10 coins or any other key to continue without paying...");
                    ConsoleKeyInfo keyInfo = Console.ReadKey();
                    if(keyInfo.Key == ConsoleKey.Enter)
                    {
                        coins -= 10;
                    }
                    else
                    {
                        strength -= 10;
                        hasHorse = false;
                    }
                    stablesActions.Remove("Change the horse’s horseshoes for 5 coins");
                    break;
                case "Join a battle contest for the grand prize of 100 coins":
                    if(strength >= 85 && health >= 90)
                    {
                        Console.WriteLine("Fueled by sheer will, you conquer opponent after opponent. The final battle tests your limits, but victory crowns your determination. You are declared the champion and claim the prize. You gained 100 coins.");
                        coins += 100;
                    }
                    else if(strength >= 70 && health >= 65)
                    {
                        Console.WriteLine("You fight valiantly, advancing through the ranks. But the arena is fierce, and you fall short, finishing fourth. Exhausted and wounded, you limp away with valuable combat lessons. You lost 15 health and gained 5 knowledge.");
                        health -= 15;
                        knowledge += 5;
                    }
                    else
                    {
                        Console.WriteLine("Despite you strong will, in the first clash, your lack of preparation becomes evident. A brutal strike finds its mark, and darkness envelops you.");
                        Lose();
                    }
                    stablesActions.Remove("Join a battle contest for the grand prize of 100 coins");
                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey();
                    break;
                //Weapons shop actions
                case "Try to sell the old knife you found in the backpack":
                    if (!hasBackpack)
                    {
                        Console.WriteLine("Seems like you do not have a backpack.");
                        Console.WriteLine("\nPress any key to continue...");
                        Console.ReadKey();
                        break;
                    }
                    Console.WriteLine("The weapon keeper’s eyes widen as he recognizes an engraved name. His voice trembles. “This belonged to a dear friend,” he whispers before calling for guards. You pay a hefty fine and return the knife, feeling the weight of regret. You lost 10 coins and 5 strength.");
                    strength -= 5;
                    coins -= 10;
                    weaponsShopActions.Remove("Try to sell the old knife you found in the backpack");
                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey();
                    break;
                case "Buy a sword for 15 coins":
                    Console.WriteLine("The weapon gleams as it passes into your hands, a relic that has tasted battle many times. You sense the strength of its history imbue your grip. You lost 15 coins and gained 20 strength.");
                    strength += 20;
                    coins -= 15;
                    weaponsShopActions.Remove("Buy a sword for 15 coins");
                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey();
                    break;
                //Armorer actions
                case "Buy an old, used armor for 10 coins":
                    Console.WriteLine("The armor is worn and battered, but its sturdy plates offer a layer of defense. With each buckle fastened, you feel a touch more secure. You lost 10 coins and gained 10 health.");
                    health += 10;
                    coins -= 10;
                    armorerActions.Remove("Buy an old, used armor for 10 coins");
                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey();
                    break;
                case "Buy a high-quality silver armor for 25 coins":
                    Console.WriteLine("The gleaming silver plates fit perfectly, radiating resilience. You know this will shield you against even the fiercest foes. You lost 25 coins and gained 40 health.");
                    health += 40;
                    coins -= 25;
                    armorerActions.Remove("Buy a high-quality silver armor for 25 coins");
                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey();
                    break;
                case "Buy armor for the horse for 7 coins":
                    if (!hasHorse)
                    {
                        Console.WriteLine("Seems like you do not have a horse.");
                        Console.WriteLine("\nPress any key to continue...");
                        Console.ReadKey();
                        break;
                    }
                    Console.WriteLine("The fitted armor grants your horse an imposing presence. With newfound vigor, it stands ready for any battle ahead. You lost 7 coins and gained 10 strength.\r\n");
                    strength += 10;
                    coins -= 7;
                    armorerActions.Remove("Buy armor for the horse for 7 coins");
                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey();
                    break;
                //City hall actions
                case "Send a present to the mayor worth 10 coins":
                    Console.WriteLine("The gift elicits a warm smile, and he invites you for tea. As he speaks of the king’s affairs, a subtle hint slips—a clue that most of the king’s guards are gamblers, easily swayed by coin. Your path forward becomes clearer. You lost 10 coins and gained 10 knowledge.");
                    knowledge += 10;
                    coins -= 10;
                    knowVenalGuards = true;
                    cityHallActions.Remove("Send a present to the mayor worth 10 coins");
                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey();
                    break;
                case "Tell the king’s secret to the mayor":
                    if (!knowKingsSecret)
                    {
                        Console.WriteLine("Seems like you do not know any secrets about the king");
                        Console.WriteLine("\nPress any key to continue...");
                        Console.ReadKey();
                        break;
                    }
                    Console.WriteLine("You speak, expecting alliance, but the mayor’s expression darkens. The room swarms with guards at his command. “Betrayal has a price,” he says coldly. You are sentenced to death.");
                    Lose();
                    break;
                //Castle actions
                case "Enter a direct battle with the king’s guard":
                    Console.WriteLine("You draw your weapon, hope burning within. But the clash is brief and brutal. Their skill is unmatched, and soon, your vision fades to black. You are slain.");
                    Lose();
                    break;
                case "Try to negotiate with the king’s guards to betray him":
                    if (knowVenalGuards)
                    {
                        if(coins >= 100)
                        {
                            Console.WriteLine("Your research pays off—you find the guards who would betray the king for the right price. For 100 coins, they promise to stand aside while you move against him.");
                            Win();
                        }
                        else
                        {
                            Console.WriteLine("Your research pays off—you find the guards who would betray the king for the right price. Their eyes gleam with greed as they name their fee, but when you reach for your purse, it’s too light. Disappointment flashes across their faces before it hardens into something more dangerous. “Not enough,” one hisses. They seize you and haul you before the king, who, with a voice cold as steel, pronounces your fate.");
                            Lose();
                        }
                    }
                    else
                    {
                        Console.WriteLine("You misjudge the loyalty of these guards. Steel flashes before you can speak another word.");
                        Lose();
                    }
                    break;
            }
        }

        static void IntroduceTheGame()
        {
            printGameName();
            Console.WriteLine("\nControls:\n");
            Console.WriteLine("-> Use the arrow keys (Up & Down) to navigate through the options.");
            Console.WriteLine("-> Press Enter to make your selection.");

            Console.WriteLine("\nRules & Objectives:\n");
            Console.WriteLine("-> Your goal is simple yet perilous: defeat the King and claim the throne to become the new ruler of the kingdom.");
            Console.WriteLine("-> You start the quest with 50 of each characteristics. Keep their value high, as they are essential for your survival and success.");
            Console.WriteLine("-> Every choice you make has consequences—some may lead to fortune and glory, while others could result in disaster or death.");
            Console.WriteLine("-> Be strategic and mindful in every step you take. The fate of your future ruler's crown depends on it!");

            Console.WriteLine("\nMay luck be on your side!\n");

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();

            RunMenu(startOptions);
        }

        static void printGameName()
        {
            Console.Clear();
            string[] gameName = {
                "██╗  ██╗██╗███╗   ██╗ ██████╗ ███████╗     ██████╗  █████╗ ███╗   ███╗██████╗ ██╗████████╗",
                "██║ ██╔╝██║████╗  ██║██╔════╝ ██╔════╝    ██╔════╝ ██╔══██╗████╗ ████║██╔══██╗██║╚══██╔══╝",
                "█████╔╝ ██║██╔██╗ ██║██║  ███╗███████╗    ██║  ███╗███████║██╔████╔██║██████╔╝██║   ██║   ",
                "██╔═██╗ ██║██║╚██╗██║██║   ██║╚════██║    ██║   ██║██╔══██║██║╚██╔╝██║██╔══██╗██║   ██║   ",
                "██║  ██╗██║██║ ╚████║╚██████╔╝███████║    ╚██████╔╝██║  ██║██║ ╚═╝ ██║██████╔╝██║   ██║   ",
                "╚═╝  ╚═╝╚═╝╚═╝  ╚═══╝ ╚═════╝ ╚══════╝     ╚═════╝ ╚═╝  ╚═╝╚═╝     ╚═╝╚═════╝ ╚═╝   ╚═╝   "
            };

            int consoleWidth = Console.WindowWidth;

            foreach (var line in gameName)
            {
                int padding = (consoleWidth - line.Length) / 2;
                string paddedLine = line.PadLeft(padding + line.Length);
                Console.WriteLine(paddedLine);
            }
        }

        static void EntryArea()
        {
            activeRoom = "Entry area";

            RunMenu(entryAreaActions);

            RunMenu(entryAreaDirections);
        }

        static void Castle()
        {
            activeRoom = "Castle";

            RunMenu(castleActions);

            RunMenu(castleDirections);
        }

        static void CitySquare()
        {
            activeRoom = "City square";

            RunMenu(citySquareActions);

            RunMenu(citySquareDirections);
        }

        static void MarketDistrict()
        {
            activeRoom = "Market district";

            RunMenu(marketDistrictActions);

            RunMenu(marketDistrictDirections);
        }

        static void WeaponsShop()
        {
            activeRoom = "Weapons shop";

            RunMenu(weaponsShopActions);

            RunMenu(weaponsShopDirections);
        }

        static void Stables()
        {
            activeRoom = "Stables";

            RunMenu(stablesActions);

            RunMenu(stablesDirections);
        }

        static void Armorer()
        {
            activeRoom = "Armorer";

            RunMenu(armorerActions);

            RunMenu(armorerDirections);
        }

        static void CityHall()
        {
            activeRoom = "City hall";

            RunMenu(cityHallActions);

            RunMenu(cityHallDirections);
        }

        static void Win()
        {
            string[] winMessage = {
                "██╗   ██╗ ██████╗ ██╗   ██╗    ██╗    ██╗██╗███╗   ██╗",
                "╚██╗ ██╔╝██╔═══██╗██║   ██║    ██║    ██║██║████╗  ██║",
                " ╚████╔╝ ██║   ██║██║   ██║    ██║ █╗ ██║██║██╔██╗ ██║",
                "  ╚██╔╝  ██║   ██║██║   ██║    ██║███╗██║██║██║╚██╗██║",
                "   ██║   ╚██████╔╝╚██████╔╝    ╚███╔███╔╝██║██║ ╚████║",
                "   ╚═╝    ╚═════╝  ╚═════╝      ╚══╝╚══╝ ╚═╝╚═╝  ╚═══╝"
            };

            int consoleWidth = Console.WindowWidth;

            Console.Write("\n\n\n\n");
            foreach (var line in winMessage)
            {
                int padding = (consoleWidth - line.Length) / 2;
                string paddedLine = line.PadLeft(padding + line.Length);
                Console.WriteLine(paddedLine);
            }

            Console.WriteLine("\n\n\n\n\nPress any key to continue...");
            Console.ReadKey();
            Environment.Exit(0);
        }
        static void Lose()
        {

            string[] loseMessage = {
                "██╗   ██╗ ██████╗ ██╗   ██╗    ██╗      ██████╗ ███████╗███████╗",
                "╚██╗ ██╔╝██╔═══██╗██║   ██║    ██║     ██╔═══██╗██╔════╝██╔════╝",
                " ╚████╔╝ ██║   ██║██║   ██║    ██║     ██║   ██║███████╗█████╗  ",
                "  ╚██╔╝  ██║   ██║██║   ██║    ██║     ██║   ██║╚════██║██╔══╝  ",
                "   ██║   ╚██████╔╝╚██████╔╝    ███████╗╚██████╔╝███████║███████╗",
                "   ╚═╝    ╚═════╝  ╚═════╝     ╚══════╝ ╚═════╝ ╚══════╝╚══════╝"
            };

            int consoleWidth = Console.WindowWidth;

            Console.Write("\n\n\n\n");
            foreach (var line in loseMessage)
            {
                int padding = (consoleWidth - line.Length) / 2;
                string paddedLine = line.PadLeft(padding + line.Length);
                Console.WriteLine(paddedLine);
            }

            Console.WriteLine("\n\n\n\n\nPress any key to continue...");
            Console.ReadKey();
            Environment.Exit(0);
        }
        static void ShowPlayerCharacteristics()
        {
            Console.WriteLine("coins: {0}   health: {1}   strength: {2}   knowledge: {3}", coins, health, strength, knowledge);
        }
        static void ShowMap()
        {
            foreach(string room in rooms)
            {
                if(room.Contains(activeRoom))
                {
                    // Highlight the active room
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write(room);
                    Console.ResetColor();
                    continue;
                }
                Console.Write(room);
            }
        }
    }
}
