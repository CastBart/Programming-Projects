#ifndef CUBE_H
#define CUBE_H

#include <iostream>
#include <gl/glew.h>
#include <gl/wglew.h>
#include <SFML/Window.hpp>
#include <SFML/OpenGL.hpp>
#include "Vector3.h"
#include "Matrix3.h"
#include <memory>
#include <time.h>


#include <glm/glm.hpp>
#include <glm/gtc/matrix_transform.hpp>
#include "ProgramIds.h"

//Cube Vertices
/*
		  (-1.0f, 1.0f, -1.0f)          (1.0f, 1.0f, -1.0f)
		          [7]                          [6]
		          #-----------------------------#
		         /|                            /|
		        / |                           / |
	  (-1.0f, 1.0f, 1.0f)           (1.0f, 1.0f, 1.0f)
		  [3] /                         [2] /
		     #-----------------------------#    |
		     |    |                        |    |
		     |    |                        |    |
		     |   [4]                       |   [5]
		  (-1.0f, -1.0f, -1.0f)         (1.0f, -1.0f, -1.0f)
		     |    #-----------------------------#
		     |   /                         |   /
		     |  /                          |  /
		     | /                           | /
		     |/                            |/
		     #-----------------------------#
		    [0]                           [1]
	(-1.0f, -1.0f, 1.0f)         (1.0f, -1.0f, 1.0f)
*/

//typedef struct
//{
//	float coordinate[3];
//	float color[4];
//	//float uvs[2];
//
//} Vertex;
const int VERTICES = 24;	// Total Number of Vertices
const int INDICES = 12;	// Total Number of Indexes
const int UVS = 48;	// Total Number of UVs
const int COLORS = 24;	// Total Number of Colors



class Cube
{
public:
	Cube(const glm::mat4&);
	Cube(const glm::mat4&, bool player, int offsetX = 0);

	~Cube();
	void render(ProgramIds& ids, glm::mat4& mpv);
	//void unload();
	void initialize(const sf::Vector3f playerPos);
	void update(double dt);

	static const GLuint indices[];
	static GLfloat vertices[];
	static const GLfloat colors[];
	

	Vector3 m_cubePoints[24];

	
	Matrix3 matrix;
	sf::Clock clock;
	sf::Time elapsed;

	Vector3 m_centrePoint;
	const glm::mat4& view;
	glm::mat4 model, projection, mvp;
	bool m_isPlayer;
	bool m_isBullet;
	float m_offsetZ;
	static float m_zMovement;
	float m_increaseSpeedTimer = 0;
	
	

	sf::Vector3f m_frontTopLeftPos;
	sf::Vector3f m_frontTopRightPos;
	sf::Vector3f m_frontBotLeftPos;
	sf::Vector3f m_frontBotRightPos;


	sf::Vector3f m_backTopLeftPos;
	sf::Vector3f m_backTopRightPos;
	sf::Vector3f m_backBotLeftPos;
	sf::Vector3f m_backBotRightPos;
};
#endif

