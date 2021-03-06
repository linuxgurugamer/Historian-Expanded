# Historian-Expanded

Historian is a screenshot utility mod for Kerbal Space Program that adds fully configurable and dynamic captions and overlay graphics to screenshots to better describe the context of screenshots and record your Kerbal adventures.

Orignal mod by [Zenobit](https://github.com/Zeenobit/). Extended development version by Aelfhe1m

## Installation

1. Download the latest version of Historian from [GitHub](https://github.com/Aelfhe1m/Historian-Expanded/releases).
2. Extract the archive into your KSP installation folder, and overwrite all existing files.
3. Enjoy!

## Configuration

You can open the Historian configuration window by using either the stock application launcher, or [Blizzy's Toolbar](http://forum.kerbalspaceprogram.com/threads/60863).

![Settings window](https://raw.githubusercontent.com/Aelfhe1m/Historian-Expanded/master/Documentation/settings-1.2.7.png)

* __Suppressed__: When suppressed, Historian will not display the overlay when taking screenshots.
* __Always Active__: If this is turned on, the overlay will always show on top of the game. This is useful when editting layouts.
* __Auto Hide UI__: Will hide the UI (equivalent to pressing F2) whenever a screenshot is taken with the F1 key. Note: if you are using another mod that also tries to hide the UI (e.g. Automated Screenshots & Saves) then it is recommended you leave this option unset.
* __Use Stock Launcher__: When selected an Historian button will be displayed in the stock app launcher toolbar
* __Use Blizzy's Toolbar__: When selected an Historian button will be available to be added to Blizzy's Toolbar.
    * __Right click action__: cycle through which option (Suppresse, AlwaysActive or AutoHideUI) to toggle when the Historian button on the Blizzy Toolbar is right clicked. 
* __Layout__: allows you to cycle through the available layouts (see below) to choose which is active
* __Custom Text__: text to be used by `TEXT` elements `<custom>` placeholders (see below). Will be cleared after first screenshot unless the `Persistent` option is active.
* __Default Space Center Name__: A preferred name to use when a text parameter returns "KSC"
* __Time to remember action key press__: The time an action key will be remembered for use in an `ACTION_TEXT` element (see below).
* __Default empty crew slot labels__: apply to the various crew tags (see below). Specifies the text to show when no crew (of the appropriate type) are in the vessel. For example if the TEXT is `<Pilots> <Scientists>` and only one pilot (Jeb) is on board this will show `Jeb None` with the default setting. Setting the field to empty will change this to show just `Jeb`. The probe field only applies to the generic `<Crew> <CrewList> <CrewShort>` tags and will always be blank for the crew type specific tags for an uncrewed vessel.
* __Load__: Reloads all layouts while the game is running.
* __Save__: Saves the current layout as the default layout (selected automatically everytime you launch KSP).


Press the configuration window again to close the configuration window.

Note that the configuration window shows up even if you have GUI disabled using the 'F2' key. This is intentional to allow layout editting while the game GUI is off.

### Layouts

All Historian layout files must be located inside `<KSP Root>/GameData/KSEA/Historian/Layouts` folder, and must have a `*.layout` extension to be recognized by Historian. Even though the files have a `*.layout` extension, they follow the same syntax as KSP's `*.cfg` files. This is to prevent the game from loading them into the database by default. You can edit these files using your favorite text editor.

The following documentation assumes that you have a basic knowledge about KSP's configuration file syntax. To create a new layout, simply create a new empty text file and call it `<Layout Name>.layout`. Make sure the file has a `*.layout` extension. To modify a layout, simply open it in any text editor.

All layouts have a root node defined by `KSEA_HISTORIAN_LAYOUT`. Inside this node, you can define your elements. Elements are the basic building blocks of a layout. Each layout has a number of elements, rendered in the order defined (this is useful to remember if you plan on layering the elements). Each element is defined by a configuration node and has a few properties that determine the element's behaviour.

#### Elements

The following element types are currently supported by Historian:

* `RECTANGLE` Draws a simple 2D rectangle with a solid color on the screen.
* `TEXT` Draws some text on the screen. Supports rich text and value placeholders.
* `PICTURE` Renders a 2D image onto the screen.
* `FLAG` Renders the current mission's flag onto the screen.
* `SITUATION_TEXT` Selects a different text to display based on flight situation and renders it on the screen much like `TEXT`.
* `ACTION_TEXT` Selects a different text to display based on a recent action - staging, abort or an action group button press.
* `TEXT_LIST` - Selects a different text to display from a list of possible options either randomly or in sequence. Different lists can be specified for different situations
* `INHERIT` - Combine another layout with the current one.

##### Common Properties

Each element has the following common properties:

* `Anchor` Anchor of the element, relative to itself, expressed as a 2D vector. _Default: 0.0,0.0_
* `Position` Anchored position of the element, relative to the screen, expressed as a 2D vector. _Default: 0.0,0.0_
* `Size` Width and height of the element, relative to the screen, expressed as a 2D vector. _Default: 0.0,0.0_

Note that all properties are expressed in relative percentage values. For example, `Anchor = 0.5,0.5` means the center of the element, `Position = 0.5,0.0` means top center of the screen, and `Size = 1.0,1.0` means the entire size of the screen.

In addition to these properties, each element may have a few additional properties:

##### Rectangle

A `RECTANGLE` fills a rectangular area of the screen with a solid color. You can use semi-transparent colors.

* `Color` The color of the rectangular area, expressed in RGBA values. _Default: 0.0,0.0,0.0,1.0_

##### Text

A `TEXT` element renders a string of text.

* `Text` Value of the text that is to be used. Supports rich text and placeholder values. _Default: Empty_
* `TextAnchor` Alignment of the text relative to the bounds of the element. Supports any one of these values: `UpperLeft`, `UpperCenter`, `UpperRight`, `MiddleLeft`,`MiddleCenter`, `MiddleRight`, `LowerLeft`, `LowerCenter`, and `LowerRight`. _Default: MiddleCenter_
* `Font` name of an OS font to use for text in the element. NOTE: this value is case sensitive and the name must EXACTLY match the name of a font installed in your operating system (e.g. `Arial`, `Comic Sans MS`, `Times New Roman`). _Default_: none.
* `FontSize` Size of the font. Note that rich text format specifiers can override this. _Default: 10_
* `FontStyle` Style of the font. Supports any one of these values: `Normal`, `Bold`, `Italic`, and `BoldAndItalic`. Note that rich text format specifiers can override this. _Default: Normal_
* `Color` Color of the font. Note that rich text format specifiers can override this. _Default: 1.0,1.0,1.0,1.0_
* `BaseYear` Offset added to year values and formatted dates. _Default: 1 (Kerbin CalendarMode) or 1940 (Earth CalendarMode)_
* `DateFormat` A C# format string for the <Date> element. Example: `dd/MM/yyyy` for UK style short date. _Default: CurrentCulture.LongDatePattern_
* `PilotColor` Unity richtext color to apply to pilot names in <Crew> or <CrewShort> elements. _Default: clear_
* `EngineerColor` Unity richtext color to apply to engineer names in <Crew> or <CrewShort> elements. _Default: clear_
* `ScientistColor` Unity richtext color to apply to scientist names in <Crew> or <CrewShort> elements. _Default: clear_
* `TouristColor` Unity richtext color to apply to tourist names in <Crew> or <CrewShort> elements. _Default: clear_

Refer to [Unity's Manual](http://docs.unity3d.com/Manual/StyledText.html) for details on Rich Text formatting syntax.

The following pre-defined placeholder values can be used inside a text element. These placeholders will be replaced with their corresponding values when a screenshot is taken.

* `<N>` Inserts a new line.
* `<Date>` Formatted date string. Standard Earth calendar dates if game settings is Earth time (24 hour days). "Fake" Kerbin dates based on 12 alternating 35 and 36 day months and six day week if game settings is Kerbin time (6 hour days). e.g. Wednesday 13 January 2016 or Esant 06 Trenam 001. __NOTE__: Kerbin day and month names can be customised from the settings window - see below for default values.
* `<DateKAC>` Formatted date string using variable length year †
* `<UT>` KSP Universal Time. Example: _Y12, D29, 2:02:12_
* `<Year>` Current year in chosen `CalendarMode`
* `<YearKAC>` Current year using variable length year †
* `<Day>` Current day in chosen `CalendarMode`
* `<DayKAC>` Current day using variable length year †
* `<Hour>` Current hour in chosen `CalendarMode`
* `<Minute>` Current minute in chosen `CalendarMode`
* `<Second>` Current second in chosen `CalendarMode`
* `<T+>` Current mission time for the active vessel, in chosen `CalendarMode` (Only available in Flight Mode). Example: _T+ 2y, 23d, 02:04:12_
* `<MET>` Alias for `<T+>`
* `<Vessel>` Name of the active vessel or Kerbal (Only available in Flight Mode). Example: _Jebediah Kerman_, _Kerbal X_
* `<Body>` Name of the main body (Only available in Flight Mode). Example: _Kerbin_
* `<Situation>` Current situation of the active vessel (Only available in Flight Mode). Example: _Flying_, _Orbiting_
* `<Biome>` Current biome of the active vessel based on its location (Only available in Flight Mode). Example: _Shores_
* `<Latitude>` Latitude of the active vessel relative to the main body expressed in decimal degrees e.g. _23.01245_ (Only available in Flight Mode)
* `<LatitudeDMS>` Latitude of the active vessel relative to the main body expressed in degrees, minutes and seconds. e.g. _23° 05' 23" N_  (Only available in Flight Mode)
* `<Longitude>` Longitude of the active vessel relative to the main body expressed in decimal degrees e.g. _-17.15_ (Only available in Flight Mode)
* `<LongitudeDMS>`Longitude of the active vessel relative to the main body expressed in degrees, minutes and seconds e.g. _17° 21' 10" W_  (Only available in Flight Mode)
* `<Altitude>` Altitude of the active vessel relative to the sea level of the main body in the most appropriate unit (Only available in Flight Mode). The unit is also included as of version 1.0.1.
* `<Mach>` The current Mach number of the active vessel (Only available in Flight Mode).
* `<Heading>` The current compass heading of the active vessel (Only available in Flight Mode).
* `<LandingZone>` The name of the current location the vessel is landed at (Only available in Flight Mode). Example: _Launchpad_
* `<Speed>` Surface speed of the active vessel in the most appropriate unit (Only available in Flight Mode). The unit is also included as of version 1.0.1.
* `<SrfSpeed>` alias for `<Speed>`
* `<OrbSpeed>` orbital speed of the active vessel in the most appropriate unit (only available in flight mode). The unit is also included.
* `<Ap>` Apoapsis of current orbit (or sub-orbital trajectory) including unit. 
* `<Pe>` Periapsis of current orbit (or sub-orbital trajectory) including unit. 
* `<Inc>` Inclination of current orbit including `°` symbol.
* `<LAN>` Longtitude of Ascending Node including `°` symbol.
* `<ArgPe>` Argument of Periapsis including `°` symbol.
* `<Ecc>` Eccentricity to 3 decimal places.
* `<Period>` Orbital period.
* `<Orbit>` Shorthand for `<Ap> x <Pe>`: Example 120.7 km x 91.3 km.
* `<Crew>` Name of all the crew members, separated by commas, on the active vessel (Only available in Flight Mode). If the vessel is a probe, it will display "Unmanned". If the vessel is space debris, it will display "N/A". Example: _Jebediah Kerman, Bill Kerman_
* `<CrewShort>` Same as above but assumes all crew members have last name of Kerman and only displays it once at end of list. Example: _Jebediah, Bill, Bob Kerman_
* `<Pilots>` List of just the pilots in the current vessel's crew.
* `<Engineers>` List of just the engineers in the current vessel's crew.
* `<Scientists>` List of just the scientists in the current vessel's crew.
* `<Tourists>` List of just the tourists in the current vessel's crew.
* `<PilotsList>, <EngineersList>, <ScientistsList>, <ToursistsList>` As above but formatted as a vertical bullet list of names rather than comma separated.
* `<PilotsShort>, <EngineersShort>, <ScientistsShort>, <TouristsShort>` As `<CrewShort>` but filtered to single trait.
* `<Target>` Name of currently targeted vessel or body (if any)
* `<LaunchSite>` If [KSCSwitcher](http://forum.kerbalspaceprogram.com/index.php?/topic/106206-105-regexs-useful-mod-emporium/) is installed will display the name of the current active space center (e.g. _Cape Canaveral_). If [Kerbal Konstructs](http://forum.kerbalspaceprogram.com/index.php?/topic/94863-112-kerbal-konstructs-v0967_ex-holy-glowing-balls-batman/) is installed then the launchsite most recently set via the menu the VAB or SPH is used. If neither mod is present then the __Default Space Center Name__ is used.
* `<RealDate>` the real world date and time.
* `<VesselType>` the type of the current vessel. Will be one of `Base`, `Debris`, `EVA`, `Flag`, `Lander`, `Probe`, `Rover`, `Ship`, `SpaceObject`, `Station`, `Unknown`
* `<KK-SpaceCenter> If [Kerbal Konstructs](http://forum.kerbalspaceprogram.com/index.php?/topic/94863-112-kerbal-konstructs-v0967_ex-holy-glowing-balls-batman/) is installed will return the BaseName of the closest open space center. If not installed then `NO KK` will be returned.
* `<KK-Distance>` If [Kerbal Konstructs](http://forum.kerbalspaceprogram.com/index.php?/topic/94863-112-kerbal-konstructs-v0967_ex-holy-glowing-balls-batman/) is installed will return the distance to the closest open space center. If not installed then `NO KK` will be returned.
* `<StageNumber>` The number of the currently active stage. This will be the same as the number displayed in the staging controls at the bottom left of the flight screen UI.
* `<Custom>` The current value of the Custom Text. You can set this value using the configuration window. If custom text is not persistent (default), it will be cleared after the next screenshot.

Note that all placeholder values are case-sensitive.

The following placeholders are also available mainly for debug purposes or when developing layouts:

* `<DateFormat>` The currently active date format string (e.g. `MM/dd/yyyy`).
* `<ListFonts>` A (long) comma separated list of the official names of all fonts installed in your OS. For use in identifying the correct name to use with the `Font` property.
* `<LastAction>` The last recognised action. See `ACTION_TEXT` below.
* `<EvaState>` The current animation state of an EVA Kerbal.

† Note for Earth calendar dates the game calculates the clock date using fixed 365 day years taking no account of leap years. [Kerbal Alarm Clock](http://forum.kerbalspaceprogram.com/index.php?/topic/22809-11x-kerbal-alarm-clock-v3610-april-25/) and [RSS Date Time Formatter](http://forum.kerbalspaceprogram.com/index.php?/topic/139335-ksp-112-rss-datetime-formatter-v10/) calculates based on seconds since start date and takes account of leap years. [RSS](http://forum.kerbalspaceprogram.com/index.php?/topic/50471-112-real-solar-system-v1110-may-1/) only shows the planets in the correct relative locations for an historical date if leap years are accounted for. The different `<Date>` and `<DateKAC>` tags allow you to choose which of these calendar schemes you wish to use.

##### Situation Text

A `SITUATION_TEXT` element behaves similar to the `TEXT` element. It has all of its properties except `Text`. Instead, it has the following additional properties, each corresponding to a different flight situation:

* `EvaOnly` Should this element be used only when there is a Kerbal on EVA. Possible values: True, False, Either. __Default__: Either

* `Default` Used when no flight situation is available.
* `Landed` Used when the vessel is landed.
* `Splashed` Used when the vessel is splashed in water.
* `Prelaunch` Used when the vessel is on the launchpad.
* `Flying` Used when the vessel is flying in atmosphere.
* `SubOrbital` Used when the vessel is in a sub-orbital trajectory.
* `Orbiting` Used when the vessel is orbiting a body.
* `Escaping` Used when the vessel is escaping from a body.
* `Docked` Used when the vessel is docked to another.

* `RagDolled` Used when an EVA Kerbal is 'ragdolled' on the ground as the result of a fall or collision
* `Clambering` Used when an EVA Kerbal is climbing over terrain or a vehicle in response to the 'F' climb key.
* `OnLadder` Used when an EVA Kerbal is holding on to a ladder

When a screen shot is taken, the `SITUATION_TEXT` element uses only one of the above values for its text, depending on the situation. This is useful for making more descriptive captions such as: `Preparing to launch from <LaunchSite>` or `Landed on <Body>'s <LandingZone>` or `Flying at Mach <Mach> (<Speed>) <Altitude> over <Body>'s <Biome>`.

Note that just like `TEXT`, `SITUATION_TEXT` also supports rich text and placeholder values.

Note also that the special EVA values `RagDolled`, `Clambering` and `OnLadder` take precedence over the flight situations if they have a value text specified. If no value is specified then the appropriate normal situation is used (e.g. `Flying`, `Landed` etc.)

##### ACTION Text

An `ACTION_TEXT` element behaves similarly to a `SITUATION_TEXT` except that the selected text is used only if the corresponding action has occurred recently (how long is considered recent can be configured from the settings window). It has all of its properties except `Text`. Instead, it has the following additional properties, each corresponding to a different action:

* `Default` Used whenever no recent action has taken place. If omitted then this element will normally be blank.
* `Abort` Used if the abort key (`backspace` by default) or abort button has been pressed recently.
* `Stage` Used if the stage key (`spacebar` by default) has been pressed recently.
* `AG1`, `AG2`,... `AG10`: Used if the corresponding action group key (`1`, `2`,... `0` by default) has been pressed recently.

Note that just like `TEXT`, `ACTION_TEXT` also supports rich text and placeholder values.

##### TEXT_LIST

A `TEXT_LIST` element behaves similarly to a `SITUATION_TEXT` element except that it allows a block of alternative `TEXT` values to be specified for each situation. The element has the can specify the same properties as a `TEXT` element with the addition of:

* `EvaOnly` Should this element be used only when there is a Kerbal on EVA. Possible values: True, False, Either. __Default__: Either
* `Random` Should the different text values be chosen in a random order or sequentially? __Default__: false
* `ResetOnLaunch` When texts are being chosen in sequence should that sequence be reset when switching to a different vessel? __Default__: false

Each situation block takes the same format with the name of the situation acting as a header then a list of `TEXT` entries enclosed between `{` and `}`. e.g.

    Prelaunch
	{
	    Text = Поехали!
		Text = Let's go!
	}
	
Any of the situations from the `SITUATION_TEXT` element can be used as a block header. As with the `SITUATION_TEXT` element the `Default` block will be used if there is no block specified for the current situation.

##### Picture

A `PICTURE` element renders a static 2D texture onto the screen. To save KSP memory usage, you can use any of the pre-loaded textures, such as missions flags, agency flags, or even part textures. You can also use any other custom texture as long as the path to your image is valid. Vintage border effects, anyone? ;)

* `Texture` Path to the image file, relative to the `GameData` directory. Example: `Squad/Flags/default`
* `Scale` Scale of the image relative to itself. For example, a value of `2.0,2.0` doubles the size of the texture, while maintaining the aspect ratio. _Default: 1.0,1.0_

If a `Size` property is not defined (or if the size is a zero vector), the size of the image is used automatically. Otherwise it denotes  the size of the image relative to screen dimensions. For example, a value of `1.0,1.0` ensures the image takes up the size of the entire screen. _Default: 0.0,0.0_

##### Flag

A `FLAG` element can be used to render the current mission's flag onto the screen. It behaves very much like a `PICTURE` otherwise.

* `DefaultTexture` Path to a default image file to show if no flag can be determined for the active vessel, or if there is no active vessel. Example: `Squad/Flags/default`
* `BackgroundTexture` Optional path to a image file to show as background behind transparent flags. Default is none. Example: `Squad/Flags/Minimalistic`
* `Scale` Scale of the image relative to itself. For example, a value of `2.0,2.0` doubles the size of the texture, while maintaining the aspect ratio. _Default: 1.0,1.0_

If a `Size` property is not defined (or if the size is a zero vector), the size of the image is used automatically. Otherwise it denotes  the size of the image relative to screen dimensions. For example, a value of `1.0,1.0` ensures the image takes up the size of the entire screen. _Default: 0.0,0.0_

##### INHERIT

An `INHERIT` element will include all the elements from another layout file (unless they are explicitly excluded) into the current layout.

* `LayoutName` The filename of the layout to include. NOTE: this is case sensitive and MUST end in `.layout`. Invalid layout names will result in the `INHERIT` element being ignored.
* `EXCLUDE` A block specifying a list of named elements from the inherited layout that should not be displayed. Unrecognised element names will be ignored. Example:
    EXCLUDE
	{
		Element = SmallFlag
		Element = DetailText
	}

#### Kerbin days and months.

The included Kerbin calendar formatter uses the following day and month names. These names can be customised from the settings window. Odd numbered months have 35 days and even numbered months have 36 days.

__Days__: Akant, Brant, Casant, Dovant, Esant, Flant  (_Note: first letter is A,B,...F_)

__Months__: Unnam, Dosnam, Trenam, Cuatnam, Cinqnam, Seinam, Sietnam, Ocnam, Nuevnam, Diznam, Oncnam, Docenam (_Note: mangled Spanish ("Spangled") numbers with "nam" suffix_)



### Sample Configuration

Below, you can find an example of a default layout:

    KSEA_HISTORIAN_LAYOUT
    {
        Name = Default
        
        RECTANGLE
        {
            Anchor = 0.0,0.5
            Size = 1.0,0.125
            Position = 0.0,0.85
            Color = 0.0,0.0,0.0,0.5
        }
    
        FLAG
        {
            Anchor = 0.5,0.5
            Position = 0.1,0.85
            Scale = 1,1
            DefaultTexture = Squad/Flags/default
        }
    
        TEXT
        {
            Anchor = 0.0,0.5
            Position = 0.25,0.85
            Size = 0.5,0.1
            Color = 1.0,1.0,1.0,1.0
            Text = <size=22><b><Vessel></b></size><N><size=8><N></size><b><UT></b> (<T+>)<N><size=12><Situation> @ <Body> (<Biome>, <Latitude>° <Longitude>°) </size>
            TextAnchor = MiddleLeft
            FontSize = 12
            FontStyle = Normal
        }
    }

This layout would produce screenshots that looks like this:

![](http://i.imgur.com/nqsvA09l.png)

Other examples are available on the [mod release thread](http://forum.kerbalspaceprogram.com/index.php?/topic/138848-112-historian-expanded/)