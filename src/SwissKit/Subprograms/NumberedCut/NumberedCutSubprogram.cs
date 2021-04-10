using System.IO;
using System.Windows.Forms;

namespace SwissKit.Subprograms.NumberedCut
{
    internal sealed class NumberedCutSubprogram : ISubprogram
    {
        // Todo: Clean up and remove copy-paste
        public void Run()
        {
            string sourceFolder; 
            string destinationFolder;

            FolderBrowserDialog folderBrowserDialogSource = new FolderBrowserDialog();
            folderBrowserDialogSource.Description = "Source folder to cut from";
            if (folderBrowserDialogSource.ShowDialog() == DialogResult.OK)
                sourceFolder = folderBrowserDialogSource.SelectedPath;
            else
                return;

            FolderBrowserDialog folderBrowserDialogDestination = new FolderBrowserDialog();
            folderBrowserDialogDestination.Description = "Destination folder to paste into";
            if (folderBrowserDialogDestination.ShowDialog() == DialogResult.OK)
                destinationFolder = folderBrowserDialogDestination.SelectedPath;
            else
                return;

            var sourceFiles = Directory.GetFiles(sourceFolder);

            var startingIndex = GetMaxIndex(destinationFolder) + 1;

            DoNumberedCut(sourceFiles, destinationFolder, startingIndex);

            MessageBox.Show($"Copied {sourceFolder.Length} from \"{sourceFolder}\" to \"{destinationFolder}\"");
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

        void DoNumberedCut(string[] filesToCut, string destinationPath, int startIndex)
        {
            foreach(var filePath in filesToCut)
            {
                var ext = Path.GetExtension(filePath);
                File.Copy(filePath, Path.Combine(destinationPath, $"{startIndex}{ext}"));
                File.Delete(filePath);
                startIndex++;
            }
        }
    }
}
