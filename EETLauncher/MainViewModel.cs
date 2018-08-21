using System;
using System.Diagnostics;
using System.IO;
using System.Reactive;
using System.Reactive.Concurrency;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using ReactiveUI;
using static EETLauncherWPF.EETLauncherConfig;
using static EETLauncherWPF.EETLauncherGlobal;

namespace EETLauncherWPF {
    class MainViewModel : ReactiveObject {

        public bool _enabled;
        public Visibility _logVisibility;
        public Visibility _mainVisibility;

        public ICommand PlayEET {get;}
        public ICommand OpenSettingsWindow {get;}
        public ICommand OpenEETReadMe {get;}
        public ICommand OpenEETHomePage {get;}
        public ICommand OpenModManagerPage {get;}
        public ICommand CheckForUpdates {get;}
        public ICommand Exit {get;}

        public MainViewModel() {

            Enabled = true;
            LogVisibility = Visibility.Hidden;
            MainVisibility = Visibility.Visible;

            PlayEET = ReactiveCommand.Create( PlayEET_OnExecuted );
            OpenSettingsWindow = ReactiveCommand.Create( OpenSettingsWindow_OnExecuted );
            OpenEETReadMe = ReactiveCommand.Create( OpenEETReadMe_OnExecuted );
            OpenEETHomePage = ReactiveCommand.Create( OpenEETHomePage_OnExecuted );
            OpenModManagerPage = ReactiveCommand.Create( OpenModManagerPage_OnExecuted );
            CheckForUpdates = ReactiveCommand.Create( CheckForUpdates_OnExecuted );
            Exit = ReactiveCommand.Create( Exit_OnExecuted );
        }
        
        public bool Enabled {
            get => _enabled;
            set => this.RaiseAndSetIfChanged(ref _enabled, value);
        }

        public Visibility LogVisibility {
            get => _logVisibility;
            set => this.RaiseAndSetIfChanged(ref _logVisibility, value);
        }

        public Visibility MainVisibility {
            get => _mainVisibility;
            set => this.RaiseAndSetIfChanged(ref _mainVisibility, value);
        }

        public void OpenSettingsWindow_OnExecuted() {
            var EETLauncherSettings = new EETLauncherSettings {
                // Owner = this 
                Owner = Application.Current.MainWindow
            };
            EETLauncherSettings.Show();
            Application.Current.MainWindow.Hide();
            //Visibility = Visibility.Hidden;
            MainVisibility = Visibility.Hidden;
        }

        public void CheckForUpdates_OnExecuted() {
            // Check for updates
        }

        public void PlayEET_OnExecuted() {
            Process.Start( AppRootPath + GameExeFileName );
            //Close();
            Application.Current.Shutdown();
        }

        public void OpenEETReadMe_OnExecuted() {
            Process.Start( AppRootPath + EETReadMeFilePath );
        }

        public void OpenModManagerPage_OnExecuted() {
            Process.Start( ModManagerHomePage );
        }

        public void OpenEETHomePage_OnExecuted() {
            Process.Start( EETHomePage );
        }

        public void Exit_OnExecuted() {
            //Close();
            Application.Current.Shutdown();
        }
    }
}
