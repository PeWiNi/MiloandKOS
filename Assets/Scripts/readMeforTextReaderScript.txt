the new script (textReader) is like a levelInstantiator2.0

Rules for it:
- requires two text files - a character map and a legend
- both in the map and the legend entries are separated by ',' (commas) for multiple entries per row, and '\n' (enter/return) for multiple rows entirely. 
- in the inspector the size of the gameElements to be instantiated needs to be specified, as well as a prefab(s) with the gameElement in question
- the legend should contain all the character that represent a gameElement, in the same order as their are placed in the GameElements array in the inspector

Voila: it can instantiate anything - no matter how many prefabs, or how big the map :D 
 p.s. prefferably to be attached to a terrain asset or a plane if it's supposed to layout the level; or the proper character for the instantiating the collectibles. 