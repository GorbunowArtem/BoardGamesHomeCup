Feature: Add new Player
	
	Background: F
		
		Given I am logged in
		When I am adding new Player with unique nickname
		Then new Player created