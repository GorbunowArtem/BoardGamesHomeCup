Feature: Adding Game

    Scenario Outline: Add new Game
        When I am filling title, min and max players:
          | Title   | MinPlayers   | MaxPlayers   |
          | <title> | <minPlayers> | <maxPlayers> |
        Then new Game added

        Examples:
          | title | minPlayers | maxPlayers |
          | Game1 | 2          | 24         |
          | Game3 | 4          | 8          |