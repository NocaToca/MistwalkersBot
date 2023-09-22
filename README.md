# MistwalkersBot
Bot for Mistwalkers DnD Campaign. Please read to get a super quick rundown of what this can do!

For the language and what exactly this bot parsed, you can check here:
https://docs.google.com/spreadsheets/d/1g8cyPx6u3KvZAdCZG3o2GtccwyS23pe3m9jsNRm4v2o/edit#gid=0

This bot parses and stores information about characters, able to dynamically change and update information regarding them! We can see characters using the /character <name> command!

I'll be using the example Characters Kindle and Ryoji throughout this. Here we can bring up Kindle:
![Using the Character Command to Bring up Kindle's Stats](https://github.com/NocaToca/MistwalkersBot/blob/main/Example%20Gifs/character_example.gif)

Next, we can easily roll for Kindle. The bot knows the current modifier for related skills and attributes:
![Rolling for an Attribute](https://github.com/NocaToca/MistwalkersBot/blob/main/Example%20Gifs/roll_attribute_example.gif)
![Rolling for a Skill](https://github.com/NocaToca/MistwalkersBot/blob/main/Example%20Gifs/roll_skill_example.gif)

On top of that, we can showcase how this bot parses and uses moves. We can list out the moves the bot successfully parsed from the excel sheet above:
![Move List](https://github.com/NocaToca/MistwalkersBot/blob/main/Example%20Gifs/move_display_example.gif)

And then we can use one of these moves in combat, provided the user knows it! Let's have Kindle attack Ryoji:
![Fire Fang](https://github.com/NocaToca/MistwalkersBot/blob/main/Example%20Gifs/use_move_example.gif)

Here we can note the effect of Fire Fang as stated on the doc (apply status\{burn\} if fail savingthrow\{Dex\} for target in single\{Enemy\})

This means Fire Fang can also apply burn, which it does here!
![Fire Fang but Burn](https://github.com/NocaToca/MistwalkersBot/blob/main/Example%20Gifs/burn_example.gif)

Keep in mind this is a rough outline for the bot, this does not showcase all features!
