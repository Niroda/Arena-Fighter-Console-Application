using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ArenaFighter.Test {
	[TestClass]
	public class CharacterTests {
		/// <summary>
		/// Ensure that the character is created when the constructor is called,
		/// and that the values are assigned to it properly
		/// </summary>
		[TestMethod]
		public void TestCreateCharacter() {
			Character pc = new Character("Name", 3, 6);
			Assert.IsNotNull(pc);
			Assert.IsTrue(pc.Name == "Name");
			Assert.IsTrue(pc.Strength == 3);
			Assert.IsTrue(pc.Health == 6);
		}

		/// <summary>
		/// Ensure that the strength and health values can be updated,
		/// and that the name cannot be chanced after the character has 
		/// been created.
		/// </summary>
		[TestMethod]
		public void TestEditCharacter() {
			Character pc = new Character("Name", 3, 6);
			Assert.IsNotNull(pc);

			// Check if health can be changed (should succeed)
			pc.Health -= 1;
			Assert.IsTrue(pc.Health == 5);

			// Check if strength can be changed (should succeed)
			pc.Strength += 1;
			Assert.IsTrue(pc.Strength == 4);
		}
	}

	[TestClass]
	public class BattleTests {
		/// <summary>
		/// Ensure that battles can be created with working object references
		/// </summary>
		[TestMethod]
		public void TestBattleIsValid() {
			Character pc = new Character();
			Character npc = new Character();
			Battle battle = new Battle(pc, npc);


			Assert.IsNotNull(battle);
			Assert.AreEqual(pc, battle.Player);
			Assert.AreEqual(npc, battle.Opponent);
		}

		/// <summary>
		/// Ensure that the dice roll results in an int value between 1 and 6
		/// </summary>
		[TestMethod]
		public void TestRollIsValid() {
			Character pc = new Character();
			Character npc = new Character();
			Battle battle = new Battle(pc, npc);
			battle.FightRound();
			Assert.IsTrue(battle.LastRound.PlayerRoll <= 6 && battle.LastRound.PlayerRoll >= 1);
			Assert.IsTrue(battle.LastRound.OpponentRoll <= 6 && battle.LastRound.OpponentRoll >= 1);
		}

		/// <summary>
		/// Ensure that when the winner is assigned, it chooses the right character
		/// </summary>
		[TestMethod]
		public void TestWinnerIsValid() {
			Character strongPC = new Character("strongPC", 10, 10);
			Character strongNPC = new Character("strongNPC", 10, 10);
			Character weakPC = new Character("weakPC", 1, 1);
			Character weakNPC = new Character("weakNPC", 1, 1);

			Battle playerWins = new Battle(strongPC, weakNPC);
			playerWins.FightRound();
			Assert.AreEqual(strongPC, playerWins.LastRound.Winner);

			Battle playerLoses = new Battle(weakPC, strongNPC);
			playerLoses.FightRound();
			Assert.AreEqual(strongNPC, playerLoses.LastRound.Winner);
		}

		/// <summary>
		/// Ensure that damage is applied when a character wins a round
		/// </summary>
		[TestMethod]
		public void TestDamageIsApplied() {
			Character PC = new Character("player", 10, 10);
			Character NPC = new Character("opponent", 1, 10);

			Battle battle = new Battle(PC, NPC);
			battle.FightRound();
			Assert.IsTrue(NPC.Health < 10);
		}

		/// <summary>
		/// Ensure that the damage amount is correct
		/// </summary>
		[TestMethod]
		public void TestDamageIsCorrect() {
			Character PC = new Character("player", 10, 10);
			Character NPC = new Character("opponent", 1, 10);

			Battle battle = new Battle(PC, NPC);
			battle.FightRound();
			Assert.AreEqual(10 - PC.Damage, NPC.Health);
		}

		/// <summary>
		/// Ensure that the game recognizes a draw
		/// </summary>
		[TestMethod]
		public void TestDraw() {
			Character PC = new Character(health: 10, strength: 10, name: "player");
			Character NPC = new Character("opponent", 10, 10);

			Battle battle = new Battle(PC, NPC);
			battle.FightRound(rollDice: false);
			Assert.IsTrue(battle.LastRound.IsDraw);
		}

		/// <summary>
		/// Ensure that characters that go below 1 health dies
		/// </summary>
		[TestMethod]
		public void TestDeath() {
			Character PC = new Character("player", 10, 10);
			Character NPC = new Character("opponent", 1, 1);
			Battle battle = new Battle(PC, NPC);
			battle.FightRound(false);
			Assert.IsTrue(NPC.IsDead);
		}

		/// <summary>
		/// ensure that death ends the battle
		/// </summary>
		[TestMethod]
		public void TestDeathEndsBattle() {
			Character PC = new Character("player", 10, 10);
			Character NPC = new Character("opponent", 1, 1);
			Battle battle = new Battle(PC, NPC);
			battle.Fight(false);
			Assert.IsTrue(battle.LastRound.IsFinal);
			Assert.IsTrue(battle.IsFinished);
		}
	}
}
