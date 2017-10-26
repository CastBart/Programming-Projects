#include "Credits.h"



Credits::Credits(Game & game) :
	m_game(&game),
	m_font(m_game->getFont())
{
	m_file.open(".//resources//CreditsText//text.txt");	//open the file
	if (m_file.is_open())		//if the file is open
	{
		while (std::getline(m_file, m_string))
		{
			sf::Text *text = new sf::Text(m_string, m_font, characterSize); //assign current string to the text in the vector
			m_vector.push_back(*text);
		}
	}
	yPos = Game::WINDOW_HEIGHT;
	initialize();
}

Credits::~Credits() //destructor
{
	std::cout << "Bye bye Credits Screen" << std::endl; 
}

///<summary>
/// Update for the credits class.
/// Handles all update.
///<summary>
void Credits::update(const sf::Time & dt)
{
	m_cumulativeTime += dt;		//accumulates time to a variable

	//m_render.transform.translate(0, -150 * dt.asSeconds()); //move text up
	for (int i = 0; i < m_vector.size(); i++)
	{
		m_vector[i].setPosition(m_vector[i].getPosition().x, m_vector[i].getPosition().y-(150.0f * dt.asSeconds()));
	}
	if (m_cumulativeTime.asSeconds() >= 10.0f) //taking that each line takes a second to move up the screen
	{							
		m_cumulativeTime = sf::Time::Zero;
		yPos = Game::WINDOW_HEIGHT;//it will change the game state to none	
		m_currentIndex = 0;
		m_titleLine = true;
		initialize();
		m_render.transform = m_render.transform.Identity;
		m_game->getCurrentState() = GameState::MainMenu;
	}
}

///<summary>
/// Render for the credits class.
/// Handles all rendering to the screen.
///<summary>
void Credits::render(sf::RenderWindow & window)
{
	for (int i = 0; i < m_vector.size(); i++)
	{
		window.draw(m_vector.at(i));
	}
}

void Credits::initialize()
{
	
	while (m_currentIndex < m_vector.size())
	{
		if (m_currentIndex < m_vector.size())//checks if the index of the of the vector is not out of bounds
		{
			std::string currentString = m_vector.at(m_currentIndex).getString();	//gets the string of the current vector
			m_vector.at(m_currentIndex).setOrigin(m_vector.at(m_currentIndex).getGlobalBounds().width / 2,		//set origin of the text 
				m_vector.at(m_currentIndex).getGlobalBounds().height / 2);

			if (m_titleLine)	//if true then the string is a title
			{
				m_vector.at(m_currentIndex).setPosition(sf::Vector2f(Game::WINDOW_WIDTH / 2, yPos + 40));	//offset position
				m_vector.at(m_currentIndex).setStyle(sf::Text::Style::Bold);				//chcnge to bold
				m_vector.at(m_currentIndex).setColor(sf::Color::Red);						//chnge colour to red
				yPos += 40;
			}
			else
			{
				m_vector.at(m_currentIndex).setPosition(Game::WINDOW_WIDTH / 2, yPos + 40);
				yPos += 40;
			}

			if (currentString == "")//if a line is empty in the next iteration the string will be title string
			{
				m_titleLine = true;
			}
			else
			{
				m_titleLine = false;
			}
			m_currentIndex++;
		}
	}
}

