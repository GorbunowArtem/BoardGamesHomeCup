Feature: Player operations

    Scenario: Full list of Player entity operations
        Given I am logged in
        When I am filling unique nickname
        Then new Player added
        When I change Player nickname
        Then Player is updated
        When I delete Player
        Then Player no longer exists in system