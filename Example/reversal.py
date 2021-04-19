#reverses the process of the spritesheetCreator script
from PIL import Image
import sys

#sets all the variables we need to accurately crop the images
imageAmount = 0
i = 1
width = 0
height = 0
maxWidth = 0
maxHeight = 0
counter = 0
row = 0
column = 0
#searches for the file saved by the spritesheet script
try:
	with open("imageAtlas.txt", "r") as imageSearch:
		data = imageSearch.read()
except IOError:
    sys.exit()		
imageSearch.close()
#changes the data of the atlas to something more readable
newData = data.replace('[', '').replace('(', '').replace(',', '').replace(')', '').replace(']', '')
#takes the new data and finds out how many images were in the spritesheet
imageAmount = (newData.count(" "))/2+1
#saves the data into a list
listNew = newData.split(' ')
imageList = zip(listNew[::2],listNew[1::2])
#uses the same math in the spritesheet script to figure out how many images per row there will be
while(((len(imageList)/i) != i)) :
	i = i + 1
	if(((len(imageList)/i) < i)) :
		break
maxFramesPerRow = i
#opens the spritesheet and finds the size of the largest image
spritesheet = Image.open("spritesheet.png")
maxWidth, maxHeight = max(imageList)
#loops through the amount of images sets the size of the current image using the atlas
#the counter then checks if the row has ended if it had it moves onto the next row
#each image will be its original size the space between them being what the largest one was
#this is figued out because the space is constant
#saves each image into the current script folder
for x in range(imageAmount):
	width, height = imageList[x]
	if(counter == maxFramesPerRow):
		row = row + 1
		column = 0
		counter = 0
		area = (0, int(maxHeight)*row, int(width), int(height)+(int(maxHeight)*row))
		cropped_img = spritesheet.crop(area)
		cropped_img.save("image" + str(x+1) + ".png", "PNG")
	else:
		if(x is not 0):
			column = column+1
		area = (int(maxWidth)*column, int(maxHeight)*row, int(width)+(int(maxWidth)*column), int(height)+(int(maxHeight)*row))
		cropped_img = spritesheet.crop(area)
		cropped_img.save("image" + str(x+1) + ".png", "PNG")
	counter = counter+1
