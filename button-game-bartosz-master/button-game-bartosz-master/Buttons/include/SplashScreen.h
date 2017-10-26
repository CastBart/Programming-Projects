#ifndef SPLASHSCREEN
#define SPLASHSCREEN
#include <SFML\Graphics.hpp>
#include "Game.h"
#include <iostream>

class Game;

class SplashScreen
{
public:
	SplashScreen(Game &game);
	~SplashScreen();

	void update(sf::Time dt);
	void render(sf::RenderWindow &window);

private:
	Game *m_game;
	sf::Time m_accumulatedTime;

	sf::RenderStates m_bzState;
	sf::RenderStates m_itsState;
	sf::RenderStates m_buttonsState;

	sf::Text m_text;

	sf::Texture m_bzTexture;
	sf::Texture m_buttonsTexture;
	sf::Texture m_itsTexture;

	sf::Sprite m_bzSprite;
	sf::Sprite m_buttonsSprite;
	sf::Sprite m_itsSprite;

};

#endif // !SPLASHSCREEN