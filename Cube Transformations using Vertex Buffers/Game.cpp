#include <Game.h>

static bool flip;

Game::Game() : window(sf::VideoMode(800, 600), "OpenGL Cube VBO")
{
}

Game::~Game() {}

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

typedef struct
{
	float coordinate[3];
	float color[3];
} Vertex;

Vertex vertex[8];
GLubyte triangles[36];

/* Variable to hold the VBO identifier */
GLuint vbo[1];
GLuint index;

void Game::initialize()
{
	isRunning = true;

	glewInit();
	glMatrixMode(GL_PROJECTION);
	glLoadIdentity();
	gluPerspective(45.0, window.getSize().x / window.getSize().y, 1.0, 500.0);
	glMatrixMode(GL_MODELVIEW);
	/* Vertices counter-clockwise winding */

	vertex[0].coordinate[0] = -1.0f;	//bottom left f 0
	vertex[0].coordinate[1] = -1.0f;	
	vertex[0].coordinate[2] = 1.0f;		

	vertex[1].coordinate[0] = -1.0f;	//top left f   1
	vertex[1].coordinate[1] = 1.0f;		
	vertex[1].coordinate[2] = 1.0f;		

	vertex[2].coordinate[0] = 1.0f;		//top right f   2
	vertex[2].coordinate[1] = 1.0f;		
	vertex[2].coordinate[2] = 1.0f;		

	vertex[3].coordinate[0] = 1.0f;		//bottom right f 3
	vertex[3].coordinate[1] = -1.0f;	
	vertex[3].coordinate[2] = 1.0f;


	vertex[4].coordinate[0] = -1.0f;	//bottom left b 4
	vertex[4].coordinate[1] = -1.0f;
	vertex[4].coordinate[2] = -1.0f;

	vertex[5].coordinate[0] = -1.0f;	//top left b   5
	vertex[5].coordinate[1] = 1.0f;
	vertex[5].coordinate[2] = -1.0f;

	vertex[6].coordinate[0] = 1.0f;		//top right b  6
	vertex[6].coordinate[1] = 1.0f;
	vertex[6].coordinate[2] = -1.0f;

	vertex[7].coordinate[0] = 1.0f;		//bottom right b 7
	vertex[7].coordinate[1] = -1.0f;
	vertex[7].coordinate[2] = -1.0f;
	

	vertex[0].color[0] = 0.0f;
	vertex[0].color[1] = 1.0f;
	vertex[0].color[2] = 0.0f;

	vertex[1].color[0] = 0.5f;
	vertex[1].color[1] = 0.5f;
	vertex[1].color[2] = 0.0f;

	vertex[2].color[0] = 1.0f;
	vertex[2].color[1] = 0.0f;
	vertex[2].color[2] = 0.0f;

	vertex[3].color[0] = 1.0f;
	vertex[3].color[1] = 0.0f;
	vertex[3].color[2] = 1.0f;

	vertex[4].color[0] = 0.0f;
	vertex[4].color[1] = 1.0f;
	vertex[4].color[2] = 0.0f;

	vertex[5].color[0] = 0.5f;
	vertex[5].color[1] = 0.5f;
	vertex[5].color[2] = 0.0f;

	vertex[6].color[0] = 1.0f;
	vertex[6].color[1] = 0.0f;
	vertex[6].color[2] = 0.0f;

	vertex[7].color[0] = 1.0f;
	vertex[7].color[1] = 0.0f;
	vertex[7].color[2] = 1.0f;
	


	triangles[30] = 0;   triangles[31] = 1;   triangles[32] = 2;//012
	triangles[33] = 2;   triangles[34] = 3;   triangles[35] = 0;//230

	triangles[6] = 0;   triangles[7] = 1;   triangles[8] = 5; 
	triangles[9] = 5;   triangles[10] = 4;   triangles[11] = 0;

	triangles[12] = 3;   triangles[13] = 2;   triangles[14] = 6;
	triangles[15] = 6;   triangles[16] = 7;   triangles[17] = 3;

	triangles[18] = 5;   triangles[19] = 1;   triangles[20] = 2;
	triangles[21] = 2;   triangles[22] = 6;   triangles[23] = 5;

	triangles[24] = 3;   triangles[25] = 0;   triangles[26] = 4;
	triangles[27] = 3;   triangles[28] = 7;   triangles[29] = 4;

	triangles[0] = 7;   triangles[1] = 6;   triangles[2] = 5; //back face
	triangles[3] = 5;   triangles[4] = 4;   triangles[5] = 7;

	


	/* Create a new VBO using VBO id */
	glGenBuffers(1, vbo);

	/* Bind the VBO */
	glBindBuffer(GL_ARRAY_BUFFER, vbo[0]);

	/* Upload vertex data to GPU */
	glBufferData(GL_ARRAY_BUFFER, sizeof(Vertex) * 8, vertex, GL_STATIC_DRAW);
	glBindBuffer(GL_ARRAY_BUFFER, 0);

	glGenBuffers(1, &index);
	glBindBuffer(GL_ELEMENT_ARRAY_BUFFER, index);
	glBufferData(GL_ELEMENT_ARRAY_BUFFER, sizeof(GLubyte) * 36, triangles, GL_STATIC_DRAW);
	glBindBuffer(GL_ELEMENT_ARRAY_BUFFER, 0);
}

void Game::update()
{
	elapsed = clock.getElapsedTime();
	//glBufferData(GL_ARRAY_BUFFER, 8 * sizeof(Vertex), triangles, GL_DYNAMIC_DRAW);
	if (elapsed.asSeconds() >= 1.0f)
	{
		clock.restart();

		if (!flip)
		{
			flip = true;
		}
		else
			flip = false;
	}
	
	if (flip)
	{
		rotationAngle += 0.005f;

		if (rotationAngle > 360.0f)
		{
			rotationAngle -= 360.0f;
		}
	}

	cout << "Update up" << endl;
}

void Game::render()
{

	cout << "Drawing" << endl;
	
	glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT);
	glLoadIdentity();
	glTranslated(0.0, 0.0, -5.0);
	glClearColor(0.0f, 0.0f, 0.0f, 0.0f);

	glBindBuffer(GL_ARRAY_BUFFER, vbo[0]);
	glBindBuffer(GL_ELEMENT_ARRAY_BUFFER, index);
	
	glBufferData(GL_ARRAY_BUFFER, sizeof(Vertex) * 8, vertex, GL_STATIC_DRAW);
	
	glEnableClientState(GL_VERTEX_ARRAY);
	glEnableClientState(GL_COLOR_ARRAY);
	glColorPointer(3, GL_FLOAT, sizeof(Vertex), (char*)NULL + 12);

	/* Draw Triangle from VBO */

	glVertexPointer(3, GL_FLOAT, sizeof(Vertex), (char*)NULL + 0);
	glDrawElements(GL_TRIANGLES, 36, GL_UNSIGNED_BYTE, (char*)NULL + 0);

	glDisableClientState(GL_VERTEX_ARRAY);
	glDisableClientState(GL_COLOR_ARRAY);

	window.display();

}

void Game::unload()
{
	cout << "Cleaning up" << endl;

	glDeleteBuffers(1, vbo);
}

void Game::transformCube(Matrix3 &matrix)
{
	for (int i = 0; i < 8; i++)
	{
		m_cubePoints[i] = Vector3(vertex[i].coordinate[0], vertex[i].coordinate[1], vertex[i].coordinate[2]);
		m_cubePoints[i] = matrix * m_cubePoints[i];

		vertex[i].coordinate[0] = m_cubePoints[i].M_X();
		vertex[i].coordinate[1] = m_cubePoints[i].M_Y();
		vertex[i].coordinate[2] = m_cubePoints[i].M_Z();
	}
}

void Game::translatePoints(double translation, const Matrix3::Axis & axis)
{
	
	for (int i = 0; i < 8; i++)
	{
		m_cubePoints[i] = Vector3(vertex[i].coordinate[0], vertex[i].coordinate[1], vertex[i].coordinate[2]);
		m_cubePoints[i] = Matrix3::translate(m_cubePoints[i], translation, axis);

		vertex[i].coordinate[0] = m_cubePoints[i].M_X();
		vertex[i].coordinate[1] = m_cubePoints[i].M_Y();
		vertex[i].coordinate[2] = m_cubePoints[i].M_Z();
	}
}

