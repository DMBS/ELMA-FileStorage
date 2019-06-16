using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyTaskApplication.Models
{
    public class Documents
    {
        public virtual Int32 DocumentID { get; set; }
        public virtual string DocumentName { get; set; }
        public virtual string DocumentType { get; set; }
        public virtual DateTime DocumentDate { get; set; }
        public virtual string DocumentAuthor { get; set; }
        public virtual Byte[] DocumentBinaryFile { get; set; }
    }

    //define ellipsis class
    public static class StringEllipsis
    {
        public static string TruncateWithStringEllipsis (string s, int length)
        {
            const string Ellipsis = "...";

            if (s.Length > length)
                return s.Substring(0, length) + Ellipsis;
            else
                return s;
        }
    }
}