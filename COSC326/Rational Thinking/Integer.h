#pragma once

// Most compilers understand the once pragma, but just in case...
#ifndef INTEGER_H_INCLUDED
#define INTEGER_H_INCLUDED

#include <iostream>
#include <string>

namespace cosc326 {

	class Integer {

	public:

		Integer();                             // Integer i;
		Integer(const Integer& i);             // Integer j(i);
		Integer(const std::string& s);         // Integer k("123");

		~Integer();

		Integer& operator=(const Integer& i);  // j = i;

		// Unary operators
		Integer operator-() const;                   // -j;
		Integer operator+() const;                   // +j;

		// Arithmetic assignment operators
		Integer& operator+=(const Integer& i); // j += i;
		Integer& operator-=(const Integer& i); // j -= i;
		Integer& operator*=(const Integer& i); // j *= i;
		Integer& operator/=(const Integer& i); // j /= i;
		Integer& operator%=(const Integer& i); // j %= i;

		// lhs < rhs -- a 'friend' means operator isn't a member, but can access the private parts of the class.
		// You may need to make some other functions friends, but do so sparingly.
		friend std::ostream& operator<<(std::ostream& os, const Integer& i);  // std::cout << i << std::endl;
		friend std::istream& operator>>(std::istream& is,  Integer& i);        // std::cin >> i;
		
		void setNeg(bool s);
		void setNum(std::string s);
		std::string getNum() const;
		std::string getInt() const;
		bool getNeg() const;
	private:
	    std::string bigNum_;
            bool isNeg_ = false;
		// Can add internal storage or methods here
	};

	// Binary operators
	Integer operator+(const Integer& lhs, const Integer& rhs); // lhs + rhs;
	Integer operator-(const Integer& lhs, const Integer& rhs); // lhs - rhs;
	Integer operator*(const Integer& lhs, const Integer& rhs); // lhs * rhs;
	Integer operator/(const Integer& lhs, const Integer& rhs); // lhs / rhs;
	Integer operator%(const Integer& lhs, const Integer& rhs); // lhs % rhs;


	bool operator<(const Integer& lhs, const Integer& rhs);
	bool operator> (const Integer& lhs, const Integer& rhs); // lhs > rhs
	bool operator<=(const Integer& lhs, const Integer& rhs); // lhs <= rhs
	bool operator>=(const Integer& lhs, const Integer& rhs); // lhs >= rhs
	bool operator==(const Integer& lhs, const Integer& rhs); // lhs == rhs
	bool operator!=(const Integer& lhs, const Integer& rhs); // lhs != rhs

	Integer gcd(const Integer& a, const Integer& b);  // i = gcd(a, b);
}

#endif
