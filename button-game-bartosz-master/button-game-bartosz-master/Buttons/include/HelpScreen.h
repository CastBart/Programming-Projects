#ifndef HELPSCREEN
#define HELPSCREEN
#include "Game.h"
#include "XboxController.h"
#include "GUI.h"
#include "Label.h"
#include "Button.h"
#include "Slider.h"
#include "CheckBox.h"
#include "Game.h"

class Game;

class HelpScreen
{
public:
	HelpScreen();
	HelpScreen(Game &game);
	~HelpScreen();

	void update(sf::Time const& dt);
	void draw(sf::RenderTarget & target, sf::RenderStates states);
	
	void helpScreenGuiSetup();
	void manageHelpNavigation();
	void manageHelpWidget();

private:
	GUI* m_gui_helpScreen;
	std::vector<Widget*> helpHolder;
	Game* m_game;

	sf::Texture m_texture;
	sf::Sprite m_sprite;

	sf::Texture m_textureBack;
	sf::Sprite m_spriteBack;

};


#endif // !HELPSCREEN

