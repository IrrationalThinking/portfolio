from PIL import Image
import os, math, time
#sets the max frames per row this is easily changable depending on the amount of images
maxFramesPerRow = 0.0
frames = []
i = 1
#stores the height and width of each image
width = 0
height = 0
#the height and width of the new image
totalHeight = 1
totalWidth = 1
#the height and width of the actual sheet created
sheetWidth = 0
sheetHeight = 0
#sorts the files if they are numbered, useful if the sprites are used for animation
files = os.listdir("exampleSourceFiles/")
files.sort()
#opens each file in the directory and stores its data into the frame list
for currentFile in files :
	try:
		im = Image.open(open("exampleSourceFiles/" + currentFile, 'rb'))
		frames.append(im.getdata())
		#print(im)
	except IOError:
		print(currentFile + " not valid file")
	except:
		print(currentFile + " can't open file")
#sets the size of the tiles to be the size of the largest image
width, height = max(Image.open("exampleSourceFiles/" + currentFile, 'r').size for currentFile in files)
#calculates the most efficent number of images per row by checking if
#i is either equal to or less than itself divided by the amount of images
while(((len(frames)/i) != i)) :
	i = i + 1
	if(((len(frames)/i) < i)) :
		break
maxFramesPerRow = i
#calculates the size of the sheet based off of the largest image
if len(frames) > maxFramesPerRow :
	spritesheetWidth = width * maxFramesPerRow
	requiredRows = math.ceil(len(frames)/maxFramesPerRow) 
	spritesheetHeight = height * requiredRows
else:
	spritesheetWidth = width*len(frames)
	spritesheetHeight = height

#sets the height and width of the actual spritesheet makes sure it is a squared size
if(spritesheetHeight >= spritesheetWidth) :
	while(totalHeight <= spritesheetHeight) :
		totalHeight = totalHeight + totalHeight
		totalWidth = totalWidth + totalWidth		
else :
	while(totalWidth <= spritesheetWidth) :
		totalHeight = totalHeight + totalHeight
		totalWidth = totalWidth + totalWidth
#creates the spritesheet with the width and height gained from the above while loops
spritesheet = Image.new("RGBA",(int(totalHeight), int(totalWidth)))
#this is when the frames get copied onto the created spritesheet the size of 
#each frame is that of the largest image,  
for currentFrame in frames :
	top = height * math.floor((frames.index(currentFrame))/maxFramesPerRow)
	left = width * (frames.index(currentFrame) % maxFramesPerRow)
	bottom = top + height
	right = left + width
	#creates each box for the image then cuts the image out of the frame and pastes it
	box = (left,top,right,bottom)
	box = [int(i) for i in box]
	cutFrame = currentFrame.crop((0,0,width,height))
	
	spritesheet.paste(cutFrame, box)
spritesheet.save("spritesheet" + time.strftime("%Y-%m-%dT%H-%M-%S") + ".png", "PNG")
