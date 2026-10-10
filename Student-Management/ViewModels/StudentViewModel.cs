using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using Student_Management.Data;
using Student_Management.Models;

namespace Student_Management.ViewModels
{
    public class StudentViewModel : INotifyPropertyChanged
    {
        private readonly StudentRepository _repository;

        private ObservableCollection<Student> _students = new ObservableCollection<Student>();
        public ObservableCollection<Student> Students
        {
            get => _students;
            set { _students = value; OnPropertyChanged(); }
        }

        private int _selectedId = -1;

        private string _nama = string.Empty;
        public string Nama
        {
            get => _nama;
            set { _nama = value; OnPropertyChanged(); }
        }

        private string _nrp = string.Empty;
        public string NRP
        {
            get => _nrp;
            set { _nrp = value; OnPropertyChanged(); }
        }

        private string? _jurusan;
        public string? Jurusan
        {
            get => _jurusan;
            set { _jurusan = value; OnPropertyChanged(); }
        }

        private string _email = string.Empty;
        public string Email
        {
            get => _email;
            set { _email = value; OnPropertyChanged(); }
        }

        private string _searchText = string.Empty;
        public string SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                OnPropertyChanged();
                LoadData(_searchText);
            }
        }

        private int _totalStudents;
        public int TotalStudents
        {
            get => _totalStudents;
            set { _totalStudents = value; OnPropertyChanged(); }
        }

        private int _countInformatika;
        public int CountInformatika
        {
            get => _countInformatika;
            set { _countInformatika = value; OnPropertyChanged(); }
        }

        private int _countRKA;
        public int CountRKA
        {
            get => _countRKA;
            set { _countRKA = value; OnPropertyChanged(); }
        }

        private int _countRPL;
        public int CountRPL
        {
            get => _countRPL;
            set { _countRPL = value; OnPropertyChanged(); }
        }

        private string _simpanButtonText = "Simpan";
        public string SimpanButtonText
        {
            get => _simpanButtonText;
            set { _simpanButtonText = value; OnPropertyChanged(); }
        }

        public ICommand SimpanCommand { get; }
        public ICommand HapusInputCommand { get; }
        public ICommand ResetCommand { get; }
        public ICommand EditGridCommand { get; }
        public ICommand DeleteGridCommand { get; }

        public StudentViewModel()
        {
            _repository = new StudentRepository();

            SimpanCommand = new RelayCommand(Simpan);
            HapusInputCommand = new RelayCommand(HapusInput);
            ResetCommand = new RelayCommand(Reset);
            EditGridCommand = new RelayCommand(EditGrid);
            DeleteGridCommand = new RelayCommand(DeleteGrid);

            LoadData();
            LoadStatistik();
        }

        public void LoadData(string keyword = "")
        {
            var list = _repository.GetAllStudents(keyword);
            Students = new ObservableCollection<Student>(list);
        }

        public void LoadStatistik()
        {
            TotalStudents = _repository.GetStudentCountByDepartment();
            CountInformatika = _repository.GetStudentCountByDepartment("Informatika");
            CountRKA = _repository.GetStudentCountByDepartment("RKA");
            CountRPL = _repository.GetStudentCountByDepartment("RPL");
        }

        private void Simpan(object? parameter)
        {
            if (string.IsNullOrWhiteSpace(Nama) || string.IsNullOrWhiteSpace(NRP))
            {
                MessageBox.Show("Nama dan NRP tidak boleh kosong!", "Peringatan", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            Student student = new Student
            {
                Id = _selectedId,
                Nama = Nama,
                NRP = NRP,
                Jurusan = Jurusan ?? string.Empty,
                Email = Email
            };

            if (_selectedId == -1)
            {
                _repository.AddStudent(student);
                MessageBox.Show("Data berhasil disimpan!", "Sukses", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                _repository.UpdateStudent(student);
                MessageBox.Show("Data berhasil diperbarui!", "Sukses", MessageBoxButton.OK, MessageBoxImage.Information);
            }

            LoadData(SearchText);
            LoadStatistik();
            Reset(null);
        }

        private void EditGrid(object? parameter)
        {
            if (parameter is Student student)
            {
                _selectedId = student.Id;
                Nama = student.Nama;
                NRP = student.NRP;
                Jurusan = student.Jurusan;
                Email = student.Email;
                SimpanButtonText = "Update";
            }
        }

        private void DeleteGrid(object? parameter)
        {
            if (parameter is Student student)
            {
                MessageBoxResult result = MessageBox.Show($"Apakah Anda yakin ingin menghapus '{student.Nama}'?", "Konfirmasi", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    _repository.DeleteStudent(student.Id);
                    MessageBox.Show("Data berhasil dihapus!", "Sukses", MessageBoxButton.OK, MessageBoxImage.Information);

                    LoadData(SearchText);
                    LoadStatistik();
                    Reset(null);
                }
            }
        }

        private void HapusInput(object? parameter)
        {
            Nama = string.Empty;
            NRP = string.Empty;
            Email = string.Empty;
            Jurusan = null;
        }

        private void Reset(object? parameter)
        {
            HapusInput(null);
            _selectedId = -1;
            SimpanButtonText = "Simpan";
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}