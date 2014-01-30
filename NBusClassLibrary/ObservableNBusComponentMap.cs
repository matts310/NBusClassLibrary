using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NBusClassLibrary
{
    public class ObservableNBusComponentMap<T,K> : Dictionary<T,K>, INotifyPropertyChanged where K: NBusComponent
    {
        
        public ObservableNBusComponentMap()
        {
            
        }

        public K this[T index]
        {
            get
            {
                return this[index];
            }
            set
            {
                if (this.ContainsKey(index))
                {
                    if (!this[index].Equals(value))
                    {
                        OnPropertyChanged(null);
                        this[index] = value;
                    }
                }
                else
                {
                    OnPropertyChanged(null);
                    this[index] = value;
                }
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
