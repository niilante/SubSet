using System;
using System.Collections.Generic;
using System.Text;

namespace SubSet.Models
{
    public class Setting
    {
        public bool SearchSubInSubdir { get; set; }
        public bool SearchMovieInSubdir { get; set; }
        public Setting(bool searchSubInSubdir = true, bool searchMovieInSubdir = false)
        {
            SearchMovieInSubdir = searchMovieInSubdir;
            SearchSubInSubdir = searchSubInSubdir;
        }
    }
}
