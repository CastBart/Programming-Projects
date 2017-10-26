#ifndef OPTIONSCREEN
#define OPTIONSCREEN
#include "XboxController.h"
#include "GUI.h"
#include "Label.h"
#include "Button.h"
#include "Slider.h"
#include "CheckBox.h"
#include "Game.h"
class Game;

class OptionScreen
{
public:
	OptionScreen();
	OptionScreen(Game& game);
	~OptionScreen();

	void update(sf::Time const& dt);
	void draw(sf::RenderTarget & target, sf::RenderStates states);

	void optionScreenGuiSetup();
	void manageOptionNavigation();
	void manageOptionWidget();

private:
	GUI* m_gui_OptionScreen;
	std::vector<Widget*> optionHolder;
	Game* m_game;
	sf::Text m_text;

};

#endif // !OPTIONSCREEN



