#ifndef  GAME
#define GAME
#include "SFML\Graphics.hpp"
#include "Licence.h"
#include "SplashScreen.h"
#include "Credits.h"
#include "HelpScreen.h"
#include "GUI.h"
#include "Label.h"
#include "Button.h"
#include "Slider.h"
#include "CheckBox.h"
#include "MainMenu.h"
#include "OptionScreen.h"
#include "PauseScreen.h"
#include "SFML\Audio\Music.hpp"
#include "SFML\Audio\Sound.hpp"


/*Pre-defined Classes*/
class Licence;
class SplashScreen;
class Credits;
class HelpScreen;
class MainMenu;
class PauseScreen;
class OptionScreen;

/*Game States*/
enum class GameState{
	None,
	Play,
	Licence,
	Splash,
	MainMenu,
	OptionScreen,
	Pause,
	HelpScreen,
	Credits
};

class Game
{
public:
	Game(sf::ContextSettings settings);
	~Game();

	/*Getters that allow me to use the objects of the game class in different screens*/
	sf::RenderWindow& getWindow();
	sf::RenderStates& getState();
	sf::Font& getFont();
	sf::Texture& getCheckedTexture();
	sf::Texture& getUnCheckedTexture();
	XboxController& getCurrentController();
	XboxController& getPrevController();
	GameState& getCurrentState();
	GameState& getPrevState();
	void run();

	static const float WINDOW_WIDTH;
	static const float WINDOW_HEIGHT;
	static float FRAMES;
	static float VOLUME;
	static bool SOUND_ON;

private:
	/*main updtade loop*/
	void update(sf::Time const &);
	/*main rendering*/
	void render();
	/*proccesses*/
	void proccessEvents();


	/* frames per second used fro updating the game */
	sf::Time FRAMES_PER_SECOND = sf::seconds(1.0f / FRAMES);

	
	

	/* clock used for rendering times */
	sf::Clock m_clock;

	/* time for tracking time in between updates */
	sf::Time m_elapsedTime;

	Licence* m_licence;				/*Licence Instance*/
	SplashScreen* m_splash;			/*Splash Screen Instance*/
	Credits* m_credits;				/*Credits Instance*/
	HelpScreen* m_helpScreen;		/*Help Screen Instance*/
	MainMenu* m_mainMenuScreen;		/*Main Menu Screen Instance*/
	OptionScreen* m_optionScreen;	/*Option Screen Instance*/ 
	PauseScreen* m_pauseScreen;		/*Pause Screen Instance*/

	
	/* Render Window used to draw to screen */
	sf::RenderWindow m_window;
	sf::RenderStates m_state;

	XboxController m_controller;
	XboxController m_prevController;

	GameState m_currentGameState;	/*Current GameState instance*/
	GameState m_previousGameState;	/*Previous GameState instance*/


	sf::Font m_font;	/*font instance*/

	/*TEXTURES FOR CHECKBOX*/
	sf::Texture m_checkedTexture;
	sf::Texture m_uncheckedTexture;

	/*TEXTURE AND SPRITE FOR BACKGROUND*/
	sf::Texture m_backgroundTexture;
	sf::Sprite m_backgroundSprite;

	/*TEXTURE AND SPRITE FOR SELECT BUTTON*/
	sf::Texture m_sellectButtonTexture;
	sf::Sprite m_sellectButtonSprite;

	/*TEXTURE AND SPRITE FOR PAUSE BUTTON*/
	sf::Texture m_pauseButtonTexture;
	sf::Sprite m_pauseButtonSprite;

	sf::Music m_music;
	
};
#endif // ! GAME
