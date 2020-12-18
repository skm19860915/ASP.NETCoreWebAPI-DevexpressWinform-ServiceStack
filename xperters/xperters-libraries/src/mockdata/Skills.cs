using System;
using System.Collections.Generic;
using System.Text;
using xperters.domain;

namespace xperters.mockdata
{
    public static class Skills
    {
        public static SkillDto SkillFirst { get; }
        public static SkillDto SkillSecond { get; }
        public static SkillDto SkillThird { get; }
        static Skills()
        {
            SkillFirst = new SkillDto
            {
                SkillName = "Skill one",
                IsActive=true
            };

            SkillSecond = new SkillDto
            {
                SkillName = "Skill Two",
                IsActive = true

            };
            SkillThird = new SkillDto
            {
                SkillName = "Skill Three",
                IsActive = true
            };
        }

        public static List<SkillDto> Get()
        {
            var list = new List<SkillDto> {
                SkillFirst,
                SkillSecond,
                SkillThird

            };
            return list;
        }
    }
}
