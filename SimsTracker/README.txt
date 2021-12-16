Sims 2 Neighborhood Tracker

Index
	1: Introduction
	2: Database Schema
	3: Instructions
	4: Technical Notes
	5: Credits



1. Introduction

	This project is meant to be a beginner's ASP.NET MVC 5 project, based on Professor Shad Sluiter's 1 hour ASP.NET Crash Course YouTube video by
freeCodeCamp.org (link : https://www.youtube.com/watch?v=BfEjDD8mWYg&t=8s). I used the tutorial's final form as the "base" for my project and 
added/removed features as i saw fit. 
	
	The theme for the project is to build a database that helps the user keep track of the amount of Sims in their save game. Sims are virtual
people that live inside a Neighborhood. The app allows the user to store their Sims and Neighborhoods inside a database to better organize their 
save game. 
	


2. Database Schema

	The project has 2 databases: SIMS and HOODS, each with their own model, view and controller. A neighborhood (HOODS instance) can house multiple 
sims (SIMS instance), but a sim can only live in a single hood. 
	Meaning that HOODS and SIMS have a one-to-many relationship. 

	SIMS is the database that handles the amount of sims. It has the following attributes:
		
		int ID (Primary Key): the id of the sim. Primarily used to retrieve the sim from the database for various operations.

		string FIRST_NAME: the sim's first name

		string LAST_NAME: the sim's last name

		int AGE: how many sim years the sim has before advancing to the next life stage
		
		string LIFE_STAGE: the sim's life stage (like Toddler, Adult, Child etc...)

		string GENDER: if the sim is male or female
		
		string ASPIRATION: what sims aspire to do with their lives (e.g. Fortune Sims want to make a lot of money)
					Note that Baby / Toddler / Child sims can only have the Grow Up aspiration and vice-versa

		string NOTES (optional): useful notes about the sim, such as who the sim will marry...

		string HOOD_NAME: the neighborhood where the sim resides. Frequently used in routing

		string STATUS: if the sim is alive or deceased
					Note that Baby / Toddler / Child sims cannot be dead

	HOODS is the database that handles the neighborhoods. It has the following attributes:
		
		int ID (Primary Key): the id of the hood

		string NAME : the hood's name

		int POPULATION : how many sims are in the hood. The user cannot influence this attribute, it automatically changes when sims are added/removed

		List<Sim> SIMS : discredited attributed prevoiusly used to make the SIMS table display Sims that live in the respective hood.



3. Instructions

	On the main page, the navigation bar presents you with 3 options: HOME (returns to the main page), MY HOODS (displays the HOODS table) and 
SEARCH (allows you to search for a sim). There will be some background music playing in the main page that you can turn on/off with the small play
button on the top-left. The second headline will also take you to the HOODS table. 

	The Hoods page displays the HOODS table. You can create a new neighborhood for your liking, edit an existing hood's name or delete a hood. Each 
hood has a SIMS button that takes you to a page that displays all the rows inside the SIMS table with sims that live inside that respective hood.

	Inside the SIMS table you will see all the sims that live inside that neighborhood. You can view them for bonus detalis, edit them, delete them
or create new sims. Next to the CREATE NEW SIM button, there is an AGE UP NEIGHBORHOOD button that makes all living sims 1 sim day older. When a sim
has no more sim days to live in their current life stage, they will automatically advance to the next life stage. Child sims that grow up to be Teens
have a _____ entry for their aspiration, living the user to change their aspiration as they see fit. Every Sim has an extra column of OPTIONS that
includes classic commnads like EDIT/DETAILS/DELETE and specialized commands that allows the user to quickly modify important attributes without using
the EDIT page. Those include : 
		-KILL: changes a sim's status from ALIVE to DECEASED. It can only be used on living sims that are teens or older. 
		-RESURRECT: changes a sim's status from DECEASED to ALIVE. It can only be used on dead sims.
		-SEND TO COLLEGE: changes a teen sim's life stage from TEEN to YOUNG ADULT, to simulate going to college. It can only be used on living sims.
		-DROP FROM COLLEGE: changes a young adult sim's life stage from YOUNG ADULT to ADULT, to simulate dropping out of college. It can only be 
							used on living sims.

	The SIMS page has a filter option in which a user can filter the displayed sims based on ASPIRATION, GENDER, LIFE_STAGE and STATUS. The RESET 
button removes all filters. 

	The SEARCH page allows the user to search for sims based on all possible imputs (except for the NOTES attribute). The page will return a table
with all sims that satisfy the given conditions that can also be removed using the RESET button.



4. Technical Notes

	The project is meant to demonstrate familiarity with many important principles of ASP.NET MVC 5 such as:

		-Routing: all the link return the correct pages. This was done by using RedirectToPage() calls with the hoodName argument passed inside
				  the URL (needed for the SIMS table to display the correct SIMS), asp-route-attributeName (needed for passing data from the view
				  to the controller) and href="javascript:history.go(-1)" (needed to make backtracking easier)

	    -Form Validation: besides the "classic" attributes, the project uses a custom ValidAgeAndAspiration attribute to check if a SIM instance's 
						  LIFE_STAGE value is compatible with its AGE, STATUS and ASPIRATION values. 

						  Example: a SIM object with STATUS="Deceased";AGE="15";LIFE_STAGE="Child";ASPIRATION="Fortune" will throw the following errors:
								-"Children can have up to 8 days before aging up!"
								-"Children can only have the Grow Up aspiration!"
								-"You can't create deceased Children!"

		-Search Algorithms: the project has a SEARCH page that can return data from the SIMS table based on almost all attributes as well as a filter
							system built-in the INDEX page. 

		-Database Interaction: there are multiple buttons that call functions inside the controller to quickly modify the database. 

	There are also some featues specific to webpage design, such as background music that can be turned on/off, a matching pallete, background picture
and better aesthethic.
	


	5. Credits


	- "ASP.NET Core Crash Course - C# App in One Hour" by Proffesor Shad Sluiter (link : https://www.youtube.com/watch?v=BfEjDD8mWYg&t=8s) - used in 
	 laying the foundation of the project.

	- www.stackoverflow.com - for various workarounds.

	- Mark Mothersbaugh - composer of The Sims 2 theme (used as background music).

	- "https://simscommunity.info/2016/03/17/the-sims-2-spotlight-neighbourhoods/" - source of the main page's background picture

	- "https://logos.fandom.com/wiki/The_Sims_2" - source of the Sims 2 logo picture
