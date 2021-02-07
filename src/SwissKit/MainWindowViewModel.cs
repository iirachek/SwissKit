using MvvmExtensions.PropertyChangedMonitoring;
using SwissKit.Subprograms.AddToStart;
using System.Collections.Generic;

namespace SwissKit
{
    internal class MainWindowViewModel : PropertyChangedImplementation
    {
        public List<SubprogramViewModel> Subprograms { get; } = new List<SubprogramViewModel>();

        public MainWindowViewModel()
        {
            Subprograms.Add(new SubprogramViewModel
            {
                Title = "Add to Start",
                Description = "Creates application shortcut in Start folder, making it discoverable through search bar",
                Subprogram = new AddToStartSubprogram(),
            });
        }
    }
}
