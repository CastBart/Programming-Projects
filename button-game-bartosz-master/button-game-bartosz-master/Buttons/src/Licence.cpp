#include "Licence.h"



Licence::Licence(Game & game) :
	m_game(&game)
{
	if (!m_bzTexture.loadFromFile(".//resources//images//BZ.png"))
	{
		std::cout << "ERROR: loading .//resources//images//BZ.png" << std::endl;
	}

	if (!m_itsTexture.loadFromFile(".//resources//images//itsInTheGame.png"))
	{
		std::cout << "ERROR: loading .//resources//images//itsInTheGame.png" << std::endl;
	}

	if (!m_buttonsTexture.loadFromFile(".//resources//images//buttons.png"))
	{
		std::cout << "ERROR: loading .//resources//images//buttons.png" << std::endl;
	}
	/*SETTING UP BZ SPRITE*/
	m_bzSprite.setTexture(m_bzTexture);
	m_bzSprite.setPosition(sf::Vector2f((Game::WINDOW_WIDTH / 2) - 250, (Game::WINDOW_HEIGHT / 2) - 100));
	m_bzSprite.setOrigin(m_bzSprite.getLocalBounds().width / 2, m_bzSprite.getLocalBounds().height / 2);

	/*SETTING UP ITS IN THE CLICKS SPRITE*/
	m_itsSprite.setTexture(m_itsTexture);
	m_itsSprite.setPosition(sf::Vector2f((Game::WINDOW_WIDTH / 2) + (m_itsSprite.getLocalBounds().width / 2) - 150, (Game::WINDOW_HEIGHT / 2) - 100));
	m_itsSprite.setOrigin(m_itsSprite.getLocalBounds().width / 2, m_itsSprite.getLocalBounds().height / 2);

	/*SETTING UP BUTTONS SPRITE*/
	m_buttonsSprite.setTexture(m_buttonsTexture);
	m_buttonsSprite.setPosition(sf::Vector2f((Game::WINDOW_WIDTH / 2) - 100 + (m_buttonsSprite.getLocalBounds().width / 2) - m_bzSprite.getLocalBounds().width - 30, Game::WINDOW_HEIGHT / 2));
	m_buttonsSprite.setOrigin(m_buttonsSprite.getLocalBounds().width / 2, m_buttonsSprite.getLocalBounds().height / 2);

	m_bzState.transform.scale(sf::Vector2f(0.01, 0.01), m_bzSprite.getPosition());
	m_itsState.transform.scale(sf::Vector2f(0.01, 0.01), m_itsSprite.getPosition());
	m_buttonsState.transform.scale(sf::Vector2f(0.01, 0.01), m_buttonsSprite.getPosition());
	//m_bzSprite.scale(sf::Vector2f(0.01, 0.01));
}

Licence::~Licence()
{
	std::cout << "Bye bye Splash Screen" << std::endl;
}

void Licence::update(sf::Time dt)
{
	m_accumulatedTime += dt;
	if (m_accumulatedTime.asSeconds() <= 1)
	{
		m_bzState.transform.scale(sf::Vector2f(1.08, 1.08), m_bzSprite.getPosition());
		//m_bzSprite.scale(sf::Vector2f(1+4.7*dt.asSeconds(), 1+4.7*dt.asSeconds()));
	}
	if (m_accumulatedTime.asSeconds() >= 1 && m_accumulatedTime.asSeconds() <= 2)
	{
		m_buttonsState.transform.scale(sf::Vector2f(1.08, 1.08), m_buttonsSprite.getPosition());
	}
	if (m_accumulatedTime.asSeconds() >= 2 && m_accumulatedTime.asSeconds() <= 3)
	{
		m_itsState.transform.scale(sf::Vector2f(1.08, 1.08), m_itsSprite.getPosition());
	}
	if (m_accumulatedTime.asSeconds() >= 3.5)
	{
		m_game->getCurrentState() = GameState::Splash;
	}
}

void Licence::render(sf::RenderWindow & window)
{
	window.draw(m_bzSprite, m_bzState);
	window.draw(m_buttonsSprite, m_buttonsState);
	window.draw(m_itsSprite, m_itsState);
}