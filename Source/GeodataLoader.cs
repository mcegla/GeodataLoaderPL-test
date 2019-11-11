using ICities;
using UnityEngine;
using ColossalFramework.UI;
using GeodataLoader.Source.Helpers;
using GeodataLoader.Source.Factories;

//============================================================================================
//=== Główna część kodu, odpowiedzialna za tworzenie interfejsu i odwołanie do pozostałych ===
//--------------------------------------------------------------------------------------------
//======= Main part of the code, responsible for GUI creation and calls to other parts =======
//============================================================================================

// posklajane przez: / glued together by: 
// Mateusz "mcegla" Cegiełka & Radosław "jaggi" Jagiełło

// kod źródłowy innych modyfikacji wykorzystany przy tworzeniu tego kodu 
//---------------------------------------------------------------------- 
// source code of other mods used to create the code:
//https://community.simtropolis.com/forums/topic/73388-mod-development-tutorialsdocumentation/
//https://github.com/brunophilipe/OverLayer
//https://github.com/rdiekema/Cities-Skylines-Mapper/tree/master/Mapper
//https://github.com/tomarus/cs-terraingen/blob/master/TerrainGen.cs
//https://github.com/yenyang/rainfall/blob/master/Source/Hydrology.cs
// i inne, opisane później / and other, described later

namespace GeodataLoader.Source
{
    public class GeodataLoaderMod : IUserMod
    {
        public string Name => "GeodataLoader";
        public string Description => "It might load geospatial data someday"; 

        // stworzenie interfejsu ustawień / sets up a settings user interface
        public void OnSettingsUI(UIHelperBase helper)
        {
            // ładowanie konfiguracji / load the configuration
            GeodataLoaderConfiguration config = Configuration<GeodataLoaderConfiguration>.Load();

            // stworzenie elementu interfejsu do wprowadzania współrzędnej X centurm obszaru
            //--------------------------------------------------
            // creating UI element for center X coordinate input 
            helper.AddTextfield("Insert center X [.]:", config.inputCenterX, delegate { }, sub =>
            {
                // zmień wartość w konfiguracji i zapisz / change config value and save config ===
                config.inputCenterX = sub;
                Configuration<GeodataLoaderConfiguration>.Save();
            });

            // stworzenie elementu interfejsu do wprowadzania współrzędnej Y centurm obszaru
            //--------------------------------------------------
            // creating UI element for center Y coordinate input 
            helper.AddTextfield("Insert center Y [.]:", config.inputCenterY, delegate { }, sub =>
            {
                // zmień wartość w konfiguracji i zapisz / change config value and save config ===
                config.inputCenterY = sub;
                Configuration<GeodataLoaderConfiguration>.Save();
            });

            // stworzenie elementu interfejsu do wprowadzania ścieżki do folderu z danymi BDOT10k
            //--------------------------------------------------
            // creating UI element for BDOT10k folder path input
            helper.AddTextfield("BDOT10k path:", config.BDOT10k, delegate { }, sub =>
            {
                // zmień wartość w konfiguracji i zapisz / change config value and save config ===
                config.BDOT10k = sub;
                Configuration<GeodataLoaderConfiguration>.Save();
            });

            // stworzenie elementu interfejsu do wprowadzania ścieżki do folderu z danymi NMT
            //--------------------------------------------------
            // creating UI element for DEM folder path input
            helper.AddTextfield("DEM path:", config.DEM, delegate { }, sub =>
            {
                // zmień wartość w konfiguracji i zapisz / change config value and save config ===
                config.DEM = sub;
                Configuration<GeodataLoaderConfiguration>.Save();
            });
        }
    }

    // tworzenie przycisku w dużej mierze pochodzi z: / button creation largely comes from:
    //https://github.com/brunophilipe/OverLayer/blob/master/OverLayer/OverLayer.cs
    public class GeodataLoaderExtension : LoadingExtensionBase//, GeodataLoader.Delegate
    {
        // zainicjalizowanie przycisku i jego obrazka / initialization of button type and sprite
        private UIButton gdButton;
        private UISprite gdButtonImage;
        private LoadMode modeU;
        public override void OnLevelLoaded(LoadMode mode)
        {
            if (mode != LoadMode.LoadGame && mode != LoadMode.NewGame && mode != LoadMode.NewMap && mode != LoadMode.LoadMap)
                return;
            modeU = mode;
            // zwraca obecny obraz interfejsu / gets a current UI view
            var currentUIView = UIView.GetAView();
            // tworzy przycisk w obecnym obrazie / creates button in current view
            gdButton = (UIButton)currentUIView.AddUIComponent(typeof(UIButton));
            // dodaje obrazek do stworzonego przycisku / adds a sprite to created button
            gdButtonImage = (UISprite)gdButton.AddUIComponent(typeof(UISprite));

            // ustawia tekst do pojawienia się na podpowiedzi przycisku / set the text to show on the button tooltip.
            gdButton.tooltip = "GeodataLoader Switch";
            gdButton.isTooltipLocalized = false;
            gdButton.RefreshTooltip();
            gdButton.spritePadding = new RectOffset();

            // ustawia wymiary przycisku / set the button dimensions.
            gdButton.width = 36;
            gdButton.height = 36;

            // ustawia obrazek globusa / set the globe image
            gdButtonImage.spriteName = "ToolbarIconZoomOutGlobeHovered";
            gdButtonImage.position = new Vector3(18, -18);
            gdButtonImage.width = 32;
            gdButtonImage.height = 32;
            //gdButtonImage.Hide();


            if (gdButtonImage.atlas == null || gdButtonImage.atlas.material == null)
            {
                CommonHelpers.Log("Could not get reference material!");
                return;
            }
            
            // włącza dźwięki przycisku / anable button sounds.
            gdButton.playAudioEvents = true;

            // umieszcza przycisk / place the button.
            gdButton.transformPosition = new Vector3(-0.50f, 1.02f);//(-1.11f, 0.98f);

            int i = 0;
            gdButton.eventClicked += (component, click) =>
            {
                CommonHelpers.Log("clicked");
                
                // część, która w przyszłości moze odpowiadać za usuwanie obiektów po ponownym kliknięciu
                //---------------------------------------------------------------------------------------
                // part that in the future might be responsible for deleting objects after another button click

                //{
                //TerrainFactory.FlattenTerrain();
                //BuildingFactory.DeleteAllBuildings();
                //PropFactory.DeleteAllProps();
                //TreeFactory.DeleteAllTrees();
                //WaterFactory.DeleteAllSources();
                //NetFactory.DelAllSegmentsNew_ToTest();
                //NetFactory.DeleteAllSegments(); //not working
                //NetFactory.DeleteAllNodes();
                //PropFactory.temp = 0;
                //TreeFactory.temp = 0;
                //BuildingFactory.temp = 0;
                //}


                if (i == 0)
                {
                    WaterFactory.setSeaLevelTo0();
                    TerrainFactory.FlattenTerrain();
                    TerrainFactory.LoadDEM();
                    
                    // ustawia obrazek globusa / set the globe image
                    gdButtonImage.spriteName = "ToolbarIconZoomOutGlobe";

                    if (gdButtonImage.atlas == null || gdButtonImage.atlas.material == null)
                    {
                        CommonHelpers.Log("Could not get reference material!");
                        return;
                    }
                    i = 1;
                }
                else if (i == 1)
                {

                    // główna klasa ładująca obiekty / main object loading class
                    BDOT10kToObj bDOT10kToObj = new BDOT10kToObj();
                    bDOT10kToObj.BDOT10k();
                    // liczniki / counters
                    CommonHelpers.Log($"Prop count: {PropFactory.temp}/{PropManager.MAX_PROP_COUNT}");
                    CommonHelpers.Log($"Tree count: {TreeFactory.temp}/{TreeManager.MAX_TREE_COUNT}");
                    CommonHelpers.Log($"Building count: {BuildingFactory.temp}/{BuildingManager.MAX_BUILDING_COUNT}");
                    CommonHelpers.Log($"Node count: {NetFactoryBase.tempN}/{NetManager.MAX_NODE_COUNT}");
                    CommonHelpers.Log($"Segment count: {NetFactoryBase.tempS}/{NetManager.MAX_SEGMENT_COUNT}");
                    gdButton.Hide();
                }
            };
            //gdButton.Show();
            CommonHelpers.Log("Loaded");
        }

        // przy wyjściu z poziomu pozbądź sie przycików / on level unloading destroy created buttons
        public override void OnLevelUnloading()
        {
            if (modeU != LoadMode.LoadGame && modeU != LoadMode.NewGame && modeU != LoadMode.NewMap && modeU != LoadMode.LoadMap)
                return;
            if (gdButton != null)
            {
                UIButton.Destroy(gdButton);
                UISprite.Destroy(gdButtonImage);
            }
        }
    }
}