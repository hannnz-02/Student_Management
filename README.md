# Student Management System (WPF / C# - MVVM Architecture)

- **Nama:** Hidayah Nur Septiani
- **NRP:** 5025241247
- **Kelas:** PBKK C

---

## Dokumentasi Tugas

Video dokumentasi aplikasi:

[Student Management System — Demo Aplikasi](https://github.com/user-attachments/assets/7bfd430c-3e09-4cfc-b553-a4046f949944)

## Dokumentasi Tampilan Aplikasi

### 1. Form Input Mahasiswa

Tampilan awal aplikasi untuk memasukkan data mahasiswa, meliputi nama, NRP, jurusan, dan email.

<p align="center">
  <img src="https://github.com/user-attachments/assets/30cd4797-9d4b-49fe-a1c7-2371774b7d7f" alt="Form Input Mahasiswa" width="650"/>
</p>

### 2. Berhasil Menambahkan Data

Tampilan aplikasi setelah data mahasiswa berhasil ditambahkan dan ditampilkan pada tabel.

<p align="center">
  <img src="https://github.com/user-attachments/assets/e446c8fa-249d-458f-a3a0-925d25cbc21b" alt="Data Mahasiswa Berhasil Ditambahkan" width="650"/>
</p>

---

## Struktur Proyek

```text
Student_Management/
├── Data/
│   └── StudentRepository.cs
├── Models/
│   └── Student.cs
├── ViewModels/
│   ├── RelayCommand.cs
│   └── StudentViewModel.cs
├── App.xaml
├── App.xaml.cs
├── MainWindow.xaml
└── MainWindow.xaml.cs
```

---

## Penjelasan File

### 1. `MainWindow.xaml`

Berfungsi untuk membuat tampilan antarmuka pengguna (UI) menggunakan XAML. File ini mengatur tata letak form input mahasiswa, tombol aksi, kartu statistik, kolom pencarian, dan tabel.

### 2. `MainWindow.xaml.cs`

Berfungsi sebagai file pendamping dari MainWindow.xaml.

### 3. `App.xaml`

Berfungsi untuk mengatur konfigurasi dasar aplikasi WPF, termasuk menentukan jendela awal melalui properti StartupUri.

### 4. `App.xaml.cs`

Berfungsi sebagai file pendamping App.xaml yang mengatur siklus hidup aplikasi. File ini dapat digunakan untuk menangani kejadian saat aplikasi dimulai, ditutup, atau ketika diperlukan inisialisasi pada tingkat aplikasi.

### 5. `Models/Student.cs`

Berfungsi untuk mendefinisikan struktur data mahasiswa dalam bentuk class Student. Class ini merepresentasikan data mahasiswa, seperti Id, Nama, NRP, Jurusan, dan Email. Objek Student digunakan untuk membawa data antara ViewModel dan Repository.

### 6. `ViewModels/StudentViewModel.cs`

Berfungsi sebagai penghubung antara tampilan (View) dan pengelolaan data dalam arsitektur MVVM. 

### 7. `ViewModels/RelayCommand.cs`

Berfungsi untuk mengimplementasikan interface ICommand sehingga aksi atau metode dalam StudentViewModel dapat dihubungkan dengan tombol melalui Command Binding.

### 8. `Data/StudentRepository.cs`

Berfungsi untuk mengelola komunikasi antara aplikasi dan database SQL Server LocalDB. 
---

## Arsitektur MVVM

Aplikasi ini menerapkan pola Model-View-ViewModel (MVVM) untuk memisahkan tanggung jawab setiap bagian aplikasi.

- **Model:** Mendefinisikan struktur data mahasiswa melalui Student.cs.
- **View:** Menampilkan antarmuka pengguna melalui MainWindow.xaml.
- **ViewModel:** Mengatur properti, perintah, dan interaksi antara View dengan Repository melalui StudentViewModel.cs.
- **Repository:** Mengelola akses dan operasi data pada database melalui StudentRepository.cs.
- **RelayCommand:** Membantu menghubungkan aksi pada ViewModel dengan tombol di View.

Dengan menerapkan arsitektur MVVM, kode aplikasi menjadi lebih terstruktur, mudah dipahami, dan lebih mudah dikembangkan maupun dipelihara.
