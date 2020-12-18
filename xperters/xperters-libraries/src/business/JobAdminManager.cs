using AutoMapper;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using xperters.business.Interfaces;
using xperters.domain;
using xperters.models.DataViews.AdminJob;
using xperters.repositories;

namespace xperters.business
{
    public class JobAdminManager : IJobAdminManager
    {
        private readonly IMapper _mapper;
        private readonly ILoggerFactory _loggerFactory;
        private readonly IRepositoryReadOnly<JobInformationView> _jobInformationRepository;
        private ILogger<JobAdminManager> _logger;

        public JobAdminManager(IMapper mapper, ILoggerFactory loggerFactory, IRepositoryReadOnly<JobInformationView> jobInformationRepository)
        {
            _mapper = mapper;
            _loggerFactory = loggerFactory;
            _jobInformationRepository = jobInformationRepository;
            _logger = _loggerFactory.CreateLogger<JobAdminManager>();
        }

        public IEnumerable<JobInformationDto> GetJobInformation(int page, int pageSize)
        {

            var result = _jobInformationRepository.Get(page, pageSize);
            var jobInfoSortedAndPaged = _mapper.Map<IEnumerable<JobInformationDto>>(result.AsEnumerable());
            _logger.LogDebug("Retrieved job admin information");

            return jobInfoSortedAndPaged;
        }

        public IEnumerable<JobInformationDto> GetFilteredJobInformation(string jobTitle, string createdDate)
        {
            DateTime? date = null;
            if(!string.IsNullOrEmpty(createdDate))
                date = Convert.ToDateTime(createdDate);

            var result = _jobInformationRepository.Search(jobTitle, date);
            var filteredJobInfo = _mapper.Map<IEnumerable<JobInformationDto>>(result.AsEnumerable());
            return filteredJobInfo;
        }
    }
}
