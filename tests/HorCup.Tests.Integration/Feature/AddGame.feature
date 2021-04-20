Feature: Add new Game

    Background:

    Scenario Outline: Game should be added
 
       Given I am logged as Administrator
        When I am adding new Game with title <title>, minimum players <minPlayers> and maximum players <maxPlayers>
        Then new Game created

        Examples:
          | title | minPlayers | maxPlayers |
          | Game1 | 2          | 24         |
          | Game2 | 3          | 7          |
          | Game3 | 4          | 8          |