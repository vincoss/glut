﻿using System;
using System.ComponentModel.DataAnnotations;

namespace Glut.Data
{
    public class GlutProject
    {
        [Key]
        public string GlutProjectName { get; set; }
        public DateTime CreatedDateTimeUtc { get; set; }
        public DateTime ModifiedDateTimeUtc { get; set; }

        public string CreatedByUserName { get; set; }

        public override string ToString()
        {
            if (string.IsNullOrWhiteSpace(GlutProjectName))
            {
                return base.ToString();
            }
            return GlutProjectName;
        }
    }
}