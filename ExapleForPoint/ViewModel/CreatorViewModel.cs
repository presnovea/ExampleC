using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

using ExapleForPoint.Modelling;

namespace ExapleForPoint.ViewModel
{
    class CreatorViewModel: INotifyPropertyChanged
    {
        private DbParams dbParams;
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public CreatorViewModel()
        {
            dbParams = SetDbPrmDefault();

        }

        internal DbParams SetDbPrmDefault()
        {
            dbParams = new DbParams();
            dbParams.DataSource = "127.0.0.1";
            dbParams.Port = "49172";
            dbParams.DbName = "PointDb";
            dbParams.UserID = "sa";
            dbParams.Password = "default";
            return dbParams;
        }

        public DbParams DBaseParams
        {
            get { return dbParams; }
            set { dbParams = value;
                OnPropertyChanged("DBaseParams");
            }
        }

    }
}
