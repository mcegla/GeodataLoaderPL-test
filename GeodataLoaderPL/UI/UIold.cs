using ColossalFramework.UI;
using ICities;
using UnityEngine;

namespace GeodataLoaderPL.UI
{
    /// <summary>
    ///     Main mod UI class, button and window creation
    /// </summary>
    public class UIold : LoadingExtensionBase
    {
        GameObject buildingWindowGameObject;
        private UIButton gdButton;
        private UISprite gdButtonImage;

        UIWindowold buildingWindow;

        private LoadMode modeU;
        public override void OnLevelLoaded(LoadMode mode)
        {
            if (mode != LoadMode.LoadGame && mode != LoadMode.NewGame && mode != LoadMode.NewMap && mode != LoadMode.LoadMap)
                return;
            modeU = mode;

            buildingWindowGameObject = new GameObject("buildingWindowObject");

            var currentUIView = UIView.GetAView();

            this.buildingWindow = buildingWindowGameObject.AddComponent<UIWindowold>();
            this.buildingWindow.transform.parent = currentUIView.transform;
            this.buildingWindow.position = new Vector3(300, 122);
            this.buildingWindow.Hide();

            gdButton = (UIButton)currentUIView.AddUIComponent(typeof(UIButton));
            gdButtonImage = (UISprite)gdButton.AddUIComponent(typeof(UISprite));

            gdButton.tooltip = "GeodataLoader Switch";
            gdButton.isTooltipLocalized = false;
            gdButton.RefreshTooltip();
            gdButton.spritePadding = new RectOffset();

            gdButton.width = 36;
            gdButton.height = 36;

            gdButtonImage.spriteName = "ToolbarIconZoomOutGlobeHovered";
            gdButtonImage.position = new Vector3(18, -18);
            gdButtonImage.width = 32;
            gdButtonImage.height = 32;


            if (gdButtonImage.atlas == null || gdButtonImage.atlas.material == null)
            {
                Debug.Log("Could not get reference material!");
                return;
            }

            gdButton.playAudioEvents = true;
            gdButton.transformPosition = new Vector3(-0.50f, 1.02f);//(-1.11f, 0.98f);
            gdButton.eventClicked += uiButton_eventClick;
            Debug.Log("Loaded");
        }

        private void uiButton_eventClick(UIComponent component, UIMouseEventParameter eventParam)
        {

            if (!this.buildingWindow.isVisible)
            {
                this.buildingWindow.isVisible = true;
                this.buildingWindow.BringToFront();
                this.buildingWindow.Show();
            }
            else
            {
                this.buildingWindow.isVisible = false;
                this.buildingWindow.Hide();
            }
        }

        // on level unloading destroy created buttons
        public override void OnLevelUnloading()
        {
            if (modeU != LoadMode.LoadGame && modeU != LoadMode.NewGame && modeU != LoadMode.NewMap && modeU != LoadMode.LoadMap)
                return;

            if (buildingWindowGameObject != null)
            {
                GameObject.Destroy(buildingWindowGameObject);
            }

            if (gdButton != null)
            {
                UIButton.Destroy(gdButton);
                UISprite.Destroy(gdButtonImage);
            }
        }
    }
}
