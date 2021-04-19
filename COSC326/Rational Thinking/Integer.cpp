#include "Integer.h"
#include <string>
#include <cstdlib>
#include <cmath>
#include <vector>
#include <algorithm>
#include <sstream>
	
namespace cosc326 {

  Integer::Integer() : bigNum_("0"){}

  Integer::Integer(const Integer& bigNum) : bigNum_(bigNum.bigNum_), isNeg_(bigNum.isNeg_){}

  Integer::Integer(const std::string& s) : bigNum_(s) {
    if(s[0] == '-'){
      bigNum_ = s.substr(1);
      setNeg(true);
    }else if(s[0] == '+'){
      bigNum_ = s.substr(1);
      setNeg(false);
    }else{
      setNeg(false);
      bigNum_ = s;
    }
  }


  Integer::~Integer() {}

	
  Integer& Integer::operator=(const Integer& i) {
    bigNum_ = i.bigNum_;
    isNeg_ = i.isNeg_;
    //std::cout << "isneg is " << i.isNeg_ << std::endl;
    //std::cout << "bigNum is " << i.bigNum_ << std::endl;
    return *this;
  }

  Integer Integer::operator-() const {
    Integer i = *this;
    i.isNeg_ = !i.isNeg_;
    return i;
  }

  Integer Integer::operator+() const {
    return *this;
  }

  Integer& Integer::operator+=(const Integer& i) {
    *this = *this + i;
    return *this;
  }

  Integer& Integer::operator-=(const Integer& i) {
    *this = *this - i;
    return *this;
  }

  Integer& Integer::operator*=(const Integer& i) {
    *this = *this * i;
    return *this;
  }

  Integer& Integer::operator/=(const Integer& i) {
    *this = *this / i;
    return *this;
  }

  Integer& Integer::operator%=(const Integer& i) {
    *this = *this % i;
    return *this;
  }

  Integer operator+(const Integer& i1, const Integer& i2) {
    Integer result;
    Integer n1 = i1;
    Integer n2 = i2;
    //std::cout << "i1 is " << i1 << std::endl; 
    std::string store = "";
    std::string number1 = i1.getNum();
    std::string number2 = i2.getNum();
    //std::cout << "i1 is neg is" << i1.getNeg() << "i2 is neg is" << i2.getNeg() << std::endl;
    //std::cout << "n1 + is " << number1 << std::endl;
    //std::cout << "n2 + is " << number2 << std::endl;
    if(i1.getNeg() || i2.getNeg()){
      if(i1.getNeg() && i2.getNeg()){
	result.setNeg(true);
      }else if(i1.getNeg() & !i2.getNeg()){
	n1.setNeg(false);
	return((Integer(n1) - Integer(n2)));
      }else{
	n2.setNeg(false);
	result = ((Integer(n1) - Integer(n2)));
	result.setNeg(true);
	if(result.getNum() == "0" && result.getNeg()){
	  result.setNum("0");
	  result.setNeg(false);
	}
	return result;
      }
    }
		
    if(number1.length() > number2.length()){
      number1.swap(number2);
    }
    
    int len1 = number1.length();
    int len2 = number2.length();
    int difference = len2 - len1;
    
    int carry = 0;
    
    for(int i = len1-1; i >=0; i--){
      int sum = ((number1[i]-'0')+(number2[i+difference]-'0') + carry);
      store.push_back(sum%10 + '0');
			
      carry = sum/10;
    }
    
    for(int i = len2-len1-1; i>=0; i--){
      int sum = ((number2[i]-'0')+ carry);
      store.push_back(sum%10 + '0');
      carry = sum/10;
    }
    if(carry){
      store.push_back(carry+'0');
    }
    reverse(store.begin(), store.end());
    
    result.setNum(store);
    if(result.getNum() == "0" && result.getNeg()){
      result.setNum("0");
      result.setNeg(false);
    }
    return result;
    
  }

  Integer operator-(const Integer& i1, const Integer& i2) {
    //bool isNeg = false;
    Integer n1 = i1;
    Integer n2 = i2;
    std::string store = "";
    std::string number1 = n1.getNum();
    std::string number2 = n2.getNum();
    //std::cout << "n1 is " << number1 << std::endl;
    //std::cout << "n2 is " << number2 << std::endl;
    Integer result;
    if(i1.getNeg() || i2.getNeg()){
      if(i1.getNeg() && i2.getNeg()){
	//isNeg = true;
	//std::cout << "both are neg" << std::endl;
	result.setNeg(true);
      }else if(i1.getNeg() & !i2.getNeg()){
	n1.setNeg(false);
	result = ((Integer(n1) + Integer(n2)));
	result.setNeg(true);
	if(result.getNum() == "0" && result.getNeg()){
	  result.setNum("0");
	  result.setNeg(false);
	}
	return result;
      }else{
	n2.setNeg(false);
	return((Integer(n2) + Integer(n1)));
      }
    }
    if(i1.getNeg() && i2.getNeg()){
      if(i1 == i2){
	result.setNum("0");
	result.setNeg(false);
	return result;
      }
    }
    
    int len1 = number1.length();
    int len2 = number2.length();
    

    if(result.getNeg()){
      if(n1 < n2){
	//swap(number2, number1);
	result.setNeg(true);
	//std::cout << "in neg swap" << std::endl;
      }else{
	swap(number2, number1);
	result.setNeg(false);
      }
      len1 = number1.length();
      len2 = number2.length();
      //result.setNeg(false);
    }else{
      if(number1.length() < number2.length()){
	//std::cout << "in non neg swap" << std::endl;
	swap(number2, number1);
	len1 = number1.length();
	len2 = number2.length();
	result.setNeg(true);
	//std::cout << "n1 is " << number1 << std::endl;
	//std::cout << "n2 is " << number2 << std::endl;
      }else if(n1 < n2 && number1.length() == number2.length()){
	swap(number1, number2);
	result.setNeg(true);
      }
    }
    /*if(number1.length() < number2.length()){
      swap(number2, number1);
      }*/
    int difference = len1-len2;
    /*if(len1 > len2){
      number1.swap(number2);
      }*/
    int carry = 0;
    for(int i = len2-1; i>=0; i--){
      int sum = ((number1[i+difference]-'0')-(number2[i]-'0') - carry);
      if(sum < 0){
	sum = sum+10;
	carry = 1;
      }else{
	carry = 0;
      }
      
      store.push_back(sum + '0');
    }
    
    for(int i = len1-len2-1; i>=0; i--){
      if(number1[i]=='0' && carry){
	store.push_back('9');
	continue;
      }
      int sub = ((number1[i]-'0')- carry);
      if(sub > 0||i>0){
	store.push_back(sub + '0');
      }
      carry = 0;
    }
    

    reverse(store.begin(), store.end());
    
    if(store[0] == '0' && store.length() > 1){
      store.erase(store.begin()+1, store.end());
    }
    result.setNum(store);
    //std::cout << store << std::endl;
    if(result.getNum() == "0" && result.getNeg()){
      result.setNum("0");
      result.setNeg(false);
    }
    
    return result;
    
  }

  Integer operator*(const Integer& i1, const Integer& i2) {
    Integer result;
    std::string store = "";
    std::string number1 = i1.getNum();
    std::string number2 = i2.getNum();
    if(i1.getNeg() || i2.getNeg()){
      //std::cout << "hi" << std::endl;
      if(i1.getNeg() && i2.getNeg()){
	
	result.setNeg(false);
      }else if(i1.getNeg() && !i2.getNeg()){
	//std::cout << "hi5" << std::endl;
	result.setNeg(true);
      }else{
	//std::cout << "hi4" << std::endl;
	result.setNeg(true);
      }
    }

		
    if(number1.length() == 0 || number2.length() == 0){
      result.setNum("0");
      return result.getNum();
    }	
    if(number1.length() > number2.length()){
      number1.swap(number2);
    }
    int len = number1.length(); 
    std::string answer = "0";
    for(int i = len-1; i>=0; i--){
      int carry = 0;
      int currentNum = number1[i] -'0';
			
      std::string index = number2;
      
      
      for(int j = index.length()-1; j >=0; j--){
	index[j] = ((index[j]-'0') * currentNum) + carry;
	if(index[j] > 9){
	  carry = (index[j]/10);
	  index[j] -= (carry*10);
	}else{
	  carry = 0;
	}
	index[j] += '0';
      }
      if(carry > 0){
	index.insert(0, 1, (carry+'0'));
      }
      index.append((number1.length()-i-1), '0');
      answer = (Integer(answer) + Integer(index)).getNum();
    }
    while(answer[0] == '0' && answer.length()!= 1){
      answer.erase(0, 1);
    }
    result.setNum(answer);
    return result;
  }

  Integer operator/(const Integer& i1, const Integer& i2) {
    //bool i1neg = false;
    //bool i2neg = false;
    Integer numerator = i1;
    Integer denominator = i2;
    Integer result;
    if(i1.getNum() == "0" && i2.getNum() == "0"){
      result.setNum("0");
      result.setNeg(false);
      return result;
    }else if(i1.getNum() == "0" || i2.getNum() == "0"){
      result.setNum("1");
      result.setNeg(false);
      return result;
    }
    
    numerator.setNeg(false);
    denominator.setNeg(false);
    
    result.setNum("0");
   
    //numerator.setNeg(false);
    //denominator.setNeg(false);
    /*if(i1.getNeg() || i2.getNeg()){
      if(i1.getNeg() && i2.getNeg()){
	//numerator.setNeg(false);
	//denominator.setNeg(false);
	i1neg = true;
	i2neg = true;
      }else if(i1.getNeg() & !i2.getNeg()){
	//numerator.setNeg(false);
	i1neg = true;
      }else{
	//denominator.setNeg(false);
	i2neg =true;
      }
      }*/
    
    if(i1.getNum() == i2.getNum() && i1.getNeg() != i2.getNeg()){
      result.setNum("1");
      return result;
    }
    Integer num;
    num.setNeg(false);
    num.setNum("1");
    //if(i1 > i2){
    //num.setNum("0");
    // return num;
    //}
    while(numerator >= denominator){
        numerator -= denominator;
	result+= num;
      //std::cout << "result" << result << std::endl;
      //result += num;
    }
    result.setNeg(true);
    if((i1.getNeg() && i2.getNeg()) || (!i1.getNeg() && !i2.getNeg())){
      result.setNeg(false);
    }
    //std::cout << "hi" << std::endl;
    if(result.getNum() == "0" && result.getNeg()){
      result.setNum("0");
      result.setNeg(false);
    }
    return result;
  }

  Integer operator%(const Integer& i1, const Integer& i2) {
    bool i1neg = false;
    bool i2neg = false;
    Integer numerator = i1;
    Integer denominator = i2;
    numerator.setNeg(false);
    denominator.setNeg(false);
    Integer result;
    if(i1.getNeg() || i2.getNeg()){
      if(i1.getNeg() && i2.getNeg()){
	i1neg = true;
	i2neg = true;
      }else if(i1.getNeg() & !i2.getNeg()){
	i1neg = true;
      }else{
	i2neg =true;
      }
    }
    if(numerator < denominator){
      if(i1.getNeg()){
	numerator.setNeg(true);
      }
      return numerator;
    }
    while(numerator >= denominator){
      numerator -= denominator;
    }
    if(i1neg || i2neg){
      numerator -= denominator;
      numerator.setNeg(true);
      // std::cout << "numerator " << numerator << std::endl;
    }
    result = numerator;
    if(result == denominator){
      result.setNum("0");
    }
    //std::cout << "result " << result << std::endl;
    return result;
	
  }

  std::ostream& operator<<(std::ostream &os, const Integer& i){
    /*stuff with negatives will have to be done here*/
    //std::cout << "hi0000000" << std::endl;
    std::string s;
    if(i.isNeg_){
      s.insert(0, 1, '-');
    }
    s += i.bigNum_;
    os << s;
    return os;
  }
  
  std::istream& operator>>(std::istream &is, Integer& i){
    std::string input;
    is>>input;
    Integer copy = Integer(input);
    i = copy;
    return is;
  }

  bool operator<(const Integer& i1, const Integer& i2) {
    Integer ii = i1;
    Integer iii = i2;
    if(i1.getNeg() || i2.getNeg()){
      //std::cout << "< ii " <<ii.getNum() << std::endl;
      //std::cout << "< iii " <<iii.getNum() << std::endl;
      if(i1.getNeg() && i2.getNeg()){
	ii.setNeg(false);
	iii.setNeg(false);
	return(Integer(ii) > Integer(iii));
      }else if(!i1.getNeg() && i2.getNeg()){
	return true;
      }else{
	return false;
      }
    }
    std::string str1 = i1.getNum();
    std::string str2 = i2.getNum();
    int n1 = str1.length(), n2 = str2.length();
    if (n1 < n2)
      return true;
    if (n2 > n1)
      return false;
    
    for (int i=0; i<n1; i++){
      if (str1[i] < str2[i])
	return true;
      else if (str1[i] > str2[i])
	return false;
    }
    return false;
  }

  bool operator> (const Integer& i1, const Integer& i2) {
    Integer ii = i1;
    Integer iii = i2;
    
    // std::cout << "ii " <<i1.getNum() << std::endl;
    //std::cout << "iii " <<i2.getNum() << std::endl;
    if(i1.getNeg() || i2.getNeg()){
      if(i1.getNeg() && i2.getNeg()){
	ii.setNeg(false);
	iii.setNeg(false);
	return(Integer(ii) < Integer(iii));
      }else if(i1.getNeg() && !i2.getNeg()){
	return true;
      }else{
	return false;
      }
    }
    std::string str1 = i1.getNum();
    std::string str2 = i2.getNum();
    int n1 = str1.length(), n2 = str2.length();
    if (n1 > n2){
      //std::cout << "> returns true" << std::endl;
      return true;
    }
    if (n2 > n1){
      //std::cout << "returns false" << std::endl;
      return false;
    }
    
    for (int i=0; i<n1; i++){
      if (str1[i] > str2[i]){
	//std::cout << " > returns true" << std::endl;
	return true;
      }
      else if (str1[i] < str2[i]){
	//std::cout << "returns false" << std::endl;
	return false;
      }
    }
    return false;
  }

  bool operator<=(const Integer& i1, const Integer& i2) {
    return((i1 < i2) || (i1 == i2));
  }

  bool operator>=(const Integer& i1, const Integer& i2) {
    return((i1 > i2) || (i1 == i2));
  }

  bool operator==(const Integer& i1, const Integer& i2) {
    if(i1.getNeg() == i2.getNeg()){
      return !((i1 > i2) || (i1 < i2));
    }
    return false;
  }

  bool operator!=(const Integer& i1, const Integer& i2) {
    return !(i1 == i2);
  }


  Integer gcd(const Integer& i1, const Integer& i2){
    Integer n1 = i1;
    Integer n2 = i2;
    n1.setNeg(false);
    n2.setNeg(false);
    //std::cout << n1 << " n2 " << n2 << std::endl;
    if(n1 == n2){
      return n1;
    }
    //n1.setNeg(false);
    //n2.setNeg(false);
    if(n1 == Integer() || n2 == Integer()){
      //std::cout << "gcd " << std::endl;
      n1.setNum("0");
      return n1;
    }
    while(n1 != n2){
      //std::cout << n1.getNum() << std::endl;
      /*if the gcd is 1 this prevents it from breaking thanks negative bullshit*/
      if(n2 > i2){
	n1.setNum("1");
	return n1;
      }else if(n1 > i1){
	n2.setNum("1");
	return n2;
      }else{
	if(n1 > n2){
	  n1-=n2;
	}else{
	  n2-=n1;	  
	}
      }
    }
    Integer result = n1;
    //std::cout << result << std::endl;
    return result;
  }

  void Integer::setNeg(bool i){
    isNeg_ = i;
  }
  void Integer::setNum(std::string i){
    bigNum_ = i;
  }	
  std::string Integer::getNum() const {
    return bigNum_;
  }
  
  bool Integer::getNeg() const{
    return isNeg_;
  }
  /*std::string Integer::getInt() const {
    if(getNeg()){
    return "-" + bigNum_;
    }else{
    return bigNum_;
    }
    }*/

}
/*int main (int argc, char* argv[]){
  cosc326::Integer i1("1");
  cosc326::Integer i2("1");
  
  std::cout << i1.getNum() << std::endl;
  std::cout << i2.getNum() << std::endl;
  if(i1 == i2){
  std::cout << "works" << std::endl;
  }
  cosc326::Integer sum;
  //sum = gcd(i1, i2);
  sum = -i1 - i2;
  std::cout << "sum is " << sum.getNum() << std::endl;
  std::cout << i1.getNum() << std::endl;
  std::cout << i2.getNum() << std::endl;
  }
*/
