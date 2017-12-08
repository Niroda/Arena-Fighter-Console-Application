using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArenaFighter {
	/// <summary>
	/// This class represents a round in a battle - one "cycle" of the battle loop.
	/// </summary>
	public class Round {
		/// <summary>
		/// A random number generator to use with the round. This is static so that it is shared across all instances, since we don't need more than one random number generator
		/// </summary>
		static Random RandomNumberGenerator = new Random();

		/// <summary>
		/// Generates a random number between 1 and 6. since the upper bound of a random number span is never reached, we use 7 as max, but 6 is the actual maximium.
		/// </summary>
		public int RollDice {
			get { return RandomNumberGenerator.Next(1, 7); }
		}

		/// <summary>
		/// The object reference that points to the Player
		/// </summary>
		public Character Player { get; set; }
		/// <summary>
		/// The object reference that points to the Opponent
		/// </summary>
		public Character Opponent { get; set; }
		/// <summary>
		/// The winner of the round (either player or opponent)
		/// </summary>
		public Character Winner { get; set; }
		/// <summary>
		/// The loser of the round (either player or opponent)
		/// </summary>
		public Character Loser { get; set; }

		/// <summary>
		/// The dice roll the player made this round
		/// </summary>
		public int PlayerRoll { get; set; }
		/// <summary>
		/// The dice roll the player made this round
		/// </summary>
		public int OpponentRoll { get; set; }

		/// <summary>
		/// Is the round a draw between the two combatants?
		/// </summary>
		public bool IsDraw { get; set; }
		/// <summary>
		/// Did one of the combatants die?
		/// </summary>
		public bool IsFinal { get; set; }

		/// <summary>
		/// A single opposed roll between the two combatants in battle.
		/// </summary>
		/// <param name="player">A character object containing the player</param>
		/// <param name="opponent">A character object containing the opponent</param>
		/// <param name="rollDice">Whether dice should be rolled for this round or not</param>
		public Round(Character player, Character opponent, bool rollDice = true) {
			// start by setting the initial values
			this.Player = player;
			this.Opponent = opponent;
			this.IsDraw = false;
			this.IsFinal = false;

			// if dice should be rolled, do so. Otherwise, just set the dice rolls to 0
			this.PlayerRoll = (rollDice) ? RollDice : 0;
			this.OpponentRoll = (rollDice) ? RollDice : 0;

			if((player.Strength + PlayerRoll) > (opponent.Strength + OpponentRoll)) {
				// if the player is stronger, then the player wins.
				this.Winner = player;
				this.Loser = opponent;
			} else if((player.Strength + PlayerRoll) == (opponent.Strength + OpponentRoll)) {
				// if the characters are evenly matched, it's a draw.
				this.IsDraw = true;
			} else {
				// if the opponent is stronger, then the opponent wins.
				this.Winner = opponent;
				this.Loser = player;
			}

			// If the round isn't a draw, deal damage to the loser.
			if(!this.IsDraw) { this.Loser.Health -= this.Winner.Damage; }

			// If one of the combatants are dead after damage has been dealt, mark this as the final round.
			if(player.IsDead || opponent.IsDead) { this.IsFinal = true; }
		}

		/// <summary>
		/// Prints the data related to this round to the console.
		/// </summary>
		public void Print() {
			Console.WriteLine($"Rolls: {Player.Name} {Player.Strength + PlayerRoll} ({Player.Strength}+{PlayerRoll}) vs "+
				$"{Opponent.Name} {Opponent.Strength + OpponentRoll} ({Opponent.Strength}+{OpponentRoll})");
			if(this.IsDraw) {
				Console.WriteLine("Evenly matched, the combatants circle each other, looking for a better opportunity.");
			} else {
				Console.ForegroundColor = (Winner == Player) ? ConsoleColor.Green:ConsoleColor.Red;
				Console.WriteLine($"{Winner.Name} attacks {Loser.Name}! {Loser.Name} takes {Winner.Damage} damage{((Loser.IsDead)?", and falls to the ground, dead":"")}.");
				Console.ResetColor();
			}
			Console.WriteLine($"Remaining Health: {Player.Name} ({Player.HealthString}), {Opponent.Name} ({Opponent.HealthString})");
		}
	}
}