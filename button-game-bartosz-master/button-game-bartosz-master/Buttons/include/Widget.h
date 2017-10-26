#ifndef WIDGET
#define WIDGET
#include "SFML\Graphics.hpp"
#include "XboxController.h"

class Widget : public sf::Drawable
{
public:
	Widget(Widget * parent = nullptr);
	virtual bool processInput(XboxController& controller);
	virtual void getFocus();
	virtual void loseFocus();
	virtual void draw(sf::RenderTarget&, sf::RenderStates) const override = 0;
	virtual void setWidgetPosition(float deg, sf::CircleShape centre);
	virtual void update(sf::Time dt);
	virtual float getValue();
	~Widget();

protected:
	sf::Vector2f m_position;

	Widget* m_parent;
	
};

#endif // !WIDGET



