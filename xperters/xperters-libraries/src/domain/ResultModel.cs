using System;
using System.Collections.Generic;

namespace xperters.domain
{
    public class ResultModel<T> : ResultModel
    {
        public List<T> DataList { get; set; }
        public T Data { get; set; }
    }
    public class ResultModel
    {
        public int TotalCount { get; set; }
        public bool Error { get; set; }
        public string Message { get; set; }
        public Guid Id { get; set; }
    }
}
