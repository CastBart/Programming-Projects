#ifndef GAME_H
#define GAME_H

#include <iostream>
#include <GL/glew.h>
#include <GL/wglew.h>


#include <glm/glm.hpp>

#include <glm/gtc/matrix_transform.hpp>


#include <SFML/Window.hpp>
#include <SFML/OpenGL.hpp>

#include <Debug.h>

#define STB_IMAGE_IMPLEMENTATION
#include <stb_image.h>

#include "ProgramIds.h"
#include "Cube.h"
#include <fstream>



class Game
{
public:
	Game();
	Game(sf::ContextSettings settings);
	~Game();
	void run();

	sf::Window window;
	bool isRunning = false;
	void initialize();
	void update(double);
	void render();
	void unload();
	void createCubes();
	void deleteCubes();
	void checkCollision();
	std::string loadShader(const std::string & fileName);
	enum class GameState { Playing, Gameover};
	GameState currentState = GameState::Playing;
private:
	
	glm::mat4 cameraView;
	Cube m_player; //player
	std::vector<std::unique_ptr<Cube>> m_cubes; //obstacles
	ProgramIds m_ids; //ids of the game 

	sf::Time m_elapsed;
	float nextWave = 0;

	const sf::Time MS_PER_UPDATE = sf::seconds(1 / 60.0f);
	const int SPAWN_DISTANCE_FROM_CENTRE = -100;
	const int GAP_BETWEEN_CUBES = 5;

};

#endif