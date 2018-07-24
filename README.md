# SmiteSimulator

## Using the Stats Grabber
## Example 1 (Using a list to manage all Gods)
#### Create a StatsGrabber instance
The StatsGrabber class offers helpers to quickly obtain God stats.
```
var s = new StatsGrabber();
```
#### Load all God names to a collection
The `s.GetAllGodNames();` method allows us to quickly obtain a list of Gods in SMITE. It returns a list of IGod except at this point each God only has it's `_name` property populated.
```
var godList = s.GetAllGodNames();
```
#### Load God stats for a given God e.g. Agni
The `s.GetGodInfo(/*God: GodObject*/);` method fetches all the stats for the selected God and adds it to the already instanciated class object for Agni.
```
s.GetGodInfo(godList.First(x => x.GetName() == "Agni"));
```
#### View all loaded God properties
The `God.Status();` method allows you to quickly print out all loaded properties for the selected God.
```
godList.First(x => x.GetName() == "Agni").Status();
```

## Example 2 (Getting information for a single God)
#### Create a StatsGrabber instance
The StatsGrabber class offers helpers to quickly obtain God stats.
```
var s = new StatsGrabber();
```
#### Create a God Object
Instanciate a new God object and pass in the God's name as a parameter.
```
var NeZha = new God("Ne Zha");
```
#### Load God stats for a given God e.g. Ne Zha
The `s.GetGodInfo(/*new God("Ne Zha")*/);` method fetches all the stats for the selected God and adds it to the already instanciated class object for the selected God.
```
s.GetGodInfo(NeZha);
```
#### View the loaded stats
Simply call `.Status();` on the God object.
```
NeZha.Status();
```

### Get a specific stat
Simply call one of the many getters on the God object. e.g. `NeZha.GetHealth();` (Full list of getters below).

# All God Getters
```
// Meta Getters
  string GetName();
  string GetTitle();
  string GetPantheon();
  string GetInhandType1();
  string GetInhandType2();
  string GetClass();
  string GetPros();
  string GetDifficulty();
  string GetReleaseDate();
  string GetFavourCost();
  string GetGemCost();
  string GetVoiceActor();
  
// Base Stat Getters
  double GetHealth();
  double GetHealthIncreasePerLevel();
  double GetMana();
  double GetManaIncreasePerLevel();
  double GetSpeed();
  double GetSpeedIncreasePerLevel();
  double GetRange();
  double GetRangeIncreasePerLevel();
  double GetAttacksPerSecond();
  double GetAttacksPerSecondIncreasePerLevelPercent();
  
// Basic Attack Stat Getters
  double GetInhandBaseDamage();
  double GetInhandBaseDamageIncreasePerLevel();
  double GetInhandScalingPercentage();
  List<double> GetProgressionDamageScaling();
  List<double> GetProgressionSpeedScaling();
  string GetProgressionDamageScalingString();
  string GetProgressionSpeedScalingString();
  
// Protections Stat Getters
  double GetPhysicalProtections();
  double GetPhysicalProtectionsIncreasePerLevel();
  double GetMagicalProtections();
  double GetMagicalProtectionsIncreasePerLevel();
  
// Regen Stat Getters
  double GetHP5();
  double GetHP5IncreasePerLevel();
  double GetMP5();
  double GetMP5IncreasePerLevel();
```
