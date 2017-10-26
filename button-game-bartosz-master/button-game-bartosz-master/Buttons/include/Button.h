#ifndef  BUTTON
#define BUTTON


#include "Label.h"

class Button :	public Label
{
public:
	Button();
	Button(sf::Font, std::string text, int textSize);
	void draw(sf::RenderTarget&, sf::RenderStates) const override;
	void setWidgetPosition(float  deg, sf::CircleShape centre)  override;
	virtual bool processInput(XboxController& controller);
	void getFocus() override;
	void loseFocus() override;
	bool focus = false;
	bool pressed = false;
	~Button();

private:
	sf::RectangleShape m_rect;
	sf::RectangleShape m_inFocusRect;
	const unsigned int MARGIN = 25;
	
};

#endif // ! BUTTON
