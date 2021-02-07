using IWshRuntimeLibrary;
using Microsoft.Win32;
using System;
using System.IO;
using System.Windows;

namespace SwissKit.Subprograms.AddToStart
{
    internal class AddToStartSubprogram : SubprogramBase
    {
        public AddToStartSubprogram() 
            : base("Add to Start", "Creates application shortcut in Start folder, making it discoverable through search bar")
        {
        }

        protected override void LaunchSubprogram()
        {
            if (GetApplicationPath(out string applicationPath))
            {

                // Resolves to "C:\ProgramData\Microsoft\Windows\Start Menu\Programs"
                var startProgramsPath = Environment.GetFolderPath(Environment.SpecialFolder.Programs);

                if (Exists(startProgramsPath, applicationPath))
                {
                    MessageBox.Show($"{Path.GetFileNameWithoutExtension(applicationPath)} already exists");
                }
                else
                {
                    CreateShortcut(startProgramsPath, applicationPath);
                    MessageBox.Show($"Successfully added {Path.GetFileNameWithoutExtension(applicationPath)} to start menu search");
                }
            }
        }

        bool GetApplicationPath(out string path)
        {
            var dialog = new OpenFileDialog
            {
                DefaultExt = ".exe",
                Filter = "Applications (*.exe)|*.exe"
            };

            var result = dialog.ShowDialog();
            if (result == true)
            {
                path = dialog.FileName;
                return true;
            }

            path = default;
            return false;
        }

        bool Exists(string shortcutFolderPath, string targetPath)
        {
            var appName = Path.GetFileNameWithoutExtension(targetPath);
            var shortcutLocation = Path.Combine(shortcutFolderPath, appName + ".lnk");
            return System.IO.File.Exists(shortcutLocation);
        }

        void CreateShortcut(string shortcutFolderPath, string targetPath)
        {
            var appName = Path.GetFileNameWithoutExtension(targetPath);
            var shortcutLocation = Path.Combine(shortcutFolderPath, appName + ".lnk");
            var shell = new WshShell();
            var shortcut = (IWshShortcut)shell.CreateShortcut(shortcutLocation);

            shortcut.Description = $"Shortcut for {appName}";
            shortcut.TargetPath = targetPath;
            shortcut.Save();
        }
    }
}
