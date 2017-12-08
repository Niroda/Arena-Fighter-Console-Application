using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArenaFighter {
	class Program {
		/// <summary>
		/// The main entry point for the program.
		/// </summary>
		/// <param name="args">External parameters. Not used.</param>
		static void Main(string[] args) {
			// First, we get the name of the player's character
			Console.WriteLine("Enter the name of your character");
			string playerName = Console.ReadLine();

			// Using that name, we create the character object
			Character player = new Character(playerName, Character.infoGen.Next(1, 4) + 4, Character.infoGen.Next(1, 4) + 4);

			// Next, we create the randomly generated opponents that the player will fight, and store them in a list
			List<Character> Opponents = Character.GenerateCharacters(20);
			
			Console.Clear();

			/* Once we have the characters created, start the game loop.
			This will keep running until the player dies or retires. */
			bool isRunning = true;
			while(isRunning) {
				// Print the player's information and the options they can take.
				player.Print();

				Console.WriteLine("\nWhat do you want to do?");
				Console.WriteLine("H - Hunt for an opponent");
				Console.WriteLine("R - Retire from fighting");

				// When a key is pressed, either (H)unt for an opponent or (R)etire the player from fighting
				ConsoleKeyInfo cki = Console.ReadKey(true);
				switch(cki.Key) {
					case ConsoleKey.H:
						//if (H)unting...
						if(Opponents.Count > 0) {
							//Clear the screen, and print both characters' stats
							Console.Clear();
							Character opponent = Opponents[0];

							Console.WriteLine("\nPlayer:");
							player.Print();

							Console.WriteLine("\nOpponent:");
							opponent.Print();

							// Let the player initiate the fight by pressing a button
							Console.ReadKey(true);

							// Create the battle object...
							Battle battle = new Battle(player, opponent);

							//...and start the fight.
							battle.Fight();

							/*
							Once the fight is done, check who is dead.
							Either remove the opponent from the list of
							opponents, or end the game if the player died.
							*/
							if(opponent.IsDead) {
								Opponents.Remove(opponent);
							} else if (player.IsDead) {
								isRunning = false;
							}
						} else {
							// if there is no one left to fight, end the
							// game by forcing the player into retirement
							Console.WriteLine("There is no one left alive. You monster!");
							isRunning = false;
							Console.ReadKey(true);
						}
						break;
					case ConsoleKey.R:
						//if (R)etiring, just end the game
						Console.WriteLine("You have ended the violence by not fighting.");
						isRunning = false;
						Console.ReadKey(true);
						break;
					default:
						break;
				}
				Console.Clear();
			}
			// Finally, once the game has ended, show the score board.
			Console.WriteLine("Final Statistics:\n");
			player.Print(includeScore: true);

			Console.ReadKey();

            
		}
	}
}
