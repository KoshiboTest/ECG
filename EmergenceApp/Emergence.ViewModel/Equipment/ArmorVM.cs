using Emergence.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Specialized;

namespace Emergence.ViewModel
{
    public class ArmorVM : INotifyPropertyChanged
    {
        public Armor model;

        public string Name
        {
            get
            {
                return model.Name;
            }
            set
            {
                model.Name = value;
                NotifyPropertyChanged("Name");
            }
        }

        public string Class
        {
            get
            {
                return model.Class;
            }
            set
            {
                model.Class = value;
                NotifyPropertyChanged("Class");
            }
        }

        public string Type
        {
            get
            {
                return model.Type;
            }
            set
            {
                model.Type = value;
                NotifyPropertyChanged("Type");
            }
        }

        public int ArmorValue
        {
            get
            {
                return model.ArmorValue;
            }
            set
            {
                model.ArmorValue = value;
                NotifyPropertyChanged("ArmorValue");
            }
        }

        public int ArmorPenalty
        {
            get
            {
                return model.ArmorPenalty;
            }
            set
            {
                model.ArmorPenalty = value;
                NotifyPropertyChanged("ArmorPenalty");
            }
        }

        public int SpeedPenalty
        {
            get
            {
                return model.SpeedPenalty;
            }
            set
            {
                model.SpeedPenalty = value;
                NotifyPropertyChanged("SpeedPenalty");
            }
        }

        public ArmorProperty Properties
        {
            get
            {
                return model.Properties;
            }
            set
            {
                model.Properties = value;
                NotifyPropertyChanged("Properties");
            } 
        }

        public ItemQuality Quality
        {
            get
            {
                return model.Quality;
            }
            set
            {
                model.Quality = value;
                NotifyPropertyChanged("Quality");
            }
        }

        public int Cost
        {
            get
            {
                return model.Cost;
            }
            set
            {
                model.Cost = value;
                NotifyPropertyChanged("Cost");
            }
        }

        public ObservableCollection<ArmorModVM> Mods
        {
            get
            {
                ObservableCollection<ArmorModVM> modelMods = new ObservableCollection<ArmorModVM>();
                foreach (var mod in model.Mods)
                {
                    modelMods.Add(new ArmorModVM(mod));
                }
                modelMods.CollectionChanged += Mods_CollectionChanged;
                return modelMods;
            }
            set
            {
                foreach (var mod in value)
                {
                    model.Mods.Clear();
                    model.Mods.Add(mod.model); // keep model's mod collection in sync
                }
                NotifyPropertyChanged("Mods");
            }
        }

        private void Mods_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
                    foreach (ArmorModVM addItem in e.NewItems)
                        model.Mods.Add(addItem.model);
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Move:
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Remove:
                    foreach (ArmorModVM removeItem in e.OldItems)
                        model.Mods.Remove(removeItem.model);
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Replace:
                    foreach (ArmorModVM addItem in e.NewItems)
                        model.Mods.Add(addItem.model);
                    foreach (ArmorModVM removeItem in e.OldItems)
                        model.Mods.Remove(removeItem.model);
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Reset:
                    model.Mods.Clear();
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        public ArmorVM()
        {
            model = new Armor();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public void IncreaseQuality()
        {
            if ((int)model.Quality < 4)
            {
                model.Quality++;
                NotifyPropertyChanged("Quality");
                NotifyPropertyChanged("Cost");
            }
        }

        public void DecreaseQuality()
        {
            if ((int)model.Quality > 0)
            {
                model.Quality--;
                NotifyPropertyChanged("Quality");
                NotifyPropertyChanged("Cost");
            }
        }
    }

    public class NaturalArmorVM : ArmorVM
    {
        public NaturalArmorVM(NaturalArmorClass c)
        {
            model = new NaturalArmor(c);
        }
    }
}
