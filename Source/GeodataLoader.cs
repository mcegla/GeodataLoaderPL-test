using System;
using System.IO;
//using System.Linq;
//using System.Collections.Generic;
using ICities;
using UnityEngine;
using ColossalFramework;
using ColossalFramework.UI;
using ColossalFramework.Plugins;
using GeodataLoader.Source.Helpers;

//=====================================
//=== This is main part of the code ===
//=====================================

namespace GeodataLoader.Source
{
    public class GeodataLoaderMod : IUserMod
    {
        public string Name => "GeodataLoader";
        public string Description => "It might load geospatial data someday"; 

        //private static readonly string[] OptionLabels =
        //{
        //    "Labelka opcji1"
        //};

        //private static readonly string[] OptionValues =
        //{
        //    "Opcja1"
        //};

        // Sets up a settings user interface
        public void OnSettingsUI(UIHelperBase helper)
        {
            //=== Load the configuration ===
            GeodataLoaderConfiguration config = Configuration<GeodataLoaderConfiguration>.Load();

            //var sciezka = @"D:\PW\Praca mgr\Zuromin\BDOT 10k\mazowieckie_pow_zurominski_1437\PL.PZGiK.330.1437\BDOT10K\PL.PZGiK.330.1437__OT_ADMS_P.xml";

            //=== Creating UI element for CityGML folder path ===
            helper.AddTextfield("Insert center X:", config.inputCenterX, delegate { }, sub =>
            {
                //=== Change config value and save config ===
                config.inputCenterX = sub;
                Configuration<GeodataLoaderConfiguration>.Save();
            });

            //=== Creating UI element for CityGML folder path ===
            helper.AddTextfield("Insert center Y:", config.inputCenterY, delegate { }, sub =>
            {
                //=== Change config value and save config ===
                config.inputCenterY = sub;
                Configuration<GeodataLoaderConfiguration>.Save();
            });

            //=== Creating UI element for BDOT10k folder path ===
            helper.AddTextfield("BDOT10k path:", config.BDOT10k, delegate { }, sub =>
            {
                //=== Change config value and save config ===
                config.BDOT10k = sub;
                Configuration<GeodataLoaderConfiguration>.Save();
            });

            //=== Creating UI element for NMT folder path ===
            helper.AddTextfield("NMT path:", config.NMT, delegate { }, sub =>
            {
                //=== Change config value and save config ===
                config.NMT = sub;
                Configuration<GeodataLoaderConfiguration>.Save();
            });

            //=== Creating UI element for CityGML folder path ===
            helper.AddTextfield("Budynki3D path:", config.Budynki3D, delegate { }, sub =>
            {
                //=== Change config value and save config ===
                config.Budynki3D = sub;
                Configuration<GeodataLoaderConfiguration>.Save();
            });
        }
    }


    public class GeodataLoaderExtension : LoadingExtensionBase//, GeodataLoader.Delegate
    {
        // Inicialization of mbutton type and sprite
        private UIButton gdButton;
        private UISprite gdButtonImage;
        private LoadMode modeU;
        public override void OnLevelLoaded(LoadMode mode)
        {
            if (mode != LoadMode.LoadGame && mode != LoadMode.NewGame && mode != LoadMode.NewMap && mode != LoadMode.LoadMap)
                return;
            modeU = mode;
            // Gets a current UI view
            var currentUIView = UIView.GetAView();
            // Creates button in current view
            gdButton = (UIButton)currentUIView.AddUIComponent(typeof(UIButton));
            // Adds a sprite to created button
            gdButtonImage = (UISprite)gdButton.AddUIComponent(typeof(UISprite));

            // Set the text to show on the button tooltip.
            gdButton.tooltip = "GeodataLoader Switch";
            gdButton.isTooltipLocalized = false;
            gdButton.RefreshTooltip();
            gdButton.spritePadding = new RectOffset();

            // Set the button dimensions.
            gdButton.width = 36;
            gdButton.height = 36;

            // Set the lock image
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

            // Enable button sounds.
            gdButton.playAudioEvents = true;

            // Place the button.
            gdButton.transformPosition = new Vector3(-1.11f, 0.98f);

            // Respond to button click.
            //gdButton.eventMouseUp += ButtonMouseUp;
            gdButton.eventClicked += (component, click) =>
            {
                CommonHelpers.Log("clicked");
                //CreateNet.Start(0, 0, 100, 100, "Basic Road");
                //CreateBuilding.Start();
                //CreateTree.Start();
                BDOT10kToObj bDOT10kToObj = new BDOT10kToObj();
                bDOT10kToObj.BUIT_P();
                bDOT10kToObj.BUWT_P();
                bDOT10kToObj.BUZT_P();
                bDOT10kToObj.KUHU_P();
                bDOT10kToObj.OIPR_L();
                bDOT10kToObj.OIPR_P();
                bDOT10kToObj.OIKM_P();
                bDOT10kToObj.OIOR_P();
                //gdButton.Hide();
            };

            gdButton.Show();

            CommonHelpers.Log("Loaded");
        }

        //private void ButtonMouseUp(UIComponent component, UIMouseEventParameter eventParam)
        //{
        //    OIPR01.Start();
        //}

        public override void OnLevelUnloading()
        {
            if (modeU != LoadMode.LoadGame && modeU != LoadMode.NewGame && modeU != LoadMode.NewMap && modeU != LoadMode.LoadMap) //CZY TEN DOUBLECHECK POTRZEBNY?
                return;
            if (gdButton != null)
            {
                UIButton.Destroy(gdButton);
                UISprite.Destroy(gdButtonImage);
            }

        }

    }
}

//WHAT DO U WANT?
//IN > folders, center XY
//OUT > on button click x3 -> load: bdot10k!!! / terrain / buildings3d - later