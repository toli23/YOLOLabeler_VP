# YOLO Labeller
Small, lightweight YOLO Labeling tool developed using `.NET Core 3.1 Windows Forms`.

## Installation
In the `Releases` tab, there are prebuilt binaries for `x86` and `x64` Windows platforms contained in the `Windows.zip` file.
## Usage
### Setup your project
Locate your `.nammes` file and select a folder with pictures (order doesn't matter).

![](https://github.com/toli23/YOLOLabeler_VP/blob/master/User%20Guide/select_stuff.gif)

### Draw boxes and save label
Click on the color button next to the label whom which you want to label. After you labeled your objects click on the button `Save Label`. This creates a new folder called `Labels` where the annotations are saved. This folder is found in the same place with the program's executable.
You can click ***Next*** or ***Previous*** to go on the next or previous picture.

![](https://github.com/toli23/YOLOLabeler_VP/blob/master/User%20Guide/draw_stuff.gif)

### Save project
You can save your project by going `File > Save` which is saved with `.ylp` extension.

![](https://github.com/toli23/YOLOLabeler_VP/blob/master/User%20Guide/draw_stuff.gif)

### Load project
You can load your project by going `File > Open` where it restores your previous work (all drawn boxes are saved and redrawn).

![](https://github.com/toli23/YOLOLabeler_VP/blob/master/User%20Guide/load_stuff.gif)

### Additional features
* Enable Crosshair: `Tools > Crosshairs Enabled`
* Border Width: `Tools > Border Width > Small|Medium|Large|`

## License
YOLO Labeller is GPL v3.0 licensed, as found in the [LICENSE](LICENSE) file.

