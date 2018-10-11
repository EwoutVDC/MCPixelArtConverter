# What is Minecraft pixelart converter?
Minecraft pixelart converter (MCPAC) converts an image into pixelart using selected minecraft blocks. It loads the block information needed from the minecraft jar file and uses the average color of the block texture. This means that it should automatically pick up new blocks as they are introduced, provided the file format is not changed.

The most important features are:
* Dynamically loads block textures from minecraft jar file
* Allow selecting a subset of blocks to use
* Set the side from which the pixelart will be viewed (up/down/north/east/south/west)
* Considers all possible block orientations (for blocks with different colors on different sides)
* Optionally apply selected dithering algorithm. This works best for pictures and/or bigger pixel arts with fine detail
* Export conversion result to image and CSV

# How to install
Unzip the installer and execute setup.exe. The application should install to local appdata and create an entry in the start menu. You can remove the installer files afterwards.
MCPAC is written in C# and requires .NET 4.5.2. The installer should download this if required.

# How to use
1. Click "Load block info" and point to the Minecraft .jar file you will be using.
2. Click "Load image" and select the image you want to create a pixel art from
3. Set the target size by changing the width or height value or adjusting the scale slider. The image shown scale for the target resolution.
4. Optionally select a dithering algorithm to apply. Dithering creates less sharp transitions and tries to mix available colors to more closely match the original image. Dithering disables multithreaded conversion since this requires the error from the previous block to convert the next block.
5. Optionally select the minecraft blocks to be used by clicking the "Select blocks" button
5.1. This will open a new window that allows you to select the block side that will be used and select which blocks (and variants) are to be used in the converted pixel art.
5.2 You can save/load the block selection to/from a simple text file with the respective buttons
5.3 You can leave this window open or close it after changing the block selection. The selection is immediately applied.
6. Click "Convert Image". This will block the application while conversion is being done. Depending on target size and selected blocks, this can take some time. Please be patient. A progress bar and background conversion are potential future features.
6.1. This will open a new window that shows an image of the converted pixel art using the selected minecraft blocks
6.2. You can save this result as the image that is shown in this window by clicking "Save Image"
6.3. You can also save the result as a CSV that lists all used blocks by text. This does not show the selected variant (orientation, state) of the block