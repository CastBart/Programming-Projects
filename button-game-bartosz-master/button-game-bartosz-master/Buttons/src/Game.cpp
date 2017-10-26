#include "Game.h"



const float Game::WINDOW_WIDTH = 1280;
const float Game::WINDOW_HEIGHT = 720;
float Game::FRAMES = 60;
float Game::VOLUME = 100;
bool Game::SOUND_ON = true;

Game::Game(sf::ContextSettings settings):
	m_window(sf::VideoMode(1280, 720), "Button Game", sf::Style::Default, settings),
	m_currentGameState(GameState::Licence)
{
	/*LOAD FONT*/
	if (!m_font.loadFromFile(".//resources//font.ttf"))
	{
		std::cout << "error loading font" << std::endl;
	}
	/*LOAD CHECKED TEXTURE*/
	if (!m_checkedTexture.loadFromFile(".//resources//images//checkbox.png"))
	{
		std::cout << "error loading checked" << std::endl;
	}
	/*LOAD UNCHECKED TEXTURE*/
	if (!m_uncheckedTexture.loadFromFile(".//resources//images//uncheckbox.png"))
	{
		std::cout << "error loading unchecked" << std::endl;
	}
	/*LOAD BACKGROUND TEXTURE*/
	if (!m_backgroundTexture.loadFromFile(".//resources//images//Background.jpg"))
	{
		std::cout << "error loading background" << std::endl;
	}
	/*LOAD SELECT BUTTON TEXTURE*/
	if (!m_sellectButtonTexture.loadFromFile(".//resources//images//selectButton.png"))
	{
		std::cout << "error loading select button" << std::endl;
	}
	/*LOAD PAUSE BUTTON TEXTURE*/
	if (!m_pauseButtonTexture.loadFromFile(".//resources//images//pauseButton.png"))
	{
		std::cout << "error loading pause button" << std::endl;
	}

	if (!m_music.openFromFile(".//resources//sounds//music.ogg"))
	{
		std::cout << "ERROR opening music" << std::endl;
	}
	/*SET ALL TEXTURES TO APPROPRIATE SPRITES*/
	m_backgroundSprite.setTexture(m_backgroundTexture);
	m_sellectButtonSprite.setTexture(m_sellectButtonTexture);
	m_pauseButtonSprite.setTexture(m_pauseButtonTexture);

	/*SET POSITION OF SELECT AND PAUSE BUTTONS*/
	m_sellectButtonSprite.setPosition(sf::Vector2f((WINDOW_WIDTH / 10)*8, WINDOW_HEIGHT / 10));
	m_pauseButtonSprite.setPosition(sf::Vector2f((WINDOW_WIDTH / 10) * 8, WINDOW_HEIGHT / 10));
	
	m_music.setVolume(VOLUME);         // reduce the volume
	m_music.setLoop(true);
	m_music.play();
}


Game::~Game()
{
	/*DELETING RAW POINTERS TO OBJECTS*/
	delete m_licence; 
	delete m_splash;
	delete m_credits;
	delete m_helpScreen;
	delete m_mainMenuScreen;
}



sf::RenderWindow& Game::getWindow()
{
	return m_window;
}

sf::RenderStates & Game::getState()
{
	return m_state;
}

sf::Font & Game::getFont()
{
	return m_font;
}

sf::Texture & Game::getCheckedTexture()
{
	return m_checkedTexture;
}

sf::Texture & Game::getUnCheckedTexture()
{
	return m_uncheckedTexture;
}

XboxController & Game::getCurrentController()
{
	return m_controller;
}

XboxController & Game::getPrevController()
{
	return m_prevController;
}

GameState & Game::getCurrentState()
{
	return m_currentGameState;
}

GameState & Game::getPrevState()
{
	return m_previousGameState;
}

///<summary>
/// Run function which handles the game loop
///<summary>
void Game::run()
{
	m_licence = new Licence(*this);
	m_splash = new SplashScreen(*this);
	m_credits = new Credits(*this);
	m_helpScreen = new HelpScreen(*this);
	m_mainMenuScreen = new MainMenu(*this);
	m_pauseScreen = new PauseScreen(*this);
	m_optionScreen = new OptionScreen(*this);
	
	m_clock.restart();
	/*MAIN LOOP OF THE GAME*/
	while(m_window.isOpen())
	{
		FRAMES_PER_SECOND = sf::seconds(1.0f / FRAMES);
		proccessEvents();
		m_elapsedTime += m_clock.restart();
		while (m_elapsedTime > FRAMES_PER_SECOND)
		{
			m_elapsedTime -= FRAMES_PER_SECOND;
			update(FRAMES_PER_SECOND);
			
		}
		//std::cout << FRAMES << std::endl;
		render();
	}
}

///<summary>
/// Update for the Game class.
/// Handles all rendering that
/// happen on the screen at a given time
///<summary>
void Game::update(sf::Time const & dt)
{
	if (SOUND_ON)
	{//check if check boxs is checked and if sound is already not playing
		if (m_music.getStatus() != sf::Sound::Status::Playing)
		{
			m_music.play();
		}
	}
	if (!SOUND_ON)
	{//check if checkboxs is unchecked and if sound is aready playing
		if (m_music.getStatus() == sf::Sound::Status::Playing)
		{
			m_music.pause();
		}
	}
	switch (m_currentGameState)
	{
	case GameState::Credits:
		m_credits->update(dt);
		break;
	case GameState::HelpScreen:
		m_helpScreen->update(dt);
		break;
	case GameState::Licence:
		m_licence->update(dt);
		break;
	case GameState::MainMenu:
		m_mainMenuScreen->update(dt);
		break;
	case GameState::OptionScreen:
		m_optionScreen->update(dt);
		m_music.setVolume(VOLUME); //set volume according to slider volume
		
		break;
	case GameState::Pause:
		m_pauseScreen->update(dt);
		break;
	case GameState::Play:
		m_controller.update();
		if (m_controller.m_currentState.m_Start)
		{
			m_currentGameState = GameState::Pause;	
		}
		break;
	case GameState::Splash:
		m_splash->update(dt);
		break;
	default:
		break;
	}
}

///<summary>
/// Update for the Game class.
/// Handles all updates to the screen 
/// that happen in the game.
///<summary>
void Game::render()
{
	m_window.clear();
	m_window.draw(m_backgroundSprite);
	switch (m_currentGameState)
	{
	case GameState::Credits:
		
		m_credits->render(m_window);
		break;
	case GameState::HelpScreen:
		m_helpScreen->draw(m_window, m_state);
		break;
	case GameState::Licence:
		m_licence->render(m_window);
		break;
	case GameState::MainMenu:
		m_mainMenuScreen->draw(m_window, m_state);
		m_window.draw(m_sellectButtonSprite);
		break;
	case GameState::OptionScreen:
		m_optionScreen->draw(m_window, m_state);
		m_window.draw(m_sellectButtonSprite);
		break;
	case GameState::Pause:
		m_pauseScreen->draw(m_window, m_state);
		m_window.draw(m_sellectButtonSprite);
		break;
	case GameState::Play:
		m_window.draw(m_pauseButtonSprite);
		break;
	case GameState::Splash:
		m_splash->render(m_window);
		break;
	default:
		break;
	}
	m_window.display();
}

///<summary>
/// Event Handler for the game
///<summary>
void Game::proccessEvents()
{
	int i;
	sf::Event event;
	while (m_window.pollEvent(event))
	{
		switch (event.type)
		{
		case sf::Event::Closed: /* "X" icon event handler */
			m_window.close();
			break;
		case sf::Event::JoystickButtonPressed:						 
			if (m_currentGameState == GameState::Splash) /*event used for splash screen. when any key on joystick pressed then change state*/
			{
				m_currentGameState = GameState::MainMenu;
			}
			break;
		default:
			break;
		}
	}
}

