const Discord = require('discord.js');
const bot = new Discord.Client();
bot.on('ready', () => {
	console.log('Logged in as $(client.user.tag)!');
});
bot.on('message', msg => {
	if (msg.content === '!fatshark'){
		
		msg.channel.send('<:quality:643588834025537536> <:quality:643588834025537536> <:quality:643588834025537536> <:quality:643588834025537536> <:quality:643588834025537536> <:quality:643588834025537536> <:quality:643588834025537536> <:quality:643588834025537536> <:quality:643588834025537536> <:quality:643588834025537536> <:quality:643588834025537536> <:quality:643588834025537536> <:quality:643588834025537536> <:quality:643588834025537536> <:quality:643588834025537536> <:quality:643588834025537536> <:quality:643588834025537536> <:quality:643588834025537536> <:quality:643588834025537536> <:quality:643588834025537536> <:quality:643588834025537536> <:quality:643588834025537536> <:quality:643588834025537536> <:quality:643588834025537536> <:quality:643588834025537536> <:quality:643588834025537536> <:quality:643588834025537536>')
	}else{
		if(msg.content.split(" ").find(word => word.toLowerCase() === "fatshark")){
			msg.react("643588834025537536");
		}
	}
	
});

bot.login('NjQzNTY0NzEzNjczNzUyNTc2.Xcndrg.laAO3WuCVh64qKpTqOQcCxzqvAM')

