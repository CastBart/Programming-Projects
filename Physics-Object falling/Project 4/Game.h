#pragma once
#include <SFML\Graphics.hpp>
#include "PhysicsObject.h"
#include <iostream>

class Game
{
public:
	Game();
	void run();
	void render();
	void update(double dt);
	void processEvents();
	void drawLine(sf::RectangleShape &, sf::Vector2f &, sf::Vector2f &);
	void hightCalc();
	void setUpText(sf::Text&, sf::Vector2f &, std::string );
	~Game();


private:
	sf::RenderWindow m_window;
	

	PhysicsObject m_circle;

	const sf::Vector2f m_gravity;
	
	sf::Clock m_clock;
	const float FPS;
	const sf::Time m_timePerFrame;
	sf::Time m_timeSinceLastUpdate;
	sf::RectangleShape lineToPass;
	sf::RectangleShape ground;
	
	float m_hight;
	double m_timeTaken;

	sf::Text m_perdictedTime;
	sf::Text m_perdictedHight; 
	sf::Text m_actualTime;
	sf::Text m_actualHight;
	sf::Font m_font;
};

