#ifndef LABEL
#define LABEL
#include "SFML\Graphics.hpp"
#include "GUI.h"

#include "Widget.h"
class Label : public Widget
{
public:
	Label();
	Label(sf::Font, std::string text, int textSize);
	~Label();
	void draw(sf::RenderTarget&, sf::RenderStates) const override;
	void setWidgetPosition(float  deg, sf::CircleShape centre)  override;
	virtual bool processInput(XboxController& controller);

protected:
	sf::Font m_font;
	sf::Text m_text;

	float OFFSET_X;
	float OFFSET_Y;
	
};

#endif // !LABEL



