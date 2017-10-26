#include "PauseScreen.h"



PauseScreen::PauseScreen()
{
}

PauseScreen::PauseScreen(Game & game):
	m_game(&game)
{
	pauseScreenGuiSetup();
	m_text.setFont(m_game->getFont());
	m_text.setString("Pause");
	m_text.setCharacterSize(50);
}


PauseScreen::~PauseScreen()
{
}

void PauseScreen::update(sf::Time const & dt)
{
	m_gui_pauseScreen->update(dt);
	m_game->getCurrentController().update();
	managePauseWidget();
	managePauseNavigation();
	m_game->getPrevController().update();
}

void PauseScreen::draw(sf::RenderTarget & target, sf::RenderStates states)
{
	m_gui_pauseScreen->draw(target, states);
	target.draw(m_text);
}

/// <summary>
/// setting up the pause screen
/// making the widgets
/// giving them position around the circle
/// and placing them into the widget vector
/// </summary>
void PauseScreen::pauseScreenGuiSetup()
{
	m_gui_pauseScreen = new GUI(m_game->getCurrentController());


	Widget*	button1 = new Button(m_game->getFont(), "Play", 20);
	pauseHolder.push_back(button1);
	Widget* button3 = new Button(m_game->getFont(), "Options", 20);
	pauseHolder.push_back(button3);
	Widget* button4 = new Button(m_game->getFont(), "Help", 20);
	pauseHolder.push_back(button4);
	Widget* button5 = new Button(m_game->getFont(), "Exit", 20);
	pauseHolder.push_back(button5);
	


	for (int i = 0; i < pauseHolder.size(); i++)
	{
		if (typeid(*pauseHolder[i]).name() != typeid(Label).name())
		{
			m_gui_pauseScreen->addWidget(pauseHolder[i]);
		}
	}

	m_gui_pauseScreen->setPositionOfWidgets();
}

/// <summary>
/// navigating the game states in pause screen
/// </summary>
void PauseScreen::managePauseNavigation()
{
	//check for input from current and previous controller
	if (pauseHolder[0]->processInput(m_game->getCurrentController()) && !pauseHolder[0]->processInput(m_game->getPrevController()))
	{
		m_game->getCurrentState() = GameState::Play;
		m_game->getPrevState() = GameState::Pause;
	}//check for input from current and previous controller
	if (pauseHolder[1]->processInput(m_game->getCurrentController()) && !pauseHolder[1]->processInput(m_game->getPrevController()))
	{
		m_game->getCurrentState() = GameState::OptionScreen;
		m_game->getPrevState() = GameState::Pause;

	}//check for input from current and previous controller
	if (pauseHolder[2]->processInput(m_game->getCurrentController()) && !pauseHolder[2]->processInput(m_game->getPrevController()))
	{
		m_game->getCurrentState() = GameState::HelpScreen;
		m_game->getPrevState() = GameState::Pause;

	}//check for input from current and previous controller
	if (pauseHolder[3]->processInput(m_game->getCurrentController()) && !pauseHolder[3]->processInput(m_game->getPrevController()))
	{
		m_game->getWindow().close();
	}
	
}

/// <summary>
/// Give focus to the appropriate
/// widget in pause screen
/// </summary>
void PauseScreen::managePauseWidget()
{

	
	float gapSize = (200 / pauseHolder.size());
	if (m_game->getCurrentController().m_currentState.m_LeftThumbStick.x <= 100 &&
		m_game->getCurrentController().m_currentState.m_LeftThumbStick.x >= 70 &&
		m_game->getCurrentController().m_currentState.m_LeftThumbStick.y <= gapSize &&
		m_game->getCurrentController().m_currentState.m_LeftThumbStick.y >= -gapSize)
	{
		m_gui_pauseScreen->currentFocus = 0;
	}
	else if (	
		m_game->getCurrentController().m_currentState.m_LeftThumbStick.x <= gapSize &&
		m_game->getCurrentController().m_currentState.m_LeftThumbStick.x >= -gapSize &&
		m_game->getCurrentController().m_currentState.m_LeftThumbStick.y <= 100 &&
		m_game->getCurrentController().m_currentState.m_LeftThumbStick.y >= 100 - gapSize)
	{
		m_gui_pauseScreen->currentFocus = 1;
	}
	else if (
		m_game->getCurrentController().m_currentState.m_LeftThumbStick.x >= -100 &&
		m_game->getCurrentController().m_currentState.m_LeftThumbStick.x <= -70 &&
		m_game->getCurrentController().m_currentState.m_LeftThumbStick.y <= gapSize &&
		m_game->getCurrentController().m_currentState.m_LeftThumbStick.y >= -gapSize)
	{
		m_gui_pauseScreen->currentFocus = 2;
	}

	else if (
		m_game->getCurrentController().m_currentState.m_LeftThumbStick.x <= gapSize &&
		m_game->getCurrentController().m_currentState.m_LeftThumbStick.x >= -gapSize &&
		m_game->getCurrentController().m_currentState.m_LeftThumbStick.y >= -100 &&
		m_game->getCurrentController().m_currentState.m_LeftThumbStick.y <= -70)
	{
		m_gui_pauseScreen->currentFocus = 3;
	}
	
}
