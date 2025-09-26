# RPG
Simple RPG game.
## Technologies Used
- C#
- SQLite
- Entity Framework
- N-tier architecture style
  
## Description

We want to create a console application RPG game with heroes and monsters

- The game board is 10x10 in “▒”
- The goal of the game is to kill the most monsters
- One game session continues until your hero dies or you exit the game
- At the start of the game, a hero is chosen from “Mage”, “Warrior”, or “Archer”.

### Characters

- Mage: Strength = 2; Agility = 1; Intelligence = 3; Range = 3; symbol = “*”
- Warrior: Strength = 3; Agility = 3; Intelligence = 0; Range = 1; symbol = “@”
- Archer: Strength = 2; Agility = 4; Intelligence = 0; Range = 2; symbol = “#”
- Monster:
    - Strength, Agility, and Intelligence are random between 1 and 3
    - Range = 1
    - symbol = “◙”

### Character “Setup” function

```csharp
{
 this.Health = this.Strenght * 5;
 this.Mana = this.Intelligence * 3;
 this.Damage = this.Agility * 2;
 }
```

### Move & Attack

- The hero has two options every turn: attack & move
    - attack: shows all monsters in the hero’s range and their remaining health to attack from
    - move: can move one block vertically, horizontally, and diagonally
- The monster attack & move patterns
    - At the beginning of every turn, a new monster is born on a random block
    - attack: if a monster is next to the hero, it always attacks
    - move: every monster moves towards the hero

### History

Each game, hero, and creation time is recorded in the Entity Framework database.
