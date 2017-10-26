#ifndef MAINMENU
#define MAINMENU
#include "XboxController.h"
#include "GUI.h"
#include "Label.h"
#include "Button.h"
#include "Slider.h"
#include "CheckBox.h"
#include "Game.h"
class Game;

class MainMenu
{
public:
	MainMenu();
	MainMenu(Game & game);
	void update(sf::Time const& dt);
	void draw(sf::RenderTarget & target, sf::RenderStates states);
	~MainMenu();

	void menuScreenGuiSetup();
	void manageMenuNavigation();
	void manageMenuWidget();

private:
	GUI* m_gui_menuScreen;
	std::vector<Widget*> menuHolder;
	Game* m_game;

	sf::Text m_text;
	
};

#endif // !MAINMENU





