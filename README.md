# 🔒 FileGuard - Duplicate File Detection System
An intelligent file management system with automatic duplicate detection using SHA-256 hashing. Even if you rename a file, the system knows it's a duplicate!

<p align="center">
  <img src="https://img.shields.io/badge/.NET-10.0-512BD4?style=for-the-badge&logo=dotnet&logoColor=white" />
  <img src="https://img.shields.io/badge/EF%20Core-10.0.7-512BD4?style=for-the-badge" />
  <img src="https://img.shields.io/badge/AutoMapper-16.1.1-orange?style=for-the-badge" />
  <img src="https://img.shields.io/badge/Bootstrap-5.3.3-7952B3?style=for-the-badge&logo=bootstrap&logoColor=white" />
  <img src="https://img.shields.io/badge/Repository%20Pattern-✓-success?style=for-the-badge" />
</p>

---

## Video

https://github.com/user-attachments/assets/6d730625-f34b-415c-929b-f7a32a0caff0


---
## 💡 Business Problem

Many businesses suffer from duplicated file uploads, which leads to:
- Wasted storage
- Increased costs
- Data inconsistency

## 🚀 Solution

FileGuard detects duplicate files using SHA-256 hashing, ensuring that even if a file is renamed, it won't be uploaded twice.

## 🎯 Value

- Saves storage costs
- Improves data integrity
- Prevents human errors during uploads
---

## ✨ Features

| Feature | Description |
|---------|-------------|
| 📁 **Folder Management** | Full CRUD operations for organizing files |
| 📄 **File Management** | Upload, view |
| 🔍 **Smart Duplicate Detection** | Detects duplicates even with different filenames and different folder |
| 📊 **Analytics Dashboard** | Real-time statistics and file distribution insights |
| 🛡️ **Upload Attempts Log** | Tracks all rejected duplicate attempts |
| ⚡ **Client-Side Hashing** | Early duplicate detection before server upload |
| 🎨 **Modern UI** | Clean, responsive interface with RTL support |

---

## 🎯 The Problem & Solution

### The Problem
You have 1000+ files. Some are duplicates with different names:
- `Report_Final.pdf`
- `Report_Final_Copy.pdf`
- `Report_Final_Final.pdf`

**Same content, different names = Wasted storage**

### The Solution
**SHA-256 Hash Fingerprinting**

File A: "Report_Final.pdf"     → SHA-256 → "a3f5b2c8d9e1..."
File B: "Report_Copy.pdf"      → SHA-256 → "a3f5b2c8d9e1..." ⚠️ DUPLICATE!

Even if the filename changes, the hash stays identical for identical content.

---

## 🏗️ Architecture
```plaintext
DFD/
├── 📁 DFD.MVC/                 # Web Application (Presentation)
│   ├── Controllers/
│   ├── Views/ (Razor + Vanilla JS)
│   └── wwwroot/
├── 📁 DFD.Application/          # ViewModels, Services, DTOs
├── 📁 DFD.Domain/               # Entities, Interfaces, Enums
├── 📁 DFD.Infrastructure/       # Data Access, Repositories, Unit of Work
└── 📁 Shared/                # Shared Functions
```
---

**Patterns Used:**
- ✅ Repository Pattern
- ✅ Unit of Work
- ✅ Clean Architecture
- ✅ Dependency Injection
- ✅ Async/Await Throughout

---

## 🛠️ Tech Stack

| Layer | Technology |
|-------|------------|
| **Backend** | ASP.NET Core 10 |
| **ORM** | Entity Framework Core 10 |
| **Database** | PostgreSQL |
| **Frontend** | Razor Views, Vanilla JavaScript |
| **Styling** | Custom CSS (No frameworks) |
| **Icons** | Font Awesome 6 |
| **Hashing** | SHA-256 (Client + Server) |

---

## 🚀 Getting Started
### Installation

```bash
# 1. Clone the repository
git clone https://github.com/yourusername/FileGuard.git
cd FileGuard

# 2. Restore NuGet packages
dotnet restore

# 3. Update database
dotnet ef database update --project DFD.Infrastructure --startup-project DFD.MVC

# 4. Run the application
dotnet run --project DFD.MVC

# Run
dotnet run
```


| Page                                | Preview                               |
| ------------------------------------| ------------------------------------- |
| **Dashboard**                       | <img width="1425" height="1287" alt="Dashboard" src="https://github.com/user-attachments/assets/8992fc35-a67f-48d7-b044-c580d06553d6" /> |
| **Upload with Duplicate Detection** | <img width="1425" height="709" alt="Files_Upload1" src="https://github.com/user-attachments/assets/a9ed8b6b-60b4-47aa-8758-9523b69398b3" /> |
| **File Management**                 | <img width="1425" height="685" alt="Files" src="https://github.com/user-attachments/assets/a54534fd-3be1-4069-bd20-0dcbdc06eb89" /> |
| **Upload Attempts Log**             | <img width="1425" height="713" alt="UploadAttempts" src="https://github.com/user-attachments/assets/e9e699ad-3f38-459f-8945-530ae0d96a7b" /> |

---

## 💼 Use Case

This system is ideal for:
- Companies handling large file uploads
- Document management systems
- Cloud storage solutions

## 📩 Need a similar system?

I can build a custom version tailored to your business needs.
Feel free to reach out via LinkedIn or email.


### 👩‍💻 Developer
## [Marim Mohamed] — .NET Backend Developer
+ https://linkedin.com/in/marim-m-03055a196
+ https://github.com/MARYAM-memo
+ mailto:marimeltaweel26@gmail.com


<p align="center">
  ⭐ If you found this project helpful, don't forget to give it a star!
</p>
