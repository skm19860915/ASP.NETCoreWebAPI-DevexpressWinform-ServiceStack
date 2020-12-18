using Xperters.Admin.UI.Common.GridDefinition;

namespace Xperters.Admin.UI.Common.LayerProgram
{
    public sealed class ProgramGridDefinitionBuilder : GridDefinitionBuilder<PaymentGridViewModel>
    {
        public ProgramGridDefinitionBuilder() : base(o => o, Constants.ColumnDefinitionContexts.Tac)
        {

            //TODO: add comment this.WithColumn(o => o.Program., "Program Level Commentary.");
        }
    }
}
