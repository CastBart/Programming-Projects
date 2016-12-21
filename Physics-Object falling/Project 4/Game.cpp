#include "Game.h"



Game::Game() :
	m_window(sf::VideoMode(800, 600, 32), "Phisics Project 4", sf::Style::Default),
	m_gravity(0, 9.8f),
	FPS(60.0f),
	m_timePerFrame(sf::seconds(1.0f / FPS)),
	m_timeSinceLastUpdate(sf::Time::Zero),
	m_timeTaken(0),
	m_hight(0)
{
	if (!m_font.loadFromFile("font.ttf"))
	{
		std::string s("Error loading texture");
		throw std::exception(s.c_str());
	}
	m_perdictedTime.setFont(m_font);
	m_perdictedHight.setFont(m_font);
	m_actualTime.setFont(m_font);
	m_actualHight.setFont(m_font);

	setUpText(m_perdictedTime, sf::Vector2f(10, 100), "Perdicted Time: 9.0684");
	setUpText(m_perdictedHight, sf::Vector2f(10, 130), "Perdicted Hight: 100.7411");

	setUpText(m_actualTime, sf::Vector2f(500, 100), "Actual Time: " + std::to_string(m_timeTaken));
	setUpText(m_actualHight, sf::Vector2f(500, 130), "Actual Hight: " + std::to_string(m_hight));

}

void Game::run()
{
	
	m_clock.restart();
	m_circle.getCricle().setFillColor(sf::Color::Green);
	drawLine(lineToPass, sf::Vector2f(0, 480), sf::Vector2f(800, 1));
	drawLine(ground, sf::Vector2f(0, 580), sf::Vector2f(800, 20));
	
	sf::Time time;

	while (m_window.isOpen())
	{

		processEvents();

		m_timeSinceLastUpdate += m_clock.restart();
		if (m_timeSinceLastUpdate > m_timePerFrame)
		{
			m_window.clear();
			
			update(m_timeSinceLastUpdate.asSeconds());

			render();
			m_timeSinceLastUpdate = sf::Time::Zero;
		}
	}

	
}

void Game::processEvents()
{
	sf::Event event;
	while (m_window.pollEvent(event))
	{
		if (event.type == sf::Event::Closed)
		{
			m_window.close();
		}
		switch (event.type)
		{
		case sf::Event::KeyPressed:
			switch (event.key.code)
			{
			case sf::Keyboard::Space:
				if (!m_circle.getIsMoving())
				{
					m_circle.setVel(sf::Vector2f(0, -44.4356376));
					m_circle.setIsMoving(true);
					m_timeTaken = 0;
				}
				break;
			default:
				break;
			}
			break;
		default:
			break;
		}
	}
}

void Game::drawLine(sf::RectangleShape & line, sf::Vector2f & pos, sf::Vector2f &size)
{
	line = sf::RectangleShape(size);
	line.setPosition(pos);
	line.setFillColor(sf::Color::Red);
}

void Game::hightCalc()
{
	if (579 - m_circle.getPos().y > m_hight)
	{
		m_hight = 579 - m_circle.getPos().y;
		std::cout << (m_hight) << std::endl;
	}
}

void Game::setUpText(sf::Text & text, sf::Vector2f &pos, std::string string)
{
	text.setCharacterSize(20);
	text.setPosition(pos);
	text.setString(string);
	
}

void Game::render()
{
	m_window.clear();
	m_window.draw(lineToPass);
	m_window.draw(ground);
	m_circle.render(m_window);
	m_window.draw(m_perdictedHight);
	m_window.draw(m_perdictedTime);
	m_window.draw(m_actualHight);
	m_window.draw(m_actualTime);
	m_window.display();
}

void Game::update(double dt)
{
	m_circle.getCricle().setPosition(m_circle.getPos());
	
	if (m_circle.getIsMoving())
	{
		
		m_timeTaken += dt;

		m_circle.update(dt, m_gravity);
		hightCalc();
		
		m_actualHight.setString("Actual Hight: " + std::to_string(m_hight));
		m_actualTime.setString("Actual Time: " + std::to_string(m_timeTaken));
	}
	if (m_circle.getIsMoving() && m_circle.getCricle().getGlobalBounds().intersects(ground.getGlobalBounds()))
	{
		m_circle.setVel(sf::Vector2f(0,0));
		m_circle.setIsMoving(false);
	}
}


Game::~Game()
{
}
