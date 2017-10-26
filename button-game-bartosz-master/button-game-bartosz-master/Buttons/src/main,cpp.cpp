#include <iostream>
#include "Game.h"
/*
	Student Name: Bartosz Zych.
	Student ID: C00205464.
	Version: 1.0.
	Application: This is an application which uses a xbox controller to play.
				 The  application is  focused UI which can be reusable in
				 other projects that i will be working on.
	Working Date --------- Working Time ---------- What was done
	22/01/2017   --------- 18:00-19:00  ---------- I have made a repository on github, created all classes except of Licence, SplashScreen and Credits.
	24/01/2017   --------- 22:00-24:00  ---------- I have made Licence, SplashScreen and Credits classes. Added a basic game enum and created instances
												   of licence, splash and credits in the game class. Added few comments before going to bed.


					    Total Working Time
					   --- 03:00 hours ---
*/

int main()
{
	sf::ContextSettings settings;
	settings.depthBits = 24u;
	settings.antialiasingLevel = 4u;
	Game& game = Game(settings);
	game.run();

}