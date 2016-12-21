#pragma once
#include <SFML\Graphics.hpp>

class PhysicsObject
{
public:
	PhysicsObject();
	~PhysicsObject();
	sf::Vector2f getPos()const;
	sf::Vector2f getVel()const;
	sf::CircleShape getCricle();
	bool getIsMoving();
	void setIsMoving(bool);
	void render(sf::RenderWindow &);
	void update(double, sf::Vector2f);
	
	void setPos(sf::Vector2f &);
	void setVel(sf::Vector2f &);

private:
	sf::CircleShape m_circle;
	sf::Vector2f m_position;
	sf::Vector2f m_velocity;

	bool m_isMoving;
};

