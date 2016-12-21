#include "Tank.h"
#include "Thor\Math.hpp"

/// <summary>
/// overloaded contructor that takes in a
/// texture(spritesheet), vector(position)
///keyhandles(handles key presses) and a leveldata(is later passed on to bullet)
/// </summary>
Tank::Tank(sf::Texture const & texture, sf::Vector2f const & pos, KeyHandler & keyHandler, LevelData & level):
																			// constructor to create the tank object
	M_TEXTURE(texture),														// tankes in texture, posit	
	m_speed(0),
	m_rotation(0),
	m_tRotation(0),
	m_tankCollided(false),
	m_keyHandler(keyHandler),
	m_level(level),
	m_reloadTime(0),
	m_fireAvailable(true)
{
	initSprites(pos);
}


/// <summary>
/// updates values of the tank. such as position rotation, speed etc.
/// </summary>
void Tank::update(double dt)
{	
	
	handleKeyInput();	//handles key presses

	for (auto &bullet : m_bulletPTR) //updates bullets
	{
		bullet->update(dt);
	}
	m_tankBase.setPosition(     //updates tanks position each cycle
		m_tankBase.getPosition().x + cos(m_rotation*DEG_TO_RAD) * m_speed * (dt / 1000),
		m_tankBase.getPosition().y + sin(m_rotation*DEG_TO_RAD) * m_speed * (dt / 1000)
	);

	m_turret.setPosition(m_tankBase.getPosition().x, m_tankBase.getPosition().y); //updates tanks turret position each cycle
	m_tankBase.setRotation(m_rotation);                                           //updates tanks rotation each cycle
	m_turret.setRotation(m_rotation + m_tRotation);								  //updates tanks turret rotation each cycle
	

}

/// <summary>
/// draw function for tank which draws the tanks base
/// tanks turret and bullets
/// </summary>
void Tank::render(sf::RenderWindow & window) 
{
	for (auto &bullet : m_bulletPTR)
	{
		bullet->render(window);
	}
	window.draw(m_tankBase);
	window.draw(m_turret);

}

/// <summary>
/// initializing sprites for the tank
/// position of the sprite, rotation etc.
/// </summary>
void Tank::initSprites(sf::Vector2f const & pos)
{
	// Initialise the tank base
	m_tankBase.setTexture(M_TEXTURE);
	sf::IntRect baseRect(2, 43, 79, 43);
	m_tankBase.setTextureRect(baseRect);
	m_tankBase.setOrigin(baseRect.width / 2.0, baseRect.height / 2.0);
	m_tankBase.setPosition(pos);

	

	// Initialise the turret
	m_turret.setTexture(M_TEXTURE);
	sf::IntRect turretRect(19, 1, 83, 31);
	m_turret.setTextureRect(turretRect);
	m_turret.setOrigin(turretRect.width / 3.0, turretRect.height / 2.0);
	m_turret.setPosition(pos);

}
double const Tank::DEG_TO_RAD = thor::Pi / 180.0f; //declaring a constant that converts degrees to radians


/// <summary>
/// increase speed of th tank
/// </summary>
void Tank::increaseSpeed()
{
	if (m_speed < 100.0)
	{
		m_speed += 0.1;
	}
}

/// <summary>
/// decreases the speed of the tank
/// </summary>
void Tank::decreaseSpeed()
{
	if (m_speed > -100.0)
	{
		m_speed -= 0.1;
	}
}

/// <summary>
/// increases th rotation of the tank by adding 1 if the turret is not collided
/// and if the tank is collided it brings the tank back a bit then rotates it
/// </summary>
void Tank::increaseRotation()
{
	if (!m_tankCollided)
	{
		m_rotation += 1;
		if (m_rotation == 360.0)
		{
			m_rotation = 0;
		}
	}
	else
	{
		m_speed = -1;
		m_tankBase.setPosition(
			m_tankBase.getPosition().x + cos(m_rotation*DEG_TO_RAD) * m_speed,
			m_tankBase.getPosition().y + sin(m_rotation*DEG_TO_RAD) * m_speed
		);
		m_rotation += 1;
		m_speed = 0;
		if (m_rotation == 360.0)
		{
			m_rotation = 0;
		}
	}
}


/// <summary>
/// decreases th rotation of the tank by subtracts 1 if the turret is not collided
/// and if the tank is collided it brings the tank back a bit then rotates it
/// </summary>
void Tank::decreaseRotation()
{
	if (!m_tankCollided)
	{
		m_rotation -= 1;
		if (m_rotation == 0.0)
		{
			m_rotation = 359.0;
		}
	}
	else 
	{
		m_speed = -1;
		m_tankBase.setPosition(
			m_tankBase.getPosition().x + cos(m_rotation*DEG_TO_RAD) * m_speed,
			m_tankBase.getPosition().y + sin(m_rotation*DEG_TO_RAD) * m_speed
		);
		m_rotation -= 1;
		m_speed = 0;
		
		if (m_rotation == 360.0)
		{
			m_rotation = 0;
		}
	}
}

/// <summary>
/// increases th rotation of the tanks turret by adding 1 
/// </summary>
void Tank::increaseTRotation()
{
	m_tRotation += 1;
	if (m_tRotation == 360.0)
	{
		m_tRotation = 0;
	}
}

/// <summary>
/// decreases th rotation of the tanks turret by subtracting 1 
/// </summary>
void Tank::decreaseTRotation()
{
	m_tRotation -= 1;
	if (m_tRotation == 0.0)
	{
		m_tRotation = 359.0;
	}
}

/// <summary>
/// ihandles key input and response
/// </summary>
void Tank::handleKeyInput()
{
	if (m_keyHandler.isPressed(sf::Keyboard::Up))
	{
		increaseSpeed();
	}
	if (m_keyHandler.isPressed(sf::Keyboard::Down))
	{
		decreaseSpeed();
	}
	if (m_keyHandler.isPressed(sf::Keyboard::Left))
	{
		decreaseRotation();
	}
	if (m_keyHandler.isPressed(sf::Keyboard::Right))
	{
		increaseRotation();
	}
	if (m_keyHandler.isPressed(sf::Keyboard::X))
	{
		decreaseTRotation();
	}
	if (m_keyHandler.isPressed(sf::Keyboard::Z))
	{
		increaseTRotation();
	}
	if (m_keyHandler.isPressed(sf::Keyboard::Space)) //creates a bullet when a space is pressed and a condition is true
	{
		if (m_fireAvailable) //can shoot every 1 sec
		{
			
			m_reloadTime = 0;
			std::unique_ptr<Bullet> bullet(new Bullet(M_TEXTURE, m_tankBase.getPosition(),  
										   m_level, m_turret.getRotation()));
			m_bulletPTR.push_back(std::move(bullet));
			m_bulletCounter++;			//bullet counter is increased
			m_fireAvailable = false;	//cant shoot again for 1 sec
		}
	}
	m_reloadTime += m_clock.restart().asMilliseconds();	//adds time in miliseconds 

	if (m_reloadTime >= 1000)	//can shoot again after 1 sec
	{
		m_fireAvailable = true;
	}

}

/// <summary>
/// returns tanks base sprite
/// </summary>
sf::Sprite const & Tank::getTurretSprite() const
{
	return m_tankBase;
}

/// <summary>
/// sets the value of tank collision bool
/// </summary>
void Tank::setTankCollided(bool collisions)
{
	m_tankCollided = collisions;
}

/// <summary>
/// responses between tank and wall sprites
///
/// it brings back the tank 1 pixels in the in the opposite direction its facing
/// </summary>
void Tank::collision()
{
	m_tankCollided = true;
	if (m_speed < 0)
	{
		m_speed = 1;
		m_tankBase.setPosition(
			m_tankBase.getPosition().x + cos(m_rotation*DEG_TO_RAD) * m_speed,
			m_tankBase.getPosition().y + sin(m_rotation*DEG_TO_RAD) * m_speed
		);
		m_speed = 0;
	}
	else
	{
		m_speed = -1;
		m_tankBase.setPosition(
			m_tankBase.getPosition().x + cos(m_rotation*DEG_TO_RAD) * m_speed,
			m_tankBase.getPosition().y + sin(m_rotation*DEG_TO_RAD) * m_speed
		);
	}
	m_speed = 0;
}


/// <summary>
/// returns the number of bullets created in game
/// </summary>
int const & Tank::getBulleTCounter() const
{
	return m_bulletCounter;
}


/// <summary>
/// returns bullets vector of unique pointers of bullet objects
/// </summary>
std::vector<std::unique_ptr<Bullet>>  & Tank::getBullet()
{
	return m_bulletPTR;
}


