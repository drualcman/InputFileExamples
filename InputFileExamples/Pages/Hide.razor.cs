using BlazorBasics.InputFileExtended;
using BlazorBasics.InputFileExtended.Models;
using BlazorBasics.InputFileExtended.ValueObjects;
using Microsoft.AspNetCore.Components.Web;

namespace InputFileExamples.Pages;
public partial class Hide
{
    InputFileComponent InputFileComponentReference;
    bool ShowInputFile = false;
    private string TextBF;

    public string Text
    {
        get { return TextBF; }
        set
        {
            TextBF = value;
            ShowInputFile = Text?.Contains('i', StringComparison.InvariantCultureIgnoreCase) ?? false;
            if (ShowInputFile)
            {
                Task.Run(InputFileComponentReference.OpenFileDialog);
            }
        }
    }
    string Messages;

    InputFileParameters Parameters;

    protected override void OnInitialized()
    {
        Parameters = new InputFileParameters
        {
            AllowPasteFiles = true,
            DragAndDropOptions = new DragAndDropOptions
            {
                CanDropFiles = true
            },
            OnShouldCancelClick = OnCancelClick
        };
    }

    void KeyUp(KeyboardEventArgs e)
    {
        Text = e.Key;
    }

    void AddFile(FilesUploadEventArgs selection)
    {
        Messages = $"Action {selection.Action}, File/s: {string.Join(',', selection.Files.Select(n => n.Name))}";
    }

    bool CancelClick = false;

    Task<bool> OnCancelClick()
    {
        return Task.FromResult(CancelClick);
    }
}