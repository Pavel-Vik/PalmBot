PROJECT INSTRUCTION

===============How to create a new Level==============

	1. NEW SCENE
1.1. Duplicate EmptyLevel.scene

1.2. Rename it like this: "Level_" + levelsSection + "-" + "levelID"

-----------------------------------------------------
	2. TILES DRAWING
2.1. Open TilePallete and select in Active Tilemap 'Tiles'

2.2. Draw level using gray and green box tiles. Change Z position to change a floor.

2.3. Changing Active Tilemap and using flat tiles draw FloorsColliders where you have box tiles (from '1' to '4'). Don't change Z position. 
Colors of flat tiles are just for understanding what floor you draw.

2.4. Select 'GreenTiles' as Active tilemap and draw flat tiles where you have green box tiles.

------------------------------------------------------------
	3. 'Bot' SETTINGS
3.1. Pressing Ctrl drag 'Bot' into position you need.

3.2. Set 'Bot Direction' in 'Bot Rotation (Script)'. It is direction the bot look when game starts. You can hover over the inscription to see a hint.

---------------------------------------------------
	4. 'GameController' SETTINGS
4.1. Write what section it is in 'Level Section ID'

4.2. Write how many green tiles this level has in 'Green Tiles Count'

----------------------------------------------------------------
	5. 'Canvas UI' SETTINGS
5.1. Find 'MainPannel' and set commands limit in 'Space' for Main Panel.

5.2. Enable 'PROC1 Pannel' and 'PROC2 Pannel' if you need. Set positions for panels.

5.3. In 'LeftButtonsPanel/Commands' enable all commands you need.


=============HOW TO CREATE A NEW SECTION====================
1. Duplicate 'SelectLevel_1'

2. In 'Level Manager' set 'Levels Section ID'

3. In 'Canvas/Panel/Levels' create how many levels you want

4. Set 'Number Image' using 'LevelNumbers 'sprites'

5. In 'LevelSelectButton' set for button function Level ID.


================BUGS==============
1. 'Bot/Trigger' (Trigger.cs) doesn't always work correctly. 
Solution: Need to be colebrated or it is better just create 3 trigger with different positions to check curent floor, floor +1, and all floors down.

==============NEED TO BE DONE====================
1. Save progress system with opening next new levels

2. Write functions for sectionID and levelID detection so it will be more comfortable to work with levels.

3. Add Grid Layout Group for Panels (Childrens: MainPanel, PROC1, PROC2) for more comfortable work

4. Create showing panel commands limit feature: grey area is for disabled slots, yellow area shows how many commands can be add into this panel on curent level.

5. Sounds and music

6. Localization (if the client wants it)

7. UI animation (if the client wants it)