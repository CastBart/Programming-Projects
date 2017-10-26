#include "HelpScreen.h"



HelpScreen::HelpScreen()
{
}

HelpScreen::HelpScreen(Game &game):
	m_game(&game)
{
	helpScreenGuiSetup();
	if (!m_texture.loadFromFile(".//resources//images//helpScreen.png"))
	{
		std::cout << "ERROR: loading helpScreen.png" << std::endl;
	}
	m_texture.setSmooth(true);
	m_sprite.setTexture(m_texture);

	if (!m_textureBack.loadFromFile(".//resources//images//helpBackTexture.png"))
	{
		std::cout << "ERROR: loading helpBackTexture.png" << std::endl;
	}
	m_textureBack.setSmooth(true);
	m_spriteBack.setTexture(m_textureBack);
	m_spriteBack.setOrigin(sf::Vector2f(m_spriteBack.getLocalBounds().width / 2, m_spriteBack.getLocalBounds().height / 2));
	m_spriteBack.setPosition(sf::Vector2f(Game::WINDOW_WIDTH / 2, (Game::WINDOW_HEIGHT / 6) * 5));
}


HelpScreen::~HelpScreen()
{
}

void HelpScreen::update(sf::Time const & dt)
{
	m_gui_helpScreen->update(dt);
	m_game->getCurrentController().update();
	manageHelpWidget();
	manageHelpNavigation();
	m_game->getPrevController().update();
}

void HelpScreen::draw(sf::RenderTarget & target, sf::RenderStates states)
{
	m_gui_helpScreen->draw(target, states);
	target.draw(m_sprite);
	target.draw(m_spriteBack);
}

/// <summary>
/// setting up the help screen
/// making the widgets
/// giving them position around the circle
/// and placing them into the widget vector
/// </summary>
void HelpScreen::helpScreenGuiSetup()
{
	m_gui_helpScreen = new GUI(m_game->getCurrentController());
	m_gui_helpScreen->currentFocus = 0;

	Widget* button1 = new Button(m_game->getFont(), "Back", 20);
	helpHolder.push_back(button1);



	for (int i = 0; i < helpHolder.size(); i++)
	{
		if (typeid(*helpHolder[i]).name() != typeid(Label).name())
		{
			m_gui_helpScreen->addWidget(helpHolder[i]);
		}
	}

	m_gui_helpScreen->setPositionOfWidgets();
}


/// <summary>
/// manage the navigation inside the help screen
/// give game states approtpraite state when a  certin button is pressed 
/// </summary>
void HelpScreen::manageHelpNavigation()
{
	if (helpHolder[0]->processInput(m_game->getCurrentController()) && !helpHolder[0]->processInput(m_game->getPrevController()))
	{
		if (m_game->getPrevState() == GameState::Pause)
		{
			m_game->getCurrentState() = GameState::Pause;
		}
		else
		{
			m_game->getCurrentState() = GameState::MainMenu;
		}
	}
}

/// <summary>
/// manage the focus of widgets in the help screen
/// give focus to appropriate widgets depending on position
/// of the thumbstick
/// </summary>
void HelpScreen::manageHelpWidget()
{
	
	m_gui_helpScreen->currentFocus = 0;

}



