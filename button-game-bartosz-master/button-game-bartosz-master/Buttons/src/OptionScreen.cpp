#include "OptionScreen.h"



OptionScreen::OptionScreen()
{
}

OptionScreen::OptionScreen(Game & game):
	m_game(&game)
{
	optionScreenGuiSetup();
	m_text.setFont(m_game->getFont());
	m_text.setString("Option");
	m_text.setCharacterSize(50);
}


OptionScreen::~OptionScreen()
{
}

void OptionScreen::update(sf::Time const & dt)
{
	m_gui_OptionScreen->update(dt);
	m_game->getCurrentController().update();
	manageOptionWidget();
	manageOptionNavigation();
	
	m_game->getPrevController().update();
}

void OptionScreen::draw(sf::RenderTarget & target, sf::RenderStates states)
{
	m_gui_OptionScreen->draw(target, states);
	target.draw(m_text);
}

/// <summary>
/// setting up the option screen
/// making the widgets
/// giving them position around the circle
/// and placing them into the widget vector
/// </summary>
void OptionScreen::optionScreenGuiSetup()
{
	m_gui_OptionScreen = new GUI(m_game->getCurrentController());


	Widget* button2 = new CheckBox(m_game->getFont(), "Volume", 20,m_game->getCheckedTexture(), m_game->getUnCheckedTexture());
	optionHolder.push_back(button2);
	Widget*	button1 = new Slider(m_game->getFont(), "Sound", 20);
	optionHolder.push_back(button1);

	Widget* button3 = new Slider(m_game->getFont(), "Frames", 20, 100, 10, 60);
	optionHolder.push_back(button3);
	Widget* button4 = new Button(m_game->getFont(), "Back", 20);
	optionHolder.push_back(button4);


	for (int i = 0; i < optionHolder.size(); i++)
	{
		if (typeid(*optionHolder[i]).name() != typeid(Label).name())
		{
			m_gui_OptionScreen->addWidget(optionHolder[i]);
		}
	}

	m_gui_OptionScreen->setPositionOfWidgets();
}

/// <summary>
/// manage the navigating inside the option screen
/// give game states approtpraite state when a  certin button is pressed 
/// </summary>
void OptionScreen::manageOptionNavigation()
{
	if (optionHolder[0]->processInput(m_game->getCurrentController()) && !optionHolder[0]->processInput(m_game->getPrevController()))
	{
		Game::SOUND_ON = !Game::SOUND_ON;
	}
	if (optionHolder[1]->processInput(m_game->getCurrentController()) && !optionHolder[1]->processInput(m_game->getPrevController()))
	{
		Game::VOLUME = optionHolder[1]->getValue();
	}
	if (optionHolder[2]->processInput(m_game->getCurrentController()) && !optionHolder[2]->processInput(m_game->getPrevController()))
	{
		//m_currentGameState = GameState::HelpScreen;
		Game::FRAMES = optionHolder[2]->getValue();
	}
	if (optionHolder[3]->processInput(m_game->getCurrentController()) && !optionHolder[3]->processInput(m_game->getPrevController()))
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
/// manage the focus of widgets in the option screen
/// give focus to appropriate widgets depending on position
/// of the thumbstick
/// </summary>
void OptionScreen::manageOptionWidget()
{
	float gapSize = (200 / optionHolder.size());
	if (m_game->getCurrentController().m_currentState.m_LeftThumbStick.x <= 100 &&
		m_game->getCurrentController().m_currentState.m_LeftThumbStick.x >= 70 &&
		m_game->getCurrentController().m_currentState.m_LeftThumbStick.y <= gapSize &&
		m_game->getCurrentController().m_currentState.m_LeftThumbStick.y >= -gapSize)
	{
		m_gui_OptionScreen->currentFocus = 0;
	}
	else if (
		m_game->getCurrentController().m_currentState.m_LeftThumbStick.x <= gapSize &&
		m_game->getCurrentController().m_currentState.m_LeftThumbStick.x >= -gapSize &&
		m_game->getCurrentController().m_currentState.m_LeftThumbStick.y <= 100 &&
		m_game->getCurrentController().m_currentState.m_LeftThumbStick.y >= 100 - gapSize)
	{
		m_gui_OptionScreen->currentFocus = 1;
	}
	else if (
		m_game->getCurrentController().m_currentState.m_LeftThumbStick.x >= -100 &&
		m_game->getCurrentController().m_currentState.m_LeftThumbStick.x <= -70 &&
		m_game->getCurrentController().m_currentState.m_LeftThumbStick.y <= gapSize &&
		m_game->getCurrentController().m_currentState.m_LeftThumbStick.y >= -gapSize)
	{
		m_gui_OptionScreen->currentFocus = 2;
	}
	else if (
		m_game->getCurrentController().m_currentState.m_LeftThumbStick.x <= gapSize &&
		m_game->getCurrentController().m_currentState.m_LeftThumbStick.x >= -gapSize &&
		m_game->getCurrentController().m_currentState.m_LeftThumbStick.y >= -100 &&
		m_game->getCurrentController().m_currentState.m_LeftThumbStick.y <= -70)
	{
		m_gui_OptionScreen->currentFocus = 3;
	}

}
