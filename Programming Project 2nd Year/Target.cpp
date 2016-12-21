#include "Target.h"

//NOTE: dont forget to make m_timeLeft = m_timeUptime in update
/// <summary>
/// default target constructor
/// </summary>
Target::Target()
{
}


/// <summary>
/// overloaded contructor
/// used for creating a new target
/// when a players hits previous target
/// or previous target ran out of time
/// </summary>
Target::Target(sf::Texture const & texture, sf::Vector2f  & pos, LevelData & lvl, int const & timeFromLastTarget) :
	m_level(lvl),
	m_timeUptime(6 + timeFromLastTarget),
	m_alive(true),
	m_timeLeft(m_timeUptime),
	m_accumulatedTime(0),
	m_offset(0.0f)
{
	m_offset = m_level.m_targets.at(0).m_offset;
	initSprite(texture, pos);
}

/// <summary>
/// targets time left getter
/// </summary>
int Target::getTimeLeft() const
{
	return m_timeLeft;
}

/// <summary>
/// updates the targts values
/// </summary>
void Target::update(double dt)
{
	

	if (m_alive)
	{
		m_elapsed = m_clock.restart();
		m_accumulatedTime += m_elapsed.asSeconds();
		if (m_accumulatedTime > 1.0)
		{
			m_accumulatedTime = 0;
			m_timeUptime--;
			m_timeLeft = m_timeUptime;
		}

	}
	targetOutOfTime();
	if (m_timeUptime <= 0)
	{
		m_alive = false;
	}

}

/// <summary>
/// draw funtion of the target
/// </summary>
void Target::render(sf::RenderWindow & window)
{
	if (m_alive)
	{
		window.draw(m_sprite);
	}
}

/// <summary>
/// targets sprite getter
/// used for collisions 
/// </summary>
sf::Sprite Target::getSprite() const
{
	return m_sprite;
}


/// <summary>
/// alive bool setter
/// </summary>
void Target::setAlive(bool alive)
{
	m_alive = alive;
}

/// <summary>
/// alive bool getter
/// </summary>
bool Target::getAlive()
{
	return m_alive;
}

/// <summary>
/// returns true if the time of the target
/// is less than 0 else false
/// </summary>
bool Target::timeGone()
{
	if (m_timeLeft <= 0)
	{
		return true;
	}
	return false;
}



/// <summary>
/// Logic for target running out of time
/// </summary>
void Target::targetOutOfTime()
{
	if (m_timeUptime <= 3)
	{
		if (scaleDown)									//scales down target if timmer	
		{												//gets to 3 and scales up if it
			m_sprite.scale(0.97f, 0.97f);				//reaches a certin scale amount
			if (m_sprite.getScale().x <= 0.3f)
			{
				scaleDown = false;
			}
		}
		else
		{
			m_sprite.scale(1.03f, 1.03f);
			if (m_sprite.getScale().x >= 1.0f)
			{
				scaleDown = true;
			}
		}
	}
}

Target::~Target()
{

}


/// <summary>
/// ginitializes the sprite of the bullet
/// using texture which is passed into the object 
/// from the game class 
/// </summary>
void Target::initSprite(sf::Texture const &texture, sf::Vector2f  & pos)
{
	m_offset *= 2;
	srand(time(NULL));
	int rndX = rand() % ((m_offset) + 1)  + (-m_offset / 2 + 1);
	int rndY = rand() % ((m_offset) + 1) + (-m_offset / 2 + 1);
	m_sprite.setTexture(texture);
	sf::IntRect targetRect(0, 0, 50, 50);
	m_sprite.setTextureRect(targetRect);
	m_sprite.setOrigin(targetRect.width / 2.0, targetRect.height / 2.0);
	pos = sf::Vector2f(pos.x + static_cast<float>(rndX), pos.y + static_cast<float>(rndY));
	m_sprite.setPosition(pos);
}


