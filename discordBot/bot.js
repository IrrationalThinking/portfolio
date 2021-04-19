const Discord = require('discord.js');
const bot = new Discord.Client();
const img = new Discord.RichEmbed();
const fs = require('fs');
bot.on('ready', () => {
	bot.user.setActivity('!Help | Showing people true quality');
	console.log('Logged in as $(client.user.tag)!');
});
bot.on('message', msg => {
	if (msg.content === '!fatshark'){
		
		msg.channel.send('<:quality:618970093510197249> <:quality:618970093510197249> <:quality:618970093510197249> <:quality:618970093510197249> <:quality:618970093510197249> <:quality:618970093510197249> <:quality:618970093510197249> <:quality:618970093510197249> <:quality:618970093510197249> <:quality:618970093510197249> <:quality:618970093510197249> <:quality:618970093510197249> <:quality:618970093510197249> <:quality:618970093510197249> <:quality:618970093510197249> <:quality:618970093510197249> <:quality:618970093510197249> <:quality:618970093510197249> <:quality:618970093510197249> <:quality:618970093510197249> <:quality:618970093510197249> <:quality:618970093510197249> <:quality:618970093510197249> <:quality:618970093510197249> <:quality:618970093510197249> <:quality:618970093510197249> <:quality:618970093510197249>')
	}else if(msg.content === '!umgak'){
		fs.readFile('imageUmgak.txt', (err, data) => {
			if (err) throw err;
			var myArray = data.toString().split('\n');
			var randomnumber = myArray[Math.floor(Math.random()*myArray.length-1)];
			img.setImage(randomnumber);
			msg.channel.send(img);
		});
	
	}else if(msg.content === '!quality'){
		img.setImage('https://cdn.discordapp.com/attachments/473574802427478025/618959544869191680/unknown.png');
		msg.channel.send(img);
	}else if(msg.content === '!help'){
		msg.channel.send("!fatshark - Spreads the good word of quality\n!quality - True quality in a better format\n!umgak - Just your classic shoddy human umgak\n!new - For the newest Umgak on the market\n!unironicq - Against the better judgement of the fabrics of time, space and reality the impossible has actually occurred\n!snap - Dwarves in total war vs Eshin.\n!grudge - Be careful who you fuck with.\n!translategrudge - For uncultured umgi who need a broken translation.\n!bridge - It's an Elgi conspiracy I tells ya");
	}else if(msg.content === '!new'){
			fs.readFile('imageUmgak.txt', (err, data) => {
			if (err) throw err;
			var myArray = data.toString().split('\n');
			var lastNumber = myArray[myArray.length-1];
			img.setImage(lastNumber);
			msg.channel.send(img);
		});
	}else if(msg.content === '!unironicq'){
		img.setImage('https://cdn.discordapp.com/attachments/473574802427478025/645379565698678801/images.png');
		msg.channel.send(img);
	}else if(msg.content === '!snap'){
		img.setImage('https://media.discordapp.net/attachments/650084532069859407/652343222583951360/og1xkmtaxv241.png?width=408&height=471');
		msg.channel.send(img);
	}else if(msg.content === '!grudge'){
		msg.channel.send("ᚹᚺᚨᛏ ᛏᚺᛖ ᚠᚢᚲᚲ ᛞᛁᛞ ᛃᛟᚢ ᛃᚢᛊᛏ ᚠᚢᚲᚲᛁᚾᚷ ᛊᚨᛃ ᚨᛒᛟᚢᛏ ᛗᛖ, ᛃᛟᚢ ᛚᛁᛏᛏᛚᛖ ᛊᚲᚱᚢᚠᚠ? ᛁ'ᛚᛚ ᚺᚨᚢᛖ ᛃᛟᚢ ᚲᚾᛟᚹ ᛁ ᚠᛁᚾᛁᛊᚺᛖᛞ ᛏᛟᛈ ᛟᚠ ᛗᛃ ᛈᚨᚱᛏᛃ ᛁᚾ ᛊᚲᛁᛏᛏᛖᚱᚷᚨᛏᛖ, ᚨᚾᛞ ᛁ'ᚢᛖ ᛒᛖᛖᚾ ᛁᚾᚢᛟᛚᚢᛖᛞ ᛁᚾ ᚾᚢᛗᛖᚱᛟᚢᛊ ᛚᛖᚷᛖᚾᛞ ᚷᚨᛗᛖᛊ ᛟᚾ ᚨᛏᚺ, ᚨᚾᛞ ᛁ ᚺᚨᚢᛖ ᛟᚢᛖᚱ 300000 ᚲᛟᚾᚠᛁᚱᛗᛖᛞ ᚲᛁᛚᛚᛊ. ᛁ ᚨᛗ ᛏᚱᚨᛁᚾᛖᛞ ᛁᚾ ᚷᛟᚱᛁᛚᛚᚨ ᚹᚨᚱᚠᚨᚱᛖ ᚨᚾᛞ ᛁ'ᛗ ᛏᚺᛖ ᛏᛟᛈ ᚱᚨᚾᚷᛖᚱ ᛁᚾ ᛏᚺᛖ ᛖᚾᛏᛁᚱᛖ ᚺᛟᛚᛞ. ᛃᛟᚢ ᚨᚱᛖ ᚾᛟᛏᚺᛁᚾᚷ ᛏᛟ ᛗᛖ ᛒᚢᛏ ᛃᚢᛊᛏ ᚨᚾᛟᛏᚺᛖᚱ ᛏᚨᚱᚷᛖᛏ. ᛁ ᚹᛁᛚᛚ ᚹᛁᛈᛖ ᛃᛟᚢ ᛏᚺᛖ ᚠᚢᚲᚲ ᛟᚢᛏ ᚹᛁᛏᚺ ᛈᚱᛖᚲᛁᛊᛁᛟᚾ ᛏᚺᛖ ᛚᛁᚲᛖᛊ ᛟᚠ ᚹᚺᛁᚲᚺ ᚺᚨᛊ ᚾᛖᚢᛖᚱ ᛒᛖᛖᚾ ᛊᛖᛖᚾ ᛒᛖᚠᛟᚱᛖ ᛟᚾ ᛏᚺᛁᛊ ᚹᛟᚱᛚᛞ, ᛗᚨᚱᚲ ᛗᛃ ᚠᚢᚲᚲᛁᚾᚷ ᚹᛟᚱᛞᛊ. ᛃᛟᚢ ᛏᚺᛁᚾᚲ ᛃᛟᚢ ᚲᚨᚾ ᚷᛖᛏ ᚨᚹ");
	}else if(msg.content === '!translategrudge'){
		msg.channel.send("What the fucc did jou just fuccing saj about me, jou little scruff? i'll haue jou cnow i finished top of mj partj in scittergate, and i'ue been inuolued in numerous legend games on ath, and i haue ouer 300000 confirmed cills. I am trained in gorilla warfare and i'm the top ranger in the entire hold. Jou are nothing to me but just another target. I will wipe jou the fucc out with precision the lices of which has neuer been seen before on this world, marc mj fuccing words. Jou thinc jou can get awaj with sajing that shit to me ouer the internet? thinc again, cruti. As we speac i am contacting mj secret networc of spies across the old world and jour position is being traced right now so jou better prepare for the storm, umgac. The storm that wipes out the pathetic little thing jou call jour life. Jou're fuccing dead, drengbarazi. I can be anjwhere, anjtime, and i can cill jou in ouer seuen hundred wajs, and that's just with mj bare hands. Not onlj am i ecᛋtensiuelj trained in unarmed combat, but i haue access to the entire arsenal of carac norn and i will use it to its full ecᛋtent to wipe jour miserable ass off the face of the continent, jou little shit. If onlj jou could haue cnown what unholj retribution jour little cleuer comment was about to bring down upon jou, majbe jou would haue held jour fuccing tongue. But jou couldn't, jou didn't, and now jou're pajing the price, jou goddamn wazzoc. I will shit furj all ouer jou and jou will drown in it. Jou're fuccing dead, scruff");
	}else if(msg.content === '!bridge'){
		img.setImage('https://preview.redd.it/pi9wwm4mey751.png?auto=webp&s=e342fbb88ee1c5392ef218685fc54774b076d431');
		msg.channel.send(img);
	}else{
		if(msg.content.split(" ").find(word => word.toLowerCase() === "fatshark")){
			msg.react("618970093510197249");
		}
		if((msg.content.split(" ").find(word => word.toLowerCase() === "hoggars")) && (msg.content.split(" ").find(word => word.toLowerCase() === "bridge"))){
			msg.react("728018824271626251");
		}
	}
	
});

bot.login('NjQzNTY0NzEzNjczNzUyNTc2.Xcndrg.laAO3WuCVh64qKpTqOQcCxzqvAM')

