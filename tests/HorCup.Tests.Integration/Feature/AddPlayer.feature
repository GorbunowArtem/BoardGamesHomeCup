Feature: Adding Player

    Scenario: Creating new Player
        Given I am logged as Admin
        When I am filling unique nickname
        Then new Player created