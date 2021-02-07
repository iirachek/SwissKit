using MvvmExtensions.Commands;
using System.Windows.Input;

namespace SwissKit.Subprograms
{
    internal abstract class SubprogramBase
    {
        public string Name { get; }

        public string Description { get; }

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

        public SubprogramBase(string name, string description)
        {
            Name = name;
            Description = description;
        }

        protected abstract void LaunchSubprogram();
    }
}
