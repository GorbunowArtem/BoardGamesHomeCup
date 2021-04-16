Feature: Add new Player
	
	Background: F
		
		Given I am logged as Administrator
		When I am adding new PLayer with unique nickname
		Then new Player created