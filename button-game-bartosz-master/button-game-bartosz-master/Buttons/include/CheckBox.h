#ifndef CHECKBOX
#define CHECKBOX
#include "Label.h"

class CheckBox : public Label
{
public:
	CheckBox();
	CheckBox(sf::Font, std::string text, int textSize, sf::Texture checked, sf::Texture unchecked);
	~CheckBox();
	void draw(sf::RenderTarget&, sf::RenderStates) const override;
	void setWidgetPosition(float  deg, sf::CircleShape centre)  override;
	virtual bool processInput(XboxController& controller);

	void getFocus() override;
	void loseFocus() override;
	bool focus = false;
	bool pressed = false;

private:
	sf::Sprite m_sprite;
	sf::Texture m_checkedTexture;
	sf::Texture m_uncheckedTexture;
	sf::RectangleShape m_inFocusRect;
	bool m_isChecked;
	const int GAP_BETWEEN_BOX_AND_TEXT = 10;
	const unsigned int MARGIN = 25;
};
	
#endif //!CHECKBOX