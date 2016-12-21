#include "Game.h"

bool flip = false;
int current = 1;

Game::Game() : window(sf::VideoMode(800, 600), "OpenGL Cube")
{

}

Game::~Game() {}

float vertices[] =
	{ //front face points
	  1.0f, 1.0f, 1.0f,		//top right			0
	 -1.0f, 1.0f, 1.0f,		//top left			1
	 -1.0f, -1.0f, 1.0f,	//bottom left		2
	  1.0f, -1.0f, 1.0f,	//bottom right		3
	  //back face points
	  1.0f, 1.0f, -1.0f,	//top right			4
	 -1.0f, 1.0f, -1.0f,	//top left			5
	 -1.0f, -1.0f, -1.0f,	//bottom left		6
	  1.0f, -1.0f, -1.0f };	//bottom right		7
float colors[] = {
	1.0f, 0.0f, 0.0f,
	0.0f, 1.0f, 0.0f,
	0.0f, 0.0f, 1.0f,
	0.0f, 1.0f, 0.0f,

	1.0f, 0.0f, 0.0f,
	0.0f, 1.0f, 0.0f,
	0.0f, 0.0f, 1.0f,
	0.0f, 1.0f, 0.0f
};
unsigned int vertex_index[] = { 
	7, 6, 5, // back face 
	5, 4, 7,

	0, 1, 5, // top face
	5, 4, 0,

	3, 2, 6, // bottom face
	6, 7, 3,

	5, 1, 2, // left face
	2, 6, 5,

	3, 0, 4, // right race
	3, 7, 4,

	0, 1, 2, //front face
	2, 3, 0
};

void Game::run()
{

	initialize();

	sf::Event event;

	while (isRunning) {

		cout << "Game running..." << endl;

		while (window.pollEvent(event))
		{
			if (event.type == sf::Event::Closed)
			{
				isRunning = false;
			}
			switch (event.type)
			{
			case sf::Event::KeyPressed:
				switch (event.key.code)
				{
				case sf::Keyboard::Up:
					//rotates ABOUT the x axis
					matrix = Matrix3::Rotation(-1, Matrix3::Axis::X);
					transformCube(matrix);
					break;
				case sf::Keyboard::Down:
					//rotates ABOUT the x axis
					matrix = Matrix3::Rotation(1, Matrix3::Axis::X);
					transformCube(matrix);
					break;
				case sf::Keyboard::Left:
					matrix = Matrix3::Rotation(-1, Matrix3::Axis::Y);
					transformCube(matrix);
					break;
				case sf::Keyboard::Right:
					matrix = Matrix3::Rotation(1, Matrix3::Axis::Y);
					transformCube(matrix);
					break;
				case sf::Keyboard::Comma:
					matrix = Matrix3::Rotation(1, Matrix3::Axis::Z);
					transformCube(matrix);
					break;
				case sf::Keyboard::Period:
					matrix = Matrix3::Rotation(-1, Matrix3::Axis::Z);
					transformCube(matrix);
					break;
				case sf::Keyboard::Add:
					matrix = Matrix3::Scale(1.1, 1.1, 1.1);
					transformCube(matrix);
					break;
				case sf::Keyboard::Subtract:
					matrix = Matrix3::Scale(0.9, 0.9, 0.9);
					transformCube(matrix);
					break;
				case sf::Keyboard::D:
					translatePoints(1, Matrix3::Axis::X);
					break;
				case sf::Keyboard::A:
					translatePoints(-1, Matrix3::Axis::X);
					break;
				default:
					break;
				}
				break;
			default:
				break;
			}
		}
		update();
		render();
	}

}

void Game::initialize()
{
	isRunning = true;

	//glClearColor(0.0f, 0.0f, 0.0f, 0.0f);
	glMatrixMode(GL_PROJECTION);
	glLoadIdentity();
	gluPerspective(45.0, window.getSize().x / window.getSize().y, 1.0, 500.0);
	glMatrixMode(GL_MODELVIEW);
}

void Game::update()
{
	elapsed = clock.getElapsedTime();

	cout << "Update up" << endl;
	m_cubePoints[0] = Vector3(vertices[0], vertices[1], vertices[2]);
	m_cubePoints[1] = Vector3(vertices[3], vertices[4], vertices[5]);
	m_cubePoints[2] = Vector3(vertices[6], vertices[7], vertices[8]);
	m_cubePoints[3] = Vector3(vertices[9], vertices[10], vertices[11]);
	m_cubePoints[4] = Vector3(vertices[12], vertices[13], vertices[14]);
	m_cubePoints[5] = Vector3(vertices[15], vertices[16], vertices[17]);
	m_cubePoints[6] = Vector3(vertices[18], vertices[19], vertices[20]);
	m_cubePoints[7] = Vector3(vertices[21], vertices[22], vertices[23]);

}

void Game::render()
{
	cout << "Drawing" << endl;
	glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT);

	glLoadIdentity();
	glTranslated(0.0, 0.0, -5.0);
	glEnableClientState(GL_VERTEX_ARRAY);
	glEnableClientState(GL_COLOR_ARRAY);


	glVertexPointer(3, GL_FLOAT, 0, &vertices);
	glColorPointer(3, GL_FLOAT, 0, &colors);

	//glDrawArrays(GL_TRIANGLES, 0, 3);

	glDrawElements(GL_TRIANGLES, 36, GL_UNSIGNED_INT, &vertex_index);

	glDisableClientState(GL_COLOR_ARRAY);
	glDisableClientState(GL_VERTEX_ARRAY);

	window.display();

}

void Game::unload()
{
	cout << "Cleaning up" << endl;
}

void Game::transformCube(Matrix3 &matrix)
{
	for (int i = 2, j = 0; i < 24 ; i+=2, j++)
	{
		m_cubePoints[j] = matrix * m_cubePoints[j];

		vertices[i - 2 + j] = m_cubePoints[j].M_X();
		vertices[i - 1 + j] = m_cubePoints[j].M_Y();
		vertices[i + j] = m_cubePoints[j].M_Z();
	}
}

void Game::translatePoints(double translation, const Matrix3::Axis & axis)
{
	for (int i = 2, j = 0; i < 24; i += 2, j++)
	{
		m_cubePoints[j] = Matrix3::translate(m_cubePoints[j], translation, axis);

		vertices[i - 2 + j] = m_cubePoints[j].M_X();
		vertices[i - 1 + j] = m_cubePoints[j].M_Y();
		vertices[i + j] = m_cubePoints[j].M_Z();
	}
}

