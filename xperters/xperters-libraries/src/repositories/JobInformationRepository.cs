using AutoMapper;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using xperters.domain;
using xperters.entities;
using xperters.entities.Entities;
using xperters.extensions;
using xperters.models;
using xperters.models.DataViews.AdminJob;

namespace xperters.repositories
{
    public class JobInformationRepository : IRepositoryReadOnly<JobInformationView>
    {
        private readonly XpertersContext _context;
        private readonly IMapper _mapper;
        private Guid _comparingJobId;

        public JobInformationRepository(XpertersContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            _comparingJobId = Guid.Empty;
        }

        public JobInformationView Get(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<JobInformationView> Get(int page, int pageSize)
        {
            var jobs = _context.Jobs.OrderByDescending(d => d.CreatedDate)
                .Skip((page - 1) * pageSize).Take(pageSize);

            var list = GetJobInformationViewList(jobs);

            return list;
        }

        public IEnumerable<JobInformationView> Search(string title, DateTime? date)
        {
            IQueryable<Job> jobs = null;
            var allJobs = _context.Jobs;

            if (!string.IsNullOrEmpty(title))
            {
                jobs = allJobs.Where(j => j.JobTitle.Contains(title));
                if (date != null)
                    jobs = jobs.Where(j => DateTime.Equals(j.CreatedDate, date));
            }
            else
                jobs = allJobs.Where(j => DateTime.Equals(j.CreatedDate, date));

            var list = GetJobInformationViewList(jobs).OrderBy(x => x.JobTitle);

            return list;
        }

        private IEnumerable<JobInformationView> GetJobInformationViewList(IQueryable<Job> jobs)
        {
            var contracts = _context.JobContracts;
            var milestones = _context.Milestones;

            var users = _context.Users
                .Select(x => new
                {
                    x.Id,
                    x.DisplayName
                });

            var jobsListWithJoins = (from l in jobs
                                     from u in users.Where(u1 => u1.Id == l.UserId)
                                     from c in contracts.Where(a => a.JobId == l.Id).DefaultIfEmpty()
                                     from m in milestones.Where(b => b.ContractId == c.Id).DefaultIfEmpty()
                                     from f in users.Where(f1 => f1.Id == c.FreelancerId).DefaultIfEmpty()
                                     orderby l.CreatedDate descending
                                     select new { l, u, f, m }
                                    )
                                    .ToList();

            Debug.WriteLine(jobsListWithJoins.Count);
            var groupsWithDefaultValues = from x in jobsListWithJoins
                                          group x.l by new
                                          {
                                              JobId = x.l.Id,
                                              JobTitle = x.l.JobTitle.IsBlank() ? "No job title" : x.l.JobTitle,
                                              JobDescription = x.l.Description.IsBlank() ? "No job description" : x.l.Description,
                                              Created = x.l.CreatedDate,
                                              Owner = x.u == null || x.u.DisplayName.IsBlank() ? "No job title" : x.u.DisplayName,
                                              Status = x.l.JobStatus == null || x.l.JobStatus.Status.IsBlank() ? "No job title" : x.l.JobStatus.Status,
                                              Freelancer = x.f == null || x.f.DisplayName.IsBlank() ? "No freelancer" : x.f.DisplayName,
                                              ActiveDate = DateTime.Now,
                                          };


            var infoSortedAndPaged = from jobInfo in groupsWithDefaultValues
                                     select new JobInformationView
                                     {
                                         JobId = jobInfo.Key.JobId,
                                         JobTitle = jobInfo.Key.JobTitle,
                                         JobDescription = jobInfo.Key.JobDescription,
                                         Created = jobInfo.Key.Created,
                                         Owner = jobInfo.Key.Owner,
                                         Status = jobInfo.Key.Status,
                                         Freelancer = jobInfo.Key.Freelancer,
                                         ActiveDate = jobInfo.Key.ActiveDate,
                                         NumberOfMilestones = _context.Milestones.Where(x => x.Contract.JobId == jobInfo.Key.JobId).Count(),
                                         MilestoneDetails = GetMap(_context.Milestones.Where(x => x.Contract.JobId == jobInfo.Key.JobId).AsEnumerable())
                                     };

            return infoSortedAndPaged.GroupBy(x => x.JobId).Select(x => x.FirstOrDefault()).ToList();
        }

        private IEnumerable<MilestoneView> GetMap(IEnumerable<Milestone> milestones)
        {
            var milestoneViews = new List<MilestoneView>();
            foreach (var milestone in milestones)
            {
                var mapDto = _mapper.Map<MilestoneDto>(milestone);
                var mapView = _mapper.Map<MilestoneView>(mapDto);
                milestoneViews.Add(mapView);
            }

            return milestoneViews;
        }

        public IQueryable<JobInformationView> Get(Expression<Func<JobInformationView, bool>> whereCondition)
        {
            throw new NotImplementedException();
        }

        public bool Exists(Expression<Func<JobInformationView, bool>> whereCondition)
        {
            throw new NotImplementedException();
        }
    }
}
