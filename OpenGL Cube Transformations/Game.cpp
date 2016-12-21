#include "Game.h"

bool flip = false;
int current = 1;

Game::Game() :
	window(sf::VideoMode(800, 600), "OpenGL Cube"),
	primitives(1)	
{
	index = glGenLists(primitives);
}

Game::~Game(){}

void Game::run()
{
	
	initialize();

	sf::Event event;

	while (isRunning){
		
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
					rotatePoint(-1, Matrix3::Axis::X);
					break;
				case sf::Keyboard::Down:
					//rotates ABOUT the x axis
					rotatePoint(1, Matrix3::Axis::X);
					break;
				case sf::Keyboard::Left:
					rotatePoint(-1, Matrix3::Axis::Y);
					break;
				case sf::Keyboard::Right:
					rotatePoint(1, Matrix3::Axis::Y);
					break;
				case sf::Keyboard::Comma:
					rotatePoint(1, Matrix3::Axis::Z);
					break;
				case sf::Keyboard::Period:
					rotatePoint(-1, Matrix3::Axis::Z);
					break;
				case sf::Keyboard::Add:
					scalePoints(1.1, 1.1, 1.1);
					break;
				case sf::Keyboard::Subtract:
					scalePoints(0.9, 0.9, 0.9);
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
		draw();
	}
	
}

void Game::initialize()
{
	

	isRunning = true;

	//initialize points for front face
	//first point is top right on the front of the cube goes anticlockwise
	m_cubePoints[0] = Vector3(1.0f, 1.0f, 1.0f);
	m_cubePoints[1] = Vector3(-1.0f, 1.0f, 1.0f);
	m_cubePoints[2] = Vector3(-1.0f, -1.0f, 1.0f);
	m_cubePoints[3] = Vector3(1.0f, -1.0f, 1.0f);
	//5th point is the 
	m_cubePoints[4] = Vector3(1.0f, -1.0f, -1.0f);
	m_cubePoints[5] = Vector3(1.0f, 1.0f, -1.0f);
	m_cubePoints[6] = Vector3(-1.0f, 1.0f, -1.0f);
	m_cubePoints[7] = Vector3(-1.0f, -1.0f, -1.0f);

	glClearColor(0.0f, 0.0f, 0.0f, 0.0f);
	glMatrixMode(GL_PROJECTION);
	glLoadIdentity();
	gluPerspective(45.0, window.getSize().x / window.getSize().y, 1.0, 500.0);
	glMatrixMode(GL_MODELVIEW);
	
	

	// glNewList(index, GL_COMPILE);
	// Creates a new Display List
	// Initalizes and Compiled to GPU
	// https://www.opengl.org/sdk/docs/man2/xhtml/glNewList.xml
	glNewList(index, GL_COMPILE);
	glBegin(GL_QUADS);
	{
		//Back Face
		glColor3f(0.0f, 1.0f, 0.0f);
		glVertex3f(m_cubePoints[5].M_X(), m_cubePoints[5].M_Y(), m_cubePoints[5].M_Z());
		glVertex3f(m_cubePoints[6].M_X(), m_cubePoints[6].M_Y(), m_cubePoints[6].M_Z());
		glVertex3f(m_cubePoints[7].M_X(), m_cubePoints[7].M_Y(), m_cubePoints[7].M_Z());
		glVertex3f(m_cubePoints[4].M_X(), m_cubePoints[4].M_Y(), m_cubePoints[4].M_Z());

		//Top Face
		glColor3f(1.0f, 0.0f, 0.0f);
		glVertex3f(m_cubePoints[0].M_X(), m_cubePoints[0].M_Y(), m_cubePoints[0].M_Z());
		glVertex3f(m_cubePoints[5].M_X(), m_cubePoints[5].M_Y(), m_cubePoints[5].M_Z());
		glVertex3f(m_cubePoints[6].M_X(), m_cubePoints[6].M_Y(), m_cubePoints[6].M_Z());
		glVertex3f(m_cubePoints[1].M_X(), m_cubePoints[1].M_Y(), m_cubePoints[1].M_Z());

		//Bottom Face
		glColor3f(1.0f, 0.0f, 1.0f);
		glVertex3f(m_cubePoints[3].M_X(), m_cubePoints[3].M_Y(), m_cubePoints[3].M_Z());
		glVertex3f(m_cubePoints[4].M_X(), m_cubePoints[4].M_Y(), m_cubePoints[4].M_Z());
		glVertex3f(m_cubePoints[7].M_X(), m_cubePoints[7].M_Y(), m_cubePoints[7].M_Z());
		glVertex3f(m_cubePoints[2].M_X(), m_cubePoints[2].M_Y(), m_cubePoints[2].M_Z());

		//Left Face
		glColor3f(0.0f, 1.0f, 1.0f);
		glVertex3f(m_cubePoints[1].M_X(), m_cubePoints[1].M_Y(), m_cubePoints[1].M_Z());
		glVertex3f(m_cubePoints[6].M_X(), m_cubePoints[6].M_Y(), m_cubePoints[6].M_Z());
		glVertex3f(m_cubePoints[7].M_X(), m_cubePoints[7].M_Y(), m_cubePoints[7].M_Z());
		glVertex3f(m_cubePoints[2].M_X(), m_cubePoints[2].M_Y(), m_cubePoints[2].M_Z());

		//Right Face
		glColor3f(1.0f, 1.0f, 1.0f);
		glVertex3f(m_cubePoints[0].M_X(), m_cubePoints[0].M_Y(), m_cubePoints[0].M_Z());
		glVertex3f(m_cubePoints[5].M_X(), m_cubePoints[5].M_Y(), m_cubePoints[5].M_Z());
		glVertex3f(m_cubePoints[4].M_X(), m_cubePoints[4].M_Y(), m_cubePoints[4].M_Z());
		glVertex3f(m_cubePoints[3].M_X(), m_cubePoints[3].M_Y(), m_cubePoints[3].M_Z());

		//Front Face
		glColor3f(0.0f, 0.0f, 1.0f);
		glVertex3f(m_cubePoints[0].M_X(), m_cubePoints[0].M_Y(), m_cubePoints[0].M_Z());
		glVertex3f(m_cubePoints[1].M_X(), m_cubePoints[1].M_Y(), m_cubePoints[1].M_Z());
		glVertex3f(m_cubePoints[2].M_X(), m_cubePoints[2].M_Y(), m_cubePoints[2].M_Z());
		glVertex3f(m_cubePoints[3].M_X(), m_cubePoints[3].M_Y(), m_cubePoints[3].M_Z());

		

		//Complete the faces of the Cube
	}
	glEnd();
	glEndList();
}

void Game::update()
{
	
	elapsed = clock.getElapsedTime();

	if (elapsed.asSeconds() >= 1.0f)
	{
		clock.restart();

		if (!flip)
		{
			flip = true;
			current++;
			if (current > primitives)
			{
				current = 1;
			}
		}
		else
			flip = false;
	}

	if (flip)
	{
		//rotationAngle += 0.005f;

		if (rotationAngle > 360.0f)
		{
			rotationAngle -= 360.0f;
		}
	}
	
	cout << "Update up" << endl;
}

void Game::draw()
{
	cout << "Drawing" << endl;

	glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT);
	
	cout << "Drawing Cube " << current << endl;
	
	
	glLoadIdentity();
	glRotatef(rotationAngle, 0, 1, 0); // Rotates the camera on Y Axis
	glTranslated(0.0, 0.0, -5.0);

	glNewList(index, GL_COMPILE);
	glBegin(GL_QUADS);
	{

		//Back Face
		glColor3f(0.0f, 1.0f, 0.0f);
		glVertex3f(m_cubePoints[5].M_X(), m_cubePoints[5].M_Y(), m_cubePoints[5].M_Z());
		glVertex3f(m_cubePoints[6].M_X(), m_cubePoints[6].M_Y(), m_cubePoints[6].M_Z());
		glVertex3f(m_cubePoints[7].M_X(), m_cubePoints[7].M_Y(), m_cubePoints[7].M_Z());
		glVertex3f(m_cubePoints[4].M_X(), m_cubePoints[4].M_Y(), m_cubePoints[4].M_Z());

		//Top Face
		glColor3f(1.0f, 0.0f, 0.0f);
		glVertex3f(m_cubePoints[0].M_X(), m_cubePoints[0].M_Y(), m_cubePoints[0].M_Z());
		glVertex3f(m_cubePoints[5].M_X(), m_cubePoints[5].M_Y(), m_cubePoints[5].M_Z());
		glVertex3f(m_cubePoints[6].M_X(), m_cubePoints[6].M_Y(), m_cubePoints[6].M_Z());
		glVertex3f(m_cubePoints[1].M_X(), m_cubePoints[1].M_Y(), m_cubePoints[1].M_Z());

		//Bottom Face
		glColor3f(1.0f, 0.0f, 1.0f);
		glVertex3f(m_cubePoints[3].M_X(), m_cubePoints[3].M_Y(), m_cubePoints[3].M_Z());
		glVertex3f(m_cubePoints[4].M_X(), m_cubePoints[4].M_Y(), m_cubePoints[4].M_Z());
		glVertex3f(m_cubePoints[7].M_X(), m_cubePoints[7].M_Y(), m_cubePoints[7].M_Z());
		glVertex3f(m_cubePoints[2].M_X(), m_cubePoints[2].M_Y(), m_cubePoints[2].M_Z());

		//Left Face
		glColor3f(0.0f, 1.0f, 1.0f);
		glVertex3f(m_cubePoints[1].M_X(), m_cubePoints[1].M_Y(), m_cubePoints[1].M_Z());
		glVertex3f(m_cubePoints[6].M_X(), m_cubePoints[6].M_Y(), m_cubePoints[6].M_Z());
		glVertex3f(m_cubePoints[7].M_X(), m_cubePoints[7].M_Y(), m_cubePoints[7].M_Z());
		glVertex3f(m_cubePoints[2].M_X(), m_cubePoints[2].M_Y(), m_cubePoints[2].M_Z());

		//Right Face
		glColor3f(1.0f, 1.0f, 0.0f);
		glVertex3f(m_cubePoints[0].M_X(), m_cubePoints[0].M_Y(), m_cubePoints[0].M_Z());
		glVertex3f(m_cubePoints[5].M_X(), m_cubePoints[5].M_Y(), m_cubePoints[5].M_Z());
		glVertex3f(m_cubePoints[4].M_X(), m_cubePoints[4].M_Y(), m_cubePoints[4].M_Z());
		glVertex3f(m_cubePoints[3].M_X(), m_cubePoints[3].M_Y(), m_cubePoints[3].M_Z());

		//Front Face
		glColor3f(0.0f, 0.0f, 1.0f);
		glVertex3f(m_cubePoints[0].M_X(), m_cubePoints[0].M_Y(), m_cubePoints[0].M_Z());
		glVertex3f(m_cubePoints[1].M_X(), m_cubePoints[1].M_Y(), m_cubePoints[1].M_Z());
		glVertex3f(m_cubePoints[2].M_X(), m_cubePoints[2].M_Y(), m_cubePoints[2].M_Z());
		glVertex3f(m_cubePoints[3].M_X(), m_cubePoints[3].M_Y(), m_cubePoints[3].M_Z());



		//Complete the faces of the Cube
	}
	glEnd();
	glEndList();
	glCallList(current);

	window.display();

}

void Game::unload()
{
	cout << "Cleaning up" << endl;
}

void Game::rotatePoint(double angle, Matrix3::Axis axis)
{
	rotationMatrix = Matrix3::Rotation(angle, axis);
	/*for (auto &point : m_cubePoints)
	{
		point = rotationMatrix * point;
	}*/
	for (int i = 0; i < 8; i++)
	{
		m_cubePoints[i] = rotationMatrix * m_cubePoints[i];
	}
}

void Game::translatePoints(double translation, const Matrix3::Axis & axis)
{
	for (int i = 0; i < 8; i++)
	{
		m_cubePoints[i] = Matrix3::translate(m_cubePoints[i], translation, axis);
	}
}



void Game::scalePoints(float x, float y, float z)
{
	scalingMatrix = Matrix3::Scale(x, y, z);

	for (int i = 0; i < 8; i++)
	{
		m_cubePoints[i] = scalingMatrix * m_cubePoints[i];
	}
}



