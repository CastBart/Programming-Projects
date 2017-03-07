#include <Game.h>

//static double const MS_PER_UPDATE = 10.0;

//const string filename = "coordinates.tga";
//const string filename = "cube.tga";
//const string filename = "grid.tga";
const std::string filename = "grid_wip.tga";
//const string filename = "minecraft.tga";
//const string filename = "texture.tga";
//const string filename = "texture_2.tga";
//const string filename = "uvtemplate.tga";


int width;			// Width of texture
int height;			// Height of texture
int comp_count;		// Component of texture

unsigned char* img_data;		// image data


Game::Game() : 
	window(sf::VideoMode(800, 600), 
	"Introduction to OpenGL Texturing")
	, cameraView(1.0f)
	, m_player(cameraView, true)
{
}

Game::Game(sf::ContextSettings settings) : 
	window(sf::VideoMode(800, 600), 
	"Introduction to OpenGL Texturing", 
	sf::Style::Default, 
	settings)
	, cameraView(1.0f)
	, m_player(cameraView, true)
{
}

Game::~Game()
{
}




void Game::run()
{
	createCubes();
	initialize();
	
	sf::Clock clock;
	sf::Int32 lag = 0;

	
	sf::Event event;

	while (isRunning){

#if (DEBUG >= 2)
		DEBUG_MSG("Game running...");
#endif
		m_elapsed += clock.restart();

		while (window.pollEvent(event))
		{
			if (event.type == sf::Event::Closed)
			{
				isRunning = false;
			}

			
		}
		while (m_elapsed > MS_PER_UPDATE) //update loop
		{
			m_elapsed -= MS_PER_UPDATE;
			update(MS_PER_UPDATE.asSeconds());
		
		}
		
		render();
		
	}

#if (DEBUG >= 2)
	DEBUG_MSG("Calling Cleanup...");
#endif
	unload();

}

void Game::initialize()
{
	isRunning = true;
	GLint isCompiled = 0;
	GLint isLinked = 0;

	cameraView = glm::lookAt(
		glm::vec3(0.0f, 4.0f, 10.0f),	// Camera (x,y,z), in World Space
		glm::vec3(0.0f, 0.0f, 0.0f),	// Camera looking at origin
		glm::vec3(0.0f, 1.0f, 0.0f)	// 0.0f, 1.0f, 0.0f Look Down and 0.0f, -1.0f, 0.0f Look Up
	);



	DEBUG_MSG(glGetString(GL_VENDOR));
	DEBUG_MSG(glGetString(GL_RENDERER));
	DEBUG_MSG(glGetString(GL_VERSION));


	glewInit();

	//Copy UV's to all faces

	/* Vertex Shader */
#pragma region 
	std::string temp = loadShader("Shader.vs.txt");
	const char* vs_src = temp.c_str();
	

	DEBUG_MSG("Setting Up Vertex Shader");
	//create id for vertex shader
	m_ids.vsid = glCreateShader(GL_VERTEX_SHADER);
	glShaderSource(m_ids.vsid, 1, (const GLchar**)&vs_src, NULL);
	glCompileShader(m_ids.vsid);

	//Check is Shader Compiled
	glGetShaderiv(m_ids.vsid, GL_COMPILE_STATUS, &isCompiled);

	if (isCompiled == GL_TRUE) {
		DEBUG_MSG("Vertex Shader Compiled");
		isCompiled = GL_FALSE;
	}
	else
	{
		DEBUG_MSG("ERROR: Vertex Shader Compilation Error");
	}

#pragma endregion 
	/* Fragment Shader*/
#pragma region
	std::string temp1 = loadShader("Shader.fv.txt");
	const char* fs_src = temp1.c_str();


	DEBUG_MSG("Setting Up Fragment Shader");
	//create id for fragment shader
	m_ids.fsid = glCreateShader(GL_FRAGMENT_SHADER);
	glShaderSource(m_ids.fsid, 1, (const GLchar**)&fs_src, NULL);
	glCompileShader(m_ids.fsid);

	//Check is Shader Compiled
	glGetShaderiv(m_ids.fsid, GL_COMPILE_STATUS, &isCompiled);

	if (isCompiled == GL_TRUE) {
		DEBUG_MSG("Fragment Shader Compiled");
		isCompiled = GL_FALSE;
	}
	else
	{
		DEBUG_MSG("ERROR: Fragment Shader Compilation Error");
	}

	DEBUG_MSG("Setting Up and Linking Shader");

	//create id for program
	m_ids.progID = glCreateProgram();
	glAttachShader(m_ids.progID, m_ids.vsid);
	glAttachShader(m_ids.progID, m_ids.fsid);
	glLinkProgram(m_ids.progID);

	//Check is Shader Linked
	glGetProgramiv(m_ids.progID, GL_LINK_STATUS, &isLinked);

	if (isLinked == 1) {
		DEBUG_MSG("Shader Linked");
	}
	else
	{
		DEBUG_MSG("ERROR: Shader Link Error");
	}

#pragma endregion

	
	//glGenVertexArrays(1, &m_ids.vao); //Gen Vertex Array
	//glBindVertexArray(m_ids.vao);

	glGenBuffers(1, &m_ids.vbo); //Gen Vertex Buffer
	glBindBuffer(GL_ARRAY_BUFFER, m_ids.vbo);

	//Vertices 3 x,y,z , Colors 4 RGBA, UV/ST 2
	glBufferData(GL_ARRAY_BUFFER, ((3 * VERTICES) + (4 *COLORS) + (2 * UVS)) * sizeof(GLfloat), NULL, GL_STATIC_DRAW);

	glGenBuffers(1, &m_ids.vib); //Gen Vertex Index Buffer
	glBindBuffer(GL_ELEMENT_ARRAY_BUFFER, m_ids.vib);
	glBufferData(GL_ELEMENT_ARRAY_BUFFER, 3 * INDICES * sizeof(GLuint), Cube::indices, GL_STATIC_DRAW);
	
	

	//Use Progam on GPU
	glUseProgram(m_ids.progID);

	img_data = stbi_load(filename.c_str(), &width, &height, &comp_count, 4);

	if (img_data == NULL)
	{
		DEBUG_MSG("ERROR: Texture not loaded");
	}

	glEnable(GL_TEXTURE_2D);
	glGenTextures(1, &m_ids.to);
	glBindTexture(GL_TEXTURE_2D, m_ids.to);

	//Wrap around
	//https://www.khronos.org/opengles/sdk/docs/man/xhtml/glTexParameter.xml
	glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_WRAP_S, GL_CLAMP_TO_BORDER);
	glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_WRAP_T, GL_CLAMP_TO_BORDER);

	//Filtering
	//https://www.khronos.org/opengles/sdk/docs/man/xhtml/glTexParameter.xml
	glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MIN_FILTER, GL_NEAREST); 
	glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MAG_FILTER, GL_LINEAR);

	//Bind to OpenGL
	//https://www.khronos.org/opengles/sdk/docs/man/xhtml/glTexImage2D.xml
	glTexImage2D(
		GL_TEXTURE_2D,		//2D Texture Image
		0,					//Mipmapping Level 
		GL_RGBA,			//GL_RED, GL_GREEN, GL_BLUE, GL_ALPHA, GL_RGB, GL_BGR, GL_RGBA 
		width,				//Width
		height,				//Height
		0,					//Border
		GL_RGBA,			//Bitmap
		GL_UNSIGNED_BYTE,	//Specifies Data type of image data
		img_data				//Image Data
		);
	


	// Find variables in the shader
	//https://www.khronos.org/opengles/sdk/docs/man/xhtml/glGetAttribLocation.xml
	m_ids.positionID = glGetAttribLocation(m_ids.progID, "sv_position");
	m_ids.colorID = glGetAttribLocation(m_ids.progID, "sv_color");
	m_ids.uvID = glGetAttribLocation(m_ids.progID, "sv_uv");
	m_ids.textureID = glGetUniformLocation(m_ids.progID, "f_texture");
	m_ids.mvpID = glGetUniformLocation(m_ids.progID, "sv_mvp");

	m_player.initialize(m_player.m_frontTopLeftPos);

	

	
	// Enable Depth Test
	glEnable(GL_DEPTH_TEST);
	glDepthFunc(GL_LESS);
	glEnable(GL_CULL_FACE);
}

void Game::update(double dt)
{

#if (DEBUG >= 2)
	DEBUG_MSG("Updating...");
#endif
	
	switch (currentState)
	{
	case GameState::Playing:
		//if last element's z component of vector is greater than spawn distance then create new batch of cubes
		if (m_cubes.back()->m_backTopLeftPos.z > SPAWN_DISTANCE_FROM_CENTRE)
		{
			createCubes();
		}


		m_player.update(dt);


		for (int i = 0; i < m_cubes.size(); i++)
		{
			m_cubes[i]->update(dt);
		}

		//move left
		if (sf::Keyboard::isKeyPressed(sf::Keyboard::Left))
		{
			cameraView = glm::translate(cameraView, glm::vec3(15 * dt, 0, 0));			// traslate camera view left
			m_player.model = glm::translate(m_player.model, glm::vec3(-15 * dt, 0, 0));	// translate the player cube
			m_player.m_frontTopLeftPos.x -= 15 * dt;									// update top left position of the front face which is used for collisinons
		}
		//move right
		if (sf::Keyboard::isKeyPressed(sf::Keyboard::Right))
		{
			cameraView = glm::translate(cameraView, glm::vec3(-15 * dt, 0, 0));			// traslate camera view right
			m_player.model = glm::translate(m_player.model, glm::vec3(15 * dt, 0, 0));  // translate the player cube
			m_player.m_frontTopLeftPos.x += 15 * dt;									// update top left position of the front face which is used for collisinons
		}
		//move up
		if (sf::Keyboard::isKeyPressed(sf::Keyboard::Up))
		{
			cameraView = glm::translate(cameraView, glm::vec3(0, -15 * dt, 0));			// traslate camera view up
			m_player.model = glm::translate(m_player.model, glm::vec3(0, 15 * dt, 0));	// translate the player cube
			m_player.m_frontTopLeftPos.y += 15 * dt;									// update top left position of the front face which is used for collisinons
		}
		//move down
		if (sf::Keyboard::isKeyPressed(sf::Keyboard::Down))
		{
			cameraView = glm::translate(cameraView, glm::vec3(0, 15 * dt, 0));			// traslate camera view down
			m_player.model = glm::translate(m_player.model, glm::vec3(0, -15 * dt, 0));	// translate the player cube
			m_player.m_frontTopLeftPos.y -= 15 * dt;									// update top left position of the front face which is used for collisinons
		}
		checkCollision();
		deleteCubes();
		break;
	case GameState::Gameover:
		break;
	default:
		break;
	}
	
}

void Game::render()
{

#if (DEBUG >= 2)
	DEBUG_MSG("Render Loop...");
#endif

	glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT);

	m_player.render(m_ids, m_player.mvp);
	for (int i = 0; i < m_cubes.size(); i++)
	{
		m_cubes[i]->render(m_ids, m_cubes[i]->mvp);
	}

	window.display();
}

void Game::unload()
{
#if (DEBUG >= 2)
	DEBUG_MSG("Cleaning up...");
#endif
	glDeleteProgram(m_ids.progID);	//Delete Shader
	glDeleteBuffers(1, &m_ids.vbo);	//Delete Vertex Buffer
	glDeleteBuffers(1, &m_ids.vib);	//Delete Vertex Index Buffer
	stbi_image_free(img_data);		//Free image
}

/// <summary>
/// create new batch of cubes
/// </summary>
void Game::createCubes()
{
	
	for (int i = 0; i < 15; i++)
	{
		m_cubes.push_back(std::unique_ptr<Cube>(new Cube(cameraView, false, (SPAWN_DISTANCE_FROM_CENTRE) + (GAP_BETWEEN_CUBES *-i))));
		
	}

	for (int i = 0; i < 15; i++)
	{
		m_cubes[(m_cubes.size() -1) - i]->initialize(m_player.m_frontTopLeftPos);
		
	}
}

/// <summary>
/// delete cubes that are behind the camera
/// </summary>
void Game::deleteCubes()
{
	if (m_cubes[0]->m_backTopLeftPos.z > 10)
	{
		m_cubes.erase(m_cubes.begin());
	}
}

/// <summary>
/// load shader from a file 
/// </summary>
/// <param name="fileName"></param>
/// <returns>the file as a string so then it can be used in a shader</returns>
std::string Game::loadShader(const std::string & fileName)
{
	std::ifstream file;
	file.open((fileName).c_str());

	std::string output;
	std::string line;


	while (!file.eof())
	{
		getline(file, line);
		output.append(line + "\n");
	}


	return output;

}


/// <summary>
/// check collisions betweeen cubes and player
/// </summary>
void Game::checkCollision()
{
	

	for (auto &cube : m_cubes)
	{
		if ( //checks for intersection on x axis
			cube->m_frontTopLeftPos.x < m_player.m_frontTopLeftPos.x &&//x pos
			cube->m_frontTopRightPos.x > m_player.m_frontTopLeftPos.x 
			||
			cube->m_frontTopLeftPos.x < m_player.m_frontTopLeftPos.x + 2 &&
			cube->m_frontTopRightPos.x > m_player.m_frontTopLeftPos.x + 2

			)
		{
			if (//checks for intersection on y axis
				cube->m_frontTopLeftPos.y > m_player.m_frontTopLeftPos.y &&//y pos
				cube->m_frontBotLeftPos.y < m_player.m_frontTopLeftPos.y
				||
				cube->m_frontTopLeftPos.y > m_player.m_frontTopLeftPos.y - 2 &&
				cube->m_frontBotLeftPos.y < m_player.m_frontTopLeftPos.y - 2
				)
			{//checks if the cube is greater than -1 for less compulation 
				if (cube->m_frontTopLeftPos.z > -1)
				{
					currentState = GameState::Gameover;
				}
				
			}
			
		}
	}

}

