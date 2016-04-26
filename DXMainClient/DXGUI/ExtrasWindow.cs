﻿using ClientCore;
using ClientGUI;
using DTAClient.domain;
using Microsoft.Xna.Framework;
using Rampastring.XNAUI;
using Rampastring.XNAUI.DXControls;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace DTAClient.DXGUI
{
    public class ExtrasWindow : DXWindow
    {
        public ExtrasWindow(Game game, WindowManager windowManager) : base(game, windowManager)
        {

        }

        public override void Initialize()
        {
            Name = "ExtrasWindow";
            ClientRectangle = new Rectangle(0, 0, 284, 190);
            BackgroundTexture = AssetLoader.LoadTexture("extrasMenu.png");

            DXButton btnExStatistics = new DXButton(Game, WindowManager);
            btnExStatistics.Name = "btnExStatistics";
            btnExStatistics.IdleTexture = AssetLoader.LoadTexture("133pxbtn.png");
            btnExStatistics.HoverTexture = AssetLoader.LoadTexture("133pxbtn_c.png");
            btnExStatistics.HoverSoundEffect = AssetLoader.LoadSound("button.wav");
            btnExStatistics.ClientRectangle = new Rectangle(76, 17, 133, 23);
            btnExStatistics.FontIndex = 1;
            btnExStatistics.Text = "Statistics";
            btnExStatistics.LeftClick += BtnExStatistics_LeftClick;

            DXButton btnExMapEditor = new DXButton(Game, WindowManager);
            btnExMapEditor.Name = "btnExMapEditor";
            btnExMapEditor.IdleTexture = AssetLoader.LoadTexture("133pxbtn.png");
            btnExMapEditor.HoverTexture = AssetLoader.LoadTexture("133pxbtn_c.png");
            btnExMapEditor.HoverSoundEffect = AssetLoader.LoadSound("button.wav");
            btnExMapEditor.ClientRectangle = new Rectangle(76, 59, 133, 23);
            btnExMapEditor.FontIndex = 1;
            btnExMapEditor.Text = "Map Editor";
            btnExMapEditor.LeftClick += BtnExMapEditor_LeftClick;

            DXButton btnExCredits = new DXButton(Game, WindowManager);
            btnExCredits.Name = "btnExCredits";
            btnExCredits.IdleTexture = AssetLoader.LoadTexture("133pxbtn.png");
            btnExCredits.HoverTexture = AssetLoader.LoadTexture("133pxbtn_c.png");
            btnExCredits.HoverSoundEffect = AssetLoader.LoadSound("button.wav");
            btnExCredits.ClientRectangle = new Rectangle(76, 101, 133, 23);
            btnExCredits.FontIndex = 1;
            btnExCredits.Text = "Credits";
            btnExCredits.LeftClick += BtnExCredits_LeftClick;

            DXButton btnExCancel = new DXButton(Game, WindowManager);
            btnExCancel.Name = "btnExCancel";
            btnExCancel.IdleTexture = AssetLoader.LoadTexture("133pxbtn.png");
            btnExCancel.HoverTexture = AssetLoader.LoadTexture("133pxbtn_c.png");
            btnExCancel.HoverSoundEffect = AssetLoader.LoadSound("button.wav");
            btnExCancel.ClientRectangle = new Rectangle(76, 160, 133, 23);
            btnExCancel.FontIndex = 1;
            btnExCancel.Text = "Cancel";
            btnExCancel.LeftClick += BtnExCancel_LeftClick;

            AddChild(btnExStatistics);
            AddChild(btnExMapEditor);
            AddChild(btnExCredits);
            AddChild(btnExCancel);

            base.Initialize();

            CenterOnParent();
        }

        private void BtnExStatistics_LeftClick(object sender, EventArgs e)
        {
            MainMenuDarkeningPanel parent = (MainMenuDarkeningPanel)Parent;
            parent.Show(parent.StatisticsWindow);
        }

        private void BtnExMapEditor_LeftClick(object sender, EventArgs e)
        {
            Process.Start(ProgramConstants.gamepath + MCDomainController.Instance.GetMapEditorExePath());
            ((MainMenuDarkeningPanel)Parent).Hide();
        }

        private void BtnExCredits_LeftClick(object sender, EventArgs e)
        {
            Process.Start(MainClientConstants.CREDITS_URL);
        }

        private void BtnExCancel_LeftClick(object sender, EventArgs e)
        {
            ((MainMenuDarkeningPanel)Parent).Hide();
        }
    }
}