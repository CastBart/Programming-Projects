#ifndef  CREDITS
#define CREDITS
#include <SFML\Graphics.hpp>
#include "Game.h"
#include <iostream>
#include <fstream>


class Game;	/*Pre-define Game class*/

class Credits
{
public:
	Credits(Game &game);	/*Constructor that takes in the game instance*/
	~Credits();				/*Destructor*/		

	void update(const sf::Time &dt);	/*Updates Function*/
	void render(sf::RenderWindow &window);	/*Render Function*/
	void initialize();

private:

	Game *m_game;	/*instance of the game object*/
	sf::Time m_cumulativeTime;		//determines the accumulated time 
	sf::Font m_font;
	sf::RenderStates m_render;		//render state 
	std::ifstream m_file;			//file strea used to read form file
	std::string m_string;			//string variable. used to assign text in text vector
	std::vector<sf::Text> m_vector;	//vector of texts
	int yPos;						//position to start the text from
	int characterSize = 30;			//character size
	int m_currentIndex = 0;			//current text in the vector
	bool m_titleLine = true;		//determines if the next line is a title
};
#endif // ! CREDITS
