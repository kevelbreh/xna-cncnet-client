﻿using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;
using dtasetup.domain.cncnet;
using dtasetup.persistence;
using System.Drawing;
using ClientCore;

namespace dtasetup.domain
{
    class MainClientConstants
    {
        public static string GAME_SETTINGS = "SUN.ini";
        public const String NEW_VERSION = "version";
        public const String LANGUAGE_DLL = "Language.dll";

        public static String gamepath = Path.GetDirectoryName(Application.ExecutablePath).TrimEnd('\\') + @"\";

        public static int CurrentUpdateMirrorId = 0;

        public static String GAME_NAME_LONG = "Tiberian Sun";
        public static String GAME_NAME_SHORT = "TS";
        public static string CNCNET_LIVE_STATUS_ID = "cncnet5_ts";

        public const String UPDATE_EXECFILE = "updateexec";
        public const String MOVIES_LOCAL = "MIX\\TIFMVS.MIX";
        public const String MOVIES_DL = "MIX/TIFMVS.MIX";
        public const String DOWNLOAD_SPEEDTEST = "MEGABYTE";
        public static String FINALSUN_INI = "FinalTI\\FinalSun.ini";
        public const String ASSOCIATED_GAME_VERSION = "3.0";
        public const String QRES_EXECUTABLE = "qres.dat";
        public const String CNCNET_SERVERS = "http://cncnet.org/servers.csv";

        public static String CHANGELOG_URL = "http://rampastring.cncnet.org/TSupdates/changelog.txt";
        public static String CREDITS_URL = "http://rampastring.cncnet.org/TS/Credits.txt";

        public static String SUPPORT_URL = "http://www.moddb.com/members/rampastring";
        public static String SUPPORT_URL_SHORT = "www.moddb.com/members/rampastring";

        public const string SPAWNMAP_INI = "spawnmap.ini";
        public const string SPAWNER_SETTINGS = "spawn.ini";

        public static volatile bool IsInGame = false;

        public const bool EnableLogging = true;

        public static OSVersion OSId = OSVersion.UNKNOWN;

        public static bool IsAutomaticInstallation = false;
        public static bool AutoRunCnCNetClient = false;
        public static bool IgnoreVersionMismatch = false;

        /// <summary>
        /// Initializes download mirrors and the custom component list.
        /// </summary>
        public static void Initialize()
        {
            GAME_SETTINGS = ClientCore.DomainController.Instance().GetSettingsIniName();

            OSId = ClientCore.DomainController.Instance().GetOperatingSystemVersion();

            GAME_NAME_SHORT = MCDomainController.Instance().GetShortGameName();
            GAME_NAME_LONG = MCDomainController.Instance().GetLongGameName();

            SUPPORT_URL = MCDomainController.Instance().GetLongSupportURL();
            SUPPORT_URL_SHORT = MCDomainController.Instance().GetShortSupportURL();

            CNCNET_LIVE_STATUS_ID = MCDomainController.Instance().GetCnCNetLiveStatusIdentifier();

            CHANGELOG_URL = MCDomainController.Instance().GetChangelogURL();
            CREDITS_URL = MCDomainController.Instance().GetCreditsURL();

            FINALSUN_INI = MCDomainController.Instance().GetFinalSunIniPath();
        }
    }
}