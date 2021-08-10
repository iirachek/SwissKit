using System;
using System.IO;
using System.Windows.Forms;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace SwissKit.Subprograms.NumberedCut
{
    internal sealed class NumberedCutSubprogram : ISubprogram
    {
        public void Run()
        {
            if (!PromtFolderSelection("Select folder to cut files from", string.Empty, out string sourceFolder))
                return;

            if (!PromtFolderSelection("Select folder to paste files into", string.Empty, out string destinationFolder))
                return;

            var sourceFiles = Directory.GetFiles(sourceFolder);
            var startingIndex = GetMaxIndex(destinationFolder) + 1;

            var msg = $"Confirm numbered cut of {sourceFiles.Length} files, starting from {startingIndex}" +
                $"{Environment.NewLine}" +
                $"{Environment.NewLine}from:{Environment.NewLine}{sourceFolder}" +
                $"{Environment.NewLine}" +
                $"{Environment.NewLine}to:{Environment.NewLine}{destinationFolder}";
            if (MessageBox.Show(msg, null, MessageBoxButtons.OKCancel) != DialogResult.OK)
                return;

            PerformNumberedCut(sourceFiles, destinationFolder, startingIndex);
            MessageBox.Show($"Copied {sourceFiles.Length} from \"{sourceFolder}\" to \"{destinationFolder}\"");
        }

        int GetMaxIndex(string directoryPath)
        {
            int maxIndex = 0;

            var files = Directory.GetFiles(directoryPath);
            foreach (var file in files)
            {
                var filename = Path.GetFileNameWithoutExtension(file);
                if (int.TryParse(filename, out int number))
                {
                    if (maxIndex < number)
                        maxIndex = number;
                }
            }

            return maxIndex;
        }

        void PerformNumberedCut(string[] filesToCut, string destinationPath, int startIndex)
        {
            foreach(var filePath in filesToCut)
            {
                var ext = Path.GetExtension(filePath);
                File.Copy(filePath, Path.Combine(destinationPath, $"{startIndex}{ext}"));
                File.Delete(filePath);
                startIndex++;
            }
        }

        bool PromtFolderSelection(string title, string startingFolder, out string selectedFolder)
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog
            {
                Title = title,
                IsFolderPicker = true,
                InitialDirectory = startingFolder
            };

            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                selectedFolder = dialog.FileName;
                return true;
            }
            else
            {
                selectedFolder = string.Empty;
                return false;
            }
        }
    }
}
