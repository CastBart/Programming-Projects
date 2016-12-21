#include <iostream>
#include <SFML/Window.hpp>
#include <SFML/OpenGL.hpp>
#include <gl/GL.h>
#include <gl/GLU.h>
#include "Matrix3.h"
#include "Vector3.h"


using namespace std;


class Game
{
public:
	Game();
	~Game();
	void run();
private:
	sf::Window window;
	bool isRunning = false;
	void initialize();
	void update();
	void render();
	void unload();
	void transformCube(Matrix3 &);
	void translatePoints(double translation, const Matrix3::Axis &axis);

	sf::Clock clock;
	sf::Time elapsed;
	Vector3 m_cubePoints[8];
	Matrix3 matrix;

	float rotationAngle = 0.0f;
};