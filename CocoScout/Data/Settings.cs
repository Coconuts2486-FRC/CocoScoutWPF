using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CocoScout.Data
{
    public class Settings : INotifyPropertyChanged
    {
        /// <summary>
        /// Name of the person entering the data.
        /// </summary>
        private string _UserName;

        public event PropertyChangedEventHandler PropertyChanged;

        public string UserName
        {
            get
            {
                return _UserName;
            }
            set
            {
                _UserName = value;
                OnPropertyChanged("Name");
            }
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Name of the event.
        /// </summary>
        public string Event { get; set; } = "";
    }
}
