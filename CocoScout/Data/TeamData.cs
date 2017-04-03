using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CocoScout.Data
{
    class TeamData
    {
        /// <summary>
        /// Team number.
        /// </summary>
        public uint TeamNumber { get; set; }

        /// <summary>
        /// String for any notes to be taken on.
        /// Default the string to being null.
        /// </summary>
        public string Notes { get; set; } = null;
    }
}
