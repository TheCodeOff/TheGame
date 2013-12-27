# THE GAME #

##SPECS AND TO-DO LiST ##
* [x] Top-down *(AIPP)*
* [x] Fog of war *(AIPP)*
* Tiled environment
	- [x] Stored in text-like file *(AIPP)*
	- [x] Has edges *(AIPP)*
	- [ ] Randomly generated
	- [ ] Uses matrices for memory storage
	- [ ] Multiple worlds
		- [ ] Portals
	- [ ] Buildings
	- Tiles:
		- [x] Have different textures *(AIPP)*
		- [ ] Have heights
		- [ ] Have different movement speed multipliers
		- [ ] Is animate-able
		- [ ] Have different overlay colors
* Player
	- [x] Has health *(AIPP)*	
		- [x] Health bar *(AIPP)*
		- [ ] Receives damage
	- [x] Has stamina *(AIPP)*
		- [x] Can sprint *(AIPP)*
		- [x] Uses Stamina to sprint *(AIPP)*
		- [ ] Stamina bar
	- [ ] Can choose from different skins
	- [ ] Has a name
		- [ ] Name is displayed next to his character
	- [ ] Stats
	- [ ] Has height
	- [ ] Levels
		- [ ] Capped
		- [ ] Rewards stats through stat points
* Enemies
	- Melee enemies
		- [x] Ogre *(AIPP)*
	- Ranged enemies
		- [ ] Skelly
	- AI movement
		- [x] Ogre *(AIPP)*
		- [ ] Skelly
	- [x] Health bars *(002)*
	- [x] Name tags	*(002)*
	- [ ] Different stats
	- [ ] Drops loot
	- [ ] Levels
* Main menu
	- [ ] New game
	- [ ] Load game
	- Options/Settings
		- [ ] Audio control
		- [ ] Name control
	- [ ] Exit
* Weapons
	- Ranged
		- [ ] Rifles
		- [ ] Bow
		- [ ] Ammo
	- Melee	
		- [ ] Swords
		- [ ] Maces
		- [ ] Axes
* Armour
	- [ ] Light, Medium, Heavy
	- [ ] Changes character l||k
* Skills
	- [x] Sprinting *(AIPP)*
	- [ ] Teleportation
	- [ ] Invisibility
	- [ ] Immolation
	- [ ] etc.
* Currency
	- [ ] Copper
	- [ ] Silver = 100 Copper
	- [ ] Gold = 100 Silver
	- [ ] Platinum = 100 Gold
* Store
	- [ ] Can buy ammunition, weapons
	- [ ] Can buy skins
	- [ ] Can buy armour
* Inventory
	- [ ] 16 slots
	- [ ] Equips slots
	- [ ] Bin for destroying
* Quests
	- [ ] Main storyline
	- [ ] Farming quests
	- [ ] Rewards
* NPCs
	- [ ] Give quests
	- [ ] Give rewards
	- [ ] Has names
	- [ ] Speech bubbles
	- [ ] Can die
		- [ ] Respawns after some time
* Death
	- [ ] Can respawn
	- [ ] Costs currency

## VERSION NOTES ##
### AIPP (Before 26 December 2013) ###
>	
	See all AIPP marked specifications.
	
### UPDATE 002	(December 2013) ###
>
	**Part 1**
	* Changed draw style to center around player.
	* Project is now available on GitHub as ***TheGame***
		* Specifications moved to readme
	* Switched over to Game State system
	* Mobs now have health bars as well as name tags
	**Part 2**
	* Completely redid the tile texture loading and map loading
		* Roads now connect their textures to each other
		* Textures are loaded on request.
	
## GLOSSARY ##
AIPP = ACHIEVED IN PARENT PROJECTS, PRE-DECEMBER 2013


