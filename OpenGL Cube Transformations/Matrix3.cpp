#pragma once
#include "Matrix3.h"




Matrix3::Matrix3() :
	m_A1_x(0),
	m_A1_y(0),
	m_A1_z(0),
	m_A2_x(0),
	m_A2_y(0),
	m_A2_z(0),
	m_A3_x(0),
	m_A3_y(0),
	m_A3_z(0)
{
}

Matrix3::Matrix3(Vector3 &row1, Vector3 &row2, Vector3 &row3):
	m_A1_x(row1.M_X()),
	m_A1_y(row1.M_Y()),
	m_A1_z(row1.M_Z()),
	m_A2_x(row2.M_X()),
	m_A2_y(row2.M_Y()),
	m_A2_z(row2.M_Z()),
	m_A3_x(row3.M_X()),
	m_A3_y(row3.M_Y()),
	m_A3_z(row3.M_Z())
{
}

Matrix3::Matrix3(double _A11, double _A12, double _A13, double _A21, double _A22, double _A23, double _A31, double _A32, double _A33):
	m_A1_x(_A11),
	m_A1_y(_A12),
	m_A1_z(_A13),
	m_A2_x(_A21),
	m_A2_y(_A22),
	m_A2_z(_A23),
	m_A3_x(_A31),
	m_A3_y(_A32),
	m_A3_z(_A33)
{
}



Matrix3	Matrix3::Transpose()
{
	return  Matrix3(Column(0), Column(1),Column(2));
} 


double Matrix3::Determinant()
{
	double det;
	det = ( m_A1_x * ((m_A2_y *  m_A3_z) - (m_A3_y * m_A2_z))
	      - m_A2_x * ((m_A3_z *  m_A1_y) - (m_A3_y * m_A1_z))
		  + m_A3_x * ((m_A2_z *  m_A1_y) - (m_A2_y * m_A1_z)));

	return det;
}

Matrix3 Matrix3::Inverse()
{
	if (Determinant() == 0)
	{
		return  Matrix3();
	}
	else
	{

		return Matrix3((1 / Determinant()) * ((m_A3_z * m_A2_y) - (m_A3_y * m_A2_z)), (1 / Determinant())*((m_A3_y * m_A1_z) - (m_A3_z * m_A1_y)), (1 / Determinant()) * ((m_A2_z * m_A1_y) - (m_A2_y * m_A1_z)),
					   (1 / Determinant()) * ((m_A3_x * m_A2_z) - (m_A3_z * m_A2_x)), (1 / Determinant())*((m_A3_z * m_A1_x) - (m_A3_x * m_A1_z)), (1 / Determinant()) * ((m_A2_x * m_A1_z) - (m_A2_z * m_A1_x)),
					   (1 / Determinant()) * ((m_A3_y * m_A2_x) - (m_A3_x * m_A2_y)), (1 / Determinant())*((m_A3_x * m_A1_y) - (m_A3_y * m_A1_x)), (1 / Determinant()) * ((m_A2_y * m_A1_x) - (m_A2_x * m_A1_y)));
	}
}

Vector3 Matrix3::Column(int c)
{
	switch (c)
	{
	case 0:
		return Vector3(m_A1_x, m_A2_x, m_A3_x);
	case 1:
		return Vector3(m_A1_y, m_A2_y, m_A3_y);
	case 2:
	default:
		return Vector3(m_A1_z, m_A2_z, m_A3_z);
	}
}

Vector3 Matrix3::Row(int r)
{
	switch (r)
	{
	case 0:
		return Vector3(m_A1_x, m_A1_y, m_A1_z);
	case 1:
		return Vector3(m_A2_x, m_A2_y, m_A2_z);
	case 2:
	default:
		return Vector3(m_A3_x, m_A3_y, m_A3_z);
	}
}

 Matrix3 Matrix3::Rotation(double angle,  const Axis &axis   ) 
{
	float radAngle = (float)(angle * ((atan(1) * 4) / 180.0));
	float cosTheta = (float)cos(radAngle);
	float sinTheta = (float)sin(radAngle);

	Vector3 row1;
	Vector3 row2;
	Vector3 row3;

	switch (axis)
	{
	case Axis::X:
		row1 = Vector3(1, 0, 0);
		row2 = Vector3(0, cosTheta, -sinTheta);
		row3 = Vector3(0, sinTheta, cosTheta);
		break;
	case Axis::Y:
		row1 = Vector3(cosTheta, 0, sinTheta);
		row2 = Vector3(0, 1, 0);
		row3 = Vector3(-sinTheta, 0, cosTheta);
		break;
	case Axis::Z:
		row1 = Vector3(cosTheta, -sinTheta, 0);
		row2 = Vector3(sinTheta, cosTheta, 0);
		row3 = Vector3(0, 0, 1);
		break;
	default:
		row1 = Vector3();
		row2 = Vector3();
		row3 = Vector3();
		break;

	}
	return Matrix3(row1, row2, row3);
}


 
 Matrix3 Matrix3::Scale(float x, float y, float z)
 {
	 Vector3 row1 = Vector3(x, 0, 0);
	 Vector3 row2 = Vector3(0, y, 0);
	 Vector3 row3 = Vector3(0, 0, z);
	 Matrix3 scalingMatrix = Matrix3(row1, row2, row3);
	 return scalingMatrix;
 }


 /// <summary>
 /// pass in the vector that u wish to be translated
 /// pass in the value that u wish the vector to be translated by
 /// pass in the axis u wish to translate the vector in
 /// </summary>
 /// <returns></returns>
 Vector3 Matrix3::translate(Vector3 & V, double translation, const Axis &axis)
 {
	
	 double row1[4];
	 double row2[4];
	 double row3[4];
	 double row4[4];

	 switch (axis)
	 {
	 case Axis::X:
		 row1[0] = 1; row1[1] = 0; row1[2] = 0; row1[3] = translation;
		 row2[0] = 0; row2[1] = 1; row2[2] = 0; row2[3] = 0;
		 row3[0] = 0; row3[1] = 0; row3[2] = 1; row3[3] = 0;
		 row4[0] = 0; row4[1] = 0; row4[2] = 0; row4[3] = 1;
		 break;
	 case Axis::Y:
		 row1[0] = 1; row1[1] = 0; row1[2] = 0; row1[3] = 0;
		 row2[0] = 0; row2[1] = 1; row2[2] = 0; row2[3] = translation;
		 row3[0] = 0; row3[1] = 0; row3[2] = 1; row3[3] = 0;
		 row4[0] = 0; row4[1] = 0; row4[2] = 0; row4[3] = 1;
		 break;
	 case Axis::Z:
		 row1[0] = 1; row1[1] = 0; row1[2] = 0; row1[3] = 0;
		 row2[0] = 0; row2[1] = 1; row2[2] = 0; row2[3] = 0;
		 row3[0] = 0; row3[1] = 0; row3[2] = 1; row3[3] = translation;
		 row4[0] = 0; row4[1] = 0; row4[2] = 0; row4[3] = 1;
		 break;
	 default:
		 row1[0] = 0; row1[1] = 0; row1[2] = 0; row1[3] = 0;
		 row2[0] = 0; row2[1] = 0; row2[2] = 0; row2[3] = 0;
		 row3[0] = 0; row3[1] = 0; row3[2] = 0; row3[3] = 0;
		 row4[0] = 0; row4[1] = 0; row4[2] = 0; row4[3] = 0;
		 break;
	 }
	
	 double answer[4]; 
	 answer[0] = ((row1[0] * V.M_X()) + (row1[1] * V.M_Y()) + (row1[2] * V.M_Z()) + row1[3] * 1);
	 answer[1] = ((row2[0] * V.M_X()) + (row2[1] * V.M_Y()) + (row2[2] * V.M_Z()) + row2[3] * 1);
	 answer[2] = ((row3[0] * V.M_X()) + (row3[1] * V.M_Y()) + (row3[2] * V.M_Z()) + row3[3] * 1);
	 answer[3] = ((row4[0] * V.M_X()) + (row4[1] * V.M_Y()) + (row4[2] * V.M_Z()) + row4[3] * 1);
	 
	 return Vector3(answer[0], answer[1], answer[2]);
 }





std::string Matrix3::toString()
{
	std::string answer;
	answer = std::to_string(m_A1_x) + "," + std::to_string(m_A1_y) + "," + std::to_string(m_A1_z) + "," +
			 std::to_string(m_A2_x) + "," + std::to_string(m_A2_y) + "," + std::to_string(m_A2_z) + "," +
			 std::to_string(m_A3_x) + "," + std::to_string(m_A3_y) + "," + std::to_string(m_A3_z);
	return answer;
}

Vector3 operator*(Matrix3 M, Vector3 & V)
{
	return Vector3((M.m_A1_x * V.M_X()) + (M.m_A1_y * V.M_Y()) + (M.m_A1_z * V.M_Z()),
				   (M.m_A2_x * V.M_X()) + (M.m_A2_y * V.M_Y()) + (M.m_A2_z * V.M_Z()),
				   (M.m_A3_x * V.M_X()) + (M.m_A3_y * V.M_Y()) + (M.m_A3_z * V.M_Z()));
}

Matrix3 operator+(Matrix3 M, Matrix3 & M1)
{
	return Matrix3((M.m_A1_x + M1.m_A1_x), (M.m_A1_y + M1.m_A1_y), (M.m_A1_z + M1.m_A1_z),
			       (M.m_A2_x + M1.m_A2_x), (M.m_A2_y + M1.m_A2_y), (M.m_A2_z + M1.m_A2_z),
				   (M.m_A3_x + M1.m_A3_x), (M.m_A3_y + M1.m_A3_y), (M.m_A3_z + M1.m_A3_z));
}

Matrix3 operator-(Matrix3 M, Matrix3 & M1)
{
	return Matrix3((M.m_A1_x - M1.m_A1_x), (M.m_A1_y - M1.m_A1_y), (M.m_A1_y - M1.m_A1_y),
				   (M.m_A2_x - M1.m_A2_x), (M.m_A2_y - M1.m_A2_y), (M.m_A2_y - M1.m_A2_y),
				   (M.m_A3_x - M1.m_A3_x), (M.m_A3_y - M1.m_A3_y), (M.m_A3_y - M1.m_A3_y));
}

Matrix3 operator*(Matrix3 M, double & d)
{
	return Matrix3((M.m_A1_x * d), (M.m_A1_y * d), (M.m_A1_y * d),
				   (M.m_A2_x * d), (M.m_A2_y * d), (M.m_A2_y * d),
				   (M.m_A3_x * d), (M.m_A3_y * d), (M.m_A3_y * d));
}

Matrix3 operator*(Matrix3 M, Matrix3 & M1)
{
	Matrix3 answer =  Matrix3();
	answer.m_A1_x = M.Row(0) * M1.Column(0);
	answer.m_A1_y = M.Row(0) * M1.Column(1);
	answer.m_A1_z = M.Row(0) * M1.Column(2);
	answer.m_A2_x = M.Row(1) * M1.Column(0);
	answer.m_A2_y = M.Row(1) * M1.Column(1);
	answer.m_A2_z = M.Row(1) * M1.Column(2);
	answer.m_A3_x = M.Row(2) * M1.Column(0);
	answer.m_A3_y = M.Row(2) * M1.Column(1);
	answer.m_A3_z = M.Row(2) * M1.Column(2);

	
	return answer;
}
