namespace Xperters.Core.UI
{
    public interface IMessageBoxService
    {
        void ShowInformation(string message);
        void ShowInformation(string message, string title);

        void ShowWarning(string message);
        void ShowWarning(string message, string title);

        void ShowError(string message);
        void ShowError(string message, string title);

        MessageBoxAnswer ShowOkCancel(string message, string title, MessageBoxKind kind, MessageBoxAnswer defaultAnswer);
        MessageBoxAnswer ShowOkCancel(string message, string title, MessageBoxKind kind);
        MessageBoxAnswer ShowOkCancel(string message, string title);
        MessageBoxAnswer ShowOkCancel(string message);

        MessageBoxAnswer ShowYesNo(string message, string title, MessageBoxKind kind, MessageBoxAnswer defaultAnswer);
        MessageBoxAnswer ShowYesNo(string message, string title, MessageBoxKind kind);
        MessageBoxAnswer ShowYesNo(string message, string title);
        MessageBoxAnswer ShowYesNo(string message);

        MessageBoxAnswer ShowYesNoCancel(string message, string title, MessageBoxKind kind, MessageBoxAnswer defaultAnswer);
        MessageBoxAnswer ShowYesNoCancel(string message, string title, MessageBoxKind kind);
        MessageBoxAnswer ShowYesNoCancel(string message, string title);
        MessageBoxAnswer ShowYesNoCancel(string message);

        MessageBoxAnswer Show(string message, string title, MessageBoxChoices choices, MessageBoxKind kind, MessageBoxAnswer defaultAnswer);
        MessageBoxAnswer Show(string message, string title, MessageBoxChoices choices, MessageBoxKind kind);
        MessageBoxAnswer Show(string message, string title, MessageBoxChoices choices);
        MessageBoxAnswer Show(string message, string title);
        MessageBoxAnswer Show(string message);
    }

    public enum MessageBoxKind
    {
        None = 0,
        Error = 16,
        Question = 32,
        Warning = 48,
        Information = 64,
    }

    public enum MessageBoxChoices
    {
        Ok = 0,
        OkCancel = 1,
        YesNoCancel = 3,
        YesNo = 4,
    }

    public enum MessageBoxAnswer
    {
        None = 0,
        Ok = 1,
        Cancel = 2,
        Yes = 6,
        No = 7,
    }
}
