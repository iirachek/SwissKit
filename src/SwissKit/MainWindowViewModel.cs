using MvvmExtensions.PropertyChangedMonitoring;
using SwissKit.Subprograms.AddToStart;
using System.Collections.Generic;

namespace SwissKit
{
    internal class MainWindowViewModel : PropertyChangedImplementation
    {
        public List<object> Subprograms { get; } = new List<object>();

        public MainWindowViewModel()
        {
            Subprograms.Add(new AddToStartSubprogram());
        }
    }
}
