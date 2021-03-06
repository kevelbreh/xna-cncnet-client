﻿using System;
using System.IO;
using System.Threading;
using System.Management;
using Microsoft.Win32;
using DTAClient.Domain;
using ClientCore;
using Updater;
using Rampastring.Tools;
using DTAClient.DXGUI;

namespace DTAClient
{
    /// <summary>
    /// A class that handles initialization of the Client.
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// The main method for startup and initialization.
        /// </summary>
        public void Execute()
        {
            int themeId = UserINISettings.Instance.ClientTheme;

            if (themeId >= ClientConfiguration.Instance.ThemeCount || themeId < 0)
            {
                themeId = 0;
                UserINISettings.Instance.ClientTheme.Value = 0;
            }

            ProgramConstants.RESOURCES_DIR = "Resources\\" + ClientConfiguration.Instance.GetThemeInfoFromIndex(themeId)[1];

            Logger.Log("Initializing updater.");

            File.Delete(ProgramConstants.GamePath + "version_u");

            CUpdater.Initialize(ClientConfiguration.Instance.LocalGame);

            Logger.Log("Operating system: " + Environment.OSVersion.VersionString);
            Logger.Log("Selected OS profile: " + MainClientConstants.OSId.ToString());

            // The query in CheckSystemSpecifications takes lots of time,
            // so we'll do it in a separate thread to make startup faster
            Thread thread = new Thread(CheckSystemSpecifications);
            thread.Start();

            if (Directory.Exists(MainClientConstants.gamepath + "Updater"))
            {
                Logger.Log("Attempting to delete temporary updater directory.");
                try
                {
                    Directory.Delete(MainClientConstants.gamepath + "Updater", true);
                }
                catch
                {
                }
            }

            if (CUpdater.CustomComponents != null)
            {
                Logger.Log("Removing partial custom component downloads.");
                foreach (CustomComponent component in CUpdater.CustomComponents)
                {
                    try
                    {
                        File.Delete(MainClientConstants.gamepath + component.LocalPath + "_u");
                    }
                    catch
                    {

                    }
                }
            }

            FinalSunSettings.WriteFinalSunIni();

            WriteInstallPathToRegistry();

            ClientConfiguration.Instance.RefreshSettings();

            GameClass gameClass = new GameClass();
            gameClass.Run();
        }

        /// <summary>
        /// Writes processor and graphics card info to the log file.
        /// </summary>
        private void CheckSystemSpecifications()
        {
            try
            {
                string cpu = String.Empty;
                string videoController = String.Empty;

                ManagementObjectSearcher searcher = 
                    new ManagementObjectSearcher("SELECT * FROM Win32_Processor");

                foreach (var proc in searcher.Get())
                {
                    cpu = cpu + proc["Name"] + " (" + proc["NumberOfCores"] + " cores) ";
                }

                searcher = new ManagementObjectSearcher("SELECT * FROM Win32_VideoController");

                foreach (ManagementObject mo in searcher.Get())
                {
                    PropertyData currentBitsPerPixel = mo.Properties["CurrentBitsPerPixel"];
                    PropertyData description = mo.Properties["Description"];
                    if (currentBitsPerPixel != null && description != null)
                    {
                        if (currentBitsPerPixel.Value != null)
                            videoController = videoController + "Video controller: " + description.Value.ToString() + " ";
                    }
                }

                Logger.Log("Hardware info: {0} {1}", cpu, videoController);
            }
            catch (Exception ex)
            {
                Logger.Log("Checking system specifications failed. Message: " + ex.Message);
            }
        }

        /// <summary>
        /// Writes the game installation path to the Windows registry.
        /// </summary>
        private void WriteInstallPathToRegistry()
        {
            if (!UserINISettings.Instance.WritePathToRegistry)
            {
                Logger.Log("Skipping writing installation path to the Windows Registry because of INI setting.");
                return;
            }

            Logger.Log("Writing installation path to the Windows registry.");

            RegistryKey key;
            key = Registry.CurrentUser.CreateSubKey("SOFTWARE\\" + ClientConfiguration.Instance.InstallationPathRegKey);
            key.SetValue("InstallPath", MainClientConstants.gamepath);
            key.Close();
        }
    }
}
