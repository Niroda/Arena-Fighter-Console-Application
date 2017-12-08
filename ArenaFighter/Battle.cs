using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArenaFighter {
	/// <summary>
	/// A representation of a complete fight between two characters, from start to finish.
	/// </summary>
	public class Battle {
		/// <summary>
		/// The player's character
		/// </summary>
		public Character Player { get; set; }
		/// <summary>
		/// The opponent's character
		/// </summary>
		public Character Opponent { get; set; }

		/// <summary>
		/// The last battle round that was done
		/// </summary>
		public Round LastRound { get; set; }
		/// <summary>
		/// All rounds that have been done already
		/// </summary>
		public List<Round> Rounds { get; set; }
		/// <summary>
		/// Is this battle completely done?
		/// </summary>
		public bool IsFinished { get; private set; }

		/// <summary>
		/// Main constructor for the battle. 
		/// </summary>
		/// <param name="player">the player's character</param>
		/// <param name="opponent">the opponent's character</param>
		public Battle(Character player, Character opponent) {
			this.Player = player;
			player.Battles.Add(this);
			this.Opponent = opponent;
			this.IsFinished = false;
		}

		/// <summary>
		/// Run the entire fight, from start to finish.
		/// </summary>
		/// <param name="inputRequired">Should the fight wait for player inputs before moving on?</param>
		public void Fight(bool inputRequired = true) {
			do {
				//run the code for a new round
				FightRound();

				//if player input is required, wait here until player responds
				if(inputRequired) { Console.ReadKey(); }
			} while(!LastRound.IsFinal); // repeat until the battle is done

			// Mark the battle as being finished.
			this.IsFinished = true;
			// and print the name of the winner to the console.
			Console.WriteLine("\n--------------");
			Console.WriteLine($"{this.LastRound.Winner.Name} is victorious!");

			//if player input is required, wait here until player responds
			if(inputRequired) { Console.ReadKey(); }
		}

		/// <summary>
		/// Runs a single battle round.
		/// </summary>
		/// <param name="rollDice">Whether dice should be rolled for this round</param>
		public void FightRound(bool rollDice = true) {
			// Create the round, roll the dice etc...
			this.LastRound = new Round(Player, Opponent, rollDice);

			// print the round's data to the console.
			Console.WriteLine("\n--------------");
			this.LastRound.Print();
		}

		/// <summary>
		/// Print this battle's log out, by printing every round to the console.
		/// </summary>
		public void Print() {
			foreach(var item in Rounds) {
				Console.WriteLine("\n--------------");
				item.Print();
			}
			Console.WriteLine("\n--------------");
			Console.WriteLine($"{this.LastRound.Winner.Name} is victorious!");
		}
	}
}
