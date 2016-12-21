#pragma once
#include <SFML/Graphics.hpp>
#include "KeyHandler.h"
#include "Bullet.h"


/// <summary>
/// @brief A simple tank controller.
/// 
/// This class will manage all tank movement and rotations.
/// </summary>
class Tank
{
public:	
	Tank(sf::Texture const & texture, sf::Vector2f const & pos, KeyHandler & keyHandler, LevelData & level); //overloaded contructor 
	
	
	//class functions
	void increaseSpeed();			//increases speed of the tank
	void decreaseSpeed();			//deacreases speed of the tank
	void increaseRotation();		//increases the rotation on the tank
	void decreaseRotation();		//decreases the rotation of the tank
	void increaseTRotation();		//increase rotation of the turret
	void decreaseTRotation();		//decrease the rotation of the turret
	void handleKeyInput();			//handles the input of the user
	void collision();				//perform the collision responses

	void update(double dt);						//updates the tank object
	void render(sf::RenderWindow & window);		//draws the tank object

	
	double static const DEG_TO_RAD;		//static used to convert degrees to radians
	
	//getters
	std::vector<std::unique_ptr<Bullet>>  & getBullet();	//returns vector of unique points that contain bullet object. Used in collisions
	sf::Sprite const & getTurretSprite() const;				//returns the sprite of the sprite of the tank. Used for collisions
	int const & getBulleTCounter()const;					//returns the number of bullets created.Used for accuracy calculations

	//setters
	void setTankCollided(bool);	//sets the value of m_tankCollided

private:
	void initSprites(sf::Vector2f const & pos);
	//constants
	sf::Texture const & M_TEXTURE;	// sprite sheet	

	sf::Sprite m_tankBase;	//sprite of the tanks base
	sf::Sprite m_turret;	//sprite of the tanks turret
	sf::Clock m_clock;		//clock. Used for firing bullets every seccond
	
	int m_reloadTime;		//intervals of tanks ability to shoot
	int m_bulletCounter;	//number of bullets created
	double m_speed;			//speed of the tank
	double m_rotation;		//rotation of the tank
	double m_tRotation;		//rotation of the tanks turret
	bool m_fireAvailable;	//bool to represnt abilty to shoot a bullet
	bool m_tankCollided;	//represents if there is collision between tank and wall
	//object variables
	KeyHandler & m_keyHandler;
	LevelData m_level;
	std::vector<std::unique_ptr<Bullet>> m_bulletPTR;
	
	
	

};
