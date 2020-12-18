namespace xperters.configurations.Interfaces
{
    public interface IHandleEnvironment
    {
        string GetVariable(string variableName);

        void SetVariable(string variableName, string value);
    }
}
