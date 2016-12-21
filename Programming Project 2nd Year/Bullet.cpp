#include "Bullet.h"
#include "Thor\Math.hpp"


Bullet::Bullet(sf::Texture const & texture, sf::Vector2f const & pos, LevelData & level, double const & rotation) :
	M_TEXTURE(texture),
	m_rotation(rotation),
	m_alive(true)
{
	m_speed = level.m_bullet.m_speed;
	m_damage = level.m_bullet.m_damage;
	initSprite(pos, rotation);
}


/// <summary>
/// Update call for bullet
/// updates the sprites position if alive
/// </summary>
void Bullet::update(double dt)
{
	if (m_alive)
	{
		m_sprite.setPosition(		//change the position of the bullet at a current rotation value
			m_sprite.getPosition().x + cos(m_rotation*DEG_TO_RAD) * m_speed * (dt / 1000), 
			m_sprite.getPosition().y + sin(m_rotation*DEG_TO_RAD) * m_speed * (dt / 1000)
		);
	}
	
}

/// <summary>
/// draw funtion of the bullet
/// </summary>
void Bullet::render(sf::RenderWindow & window)
{
	if (m_alive)
	{
		window.draw(m_sprite);
	}
}



/// <summary>
/// sets the alive bool to false
/// </summary>
void Bullet::dead()
{
	m_alive = false;
}

/// <summary>
/// gets the current position of the sprite
/// </summary>
sf::Vector2f Bullet::getPosition()
{
	return m_sprite.getPosition();
}

/// <summary>
/// get the current alive bool
/// </summary>
bool Bullet::getAlive()
{
	return m_alive;
}

/// <summary>
/// gets the sprite of the bullet
/// </summary>
sf::Sprite const & Bullet::getBulletSprite() const
{
	return m_sprite;
}

///assign a deg to rad converter. Used for rotations and movement
double const Bullet::DEG_TO_RAD = thor::Pi / 180.0f;


/// <summary>
/// ginitializes the sprite of the bullet
/// using texture which is passed into the object 
/// from the game class 
/// </summary>
void Bullet::initSprite(sf::Vector2f const & pos, double const & rot)
{
	m_sprite.setTexture(M_TEXTURE);
	sf::IntRect bulletRect(9,178,4,7);
	m_sprite.setTextureRect(bulletRect);
	m_sprite.setOrigin(bulletRect.width / 2.0, bulletRect.height / 2.0);
	m_sprite.setPosition(pos);
	m_sprite.setRotation(rot-90);
}
