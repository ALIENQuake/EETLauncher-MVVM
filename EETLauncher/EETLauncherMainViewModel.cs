//Copyright © alienquake@hotmail.com

using System.Diagnostics;
using System.Windows;
using System.Windows.Input;
using ReactiveUI;
using static EETLauncher.EETLauncherConfig;

namespace EETLauncher {
    class EETLauncherMainViewModel : ReactiveObject {

        public bool _enabled;
        public Visibility _logVisibility;

        public ICommand PlayEET { get; }
        public ICommand OpenEETReadMe { get; }
        public ICommand OpenEETHomePage { get; }
        public ICommand OpenModManagerPage { get; }
        public ICommand CheckForUpdates { get; }
        public ICommand Exit { get; }

        public EETLauncherMainViewModel() {

            Enabled = true;
            LogVisibility = Visibility.Hidden;

            PlayEET = ReactiveCommand.Create(PlayEET_OnExecuted);
            OpenEETReadMe = ReactiveCommand.Create(OpenEETReadMe_OnExecuted);
            OpenEETHomePage = ReactiveCommand.Create(OpenEETHomePage_OnExecuted);
            OpenModManagerPage = ReactiveCommand.Create(OpenModManagerPage_OnExecuted);
            CheckForUpdates = ReactiveCommand.Create(CheckForUpdates_OnExecuted);
            Exit = ReactiveCommand.Create(Exit_OnExecuted);

        }

        public bool Enabled {
            get => _enabled;
            set => this.RaiseAndSetIfChanged(ref _enabled, value);
        }

        public Visibility LogVisibility {
            get => _logVisibility;
            set => this.RaiseAndSetIfChanged(ref _logVisibility, value);
        }

        public void CheckForUpdates_OnExecuted() {
            // Check for updates
        }

        public void PlayEET_OnExecuted() {
            Process.Start(AppRootPath + GameExeFileName);
            Application.Current.Shutdown();
        }

        public void OpenEETReadMe_OnExecuted() {
            Process.Start(AppRootPath + EETReadMeFilePath);
        }

        public void OpenModManagerPage_OnExecuted() {
            Process.Start(ModManagerHomePage);
        }

        public void OpenEETHomePage_OnExecuted() {
            Process.Start(EETHomePage);
        }

        public void Exit_OnExecuted() {
            Application.Current.Shutdown();
        }
    }
}
