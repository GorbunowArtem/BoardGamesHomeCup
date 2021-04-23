Feature: Add new Game

    Background:

    Scenario: Add Game
        When I am adding new Game:
          | Title | MinPlayers | MaxPlayers |
          | Game1 | 2          | 24         |
          | Game3 | 4          | 8          |
        Then new Game added