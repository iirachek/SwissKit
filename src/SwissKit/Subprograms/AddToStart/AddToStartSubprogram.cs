using IWshRuntimeLibrary;
using Microsoft.Win32;
using System;
using System.IO;
using System.Windows;

namespace SwissKit.Subprograms.AddToStart
{
    internal sealed class AddToStartSubprogram : ISubprogram
    {
        public void Run()
        {
            if (GetApplicationPath(out string targetPath))
            {
                var appName = Path.GetFileNameWithoutExtension(targetPath);
                // Resolves to "C:\ProgramData\Microsoft\Windows\Start Menu\Programs"
                var startProgramsPath = Environment.GetFolderPath(Environment.SpecialFolder.Programs);

                var shortcutFullPath = GetShortcutFullPath(startProgramsPath, appName);

                if (System.IO.File.Exists(shortcutFullPath))
                {
                    MessageBox.Show($"{Path.GetFileNameWithoutExtension(targetPath)} already exists");
                }
                else
                {
                    CreateShortcut(shortcutFullPath, targetPath);
                    MessageBox.Show($"Successfully added {Path.GetFileNameWithoutExtension(targetPath)} to start menu search");
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

        string GetShortcutFullPath(string path, string appName)
        {
            return Path.Combine(path, appName + ".lnk");
        }

        void CreateShortcut(string shortcutFullPath, string targetPath)
        {
            var appName = Path.GetFileNameWithoutExtension(targetPath);
            var shell = new WshShell();
            var shortcut = (IWshShortcut)shell.CreateShortcut(shortcutFullPath);

            shortcut.Description = $"Shortcut for {appName}";
            shortcut.TargetPath = targetPath;
            shortcut.Save();
        }
    }
}
