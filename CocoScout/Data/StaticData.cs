using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CocoScout.Data
{
    public class StaticData
    {
        /// <summary>
        /// List of data for the matches.
        /// Collected during a match.
        /// </summary>
        public ObservableCollection<MatchData> MatchDataList = new ObservableCollection<MatchData>();
        /// <summary>
        /// List of data for pits scouting.
        /// Can be collected during final days or in the pits.
        /// </summary>
        public ObservableCollection<TeamData>  TeamDataList  = new ObservableCollection<TeamData>();

        /// <summary>
        /// List of regionals that are loaded in regionals.txt in the app folder.
        /// </summary>
        public string[] EventList;

        /// <summary>
        /// Instance of settings.
        /// </summary>
        public Settings Settings;
    }

    public class StaticDataViewModel
    {
        public static StaticData DataList = new StaticData();
    }
}
