using System;
using AutoMapper;
using xperters.domain;
using xperters.entities;
using xperters.entities.Entities;

namespace xperters.repositories
{
    public class JobsRepositoryEf : JobsRepository
    {
        private readonly XpertersContext _context;
        private readonly IMapper _mapper;

        public JobsRepositoryEf(XpertersContext context, IMapper mapper ):base(context)
        {
            _context = context;
            _mapper = mapper;
        }
        public override Job Get(Guid jobId)
        {
            var item = _context.Jobs.Find(jobId);
           
            return item;
        }
    }
}