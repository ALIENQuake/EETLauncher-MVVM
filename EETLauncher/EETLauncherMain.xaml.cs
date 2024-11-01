//Copyright © alienquake@hotmail.com
using System;
using System.Globalization;
using System.IO;
using System.Resources;
using System.Windows;
using System.Windows.Input;
using static EETLauncherMVVM.EETLauncherConfig;
using static EETLauncherMVVM.EETLauncherGlobal;

[assembly: NeutralResourcesLanguage("en")]
namespace EETLauncherMVVM {

    /// <summary>
    /// Interaction logic for <see cref="EETLauncher"/>.xaml
    /// </summary>
    public partial class EETLauncherMain {
        public EETLauncherMain() {
            CultureInfo.DefaultThreadCurrentCulture = CultureInfo.InvariantCulture;
            CultureInfo.DefaultThreadCurrentUICulture = CultureInfo.InvariantCulture;

            InitializeComponent();
            DataContext = new MainViewModel();

            AppRootPath = Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory) + Path.DirectorySeparatorChar;
            AppLogFileName = (string)FindResource("AppLogFileName");
            GameCheckFilePath = (string)FindResource("GameCheckFilePath");
            GameCfgFileName = (string)FindResource("GameCfgFileName");
            GameExeFileName = (string)FindResource("GameExeFileName");
            GameEngineFileName = (string)FindResource("GameEngineFileName");
            EETModFileName = (string)FindResource("EETModFileName");
            EETGUIComponentNumber = (string)FindResource("EETGUIComponentNumber");
            EETGUIModFileName = (string)FindResource("EETGUIModFileName");
            EETGUIExeFileName = (string)FindResource("EETGUIExeFileName");
            EETGUIUnknown = (string)FindResource("EETGUIUnknown");
            EETHomePage = (string)FindResource("EETHomePage");
            EETReadMeFilePath = (string)FindResource("EETReadMeFilePath");
            EETFlagFilePath = (string)FindResource("EETFlagFilePath");
            BG2EENotDetected = (string)FindResource("BG2EENotDetected");
            EETNotDetected = (string)FindResource("EETNotDetected");
            EETRequireFirstRun = (string)FindResource("EETRequireFirstRun");
            ModManagerHomePage = (string)FindResource("ModManagerHomePage");
            WeiDULogFileName = (string)FindResource("WeiDULogFileName");

            if (TestBG2EEDirectory()) {
                if (TestEETInstalled()) {
                    GameCfgDirectory = GetGameCfgDirectory();
                    EETBaldurLua = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + Path.DirectorySeparatorChar + GameCfgDirectory + Path.DirectorySeparatorChar + GameCfgFileName;
                    return;
                }
                DisableEETLauncherMainLB();
                EETLauncherMain_TB_LOG.Visibility = Visibility.Visible;
                EETLauncherMain_TB_LOG.Text = EETNotDetected;
            } else {
                DisableEETLauncherMainLB();
                EETLauncherMain_TB_LOG.Visibility = Visibility.Visible;
                EETLauncherMain_TB_LOG.Text = BG2EENotDetected;
            }
        }

        public void DisableEETLauncherMainLB() {
            EETLauncherMain_LB_PLAY.Visibility = Visibility.Hidden;
            EETLauncherMain_LB_SETTINGS.Visibility = Visibility.Hidden;
            EETLauncherMain_LB_UPDATES.Visibility = Visibility.Hidden;
            EETLauncherMain_LB_README.Visibility = Visibility.Hidden;
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) {
            try { DragMove(); } catch { throw; }
        }

        private void EETLauncherMain_LB_SETTINGS_Click(object sender, RoutedEventArgs e) {
            var EETLauncherSettings = new EETLauncherSettings { Owner = this };
            Visibility = Visibility.Hidden;
            EETLauncherSettings.ShowDialog();
            Visibility = Visibility.Visible;
        }
    }
}
