#ifndef PAUSESCREEN
#define PAUSESCREEN
#include "XboxController.h"
#include "GUI.h"
#include "Label.h"
#include "Button.h"
#include "Slider.h"
#include "CheckBox.h"
#include "Game.h"
class Game;

class PauseScreen
{
public:
	PauseScreen();
	PauseScreen(Game& game);
	~PauseScreen();

	void update(sf::Time const& dt);
	void draw(sf::RenderTarget & target, sf::RenderStates states);

	void pauseScreenGuiSetup();
	void managePauseNavigation();
	void managePauseWidget();

private:

	GUI* m_gui_pauseScreen;
	std::vector<Widget*> pauseHolder;
	Game* m_game;
	sf::Text m_text;

};

#endif // !PAUSESCREEN


