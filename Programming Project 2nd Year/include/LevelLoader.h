#pragma once

#include <SFML/System/Vector2.hpp>
#include <vector>
#include <sstream>
#include <fstream>
#include <iostream>
#include "yaml-cpp\yaml.h"


struct ObstacleData		//wall struct
{
  std::string m_type;		//string that holds the value of type which is read by yaml file
  sf::Vector2f m_position;	//holds the position of the wall	
  double m_rotation;		//holds the value of rotation
};

struct BackgroundData
{
   std::string m_fileName;	//holds a string which is apath to read from the folder
};

struct TankData
{
  sf::Vector2f m_position;	//holds the value of tank
};
struct BulletDate
{
	double m_speed;		//holds the value of bullets speed
	double m_damage;	//damage of the bullet
};

struct TargetData
{
	std::string m_type;			//holds that value of which type the target is read in yaml file
	sf::Vector2f m_position;	//position of the target
	int m_offset;				//offset to the postion. used in spawing the target
};


struct LevelData		//holds objects of each struct in the game that is read form yaml file 
{
   BackgroundData m_background;				
   TankData m_tank;
   BulletDate m_bullet;
   std::vector<ObstacleData> m_obstacles;
   std::vector<TargetData> m_targets;
};

class LevelLoader
{
public:

   LevelLoader();	

   static bool load(int nr, LevelData& level);
};
