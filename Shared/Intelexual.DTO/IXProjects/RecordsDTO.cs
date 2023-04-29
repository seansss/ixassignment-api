using System;
using System.Net;

namespace Intelexual.DTO.IXProjects
{
	public class RecordsDTO<Type>
    {
        // public int Page { get; set; }
        // public int Size { get; set; }
        public int TotalPages { get; set; }
        public int TotalRecords { get; set; }
        // public int CurrentPage { get; set; }
        public List<Type>? records { get; set; }
    }

    public class Filter
    {
        public string id { get; set; }
        public string value { get; set; }
    }

    public class Sorting
    {
        public string id { get; set; }
        public bool desc { get; set; }
    }
}

