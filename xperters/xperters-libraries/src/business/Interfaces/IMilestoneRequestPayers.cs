using System;
using System.Collections.Generic;
using System.Text;
using xperters.domain;

namespace xperters.business.Interfaces
{
    public interface IMilestoneRequestPayers
    {
        ResultModel<MilestoneRequestPayersDto> SubmitMileStoneRequestPayers(MilestoneRequestPayersDto milestoneRequestPayersDto);
    }
}
