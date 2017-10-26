#include "MainMenu.h"



MainMenu::MainMenu()
{
}

MainMenu::MainMenu(Game &game):
	m_game(&game)
{
	menuScreenGuiSetup();
	m_text.setFont(m_game->getFont());
	m_text.setString("Main Menu");
	m_text.setCharacterSize(50);
}

void MainMenu::update(sf::Time const& dt/*, XboxController & currentController, XboxController & prevController*/)
{
	m_gui_menuScreen->update(dt);
	m_game->getCurrentController().update();
	manageMenuWidget();
	manageMenuNavigation();
	m_game->getPrevController().update();
}

void MainMenu::draw(sf::RenderTarget & target, sf::RenderStates states)
{
	m_gui_menuScreen->draw(target, states);
	target.draw(m_text);
}


MainMenu::~MainMenu()
{
}

void MainMenu::menuScreenGuiSetup()
{
	m_gui_menuScreen = new GUI(m_game->getCurrentController());


	Widget*	button1 = new Button(m_game->getFont(), "Play", 20);
	menuHolder.push_back(button1);
	Widget*	button2 = new Button(m_game->getFont(), "Credits", 20);
	menuHolder.push_back(button2);
	Widget* button3 = new Button(m_game->getFont(), "Options", 20);
	menuHolder.push_back(button3);
	Widget* button4 = new Button(m_game->getFont(), "Help", 20);
	menuHolder.push_back(button4);
	Widget* button5 = new Button(m_game->getFont(), "Exit", 20);
	menuHolder.push_back(button5);
	


	std::cout << typeid(*button1).name() << std::endl;

	for (int i = 0; i < menuHolder.size(); i++)
	{
		if (typeid(*menuHolder[i]).name() != typeid(Label).name())
		{
			m_gui_menuScreen->addWidget(menuHolder[i]);
		}
	}


	m_gui_menuScreen->setPositionOfWidgets();
}

void MainMenu::manageMenuNavigation()
{
	if (menuHolder[0]->processInput(m_game->getCurrentController()) && !menuHolder[0]->processInput(m_game->getPrevController()))
	{
		m_game->getCurrentState() = GameState::Play;
	}
	if (menuHolder[1]->processInput(m_game->getCurrentController()) && !menuHolder[1]->processInput(m_game->getPrevController()))
	{
		m_game->getCurrentState() = GameState::Credits;
	}
	if (menuHolder[2]->processInput(m_game->getCurrentController()) && !menuHolder[2]->processInput(m_game->getPrevController()))
	{
		m_game->getCurrentState() = GameState::OptionScreen;
	}
	if (menuHolder[3]->processInput(m_game->getCurrentController()) && !menuHolder[3]->processInput(m_game->getPrevController()))
	{
		m_game->getCurrentState() = GameState::HelpScreen;
	}
	if (menuHolder[4]->processInput(m_game->getCurrentController()) && !menuHolder[4]->processInput(m_game->getPrevController()))
	{
		m_game->getWindow().close();
	}
	
}

void MainMenu::manageMenuWidget()
{
	float gapSize = (200 / menuHolder.size()) + 15;
	if (m_game->getCurrentController().m_currentState.m_LeftThumbStick.x <= 100 &&
		m_game->getCurrentController().m_currentState.m_LeftThumbStick.x >= 70 &&
		m_game->getCurrentController().m_currentState.m_LeftThumbStick.y <= gapSize &&
		m_game->getCurrentController().m_currentState.m_LeftThumbStick.y >= -gapSize)
	{
		m_gui_menuScreen->currentFocus = 0;
	}
	else if (
		m_game->getCurrentController().m_currentState.m_LeftThumbStick.x <= 100 &&
		m_game->getCurrentController().m_currentState.m_LeftThumbStick.x >= 100 - gapSize &&
		m_game->getCurrentController().m_currentState.m_LeftThumbStick.y <= 100 &&
		m_game->getCurrentController().m_currentState.m_LeftThumbStick.y >= 100 - gapSize)
	{
		m_gui_menuScreen->currentFocus = 1;
	}
	else if (
		m_game->getCurrentController().m_currentState.m_LeftThumbStick.x >= -100 &&
		m_game->getCurrentController().m_currentState.m_LeftThumbStick.x <= -100 + gapSize &&
		m_game->getCurrentController().m_currentState.m_LeftThumbStick.y <= 100 &&
		m_game->getCurrentController().m_currentState.m_LeftThumbStick.y >= 100 - gapSize)
	{
		m_gui_menuScreen->currentFocus = 2;
	}
	else if (
		m_game->getCurrentController().m_currentState.m_LeftThumbStick.x >= -100 &&
		m_game->getCurrentController().m_currentState.m_LeftThumbStick.x <= -100 &&
		m_game->getCurrentController().m_currentState.m_LeftThumbStick.y <= -100 &&
		m_game->getCurrentController().m_currentState.m_LeftThumbStick.y >= -100)
	{
		m_gui_menuScreen->currentFocus = 3;
	}
	else if (
		m_game->getCurrentController().m_currentState.m_LeftThumbStick.x <= 100 &&
		m_game->getCurrentController().m_currentState.m_LeftThumbStick.x >= 100 - gapSize &&
		m_game->getCurrentController().m_currentState.m_LeftThumbStick.y >= -100 &&
		m_game->getCurrentController().m_currentState.m_LeftThumbStick.y <= -100 + gapSize)
	{
		m_gui_menuScreen->currentFocus = 4;
	}
	
}
