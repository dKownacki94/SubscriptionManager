using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SubscriptionManager.SolutionAnalyzer;

public class TreeNode : INotifyPropertyChanged
{
    private bool _isSelected;

    public string Name { get; set; }
    public string FullPath { get; set; } = string.Empty;
    public TreeNode Parent { get; set; }
    public List<TreeNode> Children { get; } = new List<TreeNode>();

    public bool IsSelected
    {
        get => _isSelected;
        set
        {
            if (_isSelected != value)
            {
                _isSelected = value;
                OnPropertyChanged();
            }
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
