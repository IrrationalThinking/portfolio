#include <stdio.h>
#include <math.h>
#include <stdlib.h>
#include <string.h>
#include <stdbool.h>

struct S{
    char* firstName;
    char* lastName;
    char* phone;
    char* emailAddress;
};

int i, count;


int ffn(struct S** ss, char* s){
  i = 0;
  while(i < count){
    if(strcmp(ss[i]->firstName,s) == 0){
      return 1;
    }
    i++;
  }
  return 0;
}
  
int fln(struct S** ss, char* s){
  i = 0;
  while(i < count){
    printf("%s\n", ss[i]->lastName);
    if(strcmp(ss[i]->lastName,s) == 0){
      return 1;
    }
    i++;
  }
  return 0;
}

int fem(struct S** ss, char* s){
  i = 0;
  while(i < count){
    if(strcmp(ss[i]->emailAddress,s) == 0){
      return 1;
    }
    i++;
  }
  return 0;
}

int fph(struct S** ss, char* s){
  i = 0;
  while(i < count){
    if(strcmp(ss[i]->phone, s) == 0){
      return 1;
    }
    i++;
  }
  return 0;
}

int main(int argc, char** argv) {
  /*i = 0;*/
  /*int lines = 0;
    int ch;*/
  count = 0;
  /*char buffer[1000];
    char lineSize[200];*/
  char fName[30];
  char lName[30];
  char phone[30];
  char eAddress[30];
  struct S** ss = (struct S**) malloc(100*sizeof(struct S**));
  FILE *f = fopen(argv[1], "r");

  if(f == NULL){
    printf("f was null");
    return -1;
  }

   
  while((fscanf(f, "%s %s %s %s", fName, lName, phone, eAddress)) != EOF){
    
    
    struct S* s = malloc(sizeof(*s));
    s->firstName = (char*) malloc(80 * sizeof(s->firstName[0]));
    s->lastName = (char*) malloc(80 * sizeof(s->lastName[0]));
    s->phone = (char*) malloc(80*sizeof(s->phone[0]));
    s->emailAddress = (char*) malloc(80 * sizeof(s->emailAddress[0]));
    strcpy(s->firstName, fName);
    strcpy(s->lastName, lName);
    strcpy(s->phone, phone);
    strcpy(s->emailAddress, eAddress);

    printf("%s %s %s %s\n", s->firstName, s->lastName, s->phone, s->emailAddress);

    ss[count] = s;
    count += 1;

  }
  
  fclose(f);
  int command = 10;
  printf("gets here\n");
  while(command != 0){
    char* val = malloc(10*sizeof(val[0]));
    printf("Enter search query\n");
    fscanf(stdin, "%s", val);
    printf("enter num\n");
    fscanf(stdin, "%d", &command);

    switch(command) {
    case 1:
      printf("looking for firstname %s\n", val);
      printf("found it? %d\n", ffn(ss, val));
      break;
    case 2:
      printf("looking for lastname %s\n", val);
      printf("found it? %d\n", fln(ss, val));
      break;
    case 3:
      printf("looking for phone %s\n", val);;
      printf("found it? %d\n", fph(ss, val));
      break;
    case 4:
      printf("looking for email %s\n", val);
      printf("found it? %d\n", fem(ss, val));
      break;
    default:
      break;
      
    }
    free(val);
  }
  

  free(ss);
  return 1;
}


