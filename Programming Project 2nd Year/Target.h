#pragma once
#include <SFML\Graphics.hpp>
#include "LevelLoader.h"

class Target
{
public:
	Target();
	Target(sf::Texture const &, sf::Vector2f  &, LevelData &, int const & time = 0);
	
	//getters
	bool getAlive();				//getter for checking if target is alive or not
	int getTimeLeft() const;		//gets the time left of targets life
	sf::Sprite getSprite()const;	//getsthe sprite of the target. Used for collisions

	//setters
	void setAlive(bool);	//setter of alive variable
	
	//class functions
	bool timeGone();						//checks if the time of the target is gone or not	
	void targetOutOfTime();					//method that handles the indication of target nearly being out of time
	void update(double dt);					// targets update
	void render(sf::RenderWindow & window);	//targets draw

	~Target();
private:
	void initSprite(sf::Texture const &, sf::Vector2f  & pos); 
	//class variables
	sf::Sprite m_sprite;		//sprite of the target
	sf::Texture m_texture;		//texture of the target
	sf::Time m_elapsed;			//targets time.Used of targts live spam
	sf::Clock m_clock;			//targets time.Used of targts live spam
	LevelData m_level;			//level date used to read targets positions and offset

	int m_timeLeft;				//indicates how long the target is alive for
	int m_timeUptime;			//holds the value of time alive
	int m_offset;				//offset to the targets position
	bool m_alive;				//alive or dead bool
	bool scaleDown = true;		//a bool that indicates if the target was sclad down or not. Used to indicate target nearly running out of time
	float m_accumulatedTime;	//holds the value of current time that the target is alive 
};

