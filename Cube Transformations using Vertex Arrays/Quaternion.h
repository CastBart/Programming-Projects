#pragma once
#include "Vector3.h"
class Quaternion
{
public:
	Quaternion();
	Quaternion(double, double, double, double);
	Quaternion(Vector3 &);

	friend Quaternion operator+(const Quaternion &, const Quaternion &);
	friend Quaternion operator-(const Quaternion &, const Quaternion &);
	friend Quaternion operator-(const Quaternion &);
	friend Quaternion operator*(const Quaternion &, const Quaternion &);
	friend Quaternion operator*(const Quaternion &, const double &);
	friend Quaternion operator*(const Quaternion &, const float &);

	double length();
	double lengthSquared();
	Quaternion conjugate();
	Quaternion inverse();
	Quaternion normalise();
	Vector3 Rotate(Vector3 &, const float &);
	Vector3 convertToVector();

	double M_W() const;
	double M_X() const;
	double M_Y() const;
	double M_Z() const;
	~Quaternion();

private:
	double m_w;
	double m_x;
	double m_y;
	double m_z;
};

