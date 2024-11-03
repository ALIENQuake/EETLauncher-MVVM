//Copyright © alienquake@hotmail.com

using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using ReactiveUI;
using static EETLauncher.EETLauncherConfig;
using static EETLauncher.EETLauncherGlobal;

namespace EETLauncher {
    class EETLauncherSettingsViewModel : ReactiveObject {

        private string _changeTo;
        private string _currentGui;
        private Brush _color;
        public bool _enabled;
        public Visibility _logVisibility;

        public EETLauncherSettingsViewModel() {
            Color = new SolidColorBrush(Colors.White);
            CurrentGui = "BG2";
            ChangeTo = "SoD";
            Enabled = true;
            LogVisibility = Visibility.Hidden;
            OpenEETLua = ReactiveCommand.Create(OpenEETLua_OnExecuted);
            ChangeEETGuiAsync = ReactiveCommand.Create(ChangeEETGuiAsync_OnExecuted);
        }

        public string ChangeTo {
            get => _changeTo;
            set => this.RaiseAndSetIfChanged(ref _changeTo, value);
        }

        public string CurrentGui {
            get => _currentGui;
            set => this.RaiseAndSetIfChanged(ref _currentGui, value);
        }

        public Brush Color {
            get => _color;
            set => this.RaiseAndSetIfChanged(ref _color, value);
        }

        public bool Enabled {
            get => _enabled;
            set => this.RaiseAndSetIfChanged(ref _enabled, value);
        }

        public Visibility LogVisibility {
            get => _logVisibility;
            set => this.RaiseAndSetIfChanged(ref _logVisibility, value);
        }

        public ICommand OpenEETLua { get; }
        public ICommand ChangeEETGuiAsync { get; }

        public void OpenEETLua_OnExecuted() {
            if (TestEETBaldurLua()) {
                Process.Start(EETBaldurLua);
            } else {
                //EETLauncherSettings_TB_Log.Visibility = Visibility.Visible;
                LogVisibility = Visibility.Visible;
                //EETLauncherSettings_TB_Log.Text = EETRequireFirstRun;
            }
        }
        public async void ChangeEETGuiAsync_OnExecuted() {
            Enabled = false;
            Color = Brushes.White;

            using (var process = new Process { StartInfo = SetEETGUI(ChangeTo) }) {
                var EETGuiProcess = process;

                try {
                    using (var result = await Task.Run(() =>
                    {
                        EETGuiProcess.Start();
                        if (EETGuiProcess.Id >= 0) {
                            EETGuiProcess?.WaitForExit();
                        } else {
                            EETGuiProcess = null;
                        }
                        return EETGuiProcess;
                    })) {
                        if (result == null) return;
                    }

                    (ChangeTo, CurrentGui) = (CurrentGui, ChangeTo);
                    Color = Brushes.Green;

                } catch (Exception ex) {
                    Color = Brushes.Red;
                    //EETLauncherSettings_TB_Log.Text = ex.Message;
                    File.AppendAllText(Environment.SpecialFolder.ApplicationData + Path.DirectorySeparatorChar + AppLogFileName, ex.Message + Environment.NewLine);
                    throw;
                } finally {
                    Enabled = true;
                }
            }
        }
    }
}
