#include <stdio.h>
#include <string.h>
#include <strings.h>
#include <ctype.h>
#include <stdlib.h>
#define DATE_LEN 200
#define MAX_DAY_LEN 200
#define MAX_MON_LEN 200
#define MAX_YEAR_LEN 200
/*#define STRLEN 20000*/
/*Takes an input of a date with fgets then does a series of validation
 *checks returns if the date is considered valid or not.
 */

int main(void){
  /*initialsing variables*/
  int len;
  int yNum = 0, mNum= 0, dNum= 0;
  unsigned int i, f, x;
  char* expectedSymbols = "/- ";
  char d[MAX_DAY_LEN], m[MAX_MON_LEN], y[MAX_YEAR_LEN];
  char date[DATE_LEN], monLet[MAX_MON_LEN];
  int sym1 = 0, sym2 = 0, lowerCounter = 0;
  int spaceNum = 0, slashNum = 0, dashNum = 0, symNum = 0;
  int daysInMonth[12]={31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31};
  char* months[12] = {"jan", "feb", "mar", "apr", "may","jun", "jul",
		      "aug", "sep", "oct", "nov", "dec"};
  int isValid = 0;
  int monthValid = 0;
  char* reason;
  char* endStr;
  /*Takes input using fgets*/
  /*printf("Enter the date\n");*/
  while(fgets(date, DATE_LEN, stdin)!=NULL){
    /*removes the newspace that fgets reads in when we press enter*/
    date[strcspn(date,"\n")] = 0;
    /*Takes the input from the fgets and splits the string into its 3 parts
     * by using the symbols expected in the middle of the dates
     */
    for(x = 0; x < strlen(date); x++){
      if(date[x] == ' '){
	if(sym1 == 0){
	  memset(d, '\0', sizeof(d));
	  strncpy(d, date, x);
	  sym1 = x;
	  spaceNum++;
	  /*symNum++;*/
	}
	else if(sym2 == 0){
	  memset(m, '\0', sizeof(m));
	  sym2 = x;
	  strncpy(m, date+sym1+1, sym2-sym1-1);
	  spaceNum++;
	}
      }
      else if(date[x] == '/'){
	if(sym1 == 0){
	  memset(d, '\0', sizeof(d));
	  strncpy(d, date, x);
	  sym1 = x;
	  slashNum++;
	  /*symNum++;*/
	}
	else if(sym2 == 0){
	  memset(m, '\0', sizeof(m));
	  sym2 = x;
	  strncpy(m, date+sym1+1, sym2-sym1-1);
	  slashNum++;
	  
	}
      }
      else if(date[x] == '-'){
	if(sym1 == 0){
	  memset(d, '\0', sizeof(d));
	  strncpy(d, date, x);
	  sym1 = x;
	  dashNum++;
	  /*symNum++;*/
	}
	else if(sym2 == 0){
	  memset(m, '\0', sizeof(m));
	  sym2 = x;
	  strncpy(m, date+sym1+1, sym2-sym1-1);
	  dashNum++;
	  /*symNum++;*/
	}
      }
      /*this allows symbols after the inputs have been read to be counted*/
      if(date[x] == '-' || date[x] == ' ' || date[x] == '/'){
	symNum++;
      }
      
      if(!strchr(expectedSymbols, date[x]) && !isalpha(date[x]) && !isdigit(date[x])){
	isValid = 1;
	reason = "unexpected symbol found";
      }
    }
    strncpy(y, date+sym2+1, x-sym1);
    /*Validates if the amount of symbols is correct*/
    if(spaceNum == 1 || slashNum == 1 || dashNum == 1){
      isValid = 1;
      reason = "invalid symbols";
    }
    if(spaceNum == 0 && slashNum == 0 && dashNum == 0){
      isValid = 1;
      reason = "invalid symbols";
    }
    /*printf("sym num is %i\n", symNum);*/
    if(symNum > 2){
      isValid = 1;
      reason = "too many symbols";
    }
    /*Checks if there too many zeros or letters in the wrong places
     * special shoutout to Hamza for this hint*/
    len = strlen(y);
    /*printf("String len is %i", len);*/
    for(i = 0; i < sizeof(d)/sizeof(d[0]); i++){
      if(isalpha(d[i])){
	isValid = 1;
	reason = "invalid day input type";
      }
    }
    if(!isdigit(*y)){
      isValid = 1;
      reason = "invalid year input type";
    }
    if(y[0] == '0'){
      if(y[1] == '0'){
	if(len != 2){
	  isValid = 1;
	  reason = "invalid year input";
	}
      }
      else if(len != 4 && len != 2){
	isValid = 1;
	reason = "invalid year input";
      }
    }
    if(d[0] == '0' && d[1] == '0'){
      isValid = 1;
      reason = "invalid day input";
    }
    if(m[0] == '0' && m[1] == '0'){
      isValid = 1;
      reason = "invalid month input";
    }
    /*Checks if the month is known then the case validation if it is*/
    for(i = 0; i < sizeof(months)/sizeof(months[0]); i++){
      /*checks the month entered vs the months known*/
      if(!strcasecmp(m, months[i])){
	strncpy(monLet, months[i], sizeof(m));
	monthValid = 1;
	if(isupper(m[0])){
	  for(f = 1; f < strlen(m); f++){
	    if(f > 0){
	      if(!isupper(m[f])){
		lowerCounter++;
	      }
	    }
	  }
	  if(lowerCounter == 1){
	    isValid = 1;
	    reason = "Invalid casing";
	  }
	}else{
	  for(f = 1; f < strlen(m); f++){
	    if(f > 0){
	      if(isupper(m[f])){
		isValid = 1;
		reason = "Invalid casing";
	      }
	    }
	  }
	}
	mNum = i+1;
      }
    }
    /*stores the day and years as numbers for validation*/
    dNum = atoi(d);
    yNum = atoi(y);
    /*converts the original string to an int if it is a number*/
    if(strtol(m, &endStr, 10)){
      mNum = strtol(m, &endStr, 10);
      for(i = 0; i < sizeof(months)/sizeof(months[0]); i++){
	if(mNum == (int)i+1){
	  strncpy(monLet, months[i], sizeof(m));
	}
      }
      /*monLet[0] = toupper(monLet[0]);*/
      monthValid = 1;
    }
    if(monthValid == 0){
      isValid = 1;
      reason = "Month in unknown format";
    }

    /*checks if the year is in range adds 2000 or 1900 
     *if the year is below 100
     */
    if(yNum < 1753 || yNum > 3000){
      if(yNum>100){
	isValid = 1;
	reason = "Year out of range"; 
      }else if(yNum>=50){
	yNum = yNum + 1900;
      }else if(yNum<50){
	yNum = yNum + 2000;
      }
    }
    /* leap year checking, if ok add 29 days to february*/
    /*check if the number is less than 100 later*/
    if(yNum % 400 == 0 || (yNum % 100 != 0 && yNum % 4 == 0)){
      daysInMonth[1]=29;
    }
  
    /* checks the days expected in each month vs our days variable*/
    if(mNum<13){
      if(!(dNum <= daysInMonth[mNum-1])){
	isValid = 1;
	reason = "Day out of range";
      }
    }else{
      isValid = 1;
      reason = "Month out of range";
    }
    if(mNum <= 0){
      isValid = 1;
      reason = "Month out of range";
    }
    if(dNum <= 0){
      isValid = 1;
      reason = "Day out of range";
    }
    /*Prints out the date if it is considered invalid with the reason.
     *Or prints out the reformatted date with no message if it is valid.
     */
    if(isValid == 1){
      printf("%s - INVALID: %s\n", date, reason);
    }else{
      monLet[0] = toupper(monLet[0]);
      printf("%s %s %d\n", d, monLet, yNum);
    }
    /*Resets everything and reallocs memory so no more writing to memory it doesnt own*/
    memset(date, 0, sizeof date);
    daysInMonth[1] = 28;
    yNum = 0, mNum= 0, dNum= 0;
    sym1 = 0, sym2 = 0, lowerCounter = 0, symNum = 0;
    spaceNum = 0, slashNum = 0, dashNum = 0;
    isValid = 0;
    monthValid = 0;
    reason = "";
  }
  
  return 0;
}

