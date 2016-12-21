#pragma once

#include <SFML/Graphics.hpp>
#include "LevelLoader.h"
#include "Tank.h"
#include "CollisionDetector.h"
#include <Thor\Animations.hpp>
#include "Target.h"




/// <summary>
/// @brief Main class for the SFML Playground project.
/// 
/// This will be a single class framework for learning about SFML.
/// </summary>
class Game
{
public:
	Game();	
	void run();
protected:	
	enum class GameState  {	Play, EndScreen};
	GameState currentGameState = GameState::Play;
	void update(double dt);
	void render();
	void processEvents();	
	void processGameEvents(sf::Event&);
	void generateWalls();

	//collisions
	bool checkTankWallCollision();			// checks for collisions between tank and walls
	bool checkBulletWallCollision();		// checks for collisions between bullet and walls
	bool checkBulletTargetCollision();		// checks for collision between bullet and targets
	void bulletScreenCollision();			// response for bullet leaving the screen bounds
	

	// play updates & draw
	void playUpdate(double dt);	
	void playDraw();

	// end screen update & draw
	void endGameUpdate(double dt);
	void endGameDraw();

	//class funtions
	void spawnNextTarget();			//spwan target if time left on the previous one is 0
	void loadFromFile();			//loads everything i need form file
	int calcAccuracy();				//calculates accuracy %
	int calcScore();				//calculates score 
	void highScoreYaml();

	sf::RenderWindow m_window;

	//sprites
	sf::Sprite m_sprite;			//spritesheet sprite containing tank, bullet, wall
	sf::Sprite m_BSprite;			//background sprite
	sf::Sprite m_explosionSprite;	//explosion spritesheet
	sf::Sprite m_endScreenBGSprite;	//end screen sprite

	//textures
	sf::Texture m_Texture;				//spritesheet texture 
	sf::Texture m_BTexture;				//background texture
	sf::Texture m_explosionTexture;		//explosion texture
	sf::Texture m_endScreenBGTexture;	//end screen texture
	sf::Texture m_targetTexture;		//target texture
	//tank
	std::unique_ptr<Tank> m_tank;	//tank pointer
	
	//fonts
	sf::Font m_timerFont;	//font used for timer
	sf::Font m_displayFont;	//font used to display any text on the screen eg. score accuracy high score

	//texts
	sf::Text m_timerText;			//text used for timer
	sf::Text m_accuracyText;		//text used for displaying accuracy
	sf::Text m_scoreText;			//text used to display score
	sf::Text m_highScoreText;		//text to display highscore at the end of the game

	//thor variables for used for animations
	thor::Animator<sf::Sprite, std::string> animator;	//animator whish is used to run the animation
	thor::FrameAnimation animation;						// frameanimation to give "key frames" using the sprite sheet of the explosion

	//wall sprite vector
	std::vector<std::unique_ptr<sf::Sprite>> m_wallSprites;	//vector of unique pointers which hold sprite data

	//target vector
	std::vector<std::unique_ptr<Target>> m_targets;	//vector of unique pointers which hold the target object data

	//object variables
	LevelData m_level;			//used to acces data writen by yaml file
	KeyHandler m_keyHandler;	//handles input
	YAML::Node m_highScoreNode;	//declare a node used to overwrite data in highscore yaml file

	sf::Time m_elapsed;					//handles time
	int m_countDown;					//holds the time of the game
	int m_score;						//players score
	int m_targetHits;					//holds the number of targtes hits by player
	int m_targetCount;					//hold the number of targets created in the game
	float m_accumulatedTime;			//holds the value of accumulated time. it is used to stop player firing nonstop
	bool m_setFinalScore = false;		//a bool that indicates if final score was calculated
	
	
};
