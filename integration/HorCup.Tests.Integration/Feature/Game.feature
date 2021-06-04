Feature: Game operations

    Scenario: Full list of Game entity operations
        Given I am logged in
        When I am filling title, min and max players:
          | Title | MinPlayers | MaxPlayers |
          | Game1 | 2          | 24         |
        Then new Game added
        When I change Game title, min and max players
        Then Game is updated
        When I delete Game
        Then Game no longer exists in system 
