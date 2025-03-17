using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace SubscriptionManager.SolutionAnalyzer;

public partial class MainWindow : Window
{
    private readonly string solutionPath;
    private readonly string outputPath;

    public MainWindow()
    {
        InitializeComponent();

        string currentDir = Directory.GetCurrentDirectory();
        solutionPath = Path.GetFullPath(Path.Combine(currentDir, @"..\..\..\..")); // W górę do SubscriptionManager
        string resultsDir = Path.Combine(solutionPath, "SubscriptionManager.SolutionAnalyzer", "Results");
        outputPath = Path.Combine(resultsDir, "output.txt");

        Directory.CreateDirectory(resultsDir);

        LoadFileTree();
    }

    private void LoadFileTree()
    {
        string[] extensions = { ".cs",".xaml" };
        var files = GetSolutionFiles(solutionPath, extensions);
        var rootNodes = BuildTree(files);
        FileTree.ItemsSource = rootNodes;
    }

    private List<string> GetSolutionFiles(string solutionPath, string[] extensions)
    {
        return Directory.GetFiles(solutionPath, "*.*", SearchOption.AllDirectories)
            .Where(file => extensions.Contains(Path.GetExtension(file)) && !file.Contains("bin") && !file.Contains("obj"))
            .Select(file => Path.GetRelativePath(solutionPath, file))
            .ToList();
    }

    private List<TreeNode> BuildTree(List<string> files)
    {
        var rootNodes = new List<TreeNode>();
        var folderDict = new Dictionary<string, TreeNode>();

        foreach (var file in files)
        {
            var parts = file.Split(Path.DirectorySeparatorChar);
            TreeNode currentNode = null;

            for (int i = 0; i < parts.Length - 1; i++)
            {
                var folderPath = string.Join(Path.DirectorySeparatorChar, parts.Take(i + 1));
                if (!folderDict.TryGetValue(folderPath, out currentNode))
                {
                    currentNode = new TreeNode { Name = parts[i] };
                    folderDict[folderPath] = currentNode;

                    if (i == 0)
                        rootNodes.Add(currentNode);
                    else
                    {
                        var parentPath = string.Join(Path.DirectorySeparatorChar, parts.Take(i));
                        folderDict[parentPath].Children.Add(currentNode);
                        currentNode.Parent = folderDict[parentPath];
                    }
                }
            }

            var fileNode = new TreeNode { Name = parts.Last(), FullPath = file };
            if (currentNode != null)
            {
                currentNode.Children.Add(fileNode);
                fileNode.Parent = currentNode; 
            }
        }

        return rootNodes;
    }

    private void CheckBox_Checked(object sender, RoutedEventArgs e)
    {
        if (sender is CheckBox checkBox && checkBox.DataContext is TreeNode node)
        {
            SetChildrenChecked(node, true);
            UpdateParentCheckState(node.Parent);
        }
    }

    private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
    {
        if (sender is CheckBox checkBox && checkBox.DataContext is TreeNode node)
        {
            SetChildrenChecked(node, false);
            UpdateParentCheckState(node.Parent);
        }
    }

    private void SetChildrenChecked(TreeNode node, bool isChecked)
    {
        foreach (var child in node.Children)
        {
            child.IsSelected = isChecked;
            SetChildrenChecked(child, isChecked);
        }
    }

    private void UpdateParentCheckState(TreeNode parent)
    {
        while (parent != null)
        {
            if (parent.Children.All(c => c.IsSelected))
                parent.IsSelected = true;
            else if (parent.Children.Any(c => c.IsSelected))
                parent.IsSelected = false;
            else
                parent.IsSelected = false;

            parent = parent.Parent;
        }
    }

    private void GenerateOutput_Click(object sender, RoutedEventArgs e)
    {
        var selectedFiles = GetSelectedFiles(FileTree.Items.Cast<TreeNode>());
        string outputContent = GenerateOutputFile(selectedFiles, outputPath);
        Clipboard.SetText(outputContent);
        MessageBox.Show($"Plik 'output.txt' został wygenerowany w:\n{outputPath}\n\nZawartość została skopiowana do schowka!",
            "Sukces", MessageBoxButton.OK, MessageBoxImage.Information);
    }

    private List<string> GetSelectedFiles(IEnumerable<TreeNode> nodes)
    {
        var selectedFiles = new List<string>();
        foreach (var node in nodes)
        {
            if (node.IsSelected && !string.IsNullOrEmpty(node.FullPath))
                selectedFiles.Add(node.FullPath);
            selectedFiles.AddRange(GetSelectedFiles(node.Children));
        }
        return selectedFiles;
    }

    private string GenerateOutputFile(List<string> files, string outputPath)
    {
        var sb = new StringBuilder();
        foreach (var file in files)
        {
            sb.AppendLine($"Plik: {file}");
            sb.AppendLine(File.ReadAllText(Path.Combine(solutionPath, file)));
            sb.AppendLine("---");
        }
        string outputContent = sb.ToString();
        File.WriteAllText(outputPath, outputContent);
        return outputContent;
    }
}