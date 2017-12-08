using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lexicon.CSharp.InfoGenerator;

namespace ArenaFighter {
	/// <summary>
	/// Represents the characters that are being used by the player and all the opponents.
	/// </summary>
	public class Character {
		/// <summary>
		/// Info generator, included in the InfoGenerator.dll
		/// </summary>
		static public InfoGenerator infoGen = new InfoGenerator(DateTime.Now.Millisecond);

		/// <summary>
		/// (field) the character's name
		/// </summary>
		private string name;
		/// <summary>
		/// (property) the character's name
		/// </summary>
		public string Name {
			get {
				return name;
			}
		}

		/// <summary>
		/// The strength value of the character
		/// </summary>
		public int Strength { get; set; }
		/// <summary>
		/// The health value of the character
		/// </summary>
		public int Health { get; set; }
		/// <summary>
		/// Health as a string, for display purposes
		/// </summary>
		public string HealthString
		{
			get {
				if(Health > 0) {
					return Health.ToString();
				} else {
					return "Dead";
				}
			}
		}

		/// <summary>
		/// A list of the battles this character has fought
		/// </summary>
		public List<Battle> Battles { get; set; }

		/// <summary>
		/// The damage value of this character. Strength divided by 2, rounded down.
		/// </summary>
		public int Damage
		{
			get { return Strength / 2; }
		}

		/// <summary>
		/// Is this character dead?
		/// </summary>
		public bool IsDead
		{
			get
			{
				if(Health > 0) {
					return false;
				} else {
					return true;
				}
			}
		}

		/// <summary>
		/// Creates a character with default values
		/// </summary>
		public Character() : this("Tester", 5, 5) { }
		/// <summary>
		/// Creates a character with default values, and a custom name
		/// </summary>
		/// <param name="name">the name of the character</param>
		public Character(string name) : this(name, 5, 5) {}
		/// <summary>
		/// Creates a character with specific values
		/// </summary>
		/// <param name="name">the name of the character</param>
		/// <param name="strength">the character's strength</param>
		/// <param name="health">the character's health</param>
		public Character(string name, int strength, int health) {
			this.name = name;
			this.Strength = strength;
			this.Health = health;
			this.Battles = new List<Battle>();
		}

		/// <summary>
		/// Creates a number of randomly created characters
		/// </summary>
		/// <param name="count">The amount of characters to be created</param>
		/// <returns>A list object containing all the generated characters</returns>
		public static List<Character> GenerateCharacters(int count) {
			List<Character> characterList = new List<Character>();
			for(int i = 0; i < count; i++) {
				string name = infoGen.NextFirstName();
				name = name.Substring(0, 1).ToUpper() + name.Substring(1);
				characterList.Add(new Character(name, infoGen.Next(1, 8) + 2, infoGen.Next(1, 8) + 2));
			}
			return characterList;
		}

		/// <summary>
		/// Prints the scoreboard for this character, showing what the character has done so far
		/// </summary>
		public void ShowScore() {
			int score = 0;
			foreach(var item in Battles) {
				if(item.LastRound.Winner == this) {
					score += 5;
					Console.WriteLine($"{this.Name} fought and killed {item.LastRound.Opponent.Name}.");
				} else {
					score += 2;
					Console.WriteLine($"{this.Name} was killed by {item.LastRound.Opponent.Name}.");
				}
			}
			Console.WriteLine($"{this.Name} total score is {score}.");
		}

		/// <summary>
		/// Prints the character's information.
		/// </summary>
		public void Print(bool includeScore = false) {
			Console.WriteLine($"Name: {Name}");
			Console.WriteLine($"Strength: {Strength}");
			Console.WriteLine($"Damage: {Damage}");
			Console.WriteLine($"Health: {HealthString}");

			// if battle stats should be included, show them
			if(includeScore) {
				this.ShowScore();
			}
		}

	}
}
