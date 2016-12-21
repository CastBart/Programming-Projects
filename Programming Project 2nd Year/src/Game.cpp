#include "Game.h"

/// <summary>
/// @Programmer: Bartosz Zych
/// @Student Id: C00205464
/// @date June 2016
/// @version 1.0
/// @Approximate time: 35~hrs
/// </summary>

// Updates per milliseconds
static double const MS_PER_UPDATE = 10.0;

/// <summary>
/// @brief Default constructor.
/// 
/// Create a new window and initialise member objects.
/// </summary>
/// 
Game::Game()
	: m_window(sf::VideoMode(1440, 900, 32), "SFML Playground", sf::Style::Default),
	m_countDown(60),
	m_targetHits(0),
	m_targetCount(0)
{	
	m_window.setVerticalSyncEnabled(true);
	int currentLevel = 1;
	if (!LevelLoader::load(currentLevel, m_level))
	{
		return;
	}
	
	
	//loading the texture of the tank using sprite sheet
	loadFromFile();
	m_BSprite.setTexture(m_BTexture);
	
	//setting up font
	m_timerText.setFont(m_timerFont);
	m_accuracyText.setFont(m_displayFont);
	m_scoreText.setFont(m_displayFont);
	m_highScoreText.setFont(m_displayFont);
	//timer text
	m_timerText.setCharacterSize(30);
	m_timerText.setPosition(sf::Vector2f(750, 20));
	m_timerText.setColor(sf::Color::Green);
	//accuracy text
	m_accuracyText.setCharacterSize(30);
	m_accuracyText.setPosition(sf::Vector2f(20, 450));
	m_accuracyText.setColor(sf::Color::White);
	// score text
	m_scoreText.setCharacterSize(30);
	m_scoreText.setPosition(sf::Vector2f(1050,20));
	m_scoreText.setColor(sf::Color::Green);
	// highscore text
	m_highScoreText.setCharacterSize(30);
	m_highScoreText.setPosition(sf::Vector2f(550, 200));
	m_highScoreText.setColor(sf::Color::White);

	m_explosionSprite.setTexture(m_explosionTexture);
	m_explosionSprite.scale(0.7, 0.7);
	m_explosionSprite.setTextureRect(sf::IntRect(0, 0, 1, 1));

	m_endScreenBGSprite.setTexture(m_endScreenBGTexture);

	//setting the tank sprite
	m_tank.reset(new Tank(m_Texture, m_level.m_tank.m_position, m_keyHandler, m_level));
	

	//animation 
	sf::IntRect explosionRect(0, 0, 156, 158);
	m_explosionSprite.setOrigin(explosionRect.width / 2, explosionRect.height / 2);
	for (int i = 0; i < 3900 ; i +=156) 
	{
		explosionRect.left = i;
		animation.addFrame(1.f, explosionRect);
	}

	animator.addAnimation("Explosion", animation, sf::seconds(1.5f));

	//create target
	std::unique_ptr<Target> target(new Target(m_targetTexture,m_level.m_targets.at(m_targetCount).m_position, m_level));
	m_targets.push_back(std::move(target));
	m_targetCount++;
	generateWalls();
	
	
}



/// <summary>
/// Main game entry point - runs until user quits.
/// </summary>
void Game::run()
{
	sf::Clock clock;
	sf::Int32 lag = 0;
	
	
	while (m_window.isOpen())
	{
		m_elapsed = clock.restart();
	
		lag += m_elapsed.asMilliseconds();				// adds time that has passed from last cycle. Used for the update
		m_accumulatedTime += m_elapsed.asSeconds();		//adds time that has passed from last cycle. Used for game timer
		
		if (m_accumulatedTime > 1.0) //resets accumulated time to 0 and take away a second from timer
		{
			m_accumulatedTime = 0;
			m_countDown--;
		}
		processEvents();

		while (lag > MS_PER_UPDATE) //update loop
		{
			update(MS_PER_UPDATE);
			lag -= MS_PER_UPDATE;
		}
		update(MS_PER_UPDATE);
		render();
	}
}



/// <summary>
/// @brief Check for events
/// 
/// Allows window to function and exit. 
/// Events are passed on to the Game::processGameEvents() method
/// </summary>
void Game::processEvents()
{
	sf::Event event;
	while (m_window.pollEvent(event))
	{
		if (event.type == sf::Event::Closed)
		{
			m_window.close();
		}
		processGameEvents(event);
	}
}

/// <summary>
/// @brief Handle all user input.
/// 
/// Detect and handle keyboard input.
/// </summary>
/// <param name="event">system event</param>
void Game::processGameEvents(sf::Event& event)
{
	switch (event.type)
	{
	case sf::Event::KeyPressed:
	    m_keyHandler.updateKey(event.key.code, true);
		break;
	case sf::Event::KeyReleased:
		m_keyHandler.updateKey(event.key.code, false);
		break;
	default:
		break;
	}
}

/// <summary>
/// Method to handle all game updates
/// </summary>
/// <param name="time">update delta time</param>
void Game::update(double dt)
{
	switch (currentGameState)
	{
	case GameState::Play:
		playUpdate(dt);
		break;
	case GameState::EndScreen:
		endGameUpdate(dt);
		break;
	default:
		break;
	}
}


/// <summary>
/// @brief draw the window for the game.
/// 
/// </summary>
void Game::render()
{
	m_window.clear(sf::Color(0, 0, 0, 0));
	switch (currentGameState)
	{
	case GameState::Play:
		playDraw();
		break;
	case GameState::EndScreen:
		endGameDraw();
		break;
	default:
		break;
	}
	
	m_window.display();
}

/// <summary>
/// initialze the position and rotations of each wall sprite
/// </summary>
/// <returns></returns>
void Game::generateWalls()
{
	sf::IntRect wallRect(2, 129, 33, 22);

	for (ObstacleData const &obstacle : m_level.m_obstacles)
	{
		std::unique_ptr<sf::Sprite> sprite(new sf::Sprite());
		sprite->setTexture(m_Texture);
		sprite->setTextureRect(wallRect);
		sprite->setOrigin(wallRect.width / 2.0, wallRect.height / 2.0);
		sprite->setPosition(obstacle.m_position);
		sprite->setRotation(obstacle.m_rotation);
		m_wallSprites.push_back(std::move(sprite));
	}
}

/// <summary>
/// Checks collision between tank and wall sprites
/// it uses a ranged based for loop to iritate through the container of wall spites
/// to see if it has collided with the tank
/// </summary>
/// <returns></returns>
bool Game::checkTankWallCollision()
{
	for (std::unique_ptr<sf::Sprite> const & sprite : m_wallSprites)
	{
		if (CollisionDetector::collision(m_tank->getTurretSprite(), *sprite))
		{
			return true;
		}
	}
	return false;
}

/// <summary>
/// Checks collison between wall sprites and bullet sprites
/// using a nested raged for loop to iritate through each bullet and each wall
/// </summary>
/// <returns></returns>
bool Game::checkBulletWallCollision()
{
	for (auto  & bullet : m_tank->getBullet())
	{
		for (auto const & sprite : m_wallSprites )
		{
			if (bullet->getAlive())
			{
				if (CollisionDetector::collision(bullet->getBulletSprite(), *sprite))
				{
					bullet->dead();
					
					m_explosionSprite.setPosition(bullet->getPosition());
					animator.playAnimation("Explosion");
					return true;
				}
			}
		}
	}
	return false;
}

/// <summary>
/// Check for collison between target and the bullets
/// <summary>
bool Game::checkBulletTargetCollision()
{
	for (auto & bullet : m_tank->getBullet())
	{

		for (auto &target : m_targets)
		{
			if (bullet->getAlive() && target->getAlive())
			{
				if (CollisionDetector::collision(target->getSprite(), bullet->getBulletSprite()))
				{
					m_score += 100 + (target->getTimeLeft() * 10);
					bullet->dead();
					target->setAlive(false);
					m_explosionSprite.setPosition(target->getSprite().getPosition());
					
					animator.playAnimation("Explosion");
					
					return true;
				}
			}
		}
	}
	return false;
}

/// <summary>
/// adds new target to the vector array if previous 
/// target is dead(alive = false)
/// <summary>
void Game::spawnNextTarget()
{
	if (!m_targets.at(m_targetCount - 1)->getAlive())
	{
		std::unique_ptr<Target> addTarget(new Target(m_targetTexture, m_level.m_targets.at(m_targetCount).m_position, m_level, m_targets.at(m_targetCount-1)->getTimeLeft()));
		m_targets.push_back(std::move(addTarget));
	}
}


/// <summary>
/// Loading all textures,fonts etc to the program
///  this function is called at the begining of the program
/// </summary>
void Game::loadFromFile()
{
	if (!m_Texture.loadFromFile(".\\resources\\images\\SpriteSheet.png"))
	{
		std::string s("Error loading texture");
		throw std::exception(s.c_str());
	}

	if (!m_timerFont.loadFromFile(".\\resources\\fonts\\timer.ttf"))
	{
		std::string s("Error loading texture");
		throw std::exception(s.c_str());
	}

	if (!m_displayFont.loadFromFile(".\\resources\\fonts\\font.ttf"))
	{
		std::string s("Error loading texture");
		throw std::exception(s.c_str());
	}

	if (!m_BTexture.loadFromFile(m_level.m_background.m_fileName))
	{
		std::string s("Error loading texture");
		throw std::exception(s.c_str());
	}

	if (!m_explosionTexture.loadFromFile(".\\resources\\images\\ExplosionSP.png"))
	{
		std::string s("Error loading texture");
		throw std::exception(s.c_str());
	}

	if (!m_targetTexture.loadFromFile(".\\resources\\images\\pokeball.png"))
	{
		std::string s("Error loading texture");
		throw std::exception(s.c_str());
	}

	if (!m_endScreenBGTexture.loadFromFile(".\\resources\\images\\EndGameScreen.jpg"))
	{
		std::string s("Error loading texture");
		throw std::exception(s.c_str());
	}

	m_highScoreNode = YAML::LoadFile(".\\resources\\highscore\\HighScore.yaml");
}


/// <summary>
/// checks if the bullet left the screen
/// if yes make it not dead
/// </summary>
void Game::bulletScreenCollision()
{
	for (auto &bullet : m_tank->getBullet())
	{
		if (bullet->getAlive())
		{
			if (bullet->getPosition().x <= 0 || bullet->getPosition().x >= m_window.getSize().x ||
				bullet->getPosition().y <= 0 || bullet->getPosition().y >= m_window.getSize().y)
			{
				bullet->dead();
			}
		}
	}
}

/// <summary>
/// calculates the % accuracy by dividing targets hits with bullets fired 
/// </summary>
int Game::calcAccuracy()
{
	if (m_targetHits == 0 || m_tank->getBulleTCounter() == 0 )
	{
		return 0; //rerturn 0 if any of the values are 0
	}
	else
	{
		return ((m_targetHits * 100) / m_tank->getBulleTCounter());
	}
}

/// <summary>
/// calculates the final score by adding acumulated
/// score during gameplay with time left*100 
/// </summary>
int Game::calcScore()
{
	m_score += (m_countDown * 100);
	return m_score;
}

void Game::highScoreYaml()
{	
	int yamlScore = m_highScoreNode["HighScore"]["score"].as<int>();
	if (m_score >= yamlScore)
	{
		m_highScoreText.setString("High Score: " + std::to_string(m_score));
		m_highScoreNode["HighScore"]["score"] = m_score; // edit one of the nodes 
		std::ofstream osstream(".\\resources\\highscore\\HighScore.yaml");
		osstream << m_highScoreNode; // dump it back into the file
		osstream.close();
	}
	else
	{
		m_highScoreText.setString("High Score: " + std::to_string(yamlScore));
	}
}

/// <summary>
/// updating everything that needs to be
/// updated while in playing game state
/// </summary>
void Game::playUpdate(double dt)
{
	bulletScreenCollision();							//checks for screen bullet collisions
	m_scoreText.setString("Current Score " + std::to_string(m_score));		//update score string to be displayed on the screen

	m_timerText.setString(std::to_string(m_countDown));		//set timer string to current time left
	for (auto &target : m_targets)						//update targets
	{
		target->update(dt);
	}			
	m_tank->update(dt);				 //update tank
	if (checkTankWallCollision())	//check for tank wall collision
	{
		m_tank->setTankCollided(true);
		m_tank->collision();
	}
	else
	{
		m_tank->setTankCollided(false);
	}
	//bullet-wall collision
	checkBulletWallCollision();			//check if bullet/wall collision happened
	
	//bullet-target collision
	if (checkBulletTargetCollision())	//check if target collided with bullet
	{
		m_targetHits++;
	}

	if (m_countDown <= 0)				//check if the time is less than 0 if yes change game state
	{
		currentGameState = GameState::EndScreen;
	}
	if (!m_targets.at(m_targetCount - 1)->getAlive())	//checks if all targets are destoyed
	{
		
		if (m_targetCount >= 15)
		{
			currentGameState = GameState::EndScreen;
		}
		else
		{
			spawnNextTarget();
			m_targetCount++;
		}
	}

	

	animator.update(sf::milliseconds(static_cast<sf::Int32>(dt)));	//update the animations
	if (animator.isPlayingAnimation())								//check if the explosion animation is playing
	{
		animator.animate(m_explosionSprite);
	}
}

/// <summary>
/// draws everything that needs to be
/// drawn while game is in playing game state
/// </summary>
void Game::playDraw()
{
	m_window.draw(m_BSprite);

	m_tank->render(m_window);

	//drawing the walls
	for (auto &walls : m_wallSprites)
	{
		m_window.draw(*walls);
	}
	if (animator.isPlayingAnimation())
	{
		m_window.draw(m_explosionSprite);
	}
	for (auto &target : m_targets)
	{
		target->render(m_window);
	}
	m_window.draw(m_scoreText);
	m_window.draw(m_timerText);
}

/// <summary>
/// updating everything that needs to be
/// updated while in endgame game state
/// </summary>
void Game::endGameUpdate(double dt)
{
	if (!m_setFinalScore)	//loop will be performed just once
	{	
		m_scoreText.setPosition(sf::Vector2f(750, 450));										//chaning postion of score text
		m_scoreText.setColor(sf::Color::White);													//changing color of the string
		m_scoreText.setString("Your Score is: " + std::to_string(calcScore()));					//setting score string and calculating final score
		m_accuracyText.setPosition(sf::Vector2f(300, 450));										//changing position of accuracy
		m_accuracyText.setString("Your Accuracy is " + std::to_string(calcAccuracy()) + "%");	//setting accuracy string by calculating accuracy % 
		m_setFinalScore = true;
		highScoreYaml();
	}
	
	
}

/// <summary>
/// draws everything that needs to be
/// drawn while in endgame game state
/// </summary>
void Game::endGameDraw()
{
	m_window.draw(m_endScreenBGSprite);
	m_window.draw(m_accuracyText);
	m_window.draw(m_scoreText);
	m_window.draw(m_highScoreText);
}

