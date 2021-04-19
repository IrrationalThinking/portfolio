#special thanks to pylab for being the only means of displaying the circles that worked after 5 different methods
"""
- It does delete the data from the file! It shouldn't

- And it does not read all the lines of the file. If I have 8 iterations
for example from the file, it does read just three (basically 0, 1,2)...

"""
import numpy as np
import pylab
import sys
import random
from fractions import Fraction
#reads the pipped file if no file is piped it will ignore this method
def readFile():
    spacesCount = 0
    array = []
    if not sys.stdin.isatty():
        for line in sys.stdin.readlines():
            if line.strip():
                spacesCount = len(line.split())-1
                line = line.rstrip()
                array.append(line)
    prepareArrays(array, spacesCount)

def prepareArrays(array, spacesCount):
    rat = 0
    r = 0
    g = 0
    b = 0
    ratio = []
    red = []
    green = []
    blue = []
    #if no file was detected this will run
    #and generate random colours and set ratio values
    if not array:
        for x in range(0, 3):
            rand = random.randint(0,255)
            red.append(rand)
            rand = random.randint(0,255)
            green.append(rand)
            rand = random.randint(0,255)
            blue.append(rand)
        ratio.append("1")
        for x in range(0, 2):
            ratio.append("1/3")
    #uses the space count to check how many values are inputted in the files
    #if the spaces were 3 we know that a ratio was included
    #if 2 then no ratio was included
    if(spacesCount == 3):
        for line in array:
                for ch in line:                
                    rat, r, g, b = line.split(' ')
                ratio.append(rat)
                red.append(r)
                green.append(g)
                blue.append(b)
    elif(spacesCount == 2):
        ratio.append('1')
        for line in array:
                for ch in line:
                    r, g, b = line.split(' ')
                red.append(r)
                green.append(g)
                blue.append(b)
                ratio.append('1/3')
        #list will have an extra value this gets rid of it
        ratio = ratio[:-1]
    for i in range(len(red)):
        #converts the rgb values to floats because the Circle class used later
        #only uses 0-1 float values
        red[i] = float(red[i])/255
        green[i] = float(green[i])/255
        blue[i] = float(blue[i])/255
    graph(ratio, red, green, blue)

#draws the graph after scanning the arrays in the prepareCircles method    
def graph(ratio, red, green, blue):
    r = 100
    x = 0
    y = 0
    prepareCircles(x, y, r, ratio, red, green, blue, 0, len(ratio))
    pylab.axis('scaled')
    pylab.axis('off')
    pylab.savefig('circlePic')
    pylab.show()


def prepareCircles(x, y, r, ratio, red, green, blue, index, target):
    #ends the recursion when the index is equal to the target
    if(index == target):
        return
    #the 1st circle is drawn the same size as the the 2nd generation without this
    if(index == 0):
        pylab.gca().add_patch(pylab.Circle((x, y), r, fill=True, color=(red[0], green[0], blue[0])))
        ratio[index] = float(Fraction(ratio[0]))
        r = r*(float(ratio[0]))
    #happens when the index is larger than 1
    else:          
        ratio[index] = float(Fraction(ratio[index]))
        r = r*(float(ratio[index]))
        pylab.gca().add_patch(pylab.Circle((x, y), r, fill=True, color=(red[index], green[index], blue[index])))
    #this took way too long to figure out
    prepareCircles(x, y, r, ratio, red, green, blue, index+1, target) # middle
    prepareCircles(x-((r/3)*2), y, r, ratio, red, green, blue, index+1, target)  # left
    prepareCircles(x+((r/3)*2), y, r, ratio, red, green, blue, index+1, target)  # right
    prepareCircles(x-(((r/3)*2)/2), y-((r/3)+(r/2/2)), r, ratio, red, green, blue, index+1, target) # bottem left
    prepareCircles(x+(((r/3)*2)/2), y+((r/3)+(r/2/2)), r, ratio, red, green, blue, index+1, target) # top right
    prepareCircles(x-(((r/3)*2)/2), y+((r/3)+(r/2/2)), r, ratio, red, green, blue, index+1, target) # top left
    prepareCircles(x+(((r/3)*2)/2), y-((r/3)+(r/2/2)), r, ratio, red, green, blue, index+1, target) # bottem right


#main method does stuff
if __name__ == '__main__':
    readFile()
