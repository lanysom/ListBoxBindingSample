using ListBoxBindingSample.Helpers;
using ListBoxBindingSample.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ListBoxBindingSample
{
    public class PersonViewModel : INotifyPropertyChanged
    {
        private Person _selectedPerson;

        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        } 
        #endregion

        public RelayCommand AddPersonCommand { get; set; }

        public RelayCommand DeletePersonCommand { get; set; }

        public Person SelectedPerson
        {
            get => _selectedPerson;
            set
            {
                _selectedPerson = value;
                OnPropertyChanged("SelectedPerson");
                DeletePersonCommand.RaiseCanExecuteChanged();
            }
        }

        public ObservableCollection<Person> Persons { get; set; }

        public PersonViewModel()
        {
            Persons = new ObservableCollection<Person>();
            AddPersonCommand = new RelayCommand(AddPerson);
            DeletePersonCommand = new RelayCommand(DeletePerson, () => { return SelectedPerson != null; });
        }

        private void AddPerson()
        {
            Persons.Add(new Person());
        }

        private void DeletePerson()
        {
            Persons.Remove(SelectedPerson);
        }
    }
}
