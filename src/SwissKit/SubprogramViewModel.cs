using MvvmExtensions.Commands;
using SwissKit.Subprograms;
using System;
using System.Windows;
using System.Windows.Input;

namespace SwissKit
{
    internal sealed class SubprogramViewModel
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public ISubprogram Subprogram { get; set; }

        public ICommand LaunchSubprogramCommand
        {
            get
            {
                return new DelegateCommand
                {
                    CommandAction = (x) =>
                    {
                        LaunchSubprogram();
                    }
                };
            }
        }

        private void LaunchSubprogram()
        {
            try
            {
                Subprogram?.Run();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
