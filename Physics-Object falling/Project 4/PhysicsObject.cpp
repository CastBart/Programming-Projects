#include "PhysicsObject.h"



PhysicsObject::PhysicsObject() :
	m_circle(0.5f),
	m_position(400, 579),
	m_velocity(0, 0),
	m_isMoving(false)
{
}


PhysicsObject::~PhysicsObject()
{
}

sf::Vector2f PhysicsObject::getPos() const
{
	return m_position;
}

sf::Vector2f PhysicsObject::getVel() const
{
	return m_velocity;
}

void PhysicsObject::render(sf::RenderWindow &window)
{
	window.draw(m_circle);
}

void PhysicsObject::update(double  dt, sf::Vector2f gravity)
{

	m_velocity.y += gravity.y *dt;
	m_circle.setPosition(m_position.x, m_position.y += m_velocity.y*dt + ((0.5*gravity.y) * ((dt*dt))));

}

sf::CircleShape PhysicsObject::getCricle()
{
	return m_circle;
}

bool PhysicsObject::getIsMoving()
{
	return m_isMoving;
}

void PhysicsObject::setIsMoving(bool moving)
{
	m_isMoving = moving;
}

void PhysicsObject::setPos(sf::Vector2f & pos)
{
	m_circle.setPosition(pos);
}

void PhysicsObject::setVel(sf::Vector2f & vel)
{
	m_velocity = vel;
}
