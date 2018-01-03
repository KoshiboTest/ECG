﻿using Emergence.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emergence.ViewModel
{
    public class WeaponVM : INotifyPropertyChanged
    {
        public Weapon model;

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
        public int Accuracy
        {
            get
            {
                return model.Accuracy;
            }
            set
            {
                model.Accuracy = value;
                NotifyPropertyChanged("Accuracy");
            }
        }
        public int Damage
        {
            get
            {
                return model.Damage;
            }
            set
            {
                model.Damage = value;
                NotifyPropertyChanged("Damage");
            }
        }
        public int Size
        {
            get
            {
                return model.Size;
            }
            set
            {
                model.Size = value;
                NotifyPropertyChanged("Size");
            }
        }
        public DamageType Type
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
        public WeaponSkill Skill
        {
            get
            {
                return model.Skill;
            }
            set
            {
                model.Skill = value;
                NotifyPropertyChanged("Skill");
            }
        }
        public WeaponProperty Properties
        {
            get
            {
                return (ViewModel.WeaponProperty)model.Properties;
            }
            set
            {
                model.Properties = (Model.WeaponProperty)value;
                NotifyPropertyChanged("Properties");
            }
        }
        public WeaponQuality Quality
        {
            get
            {
                return (ViewModel.WeaponQuality)model.Quality;
            }
            set
            {
                model.Quality = (Model.WeaponQuality)value;
                NotifyPropertyChanged("Quality");
            }
        }
        public ObservableCollection<WeaponModVM> Mods
        {
            get
            {
                ObservableCollection<WeaponModVM> modelMods = new ObservableCollection<WeaponModVM>();
                foreach (var mod in model.Mods)
                {
                    modelMods.Add(new WeaponModVM(mod));
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

        public int CM
        {
            get
            {
                return model.CM;
            }
            set
            {
                model.CM = value;
                NotifyPropertyChanged("CM");
            }
        }

        public WeaponVM(Weapon model)
        {
            this.model = model;
            Mods.CollectionChanged += Mods_CollectionChanged;
        }

        public WeaponVM()
        {
            this.model = new Weapon();
            Mods.CollectionChanged += Mods_CollectionChanged;
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

        private void Mods_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
                    foreach (WeaponModVM addItem in e.NewItems)
                        model.Mods.Add(addItem.model);
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Move:
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Remove:
                    foreach (WeaponModVM removeItem in e.OldItems)
                        model.Mods.Remove(removeItem.model);
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Replace:
                    foreach (WeaponModVM addItem in e.NewItems)
                        model.Mods.Add(addItem.model);
                    foreach (WeaponModVM removeItem in e.OldItems)
                        model.Mods.Remove(removeItem.model);
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Reset:
                    model.Mods.Clear();
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }

    public class RangedWeaponVM : WeaponVM
    {
        public int AmmoCapacity
        {
            get
            {
                return (model as RangedWeapon).AmmoCapacity;
            }
            set
            {
                (model as RangedWeapon).AmmoCapacity = value;
                NotifyPropertyChanged("AmmoCapacity");
            }
        }
        public Range RangeType
        {
            get
            {
                return ((RangedWeapon)model).RangeType;
            }
            set
            {
                ((RangedWeapon)model).RangeType = value;
                NotifyPropertyChanged("RangeType");
            }
        }

        public RangedWeaponVM(RangedWeapon model) : base(model)
        {
        }

        public RangedWeaponVM()
        {
            this.model = new RangedWeapon();
        }
    }

    public class NaturalWeaponVM : WeaponVM
    {
        public NaturalWeaponVM(NaturalWeapon model)
        {
            this.model = model;
        }

        public NaturalWeaponVM(NaturalWeaponClass c)
        {
            this.model = new NaturalWeapon(c);
        }
    }
}
