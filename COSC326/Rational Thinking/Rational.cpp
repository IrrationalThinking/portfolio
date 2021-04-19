#include "Rational.h"

namespace cosc326 {

  Rational::Rational() : numerator(Integer()), denominator(Integer()), whole(Integer()){}
	Rational::Rational(const std::string& str) {
	  int a;
	  int b;
	  //std::cout << "hi string is " << str << std::endl;
	  if(((a = str.find(".")) != -1) && ((b = str.find("/")) != -1)){
	    whole = Integer(str.substr(0, a));
	    //std::cout << "whole is " << whole << std::endl;
	    numerator = Integer(str.substr(a+1, b-a-1));
	    //std::cout << "numerator is " << numerator << std::endl;
	    denominator = Integer(str.substr(b+1));
	    //std::cout << "denominator is " << denominator << std::endl;
	  }else if((a = str.find("/")) != -1){
	    
	    numerator = Integer(str.substr(0,a));
	    denominator = Integer(str.substr(a+1));
	  }else{
	    //std::cout << "hi" << std::endl;
	    whole = Integer(str);
	  }
	  //std::cout << a << std::endl;
	  //std::cout << b << std::endl;
	}
	Rational::Rational(const Rational& r) {
	  whole = r.whole;
	  numerator = r.numerator;
	  denominator = r.denominator;
	}
	Rational::Rational(const Integer& a) {
	  whole = a;
	}
	Rational::Rational(const Integer& a, const Integer& b) {
	  numerator = a;
	  denominator = b;
	}
	Rational::Rational(const Integer& a, const Integer& b, const Integer& c) {
	  whole = a;
	  numerator = b;
	  denominator = c;
	}
	Rational::~Rational() {}

	Rational& Rational::operator=(const Rational& r) {
		numerator = r.numerator;
		denominator = r.denominator;
		whole = r.whole;
		return *this;
	}

	Rational Rational::operator-() const {
	  Rational i = *this;
	  if(i.getWhole().getNeg()){
	    i.getWhole().setNeg(false);
	  }else{
	    i.getWhole().setNeg(true);
	  }
	  return i;
	}

	Rational Rational::operator+() const {
	  return Rational(*this);
	}

	Rational& Rational::operator+=(const Rational& r) {
		*this = *this+r;
		return *this;
	}


	Rational& Rational::operator-=(const Rational& r) {
		*this = *this-r;
		return *this;
	}

	Rational& Rational::operator*=(const Rational& r) {
		*this = *this*r;
		return *this;
	}

	Rational& Rational::operator/=(const Rational& r) {
		*this = *this/r;
		return *this;
	}

	Rational operator+(const Rational& l, const Rational& r) {
	  /* Integer denom1 = l.getDenominator();
	  Integer common = l.getDenominator()*r.getDenominator();
	  denom1 = common;
	  l.setNumerator(l.getNumerator() * common);*/
	  
	  Rational left = l;
	  Rational right = r;
	  Rational sum;
	  Integer common;
	  Integer carry;
	  Integer zero;
	  Integer one;
	  one.setNum("1");
	  zero.setNum("0");
	  if((left.getNumerator() == zero && left.getDenominator() == zero) && (right.getNumerator() == zero && right.getDenominator() == zero)){
	    return(left.getWhole()+right.getWhole());
	  }
	  if(!(left.getDenominator() == zero && right.getDenominator() == zero)){
	    if(left.getDenominator() == zero){
	      left.setDenominator(one);
	    }else if(right.getDenominator() == zero){
	      right.setDenominator(one);
	    }
	  }
	  if(!(left.getNumerator() == zero && right.getNumerator() == zero)){
	    if(left.getNumerator() == zero){
	      left.setNumerator(one);
	    }else if(right.getNumerator() == zero){
	      right.setNumerator(one);
	    }
	  }
	  sum.setWhole(left.getWhole()+right.getWhole());
	  sum.setNumerator((left.getNumerator()*right.getDenominator()) + (left.getDenominator()*right.getNumerator()));
	  sum.setDenominator(left.getDenominator()*right.getDenominator());
          if(sum.getNumerator() >= sum.getDenominator() && !sum.getNumerator().getNeg() == !sum.getDenominator().getNeg()){
	    sum.setWhole(sum.getWhole() + (sum.getNumerator()/sum.getDenominator()));
	    carry = (sum.getNumerator() - sum.getDenominator());
	    if(carry == zero){
	      sum.setNumerator(zero);
	      sum.setDenominator(zero);
	    }else{
	      sum.setNumerator(carry);
	    }
	  }
	  common = gcd(sum.getNumerator(), sum.getDenominator());
	  sum.setDenominator(sum.getDenominator() / common);
	  sum.setNumerator(sum.getNumerator()/common);
	  if(sum.getDenominator() == sum.getNumerator()){
	    sum.setDenominator(zero);
	    sum.setNumerator(zero);
	  }
	  if((sum.getDenominator().getNeg() || sum.getNumerator().getNeg())){
	    std::cout << "in here" << std::endl;
	    if((sum.getDenominator().getNum() != zero.getNum()) && (sum.getNumerator().getNum() != zero.getNum())){
	      std::cout << "jdfobosdniobdhip0" << std::endl;
	      //have no effect not sure why.........
	      sum.getWhole().setNeg(true);
	      sum.getNumerator().setNeg(false);
	      if(sum.getNumerator().getNeg() == true){
		std::cout << "sdfsdfsdfiosjfdspiodfpsjjifdspiojsdpsjpsojidfpoisjdfpoji" << std::endl;
	      }
	      sum.getDenominator().setNeg(false);
	    }
	  }
	  if(sum.getNumerator() == zero && sum.getWhole() == zero){
	    sum.setDenominator(zero);
	  }
	  return sum;
	  //return l;
	}

  Rational operator-(const Rational& l, const Rational& r) {
    //std::cout << "in method" << std::endl;
    Rational left = l;
    Rational right = r;
    Rational sum;
    Integer common;
    Integer zero;
    Integer carry;
    Integer one;
    one.setNum("1");
    zero.setNum("0");
    if((left.getNumerator() == zero && left.getDenominator() == zero) && (right.getNumerator() == zero && right.getDenominator() == zero)){
      return(left.getWhole()-right.getWhole());
    }
    if(!(left.getDenominator() == zero && right.getDenominator() == zero)){
      if(left.getDenominator() == zero){
	left.setDenominator(one);
      }else if(right.getDenominator() == zero){
	right.setDenominator(one);
      }
    }
    if(!(left.getNumerator() == zero && right.getNumerator() == zero)){
      if(left.getNumerator() == zero){
	left.setNumerator(one);
      }else if(right.getNumerator() == zero){
	right.setNumerator(one);
      }
    }
    sum.setWhole(left.getWhole()-right.getWhole());
    /*if((left.getNumerator()*right.getDenominator()) > (left.getDenominator()*right.getNumerator())){
      sum.setNumerator((left.getDenominator()*right.getNumerator()) - (left.getNumerator()*right.getDenominator()));
      }*/
    sum.setNumerator((left.getNumerator()*right.getDenominator()) - (left.getDenominator()*right.getNumerator()));
    sum.setDenominator(left.getDenominator()*right.getDenominator());
    //std::cout << sum.getNumerator() << "/" << sum.getDenominator() << std::endl;
    if(sum.getNumerator() >= sum.getDenominator() && !sum.getNumerator().getNeg() == !sum.getDenominator().getNeg()){
      //std::cout << "HI" << std::endl;
      sum.setWhole(sum.getWhole() + (sum.getNumerator()/sum.getDenominator()));
      carry = (sum.getNumerator() - sum.getDenominator());
      if(carry == zero){
	sum.setNumerator(zero);
	sum.setDenominator(zero);
      }else{
	sum.setNumerator(carry);
      }
    }
    //std::cout << "in method" << std::endl;
    common = gcd(sum.getNumerator(), sum.getDenominator());
    /*if(common == zero){
      sum.setNumerator(zero);
      sum.setDenominator(zero);
      } doesnt do anything i think. */
    //std::cout << "hhhhhhhh" << std::endl;
    sum.setDenominator(sum.getDenominator() / common);
    //std::cout << "in method" << std::endl;
    sum.setNumerator(sum.getNumerator()/common);
    if(sum.getDenominator() == sum.getNumerator()){
      sum.setDenominator(zero);
      sum.setNumerator(zero);
    }
    /*if((sum.getDenominator().getNeg() || sum.getNumerator().getNeg())){
      std::cout << "in here" << std::endl;
      if((sum.getDenominator().getNum() != zero.getNum()) && (sum.getNumerator().getNum() != zero.getNum())){
	std::cout << "jdfobosdniobdhip0" << std::endl;
	have no effect not sure why.........
	sum.getWhole().setNeg(true);
	Rational copy;
	copy.getNumerator()
	sum.getNumerator().setNeg(false);
	if(copy.getNumerator().getNeg() == true){
	  std::cout << "sdfsdfsdfiosjfdspiodfpsjjifdspiojsdpsjpsojidfpoisjdfpoji" << std::endl;
	}
	sum.getDenominator().setNeg(false);
      }
    }*/
    //std::cout << "sum is " << sum << std::endl;
    if(sum.getNumerator() == zero && sum.getWhole() == zero){
      sum.setDenominator(zero);
    }
    return sum;
  }
  
  Rational operator*(const Rational& l, const Rational& r) {
    Rational left = l;
    Rational right = r;
    Rational sum;
    Integer common;
    Integer zero;
    Integer carry;
    Integer one;
    one.setNum("1");
    if((left.getNumerator() == zero && left.getDenominator() == zero) && (right.getNumerator() == zero && right.getDenominator() == zero)){
      return(left.getWhole()*right.getWhole());
    }
    if(!(left.getWhole() == zero && right.getWhole() == zero)){
      if(left.getWhole() == zero){
	left.setWhole(one);
      }else if(right.getWhole() == zero){
	right.setWhole(one);
      }
    }
    if(!(left.getDenominator() == zero && right.getDenominator() == zero)){
      if(left.getDenominator() == zero){
	left.setDenominator(one);
      }else if(right.getDenominator() == zero){
	right.setDenominator(one);
      }
    }
    if(!(left.getNumerator() == zero && right.getNumerator() == zero)){
      if(left.getNumerator() == zero){
	left.setNumerator(one);
      }else if(right.getNumerator() == zero){
	right.setNumerator(one);
      }
    }
    sum.setWhole((left.getWhole()*right.getWhole()));
    sum.setNumerator((left.getNumerator()*right.getNumerator()));
    sum.setDenominator((left.getDenominator()*right.getDenominator()));
    if(sum.getNumerator() >= sum.getDenominator()){
      //std::cout << "in method" << std::endl;
      sum.setWhole(sum.getWhole() + (sum.getNumerator()/sum.getDenominator()));
      carry = (sum.getNumerator() - sum.getDenominator());
      if(carry == zero){
	sum.setNumerator(zero);
	sum.setDenominator(zero);
      }else{
	sum.setNumerator(carry);
      }
    }
    //std::cout << sum.getNumerator() << std::endl;
    //std::cout << sum.getDenominator() << std::endl;
    //common = gcd(sum.getNumerator(), sum.getDenominator());
    common = gcd(sum.getNumerator(), sum.getDenominator());
    //std::cout << common << std::endl;
    sum.setDenominator(sum.getDenominator() / common);
    //std::cout << "in method" << std::endl;
    sum.setNumerator(sum.getNumerator()/common);
    if(sum.getDenominator() == sum.getNumerator()){
      sum.setDenominator(zero);
      sum.setNumerator(zero);
    }
    /*if(sum.getWhole() == zero && (sum.getNumerator().getNeg() || sum.getDenominator().getNeg())){ \
      if(sum.getNumerator().getNeg() && sum.getDenominator().getNeg()){
	std::cout << "hi" << std::endl;
	sum.getNumerator().setNeg(true);
      }else{
	std::cout << "ha" << std::endl;
      }
      if(sum.getNumerator().getNeg() == true){
	std::cout << "truebtw" << sum.getNumerator().getNeg() << std::endl;
      }
      }*/
    if(sum.getNumerator().getNeg() != sum.getDenominator().getNeg()){
       if(sum.getDenominator() < sum.getNumerator()){
	 sum.setNumerator(-gcd(sum.getNumerator(), sum.getDenominator()));
       }else{
	 sum.setNumerator(-gcd(sum.getNumerator(), sum.getDenominator()));
       }
       if(sum.getNumerator().getNum() == sum.getDenominator().getNum()){
	 sum.setDenominator(zero);
	 sum.setNumerator(zero);
       }
    }
    if((sum.getDenominator().getNeg() || sum.getNumerator().getNeg())){
      std::cout << "in here" << std::endl;
      if((sum.getDenominator().getNum() != zero.getNum()) && (sum.getNumerator().getNum() != zero.getNum())){
	std::cout << "jdfobosd" << std::endl;
	/*has no effect not sure why.........*/
	sum.getWhole().setNeg(true);
	sum.getNumerator().setNeg(false);
	if(sum.getNumerator().getNeg() == true){
	  std::cout << "sdfsdfsdfiosjfdspiodfpsjjifdspiojsdpsjpsojidfpoisjdfpoji" << std::endl;
	}
	sum.getDenominator().setNeg(false);
      }
    }
    if(sum.getNumerator() == zero && sum.getWhole() == zero){
      sum.setDenominator(zero);
    }
    //std::cout << gcd(sum.getNumerator(), sum.getDenominator()) << std::endl;
    return sum;
    //return l;
  }
  
  Rational operator/(const Rational& l, const Rational& r) {

    Rational left = l;
    Rational right = r;
    Rational sum;
    Integer common;
    Integer zero;
    Integer one;
    Integer carry;
    one.setNum("1");
    if((left.getNumerator() == zero && left.getDenominator() == zero) && (right.getNumerator() == zero && right.getDenominator() == zero)){
      return(left.getWhole()/right.getWhole());
    }
    if(!(left.getWhole() == zero && right.getWhole() == zero)){
      if(left.getWhole() == zero){
	left.setWhole(one);
      }else if(right.getWhole() == zero){
	right.setWhole(one);
      }
    }

    
    sum.setWhole((left.getWhole()/right.getWhole()));
    if(left.getNumerator() == zero && left.getDenominator() == zero){
      sum.setNumerator(right.getNumerator());
      sum.setDenominator(right.getDenominator());
    }else if(right.getNumerator() == zero && right.getDenominator() == zero){
      sum.setNumerator(left.getNumerator());
      sum.setDenominator(left.getDenominator());
    }else{
      sum.setNumerator((left.getNumerator()*right.getDenominator()));
      sum.setDenominator((left.getDenominator()*right.getNumerator()));
    }
    //std::cout << left.getNumerator() << "/" << right.getDenominator() << std::endl;
    //std::cout << left.getDenominator() << "/" <<right.getNumerator() << std::endl;
    //std::cout << right.getNumerator() << std::endl;
    //std::cout << right.getDenominator() << std::endl;
    //std::cout << sum.getNumerator() << std::endl;
    //std::cout << sum.getDenominator() << std::endl;
    if(sum.getNumerator() >= sum.getDenominator()){
      //std::cout << "hi" << std::endl;
      sum.setWhole(sum.getWhole() + (sum.getNumerator()/sum.getDenominator()));
      carry = (sum.getNumerator() - sum.getDenominator());
      if(carry == zero){
	sum.setNumerator(zero);
	sum.setDenominator(zero);
      }else{
	sum.setNumerator(carry);
      }
    }
    // std::cout << "arrives here alive" << std::endl;
    common = gcd(sum.getNumerator(), sum.getDenominator());
    //std::cout << sum.getNumerator() << std::endl;
    //std::cout << sum.getDenominator() << std::endl;
    sum.setDenominator(sum.getDenominator() / common);
    //std::cout << "in method" << std::endl;
    sum.setNumerator(sum.getNumerator() / common);
    
    if(sum.getNumerator().getNeg() != sum.getDenominator().getNeg()){
       if(sum.getDenominator() < sum.getNumerator()){
	 sum.setNumerator(-gcd(sum.getNumerator(), sum.getDenominator()));
	 if(sum.getNumerator().getNum() == sum.getDenominator().getNum()){
	   sum.setDenominator(zero);
	   sum.setNumerator(zero);
	 }
       }else{
	 sum.setNumerator(-gcd(sum.getNumerator(), sum.getDenominator()));
       }
    }else{
      if(sum.getDenominator() > sum.getNumerator()){
	 sum.setNumerator(gcd(sum.getNumerator(), sum.getDenominator()));
	 if(sum.getNumerator().getNum() == sum.getDenominator().getNum()){
	   sum.setDenominator(zero);
	   sum.setNumerator(zero);
	 }
      }
    }
    if(sum.getDenominator() == sum.getNumerator()){
      sum.setDenominator(zero);
      sum.setNumerator(zero);
    }
    if((sum.getDenominator().getNeg() || sum.getNumerator().getNeg())){
      std::cout << "in here" << std::endl;
      if((sum.getDenominator().getNum() != zero.getNum()) && (sum.getNumerator().getNum() != zero.getNum())){
	std::cout << "jdfobosdniobdhip0" << std::endl;
	/*have no effect not sure why.........*/
	sum.getWhole().setNeg(true);
	sum.getNumerator().setNeg(false);
	if(sum.getNumerator().getNeg() == true){
	  std::cout << "sdfsdfsdfiosjfdspiodfpsjjifdspiojsdpsjpsojidfpoisjdfpoji" << std::endl;
	}
	sum.getDenominator().setNeg(false);
      }
    }
    //std::cout << "end of method " << std::endl;
    if(sum.getNumerator() == zero && sum.getWhole() == zero){
      sum.setDenominator(zero);
    }
    return sum;
    //return l;
  }
  
  std::ostream& operator<<(std::ostream& os, const Rational& i){
    Integer zero;
    zero.setNum("0");
    //std::cout << i.denominator << std::endl;
    if(i.whole == zero && i.numerator == zero && i.denominator == zero){
      std::cout << "here" << std::endl;
      os << "0";
      return os;
    }
    if(i.whole != Integer()){
      os << i.whole;
      if(i.numerator != Integer()){
	os << ".";
      }
    }
    if(i.numerator != Integer()){
      os << i.numerator;
      os << "/";
      os << i.denominator;
    }

    return os;
  }
  
  std::istream& operator>>(std::istream& is, Rational& i) {
    std::string s;
    is >> s;
    Rational copy = Rational(s);
    i = copy;
    return is;
  }
  
  bool operator<(const Rational& l, const Rational& r) {
    return(!(l >= r));
  }
  
  bool operator> (const Rational& l, const Rational& r) { 
    if(l.getWhole() > r.getWhole()){
      return true;
    }else if(l.getWhole() == r.getWhole()){
      if(l.getNumerator()*r.getDenominator() > l.getDenominator()*r.getNumerator()){
	return true;
      }
    }
    return false;
  }
  
  bool operator<=(const Rational& l, const Rational& r) {
    return((l < r) || (l == r));
    //return true;
  }
  
  bool operator>=(const Rational& l, const Rational& r) {
    return((l > r) || (l == r));
    //return true;
  }
  
  bool operator==(const Rational& l, const Rational& r) {
    return(l.getDenominator() == r.getDenominator() && l.getNumerator() == r.getNumerator() && l.getWhole() == r.getWhole());
    //return !((l > r) && (l < r));
    //return true;
  }

  bool operator!=(const Rational& l, const Rational& r) {
    return !(l == r);
    //return true;
  }
  
  void Rational::setNumerator(Integer i){
    numerator = i;
  }	
  Integer Rational::getNumerator() const {
    return numerator;
  }
  void Rational::setDenominator(Integer i){
    denominator = i;
  }	
  Integer Rational::getDenominator() const {
    return denominator;
  }
  void Rational::setWhole(Integer i){
    whole = i;
  }	
  Integer Rational::getWhole() const {
    return whole;
  }
  
  
}
/*int main (int argc, char* argv[]){
  std::cout << "hi" << std::endl;
  cosc326::Integer i1("1000");
  cosc326::Integer i2("200");
  std::cout << "hi" << std::endl;
  cosc326::Rational r1(i1);
  cosc326::Rational r2(i2);
  std::cout << i1.getNum() << std::endl;
  std::cout << i2.getNum() << std::endl;
  std::cout << "hi" << std::endl;
  if(i1 == i2){
    std::cout << "works" << std::endl;
  }
  cosc326::Rational sum;
  //sum = gcd(i1, i2);
  sum = r1 + r2;
  std::cout << "sum is " << sum << std::endl;
  std::cout << i1.getNum() << std::endl;
  std::cout << i2.getNum() << std::endl;
}*/
