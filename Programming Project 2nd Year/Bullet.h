#pragma once
#include <SFML\Graphics.hpp>
#include "KeyHandler.h"
#include "LevelLoader.h"
class Bullet
{
public:
	Bullet(sf::Texture const & texture, sf::Vector2f const & pos,LevelData & level , double const & rotation);
	
	
	double static const DEG_TO_RAD; 
	
	//class functions
	 
	void update(double dt);
	void render(sf::RenderWindow & window);

	//getters
	bool getAlive();							//gets the current alive
	sf::Vector2f getPosition();					//gets the currents position of bullet
	sf::Sprite const & getBulletSprite() const;	//gets bullets sprite. used for collisions

	//setters
	void dead();	//set alive to false. Used for collisions

private:
	void initSprite(sf::Vector2f const & pos, double const & rot);	
	
	//class variables
	sf::Sprite m_sprite;			//sprite of the bullet
	sf::Texture const & M_TEXTURE;	//texture of the bullet
	double m_speed;					//speed of the bullet
	double m_damage;				//damage of the bullet. Not used yet(i added it now as we will remake the game and will be used in the future version
	double m_rotation;				//rotation of the bullet
	bool m_alive;					//bullets alive or dead variable
	
	
};

