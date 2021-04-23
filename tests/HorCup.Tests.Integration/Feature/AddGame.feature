Feature: Add new Game

    Scenario Outline: Add Game
        When I am adding new Game:
          | Title   | MinPlayers   | MaxPlayers   |
          | <title> | <minPlayers> | <maxPlayers> |
        Then new Game added

        Examples:
          | title | minPlayers | maxPlayers |
          | Game1 | 2          | 24         |
          | Game3 | 4          | 8          |