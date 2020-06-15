using ColossalFramework.UI;
using System;
using System.Linq;
using UnityEngine;

namespace GeodataLoaderPL.UI
{
    public class UIWindowold : UIPanel
    {

        UILabel title;

        UILabel centerXLabel;
        UITextField centerXTextField;
        UILabel centerYLabel;
        UITextField centerYTextField;

        UILabel areaLabel;
        UIDropDown areaDropDown;

        UILabel bdot10kLabel;
        UISprite bdot10kCheck;
        UIButton bdot10kCheckBox;

        UILabel bdot10kPathLabel;
        UITextField bdot10kPathTextField;

        UILabel demLabel;
        UISprite demCheck;
        UIButton demCheckBox;

        UILabel demPathLabel;
        UITextField demPathTextField;

        UIButton importTDBButton;
        UIButton importDEMButton;

        public ICities.LoadMode mode;
        bool tdb = true;
        bool dem = false;

        public override void Awake()
        {
            this.isInteractive = true;
            this.enabled = true;

            width = 400;

            title = AddUIComponent<UILabel>();

            centerXLabel = AddUIComponent<UILabel>();
            centerXTextField = AddUIComponent<UITextField>();
            centerYLabel = AddUIComponent<UILabel>();
            centerYTextField = AddUIComponent<UITextField>();

            areaLabel = AddUIComponent<UILabel>();
            areaDropDown = AddUIComponent<UIDropDown>();

            bdot10kLabel = AddUIComponent<UILabel>();
            bdot10kCheck = AddUIComponent<UISprite>();
            bdot10kCheckBox = AddUIComponent<UIButton>();

            bdot10kPathLabel = AddUIComponent<UILabel>();
            bdot10kPathTextField = AddUIComponent<UITextField>();

            demLabel = AddUIComponent<UILabel>();
            demCheck = AddUIComponent<UISprite>();
            demCheckBox = AddUIComponent<UIButton>();

            demPathLabel = AddUIComponent<UILabel>();
            demPathTextField = AddUIComponent<UITextField>();

            importTDBButton = AddUIComponent<UIButton>();
            importDEMButton = AddUIComponent<UIButton>();
            base.Awake();
        }

        public override void Start()
        {
            base.Start();

            relativePosition = new Vector3(396, 58);
            backgroundSprite = "MenuPanel2";
            isInteractive = true;
            SetupControls();
        }

        private void SetupControls()
        {
            title.text = "BDOT10k and DEM Import";
            title.relativePosition = new Vector3(15, 15);
            title.textScale = 0.9f;
            title.size = new Vector2(200, 30);
            var vertPadding = 30;
            var x = 15;
            var y = 50;

            SetLabel(centerXLabel, "Center X", x, y);
            SetTextField(centerXTextField, "0.0", x + 150, y);
            centerXTextField.width -= 150;
            centerXTextField.horizontalAlignment = UIHorizontalAlignment.Center;
            y += vertPadding;

            SetLabel(centerYLabel, "Center Y", x, y);
            SetTextField(centerYTextField, "0.0", x + 150, y);
            centerYTextField.width -= 150;
            centerYTextField.horizontalAlignment = UIHorizontalAlignment.Center;
            y += vertPadding;

            SetLabel(bdot10kLabel, "Import BDOT10k", x, y);
            SetCheckButton(bdot10kCheckBox, x + 150, y);
            SetCheck(bdot10kCheck, true, x + 150, y);
            bdot10kCheckBox.eventClick += bdot10kCheckBox_eventClick;
            y += vertPadding;

            SetLabel(areaLabel, "Imported area side length [m]", x + 15, y);
            y += vertPadding;
            SetAreaDropDown(areaDropDown, x, y);
            y += vertPadding;

            SetLabel(bdot10kPathLabel, "BDOT10k Path", x + 15, y);
            y += vertPadding;
            SetTextField(bdot10kPathTextField, @"C:\Program Files\", x + 15, y);
            y += vertPadding;

            SetLabel(demLabel, "Import DEM", x, y);
            SetCheckButton(demCheckBox, x + 150, y);
            SetCheck(demCheck, false, x + 150, y);
            demCheckBox.eventClick += demCheckBox_eventClick;
            y += vertPadding;

            SetLabel(demPathLabel, "DEM Path", x + 15, y);
            demPathLabel.isVisible = false;
            y += vertPadding;
            SetTextField(demPathTextField, @"C:\Program Files\", x + 15, y);
            demPathTextField.isVisible = false;
            y += vertPadding;

            SetButton(importTDBButton, "Import TDB", y);
            importTDBButton.eventClick += importTDBButton_eventClick;
            y += vertPadding;

            SetButton(importDEMButton, "Import DEM", y);
            importDEMButton.eventClick += importDEMButton_eventClick;
            importDEMButton.Disable();

            height = y + vertPadding + 5;
        }

        private void bdot10kCheckBox_eventClick(UIComponent component, UIMouseEventParameter eventParam)
        {
            tdb = !tdb;
            areaLabel.isVisible = !areaLabel.isVisible;
            areaDropDown.isVisible = !areaDropDown.isVisible;
            bdot10kCheck.isVisible = !bdot10kCheck.isVisible;
            bdot10kPathLabel.isVisible = !bdot10kPathLabel.isVisible;
            bdot10kPathTextField.isVisible = !bdot10kPathTextField.isVisible;
            if (importTDBButton.isVisible)
            {
                if ((dem && !tdb) || (dem && tdb))
                {
                    importDEMButton.Enable();
                    importTDBButton.Disable();
                }
                else if (!dem && tdb)
                {
                    importDEMButton.Disable();
                    importTDBButton.Enable();
                }
                else
                {
                    importTDBButton.Disable();
                    importDEMButton.Disable();
                }
            }
        }

        private void demCheckBox_eventClick(UIComponent component, UIMouseEventParameter eventParam)
        {
            dem = !dem;
            demCheck.isVisible = !demCheck.isVisible;
            demPathLabel.isVisible = !demPathLabel.isVisible;
            demPathTextField.isVisible = !demPathTextField.isVisible;
            if (importDEMButton.isVisible)
            {
                if ((dem && !tdb) || (dem && tdb))
                {
                    importDEMButton.Enable();
                    importTDBButton.Disable();
                }
                else if (!dem && tdb)
                {
                    importDEMButton.Disable();
                    importTDBButton.Enable();
                }
                else
                {
                    importTDBButton.Disable();
                    importDEMButton.Disable();
                }
            }
        }

        private void importTDBButton_eventClick(UIComponent component, UIMouseEventParameter eventParam)
        {
            ParserInit parser = new ParserInit();

            parser.Arguments[0] = centerXTextField.text;
            parser.Arguments[1] = centerYTextField.text;
            parser.Arguments[2] = areaDropDown.selectedValue;
            try
            {
                if (tdb)
                {
                    parser.Arguments[3] = bdot10kPathTextField.text;
                    parser.ProcessTDBExternally();
                }
            }
            catch (Exception e)
            {
                Debug.Log(e);
            }
            finally
            {
                importTDBButton.isVisible = false;
                bdot10kPathTextField.Disable();
                bdot10kCheckBox.Disable();
                bdot10kCheck.Disable();
                importDEMButton.isVisible = false;
                demPathTextField.Disable();
                demCheckBox.Disable();
                demCheck.Disable();
            }
        }

        private void importDEMButton_eventClick(UIComponent component, UIMouseEventParameter eventParam)
        {
            ParserInit parser = new ParserInit();

            parser.Arguments[0] = centerXTextField.text;
            parser.Arguments[1] = centerYTextField.text;
            parser.Arguments[2] = areaDropDown.selectedValue;
            try
            {
                if (dem)
                {
                    parser.Arguments[4] = demPathTextField.text;
                    parser.ProcessDEMExternally();
                }
            }
            catch (Exception e)
            {
                Debug.Log(e);
            }
            finally
            {
                importDEMButton.isVisible = false;
                if (tdb)
                    importTDBButton.Enable();
                demPathTextField.Disable();
                demCheckBox.Disable();
                demCheck.Disable();
            }
        }

        private void SetLabel(UILabel pedestrianLabel, string text, int x, int y)
        {
            pedestrianLabel.relativePosition = new Vector3(x, y);
            pedestrianLabel.text = text;
            pedestrianLabel.textScale = 0.8f;
            pedestrianLabel.size = new Vector3(120, 20);
        }

        private void SetCheck(UISprite sprite, bool visible, int x, int y)
        {
            sprite.spriteName = "check-checked";
            sprite.relativePosition = new Vector3(x + 3, y);
            sprite.isVisible = visible;
        }

        private void SetCheckButton(UIButton checkButton, int x, int y)
        {
            checkButton.normalBgSprite = "check-unchecked";
            checkButton.color = Color.black;
            checkButton.size = new Vector2(20, 20);
            checkButton.relativePosition = new Vector3(x, y);
        }

        private void SetButton(UIButton button, string text, int y)
        {
            button.text = text;
            button.normalBgSprite = "ButtonMenu";
            button.hoveredBgSprite = "ButtonMenuHovered";
            button.disabledBgSprite = "ButtonMenuDisabled";
            button.focusedBgSprite = "ButtonMenuFocused";
            button.pressedBgSprite = "ButtonMenuPressed";
            button.size = new Vector2(200, 24);
            button.relativePosition = new Vector3((int)(width - button.size.x) / 2, y);
            button.textScale = 0.8f;
        }

        private void SetTextField(UITextField textBox, string text, int x, int y)
        {
            textBox.relativePosition = new Vector3(x, y - 4);
            textBox.horizontalAlignment = UIHorizontalAlignment.Left;
            textBox.text = text;
            textBox.textScale = 0.8f;
            textBox.color = Color.black;
            textBox.cursorBlinkTime = 0.45f;
            textBox.cursorWidth = 1;
            textBox.selectionBackgroundColor = new Color(233, 201, 148, 255);
            textBox.selectionSprite = "EmptySprite";
            textBox.verticalAlignment = UIVerticalAlignment.Middle;
            textBox.padding = new RectOffset(5, 0, 5, 0);
            textBox.foregroundSpriteMode = UIForegroundSpriteMode.Fill;
            textBox.normalBgSprite = "TextFieldPanel";
            textBox.hoveredBgSprite = "TextFieldPanelHovered";
            textBox.focusedBgSprite = "TextFieldPanel";
            textBox.size = new Vector3(width - 60, 20);
            textBox.isInteractive = true;
            textBox.enabled = true;
            textBox.readOnly = false;
            textBox.builtinKeyNavigation = true;
        }

        private void SetAreaDropDown(UIDropDown areaDropDown, int x, int y)
        {
            areaDropDown.size = new Vector3(width - 300, 20);
            areaDropDown.listBackground = "GenericPanelLight";
            areaDropDown.itemHeight = 25;
            areaDropDown.itemHover = "ListItemHover";
            areaDropDown.itemHighlight = "ListItemHighlight";
            areaDropDown.normalBgSprite = "ButtonMenu";
            areaDropDown.disabledBgSprite = "ButtonMenuDisabled";
            areaDropDown.hoveredBgSprite = "ButtonMenuHovered";
            areaDropDown.focusedBgSprite = "ButtonMenu";
            areaDropDown.listWidth = (int)width - 300;
            areaDropDown.listHeight = 700;
            areaDropDown.listPosition = UIDropDown.PopupListPosition.Below;
            areaDropDown.clampListToScreen = true;
            areaDropDown.builtinKeyNavigation = true;
            areaDropDown.foregroundSpriteMode = UIForegroundSpriteMode.Stretch;
            areaDropDown.popupColor = new Color32(45, 52, 61, 255);
            areaDropDown.popupTextColor = new Color32(170, 170, 170, 255);
            areaDropDown.zOrder = 1;
            areaDropDown.textScale = 0.8f;
            areaDropDown.verticalAlignment = UIVerticalAlignment.Middle;
            areaDropDown.horizontalAlignment = UIHorizontalAlignment.Left;
            areaDropDown.selectedIndex = 0;
            areaDropDown.textFieldPadding = new RectOffset(30, 0, 5, 0);
            areaDropDown.itemPadding = new RectOffset(10, 0, 5, 0);


            areaDropDown.relativePosition = new Vector3(x + 15, y);
            areaDropDown.isInteractive = true;
            areaDropDown.enabled = true;
            areaDropDown.autoSize = true;
            areaDropDown.builtinKeyNavigation = true;


            areaDropDown.items = Enum.GetValues(typeof(UsedArea))
                .Cast<int>()
                .Select(e => e.ToString())
                .ToArray();
            areaDropDown.selectedIndex = 0;
        }
    }
}
