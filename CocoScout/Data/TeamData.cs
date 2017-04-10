using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CocoScout.Data
{
    public class TeamData
    {
        /// <summary>
        /// Team number.
        /// </summary>
        public uint TeamNumber { get; set; }

        /// <summary>
        /// String for any notes to be taken on.
        /// Default the string to being null.
        /// </summary>
        public string Notes { get; set; } = String.Empty;

        public string Event
        {
            get
            {
                return StaticData.Settings.Event;
            }
        }

        public string User
        {
            get
            {
                return StaticData.Settings.UserName;
            }
        }

        public void ResetAllFields()
        {
            TeamNumber = 0;
            Notes = "";
        }

        public override string ToString()
        {
            return
                "Team Number: " + TeamNumber +
                "\nUser:      " + User +
                "\nEvent:     " + Event +
                "\nNotes:     " + Notes;
        }
    }
}
