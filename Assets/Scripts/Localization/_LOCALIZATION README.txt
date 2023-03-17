LOCALIZATION README

1. FILE REQUIREMENTS
1.1. An Excel sheet file (.xlsx)
1.1.1. First row     	   : Headers (i.e. )
1.1.2. First column  	   : Notes to translators
1.1.3. Second column 	   : Keys
1.1.4. Third column onward : One localization text per column 
1.2. A .txt file Saved from the Excel sheet As Unicode (.txt)

2. GAMEOBJECT SETUP
2.1. Place a LocalizationManager prefab into the base scene
2.2. Add TMPLocalizable.cs script to the desired text mesh pro to localize
2.2.1. Will automatically SubscribeToLocalization() of the LocalizationManager.cs on Start()
2.2.2. Must provide a key for the script to fetch Localizations
2.2.2.1. The key == a key from the .xlsx and .txt files