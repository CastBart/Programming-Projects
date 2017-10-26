#ifndef GUI_
#define GUI_

#include "Widget.h"

#include <math.h>

class GUI
{
public:
	GUI();
	GUI(XboxController& controller);
	void addWidget(Widget * widget);

	void draw(sf::RenderTarget& target, sf::RenderStates states) const;
	void update(sf::Time const & dt);
	void setPositionOfWidgets();
	void setFocusOfWidgets();
	void loseFocusOfWidgets();

	static const int WINDOW_HIGHT = 720;
	static const int WINDOW_WIDTH = 1280;

	int currentFocus = 0;

	/*Getters*/
	std::vector<Widget*> getWidgetHolder() const;

	~GUI();
private:
	std::vector<Widget*> m_widgetHolder;
	
	sf::CircleShape centre;
	
	int widNum;
	XboxController m_controller;
	
};

#endif // !GUI_